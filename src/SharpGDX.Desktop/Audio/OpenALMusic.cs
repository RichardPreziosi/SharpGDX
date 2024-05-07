using System.Runtime.InteropServices;
using SharpGDX.Mathematics;
using SharpGDX.OpenAL;
using Buffer = SharpGDX.Shims.Buffer;
using SharpGDX.Shims;
using SharpGDX.Utils;
using static SharpGDX.Music;

// TODO: Not fond of the creation of arrays just to pass queue and dequeue buffers.

namespace SharpGDX.Desktop.Audio
{
	/** @author Nathan Sweet */
	public abstract class OpenALMusic : Music
	{
	static private readonly int bufferSize = 4096 * 10;
	static private readonly int bufferCount = 3;
	static private readonly int bytesPerSample = 2;
	static private readonly byte[] tempBytes = new byte[bufferSize];

		// TODO: This was originally using BufferUtils.createByteBuffer, not sure if this will work.
		static private readonly ByteBuffer tempBuffer = ByteBuffer.allocate(bufferSize);

	private FloatArray renderedSecondsQueue = new FloatArray(bufferCount);

	private readonly OpenALLwjgl3Audio audio;
	private IntBuffer buffers;
	private int sourceID = -1;
	private int format, sampleRate;
	private bool _isLooping, _isPlaying;
	private float volume = 1;
	private float pan = 0;
	private float renderedSeconds, maxSecondsPerBuffer;

	protected readonly FileHandle file;

	private OnCompletionListener onCompletionListener;

	public OpenALMusic(OpenALLwjgl3Audio audio, FileHandle file)
	{
		this.audio = audio;
		this.file = file;
		this.onCompletionListener = null;
	}

	protected void setup(int channels, int sampleRate)
	{
		this.format = channels > 1 ? AL.AL_FORMAT_STEREO16 : AL.AL_FORMAT_MONO16;
		this.sampleRate = sampleRate;
		maxSecondsPerBuffer = (float)bufferSize / (bytesPerSample * channels * sampleRate);
	}

	public void play()
	{
		if (audio.noDevice) return;
		if (sourceID == -1)
		{
			sourceID = audio.obtainSource(true);
			if (sourceID == -1) return;

			audio.music.add(this);

			if (buffers == null)
			{
					// TODO: This was originally using BufferUtils.createIntBuffer, not sure if this will work
					buffers = IntBuffer.allocate(bufferCount);
					AL.alGetError();
					AL.alGenBuffers(bufferCount, buffers.array());
				int errorCode = AL.alGetError();
				if (errorCode != AL.AL_NO_ERROR)
					throw new GdxRuntimeException("Unable to allocate audio buffers. AL Error: " + errorCode);
			}

			AL.alSourcei(sourceID, AL.AL_LOOPING, AL.AL_FALSE);
			setPan(pan, volume);

			AL.alGetError();

			bool filled = false; // Check if there's anything to actually play.
			for (int i = 0; i < bufferCount; i++)
			{
				int bufferID = buffers.get(i);
				if (!fill(bufferID)) break;
				filled = true;
				// TODO: Verify
				AL.alSourceQueueBuffers(sourceID, 1, new int[]{ bufferID});

				}
			if (!filled && onCompletionListener != null) onCompletionListener.onCompletion(this);
			
			if (AL.alGetError() != AL.AL_NO_ERROR)
			{
				stop();
				return;
			}
		}
		if (!_isPlaying)
		{
			AL.alSourcePlay(sourceID);
			_isPlaying = true;
		}
	}

	public void stop()
	{
		if (audio.noDevice) return;
		if (sourceID == -1) return;
		audio.music.removeValue(this, true);
		reset();
		audio.freeSource(sourceID);
		sourceID = -1;
		renderedSeconds = 0;
		renderedSecondsQueue.clear();
		_isPlaying = false;
	}

	public void pause()
	{
		if (audio.noDevice) return;
		if (sourceID != -1) AL.alSourcePause(sourceID);
		_isPlaying = false;
	}

	public bool isPlaying()
	{
		if (audio.noDevice) return false;
		if (sourceID == -1) return false;
		return _isPlaying;
	}

	public void setLooping(bool isLooping)
	{
		this._isLooping = isLooping;
	}

	public bool isLooping()
	{
		return _isLooping;
	}

	/** @param volume Must be > 0. */
	public void setVolume(float volume)
	{
		if (volume < 0) throw new IllegalArgumentException("volume cannot be < 0: " + volume);
		this.volume = volume;
		if (audio.noDevice) return;
		if (sourceID != -1) AL.alSourcef(sourceID, AL.AL_GAIN, volume);
	}

	public float getVolume()
	{
		return this.volume;
	}

	public void setPan(float pan, float volume)
	{
		this.volume = volume;
		this.pan = pan;
		if (audio.noDevice) return;
		if (sourceID == -1) return;
		AL.alSource3f(sourceID, AL.AL_POSITION, MathUtils.cos((pan - 1) * MathUtils.HALF_PI), 0,
			MathUtils.sin((pan + 1) * MathUtils.HALF_PI));
		AL.alSourcef(sourceID, AL.AL_GAIN, volume);
	}

	public void setPosition(float position)
	{
		if (audio.noDevice) return;
		if (sourceID == -1) return;
		bool wasPlaying = _isPlaying;
		_isPlaying = false;
		AL.alSourceStop(sourceID);
		AL.alSourceUnqueueBuffers(sourceID, 1, buffers.array());
		while (renderedSecondsQueue.size > 0)
		{
			renderedSeconds = renderedSecondsQueue.pop();
		}
		if (position <= renderedSeconds)
		{
			reset();
			renderedSeconds = 0;
		}
		while (renderedSeconds < (position - maxSecondsPerBuffer))
		{
			int length = read(tempBytes);
			if (length <= 0) break;
			float currentBufferSeconds = maxSecondsPerBuffer * (float)length / (float)bufferSize;
			renderedSeconds += currentBufferSeconds;
		}
		renderedSecondsQueue.add(renderedSeconds);
		bool filled = false;
		for (int i = 0; i < bufferCount; i++)
		{
			int bufferID = buffers.get(i);
			if (!fill(bufferID)) break;
			filled = true;
			AL.alSourceQueueBuffers(sourceID, 1, new []{ bufferID });
		}
		renderedSecondsQueue.pop();
		if (!filled)
		{
			stop();
			if (onCompletionListener != null) onCompletionListener.onCompletion(this);
		}
		AL.alSourcef(sourceID, AL.AL_SEC_OFFSET, position - renderedSeconds);
		if (wasPlaying)
		{
			AL.alSourcePlay(sourceID);
			_isPlaying = true;
		}
	}

	public float getPosition()
	{
		if (audio.noDevice) return 0;
		if (sourceID == -1) return 0;
		AL.alGetSourcef(sourceID, AL.AL_SEC_OFFSET, out var offset);
		return renderedSeconds + offset;
	}

	/** Fills as much of the buffer as possible and returns the number of bytes filled. Returns <= 0 to indicate the end of the
	 * stream. */
	abstract public int read(byte[] buffer);

	/** Resets the stream to the beginning. */
	abstract public void reset();

	/** By default, does just the same as reset(). Used to add special behaviour in Ogg.Music. */
	protected void loop()
	{
		reset();
	}

	public int getChannels()
	{
		return format == AL.AL_FORMAT_STEREO16 ? 2 : 1;
	}

	public int getRate()
	{
		return sampleRate;
	}

	public void update()
	{
		if (audio.noDevice) return;
		if (sourceID == -1) return;

		bool end = false;
		AL.alGetSourcei(sourceID, AL.AL_BUFFERS_PROCESSED, out var buffers);
		while (buffers-- > 0)
		{
			// TODO: Verify
			var bufferIds = new int[1];
			AL.alSourceUnqueueBuffers(sourceID, 1, bufferIds);
			int bufferID = bufferIds[0];
			if (bufferID == AL.AL_INVALID_VALUE) break;
			if (renderedSecondsQueue.size > 0) renderedSeconds = renderedSecondsQueue.pop();
			if (end) continue;
			if (fill(bufferID))
				AL.alSourceQueueBuffers(sourceID, 1, new []{ bufferID });
			else
				end = true;
		}

		AL.alGetSourcei(sourceID, AL.AL_BUFFERS_QUEUED, out var queued);

		if (end && queued == 0)
		{
			stop();
			if (onCompletionListener != null) onCompletionListener.onCompletion(this);
		}

			// A buffer underflow will cause the source to stop.
			AL.alGetSourcei(sourceID, AL.AL_SOURCE_STATE, out var state);
		if (_isPlaying && state != AL.AL_PLAYING) AL.alSourcePlay(sourceID);
	}

	private bool fill(int bufferID)
	{
		((Buffer)tempBuffer).clear();
		int length = read(tempBytes);
		if (length <= 0)
		{
			if (_isLooping)
			{
				loop();
				length = read(tempBytes);
				if (length <= 0) return false;
				if (renderedSecondsQueue.size > 0)
				{
					renderedSecondsQueue.set(0, 0);
				}
			}
			else
				return false;
		}
		float previousLoadedSeconds = renderedSecondsQueue.size > 0 ? renderedSecondsQueue.first() : 0;
		float currentBufferSeconds = maxSecondsPerBuffer * (float)length / (float)bufferSize;
		renderedSecondsQueue.insert(0, previousLoadedSeconds + currentBufferSeconds);

		((Buffer)tempBuffer.put(tempBytes, 0, length)).flip();
		AL.alBufferData(bufferID, format, tempBuffer.array(), tempBuffer.remaining(), sampleRate);
		
		return true;
	}

	public void dispose()
	{
		stop();
		if (audio.noDevice) return;
		if (buffers == null) return;
		AL.alDeleteBuffers(1, buffers.array());
		buffers = null;
		onCompletionListener = null;
	}

	public void setOnCompletionListener(OnCompletionListener listener)
	{
		onCompletionListener = listener;
	}

	public int getSourceId()
	{
		return sourceID;
	}
}
}
