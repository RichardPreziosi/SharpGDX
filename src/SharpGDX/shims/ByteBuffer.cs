namespace SharpGDX.shims;

public class ByteBuffer : Buffer
{
	private bool isBigEndian;

	private byte[] _buffer;

	public static explicit operator byte[](ByteBuffer b)
	{
		return b._buffer;
	}

	public static ByteBuffer allocateDirect(int capacity)
	{
		if (capacity < 0)
		{
			throw new ArgumentException();
		}

		// TODO: return BufferFactory.newDirectByteBuffer(capacity);
		throw new NotImplementedException();
	}

	public ByteBuffer order(ByteOrder byteOrder)
	{
		isBigEndian = byteOrder == ByteOrder.BIG_ENDIAN;
		return this;
	}

	public ByteBuffer put(byte[] src, int off, int len)
	{
		int length = src.Length;
		if ((off < 0) || (len < 0) || ((long)off + (long)len > length))
		{
			throw new IndexOutOfRangeException();
		}

		if (len > remaining())
		{
			throw new BufferOverflowException();
		}

		for (int i = off; i < off + len; i++)
		{
			put(src[i]);
		}

		return this;
	}

	public ByteBuffer put(byte[] src)
	{
		return put(src, 0, src.Length);
	}

	public ByteBuffer put(byte b)
	{
		throw new NotImplementedException();
	}

	public IntBuffer asIntBuffer()
	{
		throw new NotImplementedException();
	}

	public DoubleBuffer asDoubleBuffer()
	{
		throw new NotImplementedException();
	}

	public FloatBuffer asFloatBuffer()
	{
		throw new NotImplementedException();
	}

	public LongBuffer asLongBuffer()
	{
		throw new NotImplementedException();
	}

	public CharBuffer asCharBuffer()
	{
		throw new NotImplementedException();
	}

	public ShortBuffer asShortBuffer()
	{
		throw new NotImplementedException();
	}
}