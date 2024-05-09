using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX.GLFW3
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct GLFWVidMode
	{
		public int Width;
		public int Height;
		public int RedBits;
		public int GreenBits;
		public int BlueBits;
		public int RefreshRate;
	}
}
