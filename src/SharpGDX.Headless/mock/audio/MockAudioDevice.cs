using SharpGDX.audio;

namespace SharpGDX.Headless.mock.audio;

/**
 * The headless backend does its best to mock elements. This is intended to make code-sharing between server and client as simple
 * as possible.
 */
public class MockAudioDevice : AudioDevice
{
	public void dispose()
	{
	}

	public int getLatency()
	{
		return 0;
	}

	public bool isMono()
	{
		return false;
	}

	public void pause()
	{
	}

	public void resume()
	{
	}

	public void setVolume(float volume)
	{
	}

	public void writeSamples(short[] samples, int offset, int numSamples)
	{
	}

	public void writeSamples(float[] samples, int offset, int numSamples)
	{
	}
}