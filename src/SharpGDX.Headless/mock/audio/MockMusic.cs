﻿using SharpGDX.audio;

namespace SharpGDX.Headless.mock.audio;

/**
 * The headless backend does its best to mock elements. This is intended to make code-sharing between server and client as simple
 * as possible.
 */
public class MockMusic : Music
{
	public void dispose()
	{
	}

	public float getPosition()
	{
		return 0;
	}

	public float getVolume()
	{
		return 0;
	}

	public bool isLooping()
	{
		return false;
	}

	public bool isPlaying()
	{
		return false;
	}

	public void pause()
	{
	}

	public void play()
	{
	}

	public void setLooping(bool isLooping)
	{
	}

	public void setOnCompletionListener(Music.OnCompletionListener listener)
	{
	}

	public void setPan(float pan, float volume)
	{
	}

	public void setPosition(float position)
	{
	}

	public void setVolume(float volume)
	{
	}

	public void stop()
	{
	}
}