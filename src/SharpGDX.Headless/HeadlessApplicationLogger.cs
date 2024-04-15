namespace SharpGDX.Headless;

/// <summary>
///     Default implementation of <see cref="ApplicationLogger" /> for headless.
/// </summary>
public class HeadlessApplicationLogger : ApplicationLogger
{
	public void debug(string tag, string message)
	{
		Console.WriteLine("[" + tag + "] " + message);
	}

	public void debug(string tag, string message, Exception exception)
	{
		Console.WriteLine("[" + tag + "] " + message);
		Console.WriteLine(exception.StackTrace);
	}

	public void error(string tag, string message)
	{
		Console.WriteLine("[" + tag + "] " + message);
	}

	public void error(string tag, string message, Exception exception)
	{
		Console.WriteLine("[" + tag + "] " + message);
		Console.WriteLine(exception.StackTrace);
	}

	public void log(string tag, string message)
	{
		Console.WriteLine("[" + tag + "] " + message);
	}

	public void log(string tag, string message, Exception exception)
	{
		Console.WriteLine("[" + tag + "] " + message);
		Console.WriteLine(exception.StackTrace);
	}
}