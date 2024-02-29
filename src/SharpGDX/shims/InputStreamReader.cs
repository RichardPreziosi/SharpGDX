using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX.shims
{
	public class InputStreamReader 
	{
	private readonly InputStream _in;

	private readonly UTF8Encoding utf8Decoder;

	public InputStreamReader(InputStream _in)
	{
		this._in = _in;
		// TODO: This was a decoder?
		this.utf8Decoder = new UTF8Encoding();;
	}

	public InputStreamReader(InputStream @in, String encoding) // TODO: throws UnsupportedEncodingException
	: this(@in)
		{
		

	// FIXME this is bad, but some APIs seem to use "ISO-8859-1", fuckers...
	// if (! encoding.equals("UTF-8")) {
	// throw new UnsupportedEncodingException(encoding);
	// }
	}

	public int read(char[] b, int offset, int length) // TODO: throws IOException
	{
		// TODO:
		throw new NotImplementedException();
		//	byte[]
		//	buffer = new byte[length];
		//int c = _in.read(buffer);

		//return c <= 0 ? c : utf8Decoder.decode(buffer, 0, c, b, offset);
	}

	public void close() // TODO: throws IOException
		{
		_in.close();
	}
}
}
