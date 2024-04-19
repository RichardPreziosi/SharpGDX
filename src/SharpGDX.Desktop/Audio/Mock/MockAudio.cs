namespace SharpGDX.Desktop.Audio.Mock;

/// <summary>
///     The headless backend does its best to mock elements.
/// </summary>
/// <remarks>
///     This is intended to make code-sharing between server and client as simple as possible.
/// </remarks>
public class MockAudio : Lwjgl3Audio
{
	public AudioDevice newAudioDevice(int samplingRate, bool isMono)
	{
		return new MockAudioDevice();
	}

	public AudioRecorder newAudioRecorder(int samplingRate, bool isMono)
	{
		return new MockAudioRecorder();
	}

	public Sound newSound(FileHandle fileHandle)
	{
		return new MockSound();
	}

	public Music newMusic(FileHandle file)
	{
		return new MockMusic();
	}

	public bool switchOutputDevice(String deviceIdentifier)
	{
		return true;
	}

	public String[] getAvailableOutputDevices()
	{
		return new String[0];
	}

	public void update()
	{
	}

	public void dispose()
	{
	}
}