using System.Diagnostics;

namespace SharpGDX.utils;

/** Wrapper around System.nanoTime() and System.currentTimeMillis(). Use this if you want to be compatible across all platforms!
 * @author mzechner */
public static class TimeUtils
{
	/** @return The current value of the system timer, in nanoseconds. */
	public static long nanoTime()
	{
		return millisToNanos(millis());
	}

	private static readonly long nanosPerMilli = 1000000;

	public static long millis()
	{
		return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
	}

	/** Convert nanoseconds time to milliseconds
	 * @param nanos must be nanoseconds
	 * @return time value in milliseconds */
	public static long nanosToMillis(long nanos)
	{
		return nanos / nanosPerMilli;
	}

	/** Convert milliseconds time to nanoseconds
	 * @param millis must be milliseconds
	 * @return time value in nanoseconds */
	public static long millisToNanos(long millis)
	{
		return millis * nanosPerMilli;
	}

	/** Get the time in nanos passed since a previous time
	 * @param prevTime - must be nanoseconds
	 * @return - time passed since prevTime in nanoseconds */
	public static long timeSinceNanos(long prevTime)
	{
		return nanoTime() - prevTime;
	}

	/** Get the time in millis passed since a previous time
	 * @param prevTime - must be milliseconds
	 * @return - time passed since prevTime in milliseconds */
	public static long timeSinceMillis(long prevTime)
	{
		return millis() - prevTime;
	}
}