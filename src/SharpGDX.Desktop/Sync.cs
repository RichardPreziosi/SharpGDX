﻿using OpenTK.Windowing.GraphicsLibraryFramework;

namespace SharpGDX.Desktop;

/**
 * A highly accurate sync method that continually adapts to the system it runs on to provide reliable results.
 * 
 * @author Riven
 * @author kappaOne
 */
internal class Sync
{
	/**
	 * number of nano seconds in a second
	 */
	private static readonly long NANOS_IN_SECOND = 1000L * 1000L * 1000L;

	/**
	 * whether the initialisation code has run
	 */
	private bool initialised;

	/**
	 * The time to sleep/yield until the next frame
	 */
	private long nextFrame;

	/**
	 * for calculating the averages the previous sleep/yield times are stored
	 */
	private readonly RunningAvg sleepDurations = new(10);

	private readonly RunningAvg yieldDurations = new(10);

	/**
	 * An accurate sync method that will attempt to run at a constant frame rate. It should be called once every frame.
	 * 
	 * @param fps - the desired frame rate, in frames per second
	 */
	public void sync(int fps)
	{
		if (fps <= 0)
		{
			return;
		}

		if (!initialised)
		{
			initialise();
		}

		try
		{
			// sleep until the average sleep time is greater than the time remaining till nextFrame
			for (long t0 = getTime(), t1; nextFrame - t0 > sleepDurations.avg(); t0 = t1)
			{
				Thread.Sleep(1);
				sleepDurations.add((t1 = getTime()) - t0); // update average sleep time
			}

			// slowly dampen sleep average if too high to avoid yielding too much
			sleepDurations.dampenForLowResTicker();

			// yield until the average yield time is greater than the time remaining till nextFrame
			for (long t0 = getTime(), t1; nextFrame - t0 > yieldDurations.avg(); t0 = t1)
			{
				Thread.Yield();
				yieldDurations.add((t1 = getTime()) - t0); // update average yield time
			}
		}
		catch (ThreadInterruptedException e)
		{
		}

		// schedule next frame, drop frame(s) if already too late for next frame
		nextFrame = Math.Max(nextFrame + NANOS_IN_SECOND / fps, getTime());
	}

	/**
	 * Get the system time in nano seconds
	 * 
	 * @return will return the current time in nano's
	 */
	private long getTime()
	{
		return (long)(GLFW.GetTime() * NANOS_IN_SECOND);
	}

	/**
	 * This method will initialise the sync method by setting initial values for sleepDurations/yieldDurations and nextFrame.
	 * 
	 * If running on windows it will start the sleep timer fix.
	 */
	private void initialise()
	{
		initialised = true;

		sleepDurations.init(1000 * 1000);
		yieldDurations.init((int)(-(getTime() - getTime()) * 1.333));

		nextFrame = getTime();

		if (OperatingSystem.IsWindows())
		{
			// On windows the sleep functions can be highly inaccurate by
			// over 10ms making in unusable. However it can be forced to
			// be a bit more accurate by running a separate sleeping daemon
			// thread.
			var timerAccuracyThread = new Thread(() =>
			{
				try
				{
					Thread.Sleep(int.MaxValue);
				}
				catch (Exception e)
				{
				}
			});

			timerAccuracyThread.Name = "LWJGL3 Timer";
			timerAccuracyThread.IsBackground = true;
			timerAccuracyThread.Start();
		}
	}

	private class RunningAvg
	{
		private static readonly float DAMPEN_FACTOR = 0.9f; // don't change: 0.9f is exactly right!

		private static readonly long DAMPEN_THRESHOLD = 10 * 1000L * 1000L; // 10ms
		private readonly long[] slots;
		private int offset;

		public RunningAvg(int slotCount)
		{
			slots = new long[slotCount];
			offset = 0;
		}

		public void add(long value)
		{
			slots[offset++ % slots.Length] = value;
			offset %= slots.Length;
		}

		public long avg()
		{
			long sum = 0;
			for (var i = 0; i < slots.Length; i++)
			{
				sum += slots[i];
			}

			return sum / slots.Length;
		}

		public void dampenForLowResTicker()
		{
			if (avg() > DAMPEN_THRESHOLD)
			{
				for (var i = 0; i < slots.Length; i++)
				{
					slots[i] = (long)(slots[i] * DAMPEN_FACTOR);
				}
			}
		}

		public void init(long value)
		{
			while (offset < slots.Length)
			{
				slots[offset++] = value;
			}
		}
	}
}