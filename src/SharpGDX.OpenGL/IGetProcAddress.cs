namespace SharpGDX.OpenGL;

internal interface IGetProcAddress
{
	IntPtr GetProcAddress(string function);
}