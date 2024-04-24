using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX.Shims
{
	public class OutputStream : Closeable
	{
		protected Stream stream;
		public void write(byte[] buffer, int offset, int length)
		{

		}

		public void flush()
		{
			stream.Flush();
		}

		public void write(byte[] bytes){}

		public void close()
		{
			// TODO: Should probably be disposing honestly. -RP
			stream.Close();
		}
	}
}
