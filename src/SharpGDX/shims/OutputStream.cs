namespace SharpGDX.shims;

public class OutputStream
{
	private readonly Stream _stream;

	public void write(byte[] buffer, int offset, int count)
	{
		_stream.Write(buffer, offset, count);
	}
}