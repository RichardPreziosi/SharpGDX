using System;
using Buffer = SharpGDX.Shims.Buffer;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SharpGDX.Shims;
using IntPtr = System.IntPtr;

namespace SharpGDX.Desktop
{
	internal static class GL
	{
		private const string Library = "opengl32";

		
		public static string? glGetString(int param)
		{
			return Marshal.PtrToStringUTF8(new IntPtr(glGetString(param)));

			[DllImport(Library)]
			static extern long glGetString(int param);
		}

		[DllImport(Library)]
		public static extern void glDeleteProgram(int program);

		[DllImport(Library)]
		public static extern void glDeleteBuffers(int buffer);

		[DllImport(Library)]
		public static extern void glCompileShader(int shader);

		[DllImport(Library)]
		public static extern void glColorMask(bool r, bool g, bool b, bool a);

		[DllImport(Library)]
		public static extern void glClearStencil(int s);

		[DllImport(Library)]
		public static extern void glClearDepth(float depth);

		[DllImport(Library)]
		public static extern void glClearColor(float red, float green, float blue, float alpha);

		[DllImport(Library)]
		public static extern void glClear(int mask);

		[DllImport(Library)]
		public static extern void glBlendFunc(int sfactor, int dfactor);

		public static void glDrawElements(int mode, Buffer buffer)
		{
			throw new NotImplementedException();
		}

		[DllImport(Library)]
		public static extern void glDrawArrays(int mode, int first, int count);

		public static GLCapabilities createCapabilities()
		{
			return new GLCapabilities();
		}

		public struct GLCapabilities { }

		[DllImport(Library)]
		public static extern void glEnable(int cap);

		[DllImport(Library)]
		public static extern int glGetError();

		[DllImport(Library)]
		public static extern void glViewport(int x, int y, int width, int height);

		[DllImport(Library)]
		public static extern void glVertexAttribPointer(int indx, int size, int type, bool normalized, int stride,
			int ptr);

		[DllImport(Library)]
		public static extern void glDrawElements(int mode, int count, int type, int indices);

		[DllImport(Library)]
		public static extern void glPolygonOffset(float factor, float units);
	}
}
