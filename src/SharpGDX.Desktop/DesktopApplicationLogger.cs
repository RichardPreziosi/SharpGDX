namespace SharpGDX.Desktop;

/**
 * Default implementation of {@link ApplicationLogger} for Lwjgl3
 */
public class Lwjgl3ApplicationLogger : ApplicationLogger
{
	public void debug(string tag, string message)
	{
		Console.WriteLine($"[{tag}] {message}");
	}

	public void debug(string tag, string message, Exception exception)
	{
		Console.WriteLine($"[{tag}] {message}");
		Console.WriteLine(exception.StackTrace);
	}

	public void error(string tag, string message)
	{
		Console.WriteLine($"[{tag}] {message}");
	}

	public void error(string tag, string message, Exception exception)
	{
		Console.WriteLine($"[{tag}] {message}");
		Console.WriteLine(exception.StackTrace);
	}

	public void log(string tag, string message)
	{
		Console.WriteLine($"[{tag}] {message}");
	}

	public void log(string tag, string message, Exception exception)
	{
		Console.WriteLine($"[{tag}] {message}");
		Console.WriteLine(exception.StackTrace);
	}
}