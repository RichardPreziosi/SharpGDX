﻿using SharpGDX.audio;

namespace SharpGDX.Desktop.audio.mock;

/**
 * The headless backend does its best to mock elements. This is intended to make code-sharing between server and client as simple
 * as possible.
 */
public class MockSound : Sound
{
	public void dispose()
	{
	}

	public long loop()
	{
		return 0;
	}

	public long loop(float volume)
	{
		return 0;
	}

	public long loop(float volume, float pitch, float pan)
	{
		return 0;
	}

	public void pause()
	{
	}

	public void pause(long soundId)
	{
	}

	public long play()
	{
		return 0;
	}

	public long play(float volume)
	{
		return 0;
	}

	public long play(float volume, float pitch, float pan)
	{
		return 0;
	}

	public void resume()
	{
	}

	public void resume(long soundId)
	{
	}

	public void setLooping(long soundId, bool looping)
	{
	}

	public void setPan(long soundId, float pan, float volume)
	{
	}

	public void setPitch(long soundId, float pitch)
	{
	}

	public void setVolume(long soundId, float volume)
	{
	}

	public void stop()
	{
	}

	public void stop(long soundId)
	{
	}
}