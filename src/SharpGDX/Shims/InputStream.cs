using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX.Shims
{
	public class InputStream
	{
		public int available()
		{
			return 0;
		}

		public int read(byte[] buffer)
		{
			return 0;
		}

		public long skip(long n) { return 0; }
	}
}
