using SharpGDX;
using SharpGDX.Utils;
using SharpGDX.Desktop.Audio;
using SharpGDX.Mathematics;
using static SharpGDX.Desktop.OpenAL;
using static SharpGDX.Desktop.OpenALC;
using SharpGDX.Shims;
using BindingFlags = System.Reflection.BindingFlags;
using Buffer = SharpGDX.Shims.Buffer;

namespace SharpGDX.Desktop.Audio
{
	/** @author Nathan Sweet */
public class OpenALLwjgl3Audio : Lwjgl3Audio {
	private readonly int deviceBufferSize;
	private readonly int deviceBufferCount;
	private IntArray idleSources, allSources;
	private LongMap<int> soundIdToSource;
	private IntMap<long> sourceToSoundId;
	private long nextSoundId = 0;
		// TODO: Can this be generic for OpenALSound?
		private ObjectMap<String, Type> extensionToSoundClass = new ();
		// TODO: Can this be generic for OpenALMusic?
		private ObjectMap<String, Type> extensionToMusicClass = new ();
	private OpenALSound[] recentSounds;
	private int mostRecetSound = -1;
	private String preferredOutputDevice = null;
	private Thread observerThread;

	internal Array<OpenALMusic> music = new Array<OpenALMusic>(false, 1, typeof(OpenALMusic));
	long device;
	long context;
	internal bool noDevice = false;

	public OpenALLwjgl3Audio () 
	: this(16, 9, 512)
	{
		
	}

	public OpenALLwjgl3Audio (int simultaneousSources, int deviceBufferCount, int deviceBufferSize) {
		this.deviceBufferSize = deviceBufferSize;
		this.deviceBufferCount = deviceBufferCount;

		//registerSound("ogg", Ogg.Sound.class);
		//registerMusic("ogg", Ogg.Music.class);
		//registerSound("wav", Wav.Sound.class);
		//registerMusic("wav", Wav.Music.class);
		//registerSound("mp3", Mp3.Sound.class);
		//registerMusic("mp3", Mp3.Music.class);

		device = alcOpenDevice((ByteBuffer)null);
		if (device == 0L) {
			noDevice = true;
			return;
		}
		// TODO: ALCCapabilities deviceCapabilities = ALC.createCapabilities(device);
			context = alcCreateContext(device, (IntBuffer)null);
		if (context == 0L) {
			alcCloseDevice(device);
			noDevice = true;
			return;
		}
		if (!alcMakeContextCurrent(context)) {
			noDevice = true;
			return;
		}
		// TODO: AL.createCapabilities(deviceCapabilities);

		alGetError();
		allSources = new IntArray(false, simultaneousSources);
		for (int i = 0; i < simultaneousSources; i++) {
			int sourceID = alGenSources();
			if (alGetError() != AL_NO_ERROR) break;
			allSources.add(sourceID);
		}
		idleSources = new IntArray(allSources);
		soundIdToSource = new LongMap<int>();
		sourceToSoundId = new IntMap<long>();

		// TODO: This was originally using the lwjgl BufferUtils.createFloatBuffer
		// TODO: Not sure if switching to the libgdx BufferUtils.newFloatBuffer will work.
		FloatBuffer orientation = (FloatBuffer)BufferUtils.newFloatBuffer(6)
			.put(new float[] {0.0f, 0.0f, -1.0f, 0.0f, 1.0f, 0.0f});
		((Buffer)orientation).flip();
		alListenerfv(AL_ORIENTATION, orientation);
		FloatBuffer velocity = (FloatBuffer)BufferUtils.newFloatBuffer(3).put(new float[] {0.0f, 0.0f, 0.0f});
		((Buffer)velocity).flip();
		alListenerfv(AL_VELOCITY, velocity);
		FloatBuffer position = (FloatBuffer)BufferUtils.newFloatBuffer(3).put(new float[] {0.0f, 0.0f, 0.0f});
		((Buffer)position).flip();
		alListenerfv(AL_POSITION, position);

		alDisable(SOFTXHoldOnDisconnect.AL_STOP_SOURCES_ON_DISCONNECT_SOFT);
		observerThread = new Thread(() => {

				while (true) {
					bool isConnected = alcGetInteger(device, ALC_CONNECTED) != 0;
					if (!isConnected) {
						// The device is at a state where it can't recover
						// This is usually the windows path on removing a device
						switchOutputDevice(null, false);
						continue;
					}
					if (preferredOutputDevice != null) {
						if (Arrays.asList(getAvailableOutputDevices()).Contains(preferredOutputDevice)) {
							if (!preferredOutputDevice.Equals(alcGetString(device, ALC_ALL_DEVICES_SPECIFIER))) {
								// The preferred output device is reconnected, let's switch back to it
								switchOutputDevice(preferredOutputDevice);
							}
						} else {
							// This is usually the mac/linux path
							if (preferredOutputDevice.Equals(alcGetString(device, ALC_ALL_DEVICES_SPECIFIER))) {
								// The preferred output device is reconnected, let's switch back to it
								switchOutputDevice(null, false);
							}
						}
					} else {
						String[] currentDevices = getAvailableOutputDevices();
						// If a new device got added, re evaluate "auto" mode
						if (!Arrays.equals(currentDevices, lastAvailableDevices)) {
							switchOutputDevice(null);
						}
						// Update last available devices
						lastAvailableDevices = currentDevices;
					}
					try {
						Thread.Sleep(1000);
					} catch (ThreadInterruptedException ignored) {
						return;
					}
				}
			
		});
		observerThread.IsBackground =(true);
		observerThread.Start();

		recentSounds = new OpenALSound[simultaneousSources];
	}

	private String[] lastAvailableDevices = new String[0];

		public void registerSound (String extension, Type soundClass) {
		if (extension == null) throw new IllegalArgumentException("extension cannot be null.");
		if (soundClass == null) throw new IllegalArgumentException("soundClass cannot be null.");
		extensionToSoundClass.put(extension, soundClass);
	}

	public void registerMusic (String extension, Type musicClass) {
		if (extension == null) throw new IllegalArgumentException("extension cannot be null.");
		if (musicClass == null) throw new IllegalArgumentException("musicClass cannot be null.");
		extensionToMusicClass.put(extension, musicClass);
	}

	public Sound newSound (FileHandle file) {
		// TODO: 
		throw new NotImplementedException();
			//if (file == null) throw new IllegalArgumentException("file cannot be null.");
			//Type soundClass = extensionToSoundClass.get(file.extension().toLowerCase());
			//if (soundClass == null) throw new GdxRuntimeException("Unknown file extension for sound: " + file);
			//try {
			//	return soundClass.getConstructor(new Class[] {OpenALLwjgl3Audio.class, FileHandle.class}).newInstance(this, file);
			//} catch (Exception ex) {
			//	throw new GdxRuntimeException("Error creating sound " + soundClass.getName() + " for file: " + file, ex);
			//}
		}

	public Music newMusic (FileHandle file) {
		//if (file == null) throw new IllegalArgumentException("file cannot be null.");
		//Type musicClass = extensionToMusicClass.get(file.extension().toLowerCase());
		//if (musicClass == null) throw new GdxRuntimeException("Unknown file extension for music: " + file);
		//try {
		//	return musicClass.getConstructor(new Class[] {OpenALLwjgl3Audio.class, FileHandle.class}).newInstance(this, file);
		//} catch (Exception ex) {
		//	throw new GdxRuntimeException("Error creating music " + musicClass.getName() + " for file: " + file, ex);
		//}
		// TODO: 
		throw new NotImplementedException();
	}

	public bool switchOutputDevice (String deviceIdentifier) {
		return switchOutputDevice(deviceIdentifier, true);
	}

	private bool switchOutputDevice (String deviceIdentifier, bool setPreferred) {
		if (setPreferred) {
			preferredOutputDevice = deviceIdentifier;
		}
		// TODO: return SOFTReopenDevice.alcReopenDeviceSOFT(device, deviceIdentifier, (IntBuffer)null);
		return false;
	}

	public String[] getAvailableOutputDevices () {
		List<String> devices = getStringList(0, ALC_ALL_DEVICES_SPECIFIER);
		if (devices == null) return new String[0];
		return devices.ToArray();
	}

	internal int obtainSource (bool isMusic) {
		if (noDevice) return 0;
		for (int i = 0, n = idleSources.size; i < n; i++) {
			int sourceId = idleSources.get(i);
			int state = alGetSourcei(sourceId, AL_SOURCE_STATE);
			if (state != AL_PLAYING && state != AL_PAUSED) {
				long oldSoundId = sourceToSoundId.remove(sourceId);
				if (oldSoundId != null) soundIdToSource.remove(oldSoundId);
				if (isMusic) {
					idleSources.removeIndex(i);
				} else {
					long soundId = nextSoundId++;
					sourceToSoundId.put(sourceId, soundId);
					soundIdToSource.put(soundId, sourceId);
				}
				alSourceStop(sourceId);
				alSourcei(sourceId, AL_BUFFER, 0);
				alSourcef(sourceId, AL_GAIN, 1);
				alSourcef(sourceId, AL_PITCH, 1);
				alSource3f(sourceId, AL_POSITION, 0, 0, 1f);
				alSourcei(sourceId, SOFTDirectChannels.AL_DIRECT_CHANNELS_SOFT, SOFTDirectChannelsRemix.AL_REMIX_UNMATCHED_SOFT);
				return sourceId;
			}
		}
		return -1;
	}

	internal void freeSource (int sourceID) {
		if (noDevice) return;
		alSourceStop(sourceID);
		alSourcei(sourceID, AL_BUFFER, 0);
		long soundId = sourceToSoundId.remove(sourceID);
		if (soundId != null) soundIdToSource.remove(soundId);
		idleSources.add(sourceID);
	}

	internal void freeBuffer (int bufferID) {
		if (noDevice) return;
		for (int i = 0, n = idleSources.size; i < n; i++) {
			int sourceID = idleSources.get(i);
			if (alGetSourcei(sourceID, AL_BUFFER) == bufferID) {
				long soundId = sourceToSoundId.remove(sourceID);
				if (soundId != null) soundIdToSource.remove(soundId);
				alSourceStop(sourceID);
				alSourcei(sourceID, AL_BUFFER, 0);
			}
		}
	}

	internal void stopSourcesWithBuffer (int bufferID) {
		if (noDevice) return;
		for (int i = 0, n = idleSources.size; i < n; i++) {
			int sourceID = idleSources.get(i);
			if (alGetSourcei(sourceID, AL_BUFFER) == bufferID) {
				long soundId = sourceToSoundId.remove(sourceID);
				if (soundId != null) soundIdToSource.remove(soundId);
				alSourceStop(sourceID);
			}
		}
	}

	internal void pauseSourcesWithBuffer (int bufferID) {
		if (noDevice) return;
		for (int i = 0, n = idleSources.size; i < n; i++) {
			int sourceID = idleSources.get(i);
			if (alGetSourcei(sourceID, AL_BUFFER) == bufferID) alSourcePause(sourceID);
		}
	}

	internal void resumeSourcesWithBuffer (int bufferID) {
		if (noDevice) return;
		for (int i = 0, n = idleSources.size; i < n; i++) {
			int sourceID = idleSources.get(i);
			if (alGetSourcei(sourceID, AL_BUFFER) == bufferID) {
				if (alGetSourcei(sourceID, AL_SOURCE_STATE) == AL_PAUSED) alSourcePlay(sourceID);
			}
		}
	}

	public void update () {
		if (noDevice) return;
		for (int i = 0; i < music.size; i++)
			music.items[i].update();
	}

	public long getSoundId (int sourceId) {
		long soundId = sourceToSoundId.get(sourceId);
		return soundId != null ? soundId : -1;
	}

	public int getSourceId (long soundId) {
		int sourceId = soundIdToSource.get(soundId);
		return sourceId != null ? sourceId : -1;
	}

	public void stopSound (long soundId) {
		int sourceId = soundIdToSource.get(soundId);
		if (sourceId != null) alSourceStop(sourceId);
	}

	public void pauseSound (long soundId) {
		int sourceId = soundIdToSource.get(soundId);
		if (sourceId != null) alSourcePause(sourceId);
	}

	public void resumeSound (long soundId) {
		int sourceId = soundIdToSource.get(soundId, -1);
		if (sourceId != -1 && alGetSourcei(sourceId, AL_SOURCE_STATE) == AL_PAUSED) alSourcePlay(sourceId);
	}

	public void setSoundGain (long soundId, float volume) {
		int sourceId = soundIdToSource.get(soundId);
		if (sourceId != null) alSourcef(sourceId, AL_GAIN, volume);
	}

	public void setSoundLooping (long soundId, bool looping) {
		int sourceId = soundIdToSource.get(soundId);
		if (sourceId != null) alSourcei(sourceId, AL_LOOPING, looping ? AL_TRUE : AL_FALSE);
	}

	public void setSoundPitch (long soundId, float pitch) {
		int sourceId = soundIdToSource.get(soundId);
		if (sourceId != null) alSourcef(sourceId, AL_PITCH, pitch);
	}

	public void setSoundPan (long soundId, float pan, float volume) {
		int sourceId = soundIdToSource.get(soundId, -1);
		if (sourceId != -1) {
			alSource3f(sourceId, AL_POSITION, MathUtils.cos((pan - 1) * MathUtils.HALF_PI), 0,
				MathUtils.sin((pan + 1) * MathUtils.HALF_PI));
			alSourcef(sourceId, AL_GAIN, volume);
		}
	}

	public void dispose () {
		if (noDevice) return;
		observerThread.Interrupt();
		for (int i = 0, n = allSources.size; i < n; i++) {
			int sourceID = allSources.get(i);
			int state = alGetSourcei(sourceID, AL_SOURCE_STATE);
			if (state != AL_STOPPED) alSourceStop(sourceID);
			alDeleteSources(sourceID);
		}

		sourceToSoundId = null;
		soundIdToSource = null;

		alcDestroyContext(context);
		alcCloseDevice(device);
	}

	public AudioDevice newAudioDevice (int sampleRate, bool isMono) {
		if (noDevice) return new NoAudioDevice(isMono) {
			
		};
		return new OpenALAudioDevice(this, sampleRate, isMono, deviceBufferSize, deviceBufferCount);
	}

	private class NoAudioDevice: AudioDevice
	{
		private readonly bool _isMono;

		public NoAudioDevice(bool isMono)
		{
			_isMono = isMono;
		}

		public void writeSamples(float[] samples, int offset, int numSamples)
		{
		}

		public void writeSamples(short[] samples, int offset, int numSamples)
		{
		}

		public void setVolume(float volume)
		{
		}

		public bool isMono()
		{
			return _isMono;
		}

		public int getLatency()
		{
			return 0;
		}

		public void dispose()
		{
		}

		public void pause()
		{
		}

		public void resume()
		{
		}
}

	public AudioRecorder newAudioRecorder (int samplingRate, bool isMono) {
		if (noDevice) return new NoAudioRecorder() {
			
		};
		return new JavaSoundAudioRecorder(samplingRate, isMono);
	}

	private class NoAudioRecorder : AudioRecorder
	{
		public void read(short[] samples, int offset, int numSamples)
		{
		}

		public void dispose()
		{
		}
}

	/** Retains a list of the most recently played sounds and stops the sound played least recently if necessary for a new sound to
	 * play */
	internal void retain (OpenALSound sound, bool stop) {
		// Move the pointer ahead and wrap
		mostRecetSound++;
		mostRecetSound %= recentSounds.Length;

		if (stop) {
			// Stop the least recent sound (the one we are about to bump off the buffer)
			if (recentSounds[mostRecetSound] != null) recentSounds[mostRecetSound].stop();
		}

		recentSounds[mostRecetSound] = sound;
	}

	/** Removes the disposed sound from the least recently played list */
	public void forget (OpenALSound sound) {
		for (int i = 0; i < recentSounds.Length; i++) {
			if (recentSounds[i] == sound) recentSounds[i] = null;
		}
	}
}
}
