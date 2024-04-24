using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX.Shims
{
	public class InputStream: Closeable
	{
		internal readonly Stream _stream;

		public InputStream(){}

		public InputStream(Stream stream)
		{
			_stream = stream;
		}

		public int available()
		{
			return 0;
		}

		public int read(byte[] buffer)
		{
			return _stream.Read(buffer);
		}

		public int read(byte[] buffer, int offset, int length)
		{
			return _stream.Read(buffer, offset, length);
		}

		public long skip(long n)
		{
			long result = 0;

			while (n > 0)
			{
				n--;

				result += _stream.ReadByte();
			}

			return result;
		}

		public void close()
		{
			throw new NotImplementedException();
		}
	}
}
