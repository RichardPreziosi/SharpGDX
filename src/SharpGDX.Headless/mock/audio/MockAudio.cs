using SharpGDX.audio;
using SharpGDX.files;

namespace SharpGDX.Headless.mock.audio;

/**
 * The headless backend does its best to mock elements. This is intended to make code-sharing between server and client as simple
 * as possible.
 */
public class MockAudio : Audio
{
	public string[] getAvailableOutputDevices()
	{
		return new string[0];
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