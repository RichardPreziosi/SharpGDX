namespace SharpGDX;

/**
 * The ApplicationLogger provides an interface for a libGDX Application to log messages and exceptions. A default implementations
 * is provided for each backend, custom implementations can be provided and set using
 * {@link Application#setApplicationLogger(ApplicationLogger) }
 */
public interface ApplicationLogger
{
	/**
	 * Logs a debug message with a tag
	 */
	public void debug(string tag, string message);

	/**
	 * Logs a debug message and exception with a tag
	 */
	public void debug(string tag, string message, ExecutionContext exception);

	/**
	 * Logs an error message with a tag
	 */
	public void error(string tag, string message);

	/**
	 * Logs an error message and exception with a tag
	 */
	public void error(string tag, string message, ExecutionContext exception);

	/**
	 * Logs a message with a tag
	 */
	public void log(string tag, string message);

	/**
	 * Logs a message and exception with a tag
	 */
	public void log(string tag, string message, ExecutionContext exception);
}