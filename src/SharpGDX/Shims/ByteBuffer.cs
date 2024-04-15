using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX.Shims
{
	public class ByteBuffer : Buffer
	{
		public ByteBuffer(int capacity) : base(capacity)
		{
		}

		public override bool isReadOnly()
		{
			throw new NotImplementedException();
		}
	}
}
