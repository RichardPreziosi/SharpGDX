using SharpGDX.files;
using SharpGDX.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using SharpGDX.shims;

namespace SharpGDX.Desktop.audio
{
	public class Wav
	{
		public class Music : OpenALMusic
		{
			private WavInputStream input;

			public Music(OpenALDesktopAudio audio, FileHandle file)
				: base(audio, file)
			{

				input = new WavInputStream(file);
				if (audio.noDevice) return;
				setup(input.channels, input.sampleRate);
			}

			public override int read(byte[] buffer)
			{
				if (input == null)
				{
					input = new WavInputStream(file);
					setup(input.channels, input.sampleRate);
				}

				try
				{
					return input.read(buffer);
				}
				catch (IOException ex)
				{
					throw new GdxRuntimeException("Error reading WAV file: " + file, ex);
				}
			}

			public override void reset()
			{
				StreamUtils.closeQuietly(input);
				input = null;
			}
		}

		public class Sound : OpenALSound
		{
			public Sound(OpenALDesktopAudio audio, FileHandle file)
				: base(audio)
			{

				if (audio.noDevice) return;

				WavInputStream input = null;
				try
				{
					input = new WavInputStream(file);
					setup(StreamUtils.copyStreamToByteArray(input, input.dataRemaining), input.channels,
						input.sampleRate);
				}
				catch (IOException ex)
				{
					throw new GdxRuntimeException("Error reading WAV file: " + file, ex);
				}
				finally
				{
					StreamUtils.closeQuietly(input);
				}
			}
		}

		/** @author Nathan Sweet */
		public class WavInputStream : Stream
		{
			private readonly BinaryReader _reader;

			public int channels, sampleRate, dataRemaining;

			public WavInputStream(FileHandle file)
			{
				_reader = new BinaryReader(file.read());

				try
				{
					if (_reader.Read() != 'R' || _reader.Read() != 'I' || _reader.Read() != 'F' ||
					    _reader.Read() != 'F')
						throw new GdxRuntimeException("RIFF header not found: " + file);

					skipFully(4);

					if (_reader.Read() != 'W' || _reader.Read() != 'A' || _reader.Read() != 'V' ||
					    _reader.Read() != 'E')
						throw new GdxRuntimeException("Invalid wave file header: " + file);

					int fmtChunkLength = seekToChunk('f', 'm', 't', ' ');

					// http://www-mmsp.ece.mcgill.ca/Documents/AudioFormats/WAVE/WAVE.html
					// http://soundfile.sapp.org/doc/WaveFormat/
					int type = _reader.Read() & 0xff | (_reader.Read() & 0xff) << 8;
					if (type != 1)
					{
						String name;
						switch (type)
						{
							case 0x0002:
								name = "ADPCM";
								break;
							case 0x0003:
								name = "IEEE float";
								break;
							case 0x0006:
								name = "8-bit ITU-T G.711 A-law";
								break;
							case 0x0007:
								name = "8-bit ITU-T G.711 u-law";
								break;
							case 0xFFFE:
								name = "Extensible";
								break;
							default:
								name = "Unknown";
								break;
						}

						throw new GdxRuntimeException("WAV files must be PCM, unsupported format: " + name + " (" +
						                              type + ")");
					}

					channels = _reader.Read() & 0xff | (_reader.Read() & 0xff) << 8;
					if (channels != 1 && channels != 2)
						throw new GdxRuntimeException("WAV files must have 1 or 2 channels: " + channels);
					sampleRate = _reader.Read() & 0xff | (_reader.Read() & 0xff) << 8 | (_reader.Read() & 0xff) << 16 |
					             (_reader.Read() & 0xff) << 24;
					skipFully(6);

					int bitsPerSample = _reader.Read() & 0xff | (_reader.Read() & 0xff) << 8;
					if (bitsPerSample != 16)
						throw new GdxRuntimeException("WAV files must have 16 bits per sample: " + bitsPerSample);

					skipFully(fmtChunkLength - 16);

					dataRemaining = seekToChunk('d', 'a', 't', 'a');
				}
				catch (Exception ex)
				{
					StreamUtils.closeQuietly(_reader);
					throw new GdxRuntimeException("Error reading WAV file: " + file, ex);
				}
			}

			private int seekToChunk(char c1, char c2, char c3, char c4) // TODO: throws IOException
			{
				while (true)
				{
					bool found = _reader.Read() == c1;
					found &= _reader.Read() == c2;
					found &= _reader.Read() == c3;
					found &= _reader.Read() == c4;
					int chunkLength = _reader.Read() & 0xff | (_reader.Read() & 0xff) << 8 |
					                  (_reader.Read() & 0xff) << 16 | (_reader.Read() & 0xff) << 24;
					if (chunkLength == -1) throw new IOException("Chunk not found: " + c1 + c2 + c3 + c4);
					if (found) return chunkLength;
					skipFully(chunkLength);
				}
			}

			private void skipFully(int count) // TODO: throws IOException
			{
				while (count > 0)
				{
					var skipped = Skip(_reader, count);
					if (skipped <= 0) throw new EndOfStreamException("Unable to skip.");
					count -= skipped;
				}
			}

			internal int Skip(BinaryReader stream, int numberOfBytes)
			{
				if (numberOfBytes <= 0)
				{
					return 0;
				}

				var n = numberOfBytes;
				var bufferLength = Math.Min(1024, n);
				var data = new char[bufferLength];

				while (n > 0)
				{
					var r = stream.Read(data, 0, Math.Min(bufferLength, n));

					if (r < 0)
					{
						break;
					}

					n -= r;
				}

				return numberOfBytes - n;
			}

			public int read(byte[] buffer) // TODO: throws IOException
			{
				if (dataRemaining == 0) return -1;
				int offset = 0;
				do
				{
					int length = Math.Min(_reader.Read(buffer, offset, buffer.Length - offset), dataRemaining);
					if (length == -1)
					{
						if (offset > 0) return offset;
						return -1;
					}

					offset += length;
					dataRemaining -= length;
				} while (offset < buffer.Length);

				return offset;
			}

			public override void Flush()
			{
				throw new NotImplementedException();
			}

			public override int Read(byte[] buffer, int offset, int count)
			{
				throw new NotImplementedException();
			}

			public override long Seek(long offset, SeekOrigin origin)
			{
				throw new NotImplementedException();
			}

			public override void SetLength(long value)
			{
				throw new NotImplementedException();
			}

			public override void Write(byte[] buffer, int offset, int count)
			{
				throw new NotImplementedException();
			}

			public override bool CanRead { get; }
			public override bool CanSeek { get; }
			public override bool CanWrite { get; }
			public override long Length { get; }
			public override long Position { get; set; }
		}
	}
}