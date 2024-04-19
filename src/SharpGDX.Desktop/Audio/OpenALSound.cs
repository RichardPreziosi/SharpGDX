using SharpGDX.Shims;
using Buffer = SharpGDX.Shims.Buffer;
using static SharpGDX.Desktop.OpenAL;

namespace SharpGDX.Desktop.Audio
{
	/** @author Nathan Sweet */
	public class OpenALSound : Sound
	{
	private int bufferID = -1;
	private readonly OpenALLwjgl3Audio audio;
	private float _duration;
	private int sampleRate, channels;

	public OpenALSound(OpenALLwjgl3Audio audio)
	{
		this.audio = audio;
	}

	protected void setup(byte[] pcm, int channels, int sampleRate)
	{
		int validBytes = pcm.Length - (pcm.Length % (channels > 1 ? 4 : 2));
		ByteBuffer buffer = BufferUtils.newByteBuffer(validBytes);
		buffer.put(pcm, 0, validBytes);
		((Buffer)buffer).flip();

		setup(buffer.asShortBuffer(), channels, sampleRate);
	}

	protected void setup(ShortBuffer pcm, int channels, int sampleRate)
	{
		this.channels = channels;
		this.sampleRate = sampleRate;
		int sampleFrames = pcm.limit() / channels;
		_duration = sampleFrames / (float)sampleRate;

		if (bufferID == -1)
		{
			bufferID = alGenBuffers();
			alBufferData(bufferID, channels > 1 ? AL_FORMAT_STEREO16 : AL_FORMAT_MONO16, pcm, sampleRate);
		}
	}

	public long play()
	{
		return play(1);
	}

	public long play(float volume)
	{
		if (audio.noDevice) return 0;
		int sourceID = audio.obtainSource(false);
		if (sourceID == -1)
		{
			// Attempt to recover by stopping the least recently played sound
			audio.retain(this, true);
			sourceID = audio.obtainSource(false);
		}
		else
			audio.retain(this, false);
		// In case it still didn't work
		if (sourceID == -1) return -1;
		long soundId = audio.getSoundId(sourceID);
		alSourcei(sourceID, AL_BUFFER, bufferID);
		alSourcei(sourceID, AL_LOOPING, AL_FALSE);
		alSourcef(sourceID, AL_GAIN, volume);
		alSourcePlay(sourceID);
		return soundId;
	}

	public long loop()
	{
		return loop(1);
	}

	public long loop(float volume)
	{
		if (audio.noDevice) return 0;
		int sourceID = audio.obtainSource(false);
		if (sourceID == -1) return -1;
		long soundId = audio.getSoundId(sourceID);
		alSourcei(sourceID, AL_BUFFER, bufferID);
		alSourcei(sourceID, AL_LOOPING, AL_TRUE);
		alSourcef(sourceID, AL_GAIN, volume);
		alSourcePlay(sourceID);
		return soundId;
	}

	public void stop()
	{
		if (audio.noDevice) return;
		audio.stopSourcesWithBuffer(bufferID);
	}

	public void dispose()
	{
		if (audio.noDevice) return;
		if (bufferID == -1) return;
		audio.freeBuffer(bufferID);
		alDeleteBuffers(bufferID);
		bufferID = -1;
		audio.forget(this);
	}

	public void stop(long soundId)
	{
		if (audio.noDevice) return;
		audio.stopSound(soundId);
	}

	public void pause()
	{
		if (audio.noDevice) return;
		audio.pauseSourcesWithBuffer(bufferID);
	}

	public void pause(long soundId)
	{
		if (audio.noDevice) return;
		audio.pauseSound(soundId);
	}

	public void resume()
	{
		if (audio.noDevice) return;
		audio.resumeSourcesWithBuffer(bufferID);
	}

	public void resume(long soundId)
	{
		if (audio.noDevice) return;
		audio.resumeSound(soundId);
	}

	public void setPitch(long soundId, float pitch)
	{
		if (audio.noDevice) return;
		audio.setSoundPitch(soundId, pitch);
	}

	public void setVolume(long soundId, float volume)
	{
		if (audio.noDevice) return;
		audio.setSoundGain(soundId, volume);
	}

	public void setLooping(long soundId, bool looping)
	{
		if (audio.noDevice) return;
		audio.setSoundLooping(soundId, looping);
	}

	public void setPan(long soundId, float pan, float volume)
	{
		if (audio.noDevice) return;
		audio.setSoundPan(soundId, pan, volume);
	}

	public long play(float volume, float pitch, float pan)
	{
		long id = play();
		setPitch(id, pitch);
		setPan(id, pan, volume);
		return id;
	}

	public long loop(float volume, float pitch, float pan)
	{
		long id = loop();
		setPitch(id, pitch);
		setPan(id, pan, volume);
		return id;
	}

	/** Returns the length of the sound in seconds. */
	public float duration()
	{
		return _duration;
	}

	/** returns the original sample rate of the sound in Hz. */
	public int getRate()
	{
		return sampleRate;
	}

	/** returns the number of channels of the sound (1 for mono, 2 for stereo). */
	public int getChannels()
	{
		return channels;
	}
}
}
