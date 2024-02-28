using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGDX.audio;
using SharpGDX.utils;

namespace SharpGDX.Desktop.audio
{
	/** @author mzechner */
	public class DesktopAudioRecorder : AudioRecorder
	{
	// TODO: private TargetDataLine line;
	private byte[] buffer = new byte[1024 * 4];

	public DesktopAudioRecorder(int samplingRate, bool isMono)
	{
		// TODO:
		//try
		//{
		//	AudioFormat format = new AudioFormat(Encoding.PCM_SIGNED, samplingRate, 16, isMono ? 1 : 2, isMono ? 2 : 4, samplingRate,
		//		false);
		//	line = AudioSystem.getTargetDataLine(format);
		//	line.open(format, buffer.length);
		//	line.start();
		//}
		//catch (Exception ex)
		//{
		//	throw new GdxRuntimeException("Error creating JavaSoundAudioRecorder.", ex);
		//}
	}

	public void read(short[] samples, int offset, int numSamples)
	{
		// TODO: 
		throw new NotImplementedException();
		//if (buffer.Length < numSamples * 2) buffer = new byte[numSamples * 2];

		//int toRead = numSamples * 2;
		//int read = 0;
		//while (read != toRead)
		//	read += line.read(buffer, read, toRead - read);

		//for (int i = 0, j = 0; i < numSamples * 2; i += 2, j++)
		//	samples[offset + j] = (short)((buffer[i + 1] << 8) | (buffer[i] & 0xff));
	}

	public void dispose()
	{
		// TODO: line.close();
	}
	}
}
