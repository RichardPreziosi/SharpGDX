using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX.Headless
{
	/** Default implementation of {@link ApplicationLogger} for headless */
	public class HeadlessApplicationLogger : ApplicationLogger
	{

	public void log(String tag, String message)
	{
		Console.WriteLine($"[{tag}] {message}");
		}

	public void log(String tag, String message, Exception exception)
	{
		Console.WriteLine($"[{tag}] {message}");
		Console.WriteLine(exception.StackTrace);
		}

	public void error(String tag, String message)
	{
		Console.WriteLine($"[{tag}] {message}");
		}

	public void error(String tag, String message, Exception exception)
	{
		Console.WriteLine($"[{tag}] {message}");
		Console.WriteLine(exception.StackTrace);
		}

	
	public void debug(String tag, String message)
	{
		Console.WriteLine($"[{tag}] {message}");
		}

	public void debug(String tag, String message, Exception exception)
	{
		Console.WriteLine($"[{tag}] {message}");
		Console.WriteLine(exception.StackTrace);
		}
	}
}
