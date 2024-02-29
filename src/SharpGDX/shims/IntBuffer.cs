namespace SharpGDX.shims;

public class IntBuffer
{
	private readonly int[] _buffer = new int[4];

	public IntBuffer(){}

	internal IntBuffer(byte[] byteBuffer)
	{
		// TODO: This is garbage and really should be using bitconverter.
		_buffer = new int[byteBuffer.Length];

		for (var i = 0; i< byteBuffer.Length; i++)
		{
			_buffer[i] = byteBuffer[i];
		}
	}


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