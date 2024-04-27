using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX.Shims
{
	public class ByteArrayOutputStream : OutputStream
	{
		public ByteArrayOutputStream(int  length){}

		public void write(byte[] buffer, int offset, int count)
		{
		}

		public byte[] toByteArray()
		{
			// TODO: Should this stay?
				return stream.ToArray();
		}
	}
}
