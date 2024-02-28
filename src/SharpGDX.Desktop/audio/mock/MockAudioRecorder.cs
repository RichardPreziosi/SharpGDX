using SharpGDX.audio;

namespace SharpGDX.Desktop.audio.mock;

/**
 * The headless backend does its best to mock elements. This is intended to make code-sharing between server and client as simple
 * as possible.
 */
public class MockAudioRecorder : AudioRecorder
{
	public void dispose()
	{
	}

	public void read(short[] samples, int offset, int numSamples)
	{
	}
}