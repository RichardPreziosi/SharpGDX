using SharpGDX.math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX.utils
{
	internal class ObjectSet
	{
		internal static int tableSize(int capacity, float loadFactor)
		{
			if (capacity < 0) throw new ArgumentException("capacity must be >= 0: " + capacity);
			int tableSize = MathUtils.nextPowerOfTwo(Math.Max(2, (int)Math.Ceiling(capacity / loadFactor)));
			if (tableSize > 1 << 30) throw new ArgumentException("The required capacity is too large: " + capacity);
			return tableSize;
		}
	}
}
