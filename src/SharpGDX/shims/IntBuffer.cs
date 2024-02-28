namespace SharpGDX.shims;

public class IntBuffer
{
	private readonly int[] _buffer = new int[4];

	public ref int this[int index]
	{
		get =>ref _buffer[index];
	}

	public static explicit operator int[](IntBuffer b)
	{
		return b._buffer;
	}

	public int get(int index)
	{
		return _buffer[index];
	}
}