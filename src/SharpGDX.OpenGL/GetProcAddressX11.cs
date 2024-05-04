using System.Runtime.InteropServices;

namespace SharpGDX.OpenGL;

internal sealed class GetProcAddressX11 : IGetProcAddress
{
	public IntPtr GetProcAddress(string function)
	{
		return glxGetProcAddress(function);
	}

	[DllImport("opengl32.dll", EntryPoint = "glXGetProcAddress")]
	private static extern IntPtr glxGetProcAddress([MarshalAs(UnmanagedType.LPTStr)] string procName);
}