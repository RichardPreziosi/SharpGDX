using OpenTK.Audio.OpenAL;
using SharpGDX.audio;
using SharpGDX.Desktop.audio;
using SharpGDX.files;
using SharpGDX.math;
using SharpGDX.shims;
using SharpGDX.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Buffer = SharpGDX.shims.Buffer;

namespace SharpGDX.Desktop.audio
{
	/** @author Nathan Sweet */
	public unsafe class OpenALDesktopAudio : DesktopAudio
	{
	private readonly int deviceBufferSize;
	private readonly int deviceBufferCount;
	private IntArray idleSources, allSources;
	private LongMap<int> soundIdToSource;
	private IntMap<long> sourceToSoundId;
	private long nextSoundId = 0;
	private ObjectMap<String, Type> extensionToSoundClass = new ();
	private ObjectMap<String, Type> extensionToMusicClass = new ();
	private OpenALSound[] recentSounds;
	private int mostRecetSound = -1;
	private String preferredOutputDevice = null;
	private Thread observerThread;

	internal Array<OpenALMusic> music = new(false, 1, typeof(OpenALMusic));
	ALDevice device;
	ALContext context;
	internal bool noDevice = false;

	public OpenALDesktopAudio()
	: this(16, 9, 512)
		{
	}

	public OpenALDesktopAudio(int simultaneousSources, int deviceBufferCount, int deviceBufferSize)
	{
		this.deviceBufferSize = deviceBufferSize;
		this.deviceBufferCount = deviceBufferCount;

		//registerSound("ogg", Ogg.Sound.class);
		//registerMusic("ogg", Ogg.Music.class);
		//registerSound("wav", Wav.Sound.class);
		//registerMusic("wav", Wav.Music.class);
		//registerSound("mp3", Mp3.Sound.class);
		//registerMusic("mp3", Mp3.Music.class);

		device = ALC.OpenDevice(null);
		if (device == 0L) {
			noDevice = true;
			return;
		}
// TODO: ALC.Capabilities deviceCapabilities = ALC.createCapabilities(device);
context = ALC.CreateContext(device, (int*)null);
if (context == 0L)
{
	ALC.CloseDevice(device);
	noDevice = true;
	return;
}
if (!ALC.MakeContextCurrent(context))
{
	noDevice = true;
	return;
}

// TODO: AL.createCapabilities(deviceCapabilities);

AL.GetError();
allSources = new IntArray(false, simultaneousSources);
for (int i = 0; i < simultaneousSources; i++)
{
	int sourceID = AL.GenSource();
	if (AL.GetError() != ALError.NoError) break;
	allSources.add(sourceID);
}
idleSources = new IntArray(allSources);
soundIdToSource = new LongMap<int>();
sourceToSoundId = new IntMap<long>();

FloatBuffer orientation = (FloatBuffer)BufferUtils.createFloatBuffer(6)
	.put(new float[] { 0.0f, 0.0f, -1.0f, 0.0f, 1.0f, 0.0f });
((Buffer)orientation).flip();
AL.Listener(ALListenerfv.Orientation, (float[])orientation);
FloatBuffer velocity = (FloatBuffer)BufferUtils.createFloatBuffer(3).put(new float[] { 0.0f, 0.0f, 0.0f });
((Buffer)velocity).flip();
AL.Listener(ALListener3f.Velocity, velocity[0], velocity[1], velocity[2]);
FloatBuffer position = (FloatBuffer)BufferUtils.createFloatBuffer(3).put(new float[] { 0.0f, 0.0f, 0.0f });
((Buffer)position).flip();
AL.Listener(ALListener3f.Position, position[0], position[1], position[2]);

// TODO: AL.Disable(SOFTXHoldOnDisconnect.AL_STOP_SOURCES_ON_DISCONNECT_SOFT);
observerThread = new Thread(() =>
{

			String[] lastAvailableDevices = new String[0];

	while (true)
	{
		// TODO: Not sure if this is possible like this, might have to re-enumerate the available devices completely.
		//bool isConnected = ALC.GetInteger(device, ALCgetinteger, connected) != 0;
		//if (!isConnected)
		//{
		//	// The device is at a state where it can't recover
		//	// This is usually the windows path on removing a device
		//	switchOutputDevice(null, false);
		//	continue;
		//}

		if (preferredOutputDevice != null)
		{
			// TODO: Why cast to a list, this was Arrays.asList(getAvailableOutputDevices()).contains(preferredOutputDevice)
			if (getAvailableOutputDevices().ToList().Contains(preferredOutputDevice))
			{
				if (!preferredOutputDevice.Equals(ALC.GetString(device, AlcGetString.AllDevicesSpecifier)))
				{
					// The preferred output device is reconnected, let's switch back to it
					switchOutputDevice(preferredOutputDevice);
				}
			}
			else
			{
				// This is usually the mac/linux path
				if (preferredOutputDevice.Equals(ALC.GetString(device, AlcGetString.AllDevicesSpecifier)))
				{
					// The preferred output device is reconnected, let's switch back to it
					switchOutputDevice(null, false);
				}
			}
		}
		else
		{
			String[] currentDevices = getAvailableOutputDevices();
			// If a new device got added, re evaluate "auto" mode
			// TODO: Does order matter? was !Arrays.equals(currentDevices, lastAvailableDevices)
			if (!currentDevices.SequenceEqual(lastAvailableDevices))
			{
				switchOutputDevice(null);
			}
			// Update last available devices
			lastAvailableDevices = currentDevices;
		}
		try
		{
			Thread.Sleep(1000);
		}
		catch (ThreadInterruptedException ignored)
		{
			return;
		}
	}
		});
observerThread.IsBackground = (true);
observerThread.Start();

recentSounds = new OpenALSound[simultaneousSources];
	}

	public void registerSound(String extension, Type soundClass)
{
	if (extension == null) throw new ArgumentException("extension cannot be null.");
	if (soundClass == null) throw new ArgumentException("soundClass cannot be null.");
	extensionToSoundClass.put(extension, soundClass);
}

public void registerMusic(String extension, Type musicClass)
{
	if (extension == null) throw new ArgumentException("extension cannot be null.");
	if (musicClass == null) throw new ArgumentException("musicClass cannot be null.");
	extensionToMusicClass.put(extension, musicClass);
}

public Sound newSound(FileHandle file)
{
	if (file == null) throw new ArgumentException("file cannot be null.");
	var soundClass = extensionToSoundClass.get(file.extension().ToLower());
	if (soundClass == null) throw new GdxRuntimeException("Unknown file extension for sound: " + file);
	try
	{
		return (Sound)soundClass.GetConstructor([typeof(OpenALDesktopAudio), typeof(FileHandle)]).Invoke([this, file]);
		} catch (Exception ex) {
			throw new GdxRuntimeException("Error creating sound " + soundClass.Name + " for file: " + file, ex);
		}
	}
		
	public Music newMusic(FileHandle file)
{
	if (file == null) throw new ArgumentException("file cannot be null.");
	 Type musicClass = extensionToMusicClass.get(file.extension().ToLower());
	if (musicClass == null) throw new GdxRuntimeException("Unknown file extension for music: " + file);
	try
	{
		return (Music)musicClass.GetConstructor([ typeof(OpenALDesktopAudio), typeof(FileHandle)]).Invoke([this, file]);
		} catch (Exception ex) {
			throw new GdxRuntimeException("Error creating music " + musicClass.Name + " for file: " + file, ex);
		}
	}

	public bool switchOutputDevice(String deviceIdentifier)
{
	return switchOutputDevice(deviceIdentifier, true);
}

private bool switchOutputDevice(String deviceIdentifier, bool setPreferred)
{
	if (setPreferred)
	{
		preferredOutputDevice = deviceIdentifier;
	}
	// TODO: return SOFTReopenDevice.alcReopenDeviceSOFT(device, deviceIdentifier, (IntBuffer)null);
	// TODO: Remove this
	return true;
}

	public String[] getAvailableOutputDevices()
{
	List<String> devices = ALC.GetStringList(GetEnumerationStringList.DeviceSpecifier).ToList();
			if (devices == null) return new String[0];
	return devices.ToArray();
}

internal int obtainSource(bool isMusic)
{
	if (noDevice) return 0;
	for (int i = 0, n = idleSources.size; i < n; i++)
	{
		int sourceId = idleSources.get(i);
		int state = AL.GetSource(sourceId, ALGetSourcei.SourceState);
		if (state != (int)ALSourceState.Playing && state != (int)ALSourceState.Paused)
		{
			long oldSoundId = sourceToSoundId.remove(sourceId);
			if (oldSoundId != null) soundIdToSource.remove(oldSoundId);
			if (isMusic)
			{
				idleSources.removeIndex(i);
			}
			else
			{
				long soundId = nextSoundId++;
				sourceToSoundId.put(sourceId, soundId);
				soundIdToSource.put(soundId, sourceId);
			}
			AL.SourceStop(sourceId);
			AL.Source(sourceId, ALSourcei.Buffer, 0);
			AL.Source(sourceId, ALSourcef.Gain, 1);
			AL.Source(sourceId, ALSourcef.Pitch, 1);
			AL.Source(sourceId, ALSource3f.Position, 0, 0, 1f);
			// TODO: Not sure that this is available in OpenTK AL.Source(sourceId, SOFTDirectChannels.AL_DIRECT_CHANNELS_SOFT, SOFTDirectChannelsRemix.AL_REMIX_UNMATCHED_SOFT);
					return sourceId;
		}
	}
	return -1;
}

internal void freeSource(int sourceID)
{
	if (noDevice) return;
	AL.SourceStop(sourceID);
	AL.Source(sourceID, ALSourcei.Buffer, 0);
	long soundId = sourceToSoundId.remove(sourceID);
	if (soundId != null) soundIdToSource.remove(soundId);
	idleSources.add(sourceID);
}

internal void freeBuffer(int bufferID)
{
	if (noDevice) return;
	for (int i = 0, n = idleSources.size; i < n; i++)
	{
		int sourceID = idleSources.get(i);
		if (AL.GetSource(sourceID, ALGetSourcei.Buffer) == bufferID)
		{
			long soundId = sourceToSoundId.remove(sourceID);
			if (soundId != null) soundIdToSource.remove(soundId);
			AL.SourceStop(sourceID);
			AL.Source(sourceID, ALSourcei.Buffer, 0);
		}
	}
}

internal void stopSourcesWithBuffer(int bufferID)
{
	if (noDevice) return;
	for (int i = 0, n = idleSources.size; i < n; i++)
	{
		int sourceID = idleSources.get(i);
		if (AL.GetSource(sourceID, ALGetSourcei.Buffer) == bufferID)
		{
			var soundId = sourceToSoundId.remove(sourceID);
			if (soundId != null) soundIdToSource.remove(soundId);
			AL.SourceStop(sourceID);
		}
	}
}

internal void pauseSourcesWithBuffer(int bufferID)
{
	if (noDevice) return;
	for (int i = 0, n = idleSources.size; i < n; i++)
	{
		int sourceID = idleSources.get(i);
		if (AL.GetSource(sourceID, ALGetSourcei.Buffer) == bufferID) AL.SourcePause(sourceID);
	}
}

internal void resumeSourcesWithBuffer(int bufferID)
{
	if (noDevice) return;
	for (int i = 0, n = idleSources.size; i < n; i++)
	{
		int sourceID = idleSources.get(i);
		if (AL.GetSource(sourceID, ALGetSourcei.Buffer) == bufferID)
		{
			if (AL.GetSource(sourceID, ALGetSourcei.SourceState) == (int)ALSourceState.Paused) AL.SourcePlay(sourceID);
		}
	}
}

	public void update()
{
	if (noDevice) return;
	for (int i = 0; i < music.size; i++)
		music.items[i].update();
}

public long getSoundId(int sourceId)
{
	long soundId = sourceToSoundId.get(sourceId);
	return soundId != null ? soundId : -1;
}

public int getSourceId(long soundId)
{
	int sourceId = soundIdToSource.get(soundId);
	return sourceId != null ? sourceId : -1;
}

public void stopSound(long soundId)
{
	int sourceId = soundIdToSource.get(soundId);
	if (sourceId != null) AL.SourceStop(sourceId);
}

public void pauseSound(long soundId)
{
	var sourceId = soundIdToSource.get(soundId);
	if (sourceId != null) AL.SourcePause(sourceId);
}

public void resumeSound(long soundId)
{
	int sourceId = soundIdToSource.get(soundId, -1);
	if (sourceId != -1 && AL.GetSource(sourceId, ALGetSourcei.SourceState) == (int)ALSourceState.Paused) AL.SourcePlay(sourceId);
}

public void setSoundGain(long soundId, float volume)
{
	var sourceId = soundIdToSource.get(soundId);
	if (sourceId != null) AL.Source(sourceId, ALSourcef.Gain, volume);
}

public void setSoundLooping(long soundId, bool looping)
{
	var sourceId = soundIdToSource.get(soundId);
	if (sourceId != null) AL.Source(sourceId, ALSourceb.Looping, looping ? true : false);
}

public void setSoundPitch(long soundId, float pitch)
{
	var sourceId = soundIdToSource.get(soundId);
	if (sourceId != null) AL.Source(sourceId, ALSourcef.Pitch, pitch);
}

public void setSoundPan(long soundId, float pan, float volume)
{
	int sourceId = soundIdToSource.get(soundId, -1);
	if (sourceId != -1)
	{
		AL.Source(sourceId, ALSource3f.Position, MathUtils.cos((pan - 1) * MathUtils.HALF_PI), 0,
			MathUtils.sin((pan + 1) * MathUtils.HALF_PI));
		AL.Source(sourceId, ALSourcef.Gain, volume);
	}
}

public void dispose()
{
	if (noDevice) return;
	observerThread.Interrupt();
	for (int i = 0, n = allSources.size; i < n; i++)
	{
		int sourceID = allSources.get(i);
		int state = AL.GetSource(sourceID, ALGetSourcei.SourceState);
		if (state != (int)ALSourceState.Stopped) AL.SourceStop(sourceID);
		AL.DeleteSource(sourceID);
	}

	sourceToSoundId = null;
	soundIdToSource = null;

	ALC.DestroyContext(context);
	ALC.CloseDevice(device);
}

private class NoAudioDevice : AudioDevice
{
	private bool _isMono;

	public NoAudioDevice(bool isMono)
	{
		_isMono = isMono;
	}

	public bool isMono()
	{
		return _isMono;
	}

	public void writeSamples(short[] samples, int offset, int numSamples)
	{
	}

	public void writeSamples(float[] samples, int offset, int numSamples)
	{
	}

	public int getLatency()
	{
		return 0;
	}
	
	public void setVolume(float volume)
	{
	}

	public void pause()
	{
	}

	public void resume()
	{
	}

	public void dispose()
	{
	}
}

public AudioDevice newAudioDevice(int sampleRate, bool isMono)
{
	if (noDevice) return new NoAudioDevice(isMono);
	return new OpenALAudioDevice(this, sampleRate, isMono, deviceBufferSize, deviceBufferCount);
}

private class NoAudioRecorder : AudioRecorder
{
	public void read(short[] samples, int offset, int numSamples)
	{
		throw new NotImplementedException();
	}
	
	public void dispose()
	{
		throw new NotImplementedException();
	}
}

public AudioRecorder newAudioRecorder(int samplingRate, bool isMono)
{
	if (noDevice) return new NoAudioRecorder();
return new DesktopAudioRecorder(samplingRate, isMono);
	}

	/** Retains a list of the most recently played sounds and stops the sound played least recently if necessary for a new sound to
	 * play */
	internal void retain(OpenALSound sound, bool stop)
{
	// Move the pointer ahead and wrap
	mostRecetSound++;
	mostRecetSound %= recentSounds.Length;

	if (stop)
	{
		// Stop the least recent sound (the one we are about to bump off the buffer)
		if (recentSounds[mostRecetSound] != null) recentSounds[mostRecetSound].stop();
	}

	recentSounds[mostRecetSound] = sound;
}

/** Removes the disposed sound from the least recently played list */
public void forget(OpenALSound sound)
{
	for (int i = 0; i < recentSounds.Length; i++)
	{
		if (recentSounds[i] == sound) recentSounds[i] = null;
	}
}
}
}
