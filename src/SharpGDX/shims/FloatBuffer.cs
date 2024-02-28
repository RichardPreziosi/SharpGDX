using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX.shims
{
	public class FloatBuffer : Buffer
	{
		public ref float this[int index]
		{
			get => ref _buffer[index];
		}

		private float[] _buffer;

		public static explicit operator float[](FloatBuffer b)
		{
			return b._buffer;
		}

		public FloatBuffer put(float[] src)
		{
			return put(src, 0, src.Length);
		}

		public FloatBuffer put(float f)
		{
			throw new NotImplementedException();
		}

		public FloatBuffer put(float[] src, int off, int len)
		{
			int length = src.Length;
			if (off < 0 || len < 0 || (long)off + (long)len > length)
			{
				throw new IndexOutOfRangeException();
			}

			if (len > remaining())
			{
				throw new BufferOverflowException();
			}
			for (int i = off; i < off + len; i++)
			{
				put(src[i]);
			}
			return this;
		}
	}
}
