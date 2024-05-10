using System.Runtime.InteropServices;
using System.Security;

namespace SharpGDX.OpenGL;

// TODO: Should we just do this in GLFW?
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
[SuppressUnmanagedCodeSecurity]
public unsafe delegate void DebugProc(int source, int type, int id, int severity, int length, IntPtr message,
	IntPtr userParam);