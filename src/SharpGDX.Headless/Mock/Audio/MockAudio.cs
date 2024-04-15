namespace SharpGDX.Headless.Mock.Audio;

/// <summary>
///     The headless backend does its best to mock elements.
/// </summary>
/// <remarks>
///     This is intended to make code-sharing between server and client as simple as possible.
/// </remarks>
public class MockAudio : SharpGDX.Audio
{
	public string[] getAvailableOutputDevices()
	{
		return [];
	}

	public AudioDevice newAudioDevice(int samplingRate, bool isMono)
	{
		return new MockAudioDevice();
	}

	public AudioRecorder newAudioRecorder(int samplingRate, bool isMono)
	{
		return new MockAudioRecorder();
	}

	public Music newMusic(FileHandle file)
	{
		return new MockMusic();
	}

	public Sound newSound(FileHandle fileHandle)
	{
		return new MockSound();
	}

	public bool switchOutputDevice(string deviceIdentifier)
	{
		return true;
	}
}