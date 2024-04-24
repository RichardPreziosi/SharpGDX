using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX.Shims
{
	public class FilterInputStream : InputStream, Closeable
	{
		public int read()
		{
			return @in._stream.ReadByte();
		}

		protected InputStream @in;

		protected FilterInputStream(InputStream @in)
		{
			this.@in = @in;
		}

		public int  read(byte[] b, int off, int len)
		{
			return @in._stream.Read(b, off, len);
		}

		public void close() { }
	}
}
