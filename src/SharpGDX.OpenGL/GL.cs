using System.Reflection;
using System.Runtime.InteropServices;

namespace SharpGDX.OpenGL;

public static class GL
{
	private static IGetProcAddress? _getProcAddress;

	static GL()
	{
		foreach (var f in typeof(Functions).GetFields(BindingFlags.Static | BindingFlags.NonPublic))
		{
			f.SetValue(null, GetDelegate(f.Name, f.FieldType));
		}

		// TODO: rebuildExtensionList = true;
	}

	public static Delegate? GetDelegate(string name, Type signature)
	{
		return GetExtensionDelegate(name, signature);
	}

	public static void glBindTexture(int target, int texture)
	{
		Functions.glBindTexture(target, (uint)texture);
	}

	public static void glBlendFunc(int sfactor, int dfactor)
	{
		Functions.glBlendFunc(sfactor, dfactor);
	}

	public static void glClear(int mask)
	{
		Functions.glClear(mask);
	}

	public static void glClearColor(float red, float green, float blue, float alpha)
	{
		Functions.glClearColor(red, green, blue, alpha);
	}

	public static void glClearDepth(double depth)
	{
		Functions.glClearDepth(depth);
	}

	public static void glClearStencil(int s)
	{
		Functions.glClearStencil(s);
	}

	public static void glColorMask(bool red, bool green, bool blue, bool alpha)
	{
		Functions.glColorMask(red, green, blue, alpha);
	}

	public static void glCompileShader(int shader)
	{
		Functions.glCompileShader((uint)shader);
	}

	public static int glCreateShader(int type)
	{
		return Functions.glCreateShader(type);
	}

	public static void glDeleteBuffers(int n, IntPtr buffers)
	{
		// TODO: Verify
		Functions.glDeleteBuffers(n, buffers);
	}

	public static void glDeleteProgram(int program)
	{
		Functions.glDeleteProgram((uint)program);
	}

	public static void glDrawArrays(int mode, int first, int count)
	{
		Functions.glDrawArrays(mode, first, count);
	}

	public static void glDrawElements(int mode, int count, int type, IntPtr indices)
	{
		Functions.glDrawElements(mode, count, type, indices);
	}

	public static void glEnable(int cap)
	{
		Functions.glEnable(cap);
	}

	public static void glGenTextures(int n, IntPtr textures)
	{
		Functions.glGenTextures(n, textures);
	}

	public static int glGetError()
	{
		return Functions.glGetError();
	}

	public static void glGetShaderInfoLog(int shader, int bufSize, IntPtr length, IntPtr infoLog)
	{
		Functions.glGetShaderInfoLog((uint)shader, bufSize, length, infoLog);
	}

	public static void glGetShaderiv(int shader, int pname, [Out] IntPtr @params)
	{
		Functions.glGetShaderiv((uint)shader, pname, @params);
	}

	public static string glGetString(int name)
	{
		return Marshal.PtrToStringAnsi(Functions.glGetString(name));
	}

	public static void glPixelStorei(int pname, int param)
	{
		Functions.glPixelStorei(pname, param);
	}

	public static void glPolygonOffset(float factor, float units)
	{
		Functions.glPolygonOffset(factor, units);
	}

	public static void glShaderSource(int shader, int count, string[] @string, IntPtr length)
	{
		Functions.glShaderSource((uint)shader, count, @string, length);
	}

	public static void glTexImage2D(int target, int level, int internalformat, int width, int height, int border,
		int format, int type, IntPtr pixels)
	{
		Functions.glTexImage2D(target, level, internalformat, width, height, border, format, type, pixels);
	}

	public static void glTexParameteri(int target, int pname, int param)
	{
		Functions.glTexParameteri(target, pname, param);
	}

	public static void glVertexAttribPointer(int index, int size, int type, bool normalized, int stride, IntPtr pointer)
	{
		Functions.glVertexAttribPointer((uint)index, size, type, normalized, stride, pointer);
	}

	public static void glViewport(int x, int y, int width, int height)
	{
		Functions.glViewport(x, y, width, height);
	}

	internal static Delegate? GetExtensionDelegate(string name, Type signature)
	{
		var address = GetAddress(name);

		return address == IntPtr.Zero ? null : Marshal.GetDelegateForFunctionPointer(address, signature);
	}

	private static IntPtr GetAddress(string function)
	{
		if (_getProcAddress == null)
		{
			if (OperatingSystem.IsWindows())
			{
				_getProcAddress = new GetProcAddressWindows();
			}
			else if (OperatingSystem.IsLinux())
			{
				_getProcAddress = new GetProcAddressX11();
			}
			else if (OperatingSystem.IsMacOS())
			{
				_getProcAddress = new GetProcAddressOSX();
			}
			else
			{
				throw new PlatformNotSupportedException(
					"Extension loading is only supported under Mac OS X, Unix/X11 and Windows.");
			}
		}

		return _getProcAddress.GetProcAddress(function);
	}
}