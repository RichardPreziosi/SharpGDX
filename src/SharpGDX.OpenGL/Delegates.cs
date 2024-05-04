using System.Runtime.InteropServices;

namespace SharpGDX.OpenGL;

internal static class Delegates
{
	internal delegate void BindTexture(int target, uint texture);

	internal delegate void GetShaderiv(UInt32 shader, int pname, IntPtr @params);

	internal delegate void BlendFunc(int sfactor, int dfactor);

	internal delegate void GetShaderInfoLog(UInt32 shader, Int32 bufSize, IntPtr length, IntPtr infoLog);

	internal delegate void Clear(int mask);

	internal delegate void ClearColor(float red, float green, float blue, float alpha);

	internal delegate void ClearDepth(double depth);

	internal delegate void ClearStencil(int s);

	internal delegate void ColorMask(bool red, bool green, bool blue, bool alpha);

	internal delegate void CompileShader(uint shader);

	internal delegate int CreateShader(int type);

	internal delegate void DeleteBuffers(int n, IntPtr buffers);

	internal delegate void DeleteProgram(uint program);

	internal delegate void DrawArrays(int mode, int first, int count);

	internal delegate void DrawElements(int mode, int count, int type, IntPtr indices);

	internal delegate void Enable(int cap);

	internal delegate void GenTextures(int n, IntPtr textures);

	internal delegate int GetError();

	internal delegate IntPtr GetString(int name);

	internal delegate void PixelStorei(int pname, int param);

	internal delegate void PolygonOffset(float factor, float units);

	internal delegate void ShaderSource(uint shader, int count, string[] @string, IntPtr length);

	internal delegate void TexImage2D(int target, int level, int internalformat, int width, int height, int border,
		int format, int type, IntPtr pixels);

	internal delegate void TexParameteri(int target, int pname, int param);

	internal delegate void VertexAttribPointer(uint index, int size, int type, bool normalized, int stride,
		IntPtr pointer);

	internal delegate void Viewport(int x, int y, int width, int height);
}