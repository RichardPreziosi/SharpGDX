using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX.Shims
{
	public class ShortBuffer : Buffer
	{
		public ShortBuffer(int capacity) : base(capacity)
		{
		}

		public override bool isReadOnly()
		{
			throw new NotImplementedException();
		}
	}
}
