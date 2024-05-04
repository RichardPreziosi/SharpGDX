using System.Runtime.InteropServices;

namespace SharpGDX.OpenGL;

internal sealed class GetProcAddressWindows : IGetProcAddress
{
	private readonly IntPtr _openGL32 = NativeLibrary.Load("opengl32.dll");

	public IntPtr GetProcAddress(string function)
	{
		var address = wglGetProcAddress(function);

		if (address == IntPtr.Zero)
		{
			address = GetProcAddress(_openGL32, function);
		}

		return address;
	}

	[DllImport("Kernel32.dll")]
	private static extern IntPtr GetProcAddress(IntPtr library, string lpszProc);

	[DllImport("opengl32", EntryPoint = "wglGetProcAddress", ExactSpelling = true)]
	private static extern IntPtr wglGetProcAddress(string lpszProc);
}