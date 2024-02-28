namespace SharpGDX.shims;

public class ShortBuffer : Buffer
{
	private short[] _buffer;

	public static explicit operator short[](ShortBuffer b)
	{
		return b._buffer;
	}
}