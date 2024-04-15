namespace SharpGDX.Shims;

public class RuntimeException : Exception
{
	public RuntimeException()
	{
	}

	public RuntimeException(string message) : base(message)
	{
	}
}