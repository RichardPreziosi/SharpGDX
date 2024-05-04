using static SharpGDX.OpenGL.Delegates;

namespace SharpGDX.OpenGL;

internal static class Functions
{
	internal static BindTexture glBindTexture;
	internal static BlendFunc glBlendFunc;
	internal static Clear glClear;
	internal static ClearColor glClearColor;
	internal static ClearDepth glClearDepth;
	internal static ClearStencil glClearStencil;
	internal static ColorMask glColorMask;
	internal static CompileShader glCompileShader;
	internal static CreateShader glCreateShader;
	internal static DeleteBuffers glDeleteBuffers;
	internal static DeleteProgram glDeleteProgram;
	internal static DrawArrays glDrawArrays;
	internal static DrawElements glDrawElements;
	internal static Enable glEnable;
	internal static GenTextures glGenTextures;
	internal static GetError glGetError;
	internal static GetString glGetString;
	internal static PixelStorei glPixelStorei;
	internal static PolygonOffset glPolygonOffset;
	internal static ShaderSource glShaderSource;
	internal static TexImage2D glTexImage2D;
	internal static TexParameteri glTexParameteri;
	internal static VertexAttribPointer glVertexAttribPointer;
	internal static Viewport glViewport;
	internal static GetShaderiv glGetShaderiv;
	internal static GetShaderInfoLog glGetShaderInfoLog;
}