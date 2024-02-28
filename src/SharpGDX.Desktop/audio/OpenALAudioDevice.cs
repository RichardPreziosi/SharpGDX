using OpenTK.Audio.OpenAL;
using Buffer = SharpGDX.shims.Buffer;
using SharpGDX.math;
using SharpGDX.shims;
using SharpGDX.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using SharpGDX.audio;
using static SharpGDX.graphics.Pixmap;

namespace SharpGDX.Desktop.audio
{
	/** @author Nathan Sweet */
	public class OpenALAudioDevice : AudioDevice
	{
		static private readonly int bytesPerSample = 2;

		private readonly OpenALDesktopAudio audio;
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

		public OpenALAudioDevice(OpenALDesktopAudio audio, int sampleRate, bool isMono, int bufferSize, int bufferCount)
		{
			this.audio = audio;
			channels = isMono ? 1 : 2;
			this.bufferSize = bufferSize;
			this.bufferCount = bufferCount;
			this.format = channels > 1 ? (int)ALFormat.Stereo16 : (int)ALFormat.Mono16;
			this.sampleRate = sampleRate;
			secondsPerBuffer = (float)bufferSize / bytesPerSample / channels / sampleRate;
			tempBuffer = BufferUtils.createByteBuffer(bufferSize);
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
			if (length < 0) throw new ArgumentException("length cannot be < 0.");

			if (sourceID == -1)
			{
				sourceID = audio.obtainSource(true);
				if (sourceID == -1) return;
				if (buffers == null)
				{
					buffers = BufferUtils.createIntBuffer(bufferCount);
					AL.GetError();
					AL.GenBuffers((int[])buffers);
					if (AL.GetError() != ALError.NoError)
						throw new GdxRuntimeException("Unabe to allocate audio buffers.");
				}

				AL.Source(sourceID, ALSourceb.Looping, false);
				AL.Source(sourceID, ALSourcef.Gain, volume);
				// Fill initial buffers.
				for (int i = 0; i < bufferCount; i++)
				{
					int bufferID = buffers.get(i);
					int written = Math.Min(bufferSize, length);
					((Buffer)tempBuffer).clear();
					((Buffer)tempBuffer.put(data, offset, written)).flip();
					AL.BufferData(bufferID, (ALFormat)format, (byte[])tempBuffer, sampleRate);
					AL.SourceQueueBuffer(sourceID, bufferID);
					length -= written;
					offset += written;
				}

				AL.SourcePlay(sourceID);
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
				int buffers = AL.GetSource(sourceID, ALGetSourcei.BuffersProcessed);
				while (buffers-- > 0)
				{
					int bufferID = AL.SourceUnqueueBuffer(sourceID);
					if (bufferID == (int)ALError.InvalidValue) break;
					renderedSeconds += secondsPerBuffer;

					((Buffer)tempBuffer).clear();
					((Buffer)tempBuffer.put(data, offset, written)).flip();
					AL.BufferData(bufferID, (ALFormat)format, (byte[])tempBuffer, sampleRate);

					AL.SourceQueueBuffer(sourceID, bufferID);
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

			// A buffer underflow will cause the source to stop.
			if (!_isPlaying || AL.GetSource(sourceID, ALGetSourcei.SourceState) != (int)ALSourceState.Playing)
			{
				AL.SourcePlay(sourceID);
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
			if (sourceID != -1) AL.Source(sourceID, ALSourcef.Gain, volume);
		}

		public float getPosition()
		{
			if (sourceID == -1) return 0;
			return renderedSeconds + AL.GetSource(sourceID, ALSourcef.SecOffset);
		}

		public void setPosition(float position)
		{
			renderedSeconds = position;
		}

		public int getChannels()
		{
			return format == (int)ALFormat.Stereo16 ? 2 : 1;
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

			AL.DeleteBuffers((int[])buffers);
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