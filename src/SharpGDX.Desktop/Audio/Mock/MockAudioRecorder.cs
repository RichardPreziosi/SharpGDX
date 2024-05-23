﻿using SharpGDX.Audio;

namespace SharpGDX.Desktop.Audio.Mock;

/// <summary>
///     The headless backend does its best to mock elements.
/// </summary>
/// <remarks>
///     This is intended to make code-sharing between server and client as simple as possible.
/// </remarks>
public class MockAudioRecorder : IAudioRecorder
{
	public void dispose()
	{
	}

	public void read(short[] samples, int offset, int numSamples)
	{
	}
}