using System;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGDX.graphics;
using SharpGDX.shims;
using Buffer = SharpGDX.shims.Buffer;

namespace SharpGDX.Desktop
{
	public class DesktopGL20 : GL20
	{
		public void glActiveTexture(int texture)
		{
			throw new NotImplementedException();
		}

		public void glBindTexture(int target, int texture)
		{
			throw new NotImplementedException();
		}

		public void glBlendFunc(int sfactor, int dfactor)
		{
			throw new NotImplementedException();
		}

		public void glClear(int mask)
		{
			throw new NotImplementedException();
		}

		public void glClearColor(float red, float green, float blue, float alpha)
		{
			throw new NotImplementedException();
		}

		public void glClearDepthf(float depth)
		{
			throw new NotImplementedException();
		}

		public void glClearStencil(int s)
		{
			throw new NotImplementedException();
		}

		public void glColorMask(bool red, bool green, bool blue, bool alpha)
		{
			throw new NotImplementedException();
		}

		public void glCompressedTexImage2D(int target, int level, int internalformat, int width, int height, int border, int imageSize,
			Buffer data)
		{
			throw new NotImplementedException();
		}

		public void glCompressedTexSubImage2D(int target, int level, int xoffset, int yoffset, int width, int height, int format,
			int imageSize, Buffer data)
		{
			throw new NotImplementedException();
		}

		public void glCopyTexImage2D(int target, int level, int internalformat, int x, int y, int width, int height, int border)
		{
			throw new NotImplementedException();
		}

		public void glCopyTexSubImage2D(int target, int level, int xoffset, int yoffset, int x, int y, int width, int height)
		{
			throw new NotImplementedException();
		}

		public void glCullFace(int mode)
		{
			throw new NotImplementedException();
		}

		public void glDeleteTextures(int n, IntBuffer textures)
		{
			throw new NotImplementedException();
		}

		public void glDeleteTexture(int texture)
		{
			throw new NotImplementedException();
		}

		public void glDepthFunc(int func)
		{
			throw new NotImplementedException();
		}

		public void glDepthMask(bool flag)
		{
			throw new NotImplementedException();
		}

		public void glDepthRangef(float zNear, float zFar)
		{
			throw new NotImplementedException();
		}

		public void glDisable(int cap)
		{
			throw new NotImplementedException();
		}

		public void glDrawArrays(int mode, int first, int count)
		{
			throw new NotImplementedException();
		}

		public void glDrawElements(int mode, int count, int type, Buffer indices)
		{
			throw new NotImplementedException();
		}

		public void glEnable(int cap)
		{
			GL.Enable((EnableCap)cap);
		}

		public void glFinish()
		{
			throw new NotImplementedException();
		}

		public void glFlush()
		{
			throw new NotImplementedException();
		}

		public void glFrontFace(int mode)
		{
			throw new NotImplementedException();
		}

		public void glGenTextures(int n, IntBuffer textures)
		{
			throw new NotImplementedException();
		}

		public int glGenTexture()
		{
			throw new NotImplementedException();
		}

		public int glGetError()
		{
			throw new NotImplementedException();
		}

		public void glGetIntegerv(int pname, IntBuffer @params)
		{
			throw new NotImplementedException();
		}

		public string glGetString(int name)
		{
			return GL.GetString((StringName)name);
		}

		public void glHint(int target, int mode)
		{
			throw new NotImplementedException();
		}

		public void glLineWidth(float width)
		{
			throw new NotImplementedException();
		}

		public void glPixelStorei(int pname, int param)
		{
			throw new NotImplementedException();
		}

		public void glPolygonOffset(float factor, float units)
		{
			throw new NotImplementedException();
		}

		public void glReadPixels(int x, int y, int width, int height, int format, int type, Buffer pixels)
		{
			throw new NotImplementedException();
		}

		public void glScissor(int x, int y, int width, int height)
		{
			throw new NotImplementedException();
		}

		public void glStencilFunc(int func, int @ref, int mask)
		{
			throw new NotImplementedException();
		}

		public void glStencilMask(int mask)
		{
			throw new NotImplementedException();
		}

		public void glStencilOp(int fail, int zfail, int zpass)
		{
			throw new NotImplementedException();
		}

		public void glTexImage2D(int target, int level, int internalformat, int width, int height, int border, int format, int type,
			Buffer pixels)
		{
			throw new NotImplementedException();
		}

		public void glTexParameterf(int target, int pname, float param)
		{
			throw new NotImplementedException();
		}

		public void glTexSubImage2D(int target, int level, int xoffset, int yoffset, int width, int height, int format, int type,
			Buffer pixels)
		{
			throw new NotImplementedException();
		}

		public void glViewport(int x, int y, int width, int height)
		{
			throw new NotImplementedException();
		}

		public void glAttachShader(int program, int shader)
		{
			throw new NotImplementedException();
		}

		public void glBindAttribLocation(int program, int index, string name)
		{
			throw new NotImplementedException();
		}

		public void glBindBuffer(int target, int buffer)
		{
			throw new NotImplementedException();
		}

		public void glBindFramebuffer(int target, int framebuffer)
		{
			throw new NotImplementedException();
		}

		public void glBindRenderbuffer(int target, int renderbuffer)
		{
			throw new NotImplementedException();
		}

		public void glBlendColor(float red, float green, float blue, float alpha)
		{
			throw new NotImplementedException();
		}

		public void glBlendEquation(int mode)
		{
			throw new NotImplementedException();
		}

		public void glBlendEquationSeparate(int modeRGB, int modeAlpha)
		{
			throw new NotImplementedException();
		}

		public void glBlendFuncSeparate(int srcRGB, int dstRGB, int srcAlpha, int dstAlpha)
		{
			throw new NotImplementedException();
		}

		public void glBufferData(int target, int size, Buffer data, int usage)
		{
			throw new NotImplementedException();
		}

		public void glBufferSubData(int target, int offset, int size, Buffer data)
		{
			throw new NotImplementedException();
		}

		public int glCheckFramebufferStatus(int target)
		{
			throw new NotImplementedException();
		}

		public void glCompileShader(int shader)
		{
			throw new NotImplementedException();
		}

		public int glCreateProgram()
		{
			throw new NotImplementedException();
		}

		public int glCreateShader(int type)
		{
			throw new NotImplementedException();
		}

		public void glDeleteBuffer(int buffer)
		{
			throw new NotImplementedException();
		}

		public void glDeleteBuffers(int n, IntBuffer buffers)
		{
			throw new NotImplementedException();
		}

		public void glDeleteFramebuffer(int framebuffer)
		{
			throw new NotImplementedException();
		}

		public void glDeleteFramebuffers(int n, IntBuffer framebuffers)
		{
			throw new NotImplementedException();
		}

		public void glDeleteProgram(int program)
		{
			throw new NotImplementedException();
		}

		public void glDeleteRenderbuffer(int renderbuffer)
		{
			throw new NotImplementedException();
		}

		public void glDeleteRenderbuffers(int n, IntBuffer renderbuffers)
		{
			throw new NotImplementedException();
		}

		public void glDeleteShader(int shader)
		{
			throw new NotImplementedException();
		}

		public void glDetachShader(int program, int shader)
		{
			throw new NotImplementedException();
		}

		public void glDisableVertexAttribArray(int index)
		{
			throw new NotImplementedException();
		}

		public void glDrawElements(int mode, int count, int type, int indices)
		{
			throw new NotImplementedException();
		}

		public void glEnableVertexAttribArray(int index)
		{
			throw new NotImplementedException();
		}

		public void glFramebufferRenderbuffer(int target, int attachment, int renderbuffertarget, int renderbuffer)
		{
			throw new NotImplementedException();
		}

		public void glFramebufferTexture2D(int target, int attachment, int textarget, int texture, int level)
		{
			throw new NotImplementedException();
		}

		public int glGenBuffer()
		{
			throw new NotImplementedException();
		}

		public void glGenBuffers(int n, IntBuffer buffers)
		{
			throw new NotImplementedException();
		}

		public void glGenerateMipmap(int target)
		{
			throw new NotImplementedException();
		}

		public int glGenFramebuffer()
		{
			throw new NotImplementedException();
		}

		public void glGenFramebuffers(int n, IntBuffer framebuffers)
		{
			throw new NotImplementedException();
		}

		public int glGenRenderbuffer()
		{
			throw new NotImplementedException();
		}

		public void glGenRenderbuffers(int n, IntBuffer renderbuffers)
		{
			throw new NotImplementedException();
		}

		public string glGetActiveAttrib(int program, int index, IntBuffer size, IntBuffer type)
		{
			throw new NotImplementedException();
		}

		public string glGetActiveUniform(int program, int index, IntBuffer size, IntBuffer type)
		{
			throw new NotImplementedException();
		}

		public void glGetAttachedShaders(int program, int maxcount, Buffer count, IntBuffer shaders)
		{
			throw new NotImplementedException();
		}

		public int glGetAttribLocation(int program, string name)
		{
			throw new NotImplementedException();
		}

		public void glGetBooleanv(int pname, Buffer @params)
		{
			throw new NotImplementedException();
		}

		public void glGetBufferParameteriv(int target, int pname, IntBuffer @params)
		{
			throw new NotImplementedException();
		}

		public void glGetFloatv(int pname, FloatBuffer @params)
		{
			throw new NotImplementedException();
		}

		public void glGetFramebufferAttachmentParameteriv(int target, int attachment, int pname, IntBuffer @params)
		{
			throw new NotImplementedException();
		}

		public void glGetProgramiv(int program, int pname, IntBuffer @params)
		{
			throw new NotImplementedException();
		}

		public string glGetProgramInfoLog(int program)
		{
			throw new NotImplementedException();
		}

		public void glGetRenderbufferParameteriv(int target, int pname, IntBuffer @params)
		{
			throw new NotImplementedException();
		}

		public void glGetShaderiv(int shader, int pname, IntBuffer @params)
		{
			throw new NotImplementedException();
		}

		public string glGetShaderInfoLog(int shader)
		{
			throw new NotImplementedException();
		}

		public void glGetShaderPrecisionFormat(int shadertype, int precisiontype, IntBuffer range, IntBuffer precision)
		{
			throw new NotImplementedException();
		}

		public void glGetTexParameterfv(int target, int pname, FloatBuffer @params)
		{
			throw new NotImplementedException();
		}

		public void glGetTexParameteriv(int target, int pname, IntBuffer @params)
		{
			throw new NotImplementedException();
		}

		public void glGetUniformfv(int program, int location, FloatBuffer @params)
		{
			throw new NotImplementedException();
		}

		public void glGetUniformiv(int program, int location, IntBuffer @params)
		{
			throw new NotImplementedException();
		}

		public int glGetUniformLocation(int program, string name)
		{
			throw new NotImplementedException();
		}

		public void glGetVertexAttribfv(int index, int pname, FloatBuffer @params)
		{
			throw new NotImplementedException();
		}

		public void glGetVertexAttribiv(int index, int pname, IntBuffer @params)
		{
			throw new NotImplementedException();
		}

		public void glGetVertexAttribPointerv(int index, int pname, Buffer pointer)
		{
			throw new NotImplementedException();
		}

		public bool glIsBuffer(int buffer)
		{
			throw new NotImplementedException();
		}

		public bool glIsEnabled(int cap)
		{
			throw new NotImplementedException();
		}

		public bool glIsFramebuffer(int framebuffer)
		{
			throw new NotImplementedException();
		}

		public bool glIsProgram(int program)
		{
			throw new NotImplementedException();
		}

		public bool glIsRenderbuffer(int renderbuffer)
		{
			throw new NotImplementedException();
		}

		public bool glIsShader(int shader)
		{
			throw new NotImplementedException();
		}

		public bool glIsTexture(int texture)
		{
			throw new NotImplementedException();
		}

		public void glLinkProgram(int program)
		{
			throw new NotImplementedException();
		}

		public void glReleaseShaderCompiler()
		{
			throw new NotImplementedException();
		}

		public void glRenderbufferStorage(int target, int internalformat, int width, int height)
		{
			throw new NotImplementedException();
		}

		public void glSampleCoverage(float value, bool invert)
		{
			throw new NotImplementedException();
		}

		public void glShaderBinary(int n, IntBuffer shaders, int binaryformat, Buffer binary, int length)
		{
			throw new NotImplementedException();
		}

		public void glShaderSource(int shader, string @string)
		{
			throw new NotImplementedException();
		}

		public void glStencilFuncSeparate(int face, int func, int @ref, int mask)
		{
			throw new NotImplementedException();
		}

		public void glStencilMaskSeparate(int face, int mask)
		{
			throw new NotImplementedException();
		}

		public void glStencilOpSeparate(int face, int fail, int zfail, int zpass)
		{
			throw new NotImplementedException();
		}

		public void glTexParameterfv(int target, int pname, FloatBuffer @params)
		{
			throw new NotImplementedException();
		}

		public void glTexParameteri(int target, int pname, int param)
		{
			throw new NotImplementedException();
		}

		public void glTexParameteriv(int target, int pname, IntBuffer @params)
		{
			throw new NotImplementedException();
		}

		public void glUniform1f(int location, float x)
		{
			throw new NotImplementedException();
		}

		public void glUniform1fv(int location, int count, FloatBuffer v)
		{
			throw new NotImplementedException();
		}

		public void glUniform1fv(int location, int count, float[] v, int offset)
		{
			throw new NotImplementedException();
		}

		public void glUniform1i(int location, int x)
		{
			throw new NotImplementedException();
		}

		public void glUniform1iv(int location, int count, IntBuffer v)
		{
			throw new NotImplementedException();
		}

		public void glUniform1iv(int location, int count, int[] v, int offset)
		{
			throw new NotImplementedException();
		}

		public void glUniform2f(int location, float x, float y)
		{
			throw new NotImplementedException();
		}

		public void glUniform2fv(int location, int count, FloatBuffer v)
		{
			throw new NotImplementedException();
		}

		public void glUniform2fv(int location, int count, float[] v, int offset)
		{
			throw new NotImplementedException();
		}

		public void glUniform2i(int location, int x, int y)
		{
			throw new NotImplementedException();
		}

		public void glUniform2iv(int location, int count, IntBuffer v)
		{
			throw new NotImplementedException();
		}

		public void glUniform2iv(int location, int count, int[] v, int offset)
		{
			throw new NotImplementedException();
		}

		public void glUniform3f(int location, float x, float y, float z)
		{
			throw new NotImplementedException();
		}

		public void glUniform3fv(int location, int count, FloatBuffer v)
		{
			throw new NotImplementedException();
		}

		public void glUniform3fv(int location, int count, float[] v, int offset)
		{
			throw new NotImplementedException();
		}

		public void glUniform3i(int location, int x, int y, int z)
		{
			throw new NotImplementedException();
		}

		public void glUniform3iv(int location, int count, IntBuffer v)
		{
			throw new NotImplementedException();
		}

		public void glUniform3iv(int location, int count, int[] v, int offset)
		{
			throw new NotImplementedException();
		}

		public void glUniform4f(int location, float x, float y, float z, float w)
		{
			throw new NotImplementedException();
		}

		public void glUniform4fv(int location, int count, FloatBuffer v)
		{
			throw new NotImplementedException();
		}

		public void glUniform4fv(int location, int count, float[] v, int offset)
		{
			throw new NotImplementedException();
		}

		public void glUniform4i(int location, int x, int y, int z, int w)
		{
			throw new NotImplementedException();
		}

		public void glUniform4iv(int location, int count, IntBuffer v)
		{
			throw new NotImplementedException();
		}

		public void glUniform4iv(int location, int count, int[] v, int offset)
		{
			throw new NotImplementedException();
		}

		public void glUniformMatrix2fv(int location, int count, bool transpose, FloatBuffer value)
		{
			throw new NotImplementedException();
		}

		public void glUniformMatrix2fv(int location, int count, bool transpose, float[] value, int offset)
		{
			throw new NotImplementedException();
		}

		public void glUniformMatrix3fv(int location, int count, bool transpose, FloatBuffer value)
		{
			throw new NotImplementedException();
		}

		public void glUniformMatrix3fv(int location, int count, bool transpose, float[] value, int offset)
		{
			throw new NotImplementedException();
		}

		public void glUniformMatrix4fv(int location, int count, bool transpose, FloatBuffer value)
		{
			throw new NotImplementedException();
		}

		public void glUniformMatrix4fv(int location, int count, bool transpose, float[] value, int offset)
		{
			throw new NotImplementedException();
		}

		public void glUseProgram(int program)
		{
			throw new NotImplementedException();
		}

		public void glValidateProgram(int program)
		{
			throw new NotImplementedException();
		}

		public void glVertexAttrib1f(int indx, float x)
		{
			throw new NotImplementedException();
		}

		public void glVertexAttrib1fv(int indx, FloatBuffer values)
		{
			throw new NotImplementedException();
		}

		public void glVertexAttrib2f(int indx, float x, float y)
		{
			throw new NotImplementedException();
		}

		public void glVertexAttrib2fv(int indx, FloatBuffer values)
		{
			throw new NotImplementedException();
		}

		public void glVertexAttrib3f(int indx, float x, float y, float z)
		{
			throw new NotImplementedException();
		}

		public void glVertexAttrib3fv(int indx, FloatBuffer values)
		{
			throw new NotImplementedException();
		}

		public void glVertexAttrib4f(int indx, float x, float y, float z, float w)
		{
			throw new NotImplementedException();
		}

		public void glVertexAttrib4fv(int indx, FloatBuffer values)
		{
			throw new NotImplementedException();
		}

		public void glVertexAttribPointer(int indx, int size, int type, bool normalized, int stride, Buffer ptr)
		{
			throw new NotImplementedException();
		}

		public void glVertexAttribPointer(int indx, int size, int type, bool normalized, int stride, int ptr)
		{
			throw new NotImplementedException();
		}
	}
}
