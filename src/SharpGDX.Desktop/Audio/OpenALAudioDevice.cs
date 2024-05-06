using SharpGDX.Mathematics;
using SharpGDX.Shims;
using SharpGDX.Utils;
using System.Linq;
using System.Runtime.InteropServices;
using static SharpGDX.OpenAL.AL;
using Buffer = SharpGDX.Shims.Buffer;

namespace SharpGDX.Desktop.Audio
{
	/** @author Nathan Sweet */
	public class OpenALAudioDevice : AudioDevice
	{
		static private readonly int bytesPerSample = 2;

		private readonly OpenALLwjgl3Audio audio;
		private readonly int channels;
		private IntBuffer buffers;
		private int sourceID = -1;
		private int format, sampleRate;
		private bool _isPlaying;
		private float volume = 1;
		private float renderedSeconds, secondsPerBuffer;
		private byte[] bytes;
		private readonly int bufferSize;
		private readonly int bufferCount;
		private readonly ByteBuffer tempBuffer;

		public OpenALAudioDevice(OpenALLwjgl3Audio audio, int sampleRate, bool isMono, int bufferSize, int bufferCount)
		{
			this.audio = audio;
			channels = isMono ? 1 : 2;
			this.bufferSize = bufferSize;
			this.bufferCount = bufferCount;
			this.format = channels > 1 ? AL_FORMAT_STEREO16 : AL_FORMAT_MONO16;
			this.sampleRate = sampleRate;
			secondsPerBuffer = (float)bufferSize / bytesPerSample / channels / sampleRate;

			// TODO: This appeared to use the lwjgl BufferUtils.createByteBuffer, not sure if this is technically the same thing.
			tempBuffer = ByteBuffer.allocate(bufferSize);
		}

		public void writeSamples(short[] samples, int offset, int numSamples)
		{
			if (bytes == null || bytes.Length < numSamples * 2) bytes = new byte[numSamples * 2];
			int end = Math.Min(offset + numSamples, samples.Length);
			for (int i = offset, ii = 0; i < end; i++)
			{
				short sample = samples[i];
				bytes[ii++] = (byte)(sample & 0xFF);
				bytes[ii++] = (byte)((sample >> 8) & 0xFF);
			}

			writeSamples(bytes, 0, numSamples * 2);
		}

		public void writeSamples(float[] samples, int offset, int numSamples)
		{
			if (bytes == null || bytes.Length < numSamples * 2) bytes = new byte[numSamples * 2];
			int end = Math.Min(offset + numSamples, samples.Length);
			for (int i = offset, ii = 0; i < end; i++)
			{
				float floatSample = samples[i];
				floatSample = MathUtils.clamp(floatSample, -1f, 1f);
				int intSample = (int)(floatSample * 32767);
				bytes[ii++] = (byte)(intSample & 0xFF);
				bytes[ii++] = (byte)((intSample >> 8) & 0xFF);
			}

			writeSamples(bytes, 0, numSamples * 2);
		}

		public void writeSamples(byte[] data, int offset, int length)
		{
			if (length < 0) throw new IllegalArgumentException("length cannot be < 0.");

			if (sourceID == -1)
			{
				sourceID = audio.obtainSource(true);
				if (sourceID == -1) return;
				if (buffers == null)
				{
					// TODO: This appeared to use the lwjgl BufferUtils.createIntBuffer, not sure if this is technically the same thing.
					buffers = IntBuffer.allocate(bufferCount);
					alGetError();
					alGenBuffers(buffers.remaining(), buffers.array());
					if (alGetError() != AL_NO_ERROR) throw new GdxRuntimeException("Unable to allocate audio buffers.");
				}

				alSourcei(sourceID, AL_LOOPING, AL_FALSE);
				alSourcef(sourceID, AL_GAIN, volume);
				// Fill initial buffers.
				for (int i = 0; i < bufferCount; i++)
				{
					int bufferID = buffers.get(i);
					int written = Math.Min(bufferSize, length);
					((Buffer)tempBuffer).clear();
					((Buffer)tempBuffer.put(data, offset, written)).flip();
					alBufferData(bufferID, format, tempBuffer.array(), tempBuffer.remaining(), sampleRate);
					alSourceQueueBuffers(sourceID, 1, new []{ bufferID });
					length -= written;
					offset += written;
				}

				alSourcePlay(sourceID);
				_isPlaying = true;
			}

			while (length > 0)
			{
				int written = fillBuffer(data, offset, length);
				length -= written;
				offset += written;
			}
		}

		/** Blocks until some of the data could be buffered. */
		private int fillBuffer(byte[] data, int offset, int length)
		{
			int written = Math.Min(bufferSize, length);

			outer:
			while (true)
			{
				alGetSourcei(sourceID, AL_BUFFERS_PROCESSED, out int buffers);
				while (buffers-- > 0)
				{
					// TODO: Verify it fills the bufferID
					int bufferID = 0;
					alSourceUnqueueBuffers(sourceID, 1,new []{ bufferID});
					if (bufferID == AL_INVALID_VALUE) break;
					renderedSeconds += secondsPerBuffer;

					((Buffer)tempBuffer).clear();
					((Buffer)tempBuffer.put(data, offset, written)).flip();
					
					alBufferData(bufferID, format, tempBuffer.array(), tempBuffer.remaining(), sampleRate);

					// TODO: Why?
					int buffer = 0;
					alSourceQueueBuffers(sourceID, bufferID, new[] { buffer});
					goto endOfOuter;
				}

				// Wait for buffer to be free.
				try
				{
					Thread.Sleep((int)(1000 * secondsPerBuffer));
				}
				catch (ThreadInterruptedException ignored)
				{
				}
			}

			endOfOuter:

			alGetSourcei(sourceID, AL_SOURCE_STATE, out var state);
			// A buffer underflow will cause the source to stop.
			if (!_isPlaying || state != AL_PLAYING)
			{
				alSourcePlay(sourceID);
				_isPlaying = true;
			}

			return written;
		}

		public void stop()
		{
			if (sourceID == -1) return;
			audio.freeSource(sourceID);
			sourceID = -1;
			renderedSeconds = 0;
			_isPlaying = false;
		}

		public bool isPlaying()
		{
			if (sourceID == -1) return false;
			return _isPlaying;
		}

		public void setVolume(float volume)
		{
			this.volume = volume;
			if (sourceID != -1) alSourcef(sourceID, AL_GAIN, volume);
		}

		public float getPosition()
		{
			if (sourceID == -1) return 0;

			alGetSourcef(sourceID, AL_SEC_OFFSET, out var offset);

			return renderedSeconds + offset;
		}

		public void setPosition(float position)
		{
			renderedSeconds = position;
		}

		public int getChannels()
		{
			return format == AL_FORMAT_STEREO16 ? 2 : 1;
		}

		public int getRate()
		{
			return sampleRate;
		}

		public void dispose()
		{
			if (buffers == null) return;
			if (sourceID != -1)
			{
				audio.freeSource(sourceID);
				sourceID = -1;
			}

			// TODO: Verify
			alDeleteBuffers(buffers.remaining(), buffers.array());
			buffers = null;
		}

		public bool isMono()
		{
			return channels == 1;
		}

		public int getLatency()
		{
			return (int)((float)bufferSize / bytesPerSample / channels * bufferCount);
		}

		public void pause()
		{
			// A buffer underflow will cause the source to stop.
		}

		public void resume()
		{
			// Automatically resumes when samples are written
		}
	}
}