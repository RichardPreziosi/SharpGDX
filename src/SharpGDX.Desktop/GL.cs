using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using SharpGDX.Shims;
using SharpGDX.Utils;
using Buffer = SharpGDX.Shims.Buffer;

namespace SharpGDX.Desktop;

internal static class GL
{
	private const string Library = "opengl32.dll";
	private static readonly ThreadLocal<GLCapabilities> capabilitiesTLS = new();
	private static readonly IFunctionProvider FunctionProvider;

	private class Delegates
	{
		public delegate int glGetError();
		public delegate int glGetIntegerv(int pname, long parameters);
		public delegate long glGetString(int param);
		public delegate long glGetStringi(int name, uint index);
		public delegate int glCreateShader(int type);
		public delegate void glShaderSource(int shader, int count, long strings, long length);
	}
	
	public static int glGetError()
	{
		return FunctionProvider.Get<Delegates.glGetError>().Invoke();
	}

	static GL()
	{
		if (OperatingSystem.IsWindows())
		{
			FunctionProvider = new WindowsFunctionProvider();
		}
		else
		{
			// TODO: Implement Mac and Linux
			throw new NotImplementedException();
		}
	}

	public static int glGetIntegerv(int pname, IntBuffer parameters)
	{
		GCHandle parametersHandle = GCHandle.Alloc(parameters.array(), GCHandleType.Pinned);

		var result =
			FunctionProvider!.Get<Delegates.glGetIntegerv>()!.Invoke(pname, parametersHandle.AddrOfPinnedObject());

		parametersHandle.Free();

		return result;
	}

	public static GLCapabilities createCapabilities()
	{
		// TODO: This is not needed
		return new GLCapabilities();
	}

	[DllImport(Library)]
	public static extern void glBindTexture(int target, int texture);

	[DllImport(Library)]
	public static extern void glBlendFunc(int sfactor, int dfactor);

	[DllImport(Library)]
	public static extern void glClear(int mask);

	[DllImport(Library)]
	public static extern void glClearColor(float red, float green, float blue, float alpha);

	[DllImport(Library)]
	public static extern void glClearDepth(float depth);

	[DllImport(Library)]
	public static extern void glClearStencil(int s);

	[DllImport(Library)]
	public static extern void glColorMask(bool r, bool g, bool b, bool a);

	[DllImport(Library)]
	public static extern void glCompileShader(int shader);

	public static int glCreateShader(int type)
	{
		return FunctionProvider.Get<Delegates.glCreateShader>().Invoke(type);
	}

	public static void glShaderSource(int shader, string @string)
	{
		var stringAddresses = Marshal.StringToCoTaskMemUTF8(@string);
		
		FunctionProvider.Get<Delegates.glShaderSource>().Invoke(shader, 1, stringAddresses, stringAddresses -4);
	}

	[DllImport(Library)]
	public static extern void glDeleteBuffers(int buffer);

	[DllImport(Library)]
	public static extern void glDeleteProgram(int program);

	[DllImport(Library)]
	public static extern void glDrawArrays(int mode, int first, int count);

	public static void glDrawElements(int mode, Buffer buffer)
	{
		throw new NotImplementedException();
	}

	[DllImport(Library)]
	public static extern void glDrawElements(int mode, int count, int type, int indices);

	[DllImport(Library)]
	public static extern void glEnable(int cap);

	public static void glGenTextures(IntBuffer textures)
	{
		var xHandle = GCHandle.Alloc(textures.array(), GCHandleType.Pinned);
		glGenTextures(textures.remaining(), xHandle.AddrOfPinnedObject());

		xHandle.Free();
	}

	public static int glGenTextures()
	{
		var textures = IntBuffer.allocate(1);
		var xHandle = GCHandle.Alloc(textures.array(), GCHandleType.Pinned);
		glGenTextures(textures.remaining(), xHandle.AddrOfPinnedObject());

		xHandle.Free();

		return textures.get(0);
	}
	
	public static string? glGetString(int param)
	{
		return Marshal.PtrToStringUTF8(new IntPtr(FunctionProvider.Get<Delegates.glGetString>().Invoke(param)));
	}

	public static string? glGetStringi(int param, uint index)
	{
		return Marshal.PtrToStringUTF8(new IntPtr(FunctionProvider.Get<Delegates.glGetStringi>().Invoke(param, index)));
	}

	[DllImport(Library)]
	public static extern void glPixelStorei(int pname, int param);

	[DllImport(Library)]
	public static extern void glPolygonOffset(float factor, float units);

	[DllImport(Library)]
	public static extern void glTexImage2D(int target, int level, int internalformat, int width, int height, int border,
		int format, int type, long pixels);

	public static void glTexImage2D(int target, int level, int internalformat, int width, int height, int border,
		int format, int type, ByteBuffer pixels)
	{
		var xHandle = GCHandle.Alloc(pixels.array(), GCHandleType.Pinned);

		glTexImage2D(target, level, internalformat, width, height, border, format, type, xHandle.AddrOfPinnedObject());

		xHandle.Free();
	}

	public static void glTexImage2D(int target, int level, int internalformat, int width, int height, int border,
		int format, int type, ShortBuffer pixels)
	{
		var xHandle = GCHandle.Alloc(pixels.array(), GCHandleType.Pinned);
		glTexImage2D(target, level, internalformat, width, height, border, format, type, xHandle.AddrOfPinnedObject());
	}

	public static void glTexImage2D(int target, int level, int internalformat, int width, int height, int border,
		int format, int type, FloatBuffer pixels)
	{
		var xHandle = GCHandle.Alloc(pixels.array(), GCHandleType.Pinned);
		glTexImage2D(target, level, internalformat, width, height, border, format, type, xHandle.AddrOfPinnedObject());
	}

	public static void glTexImage2D(int target, int level, int internalformat, int width, int height, int border,
		int format, int type, IntBuffer pixels)
	{
		var xHandle = GCHandle.Alloc(pixels.array(), GCHandleType.Pinned);
		glTexImage2D(target, level, internalformat, width, height, border, format, type, xHandle.AddrOfPinnedObject());
	}

	public static void glTexImage2D(int target, int level, int internalformat, int width, int height, int border,
		int format, int type, DoubleBuffer pixels)
	{
		var xHandle = GCHandle.Alloc(pixels.array(), GCHandleType.Pinned);
		glTexImage2D(target, level, internalformat, width, height, border, format, type, xHandle.AddrOfPinnedObject());
	}

	[DllImport(Library)]
	public static extern void glTexParameteri(int target, int pname, int param);

	[DllImport(Library)]
	public static extern void glVertexAttribPointer(int indx, int size, int type, bool normalized, int stride,
		int ptr);

	[DllImport(Library)]
	public static extern void glViewport(int x, int y, int width, int height);

	[DllImport(Library)]
	private static extern void glGenTextures(int n, long textures);

	public struct GLCapabilities
	{
	}

	private class WindowsFunctionProvider : IFunctionProvider
	{
		private readonly IntPtr _library = LoadLibrary("opengl32.dll");
		private readonly Dictionary<string, Delegate> _functions = new();

		public T Get<T>()
			where T : class
		{
			return _functions.TryGetValue(typeof(T).Name, out var function)
				? ConvertDelegate<T>(function)
				: LoadFromNativeLibrary<T>(typeof(T).Name);
		}

		private static T ConvertDelegate<T>(Delegate callback)
			where T : class
		{
			return (T)Convert.ChangeType(callback, typeof(T));
		}

		private T LoadFromNativeLibrary<T>(string name)
			where T : class
		{
			_functions.Add(name, null);
			IntPtr proc = GetFunctionPointer(name);
			if (proc == IntPtr.Zero)
				return null;
			Delegate deleg = Marshal.GetDelegateForFunctionPointer(proc, typeof(T));
			_functions[name] = deleg;
			return ConvertDelegate<T>(deleg);
		}
		
		private IntPtr GetFunctionPointer(string name)
		{
			IntPtr pointer = wglGetProcAddress(name);

			if (pointer == IntPtr.Zero)
			{
				pointer = GetProcAddress(_library, name);

				if (pointer == IntPtr.Zero)
				{
					throw new Exception("Failed to get func " + name + " library=" + _library);
				}
			}
			return pointer;
		}

		[DllImport("kernel32", CharSet = CharSet.Ansi)]
		private static extern IntPtr GetProcAddress(IntPtr library, string functionName);

		[DllImport("kernel32", CharSet = CharSet.Ansi)]
		private static extern IntPtr LoadLibrary(string lpFileName);

		[DllImport("opengl32", CharSet = CharSet.Ansi)]
		private static extern IntPtr wglGetProcAddress(string functionName);
	}
}