using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGDX.Shims;
using SharpGDX.Utils;
using Buffer = SharpGDX.Shims.Buffer;

namespace SharpGDX.Desktop
{
	class Lwjgl3GL20 : GL20
	{
	private ByteBuffer buffer = null;
	private FloatBuffer floatBuffer = null;
	private IntBuffer intBuffer = null;

	private void ensureBufferCapacity(int numBytes)
	{
		if (buffer == null || buffer.capacity() < numBytes)
		{
			buffer = BufferUtils.newByteBuffer(numBytes);
			floatBuffer = buffer.asFloatBuffer();
			intBuffer = buffer.asIntBuffer();
		}
	}

	private FloatBuffer toFloatBuffer(float[] v, int offset, int count)
	{
		ensureBufferCapacity(count << 2);
		((Buffer)floatBuffer).clear();
		((Buffer)floatBuffer).limit(count);
		floatBuffer.put(v, offset, count);
		((Buffer)floatBuffer).position(0);
		return floatBuffer;
	}

	private IntBuffer toIntBuffer(int[] v, int offset, int count)
	{
		ensureBufferCapacity(count << 2);
		((Buffer)intBuffer).clear();
		((Buffer)intBuffer).limit(count);
		intBuffer.put(v, offset, count);
		((Buffer)intBuffer).position(0);
		return intBuffer;
	}

	public void glActiveTexture(int texture)
	{
		throw new NotImplementedException();
		//GL.glActiveTexture(texture);
		}

		public void glAttachShader(int program, int shader)
	{
		throw new NotImplementedException();
		//GL.glAttachShader(program, shader);
		}

		public void glBindAttribLocation(int program, int index, String name)
	{
		throw new NotImplementedException();
		//GL.glBindAttribLocation(program, index, name);
		}

		public void glBindBuffer(int target, int buffer)
	{
		throw new NotImplementedException();
		//GL.glBindBuffer(target, buffer);
		}

		public void glBindFramebuffer(int target, int framebuffer)
	{
		throw new NotImplementedException();
		//	EXTFramebufferObject.glBindFramebufferEXT(target, framebuffer);
		}

		public void glBindRenderbuffer(int target, int renderbuffer)
	{
		throw new NotImplementedException();
		//EXTFramebufferObject.glBindRenderbufferEXT(target, renderbuffer);
		}

		public void glBindTexture(int target, int texture)
	{
			GL.glBindTexture(target, texture);
		}

		public void glBlendColor(float red, float green, float blue, float alpha)
	{
		throw new NotImplementedException();
		//GL.glBlendColor(red, green, blue, alpha);
		}

		public void glBlendEquation(int mode)
	{
		throw new NotImplementedException();
		//GL.glBlendEquation(mode);
		}

		public void glBlendEquationSeparate(int modeRGB, int modeAlpha)
	{
		throw new NotImplementedException();
		//GL.glBlendEquationSeparate(modeRGB, modeAlpha);
		}

		public void glBlendFunc(int sfactor, int dfactor)
	{
		GL.glBlendFunc(sfactor, dfactor);
	}

	public void glBlendFuncSeparate(int srcRGB, int dstRGB, int srcAlpha, int dstAlpha)
	{
		throw new NotImplementedException();
		//GL.glBlendFuncSeparate(srcRGB, dstRGB, srcAlpha, dstAlpha);
		}

		public void glBufferData(int target, int size, Buffer data, int usage)
	{
		throw new NotImplementedException();
		// //if (data == null)
		//	GL.glBufferData(target, size, usage);
		//else if (data is ByteBuffer)
		//	GL.glBufferData(target, (ByteBuffer)data, usage);
		//else if (data is IntBuffer)
		//	GL.glBufferData(target, (IntBuffer)data, usage);
		//else if (data is FloatBuffer)
		//	GL.glBufferData(target, (FloatBuffer)data, usage);
		//else if (data is DoubleBuffer)
		//	GL.glBufferData(target, (DoubleBuffer)data, usage);
		//else if (data is ShortBuffer) //
		//	GL.glBufferData(target, (ShortBuffer)data, usage);
	}

	public void glBufferSubData(int target, int offset, int size, Buffer data)
	{
		throw new NotImplementedException();
		// //if (data == null)
		//	throw new GdxRuntimeException("Using null for the data not possible, blame LWJGL");
		//else if (data is ByteBuffer)
		//	GL.glBufferSubData(target, offset, (ByteBuffer)data);
		//else if (data is IntBuffer)
		//	GL.glBufferSubData(target, offset, (IntBuffer)data);
		//else if (data is FloatBuffer)
		//	GL.glBufferSubData(target, offset, (FloatBuffer)data);
		//else if (data is DoubleBuffer)
		//	GL.glBufferSubData(target, offset, (DoubleBuffer)data);
		//else if (data is ShortBuffer) //
		//	GL.glBufferSubData(target, offset, (ShortBuffer)data);
	}

	public int glCheckFramebufferStatus(int target)
	{
		throw new NotImplementedException();
		//return EXTFramebufferObject.glCheckFramebufferStatusEXT(target);
		}

		public void glClear(int mask)
	{
		GL.glClear(mask);
	}

	public void glClearColor(float red, float green, float blue, float alpha)
	{
		GL.glClearColor(red, green, blue, alpha);
	}

	public void glClearDepthf(float depth)
	{
		GL.glClearDepth(depth);
	}
	public void glClearStencil(int s)
	{
		GL.glClearStencil(s);
	}
	public void glColorMask(bool red, bool green, bool blue, bool alpha)
	{
		GL.glColorMask(red, green, blue, alpha);
	}
	public void glCompileShader(int shader)
	{
		GL.glCompileShader(shader);
	}

	public void glCompressedTexImage2D(int target, int level, int internalformat, int width, int height, int border,
		int imageSize, Buffer data)
	{
		if (data is ByteBuffer) {
			throw new NotImplementedException();
			//	GL.glCompressedTexImage2D(target, level, internalformat, width, height, border, (ByteBuffer)data);
			}
			else
		{
			throw new GdxRuntimeException("Can't use " + data.GetType().Name + " with this method. Use ByteBuffer instead.");
		}
	}

	public void glCompressedTexSubImage2D(int target, int level, int xoffset, int yoffset, int width, int height, int format,
		int imageSize, Buffer data)
	{
		throw new GdxRuntimeException("not implemented");
	}

	public void glCopyTexImage2D(int target, int level, int internalformat, int x, int y, int width, int height, int border)
	{
		throw new NotImplementedException();
		//GL.glCopyTexImage2D(target, level, internalformat, x, y, width, height, border);
		}

		public void glCopyTexSubImage2D(int target, int level, int xoffset, int yoffset, int x, int y, int width, int height)
	{
		throw new NotImplementedException();
		//GL.glCopyTexSubImage2D(target, level, xoffset, yoffset, x, y, width, height);
		}

		public int glCreateProgram()
	{
		throw new NotImplementedException();
		//return GL.glCreateProgram();
		}

		public int glCreateShader(int type)
	{
		return GL.glCreateShader(type);
		}

		public void glCullFace(int mode)
	{
		throw new NotImplementedException();
		//GL.glCullFace(mode);
		}

		public void glDeleteBuffers(int n, IntBuffer buffers)
	{
		// TODO: GL.glDeleteBuffers(buffers);
		throw new NotImplementedException();
	}

	public void glDeleteBuffer(int buffer)
	{
		GL.glDeleteBuffers(buffer);
	}

	public void glDeleteFramebuffers(int n, IntBuffer framebuffers)
	{
		throw new NotImplementedException();
		//EXTFramebufferObject.glDeleteFramebuffersEXT(framebuffers);
		}

		public void glDeleteFramebuffer(int framebuffer)
	{
		throw new NotImplementedException();
		//EXTFramebufferObject.glDeleteFramebuffersEXT(framebuffer);
		}

		public void glDeleteProgram(int program)
	{
		GL.glDeleteProgram(program);
	}

	public void glDeleteRenderbuffers(int n, IntBuffer renderbuffers)
	{
		throw new NotImplementedException();
		//EXTFramebufferObject.glDeleteRenderbuffersEXT(renderbuffers);
		}

		public void glDeleteRenderbuffer(int renderbuffer)
	{
		throw new NotImplementedException();
		//EXTFramebufferObject.glDeleteRenderbuffersEXT(renderbuffer);
		}

		public void glDeleteShader(int shader)
	{
		throw new NotImplementedException();
		//GL.glDeleteShader(shader);
		}

		public void glDeleteTextures(int n, IntBuffer textures)
	{
		throw new NotImplementedException();
		//	GL.glDeleteTextures(textures);
		}

		public void glDeleteTexture(int texture)
	{
		throw new NotImplementedException();
		//GL.glDeleteTextures(texture);
		}

		public void glDepthFunc(int func)
	{
		throw new NotImplementedException();
		//GL.glDepthFunc(func);
		}

		public void glDepthMask(bool flag)
	{
		throw new NotImplementedException();
		//GL.glDepthMask(flag);
		}

		public void glDepthRangef(float zNear, float zFar)
	{
		throw new NotImplementedException();
		//	GL.glDepthRange(zNear, zFar);
		}

		public void glDetachShader(int program, int shader)
	{
		throw new NotImplementedException();
		//	GL.glDetachShader(program, shader);
		}

		public void glDisable(int cap)
	{
		throw new NotImplementedException();
		//GL.glDisable(cap);
		}

		public void glDisableVertexAttribArray(int index)
	{
		throw new NotImplementedException();
		//GL.glDisableVertexAttribArray(index);
		}

		public void glDrawArrays(int mode, int first, int count)
	{
		GL.glDrawArrays(mode, first, count);
	}

	public void glDrawElements(int mode, int count, int type, Buffer indices)
	{
		if (indices is ShortBuffer && type == GL20.GL_UNSIGNED_SHORT) {
			ShortBuffer sb = (ShortBuffer)indices;
			int position = sb.position();
			int oldLimit = sb.limit();
			sb.limit(position + count);
			GL.glDrawElements(mode, sb);
			sb.limit(oldLimit);
		} else if (indices is ByteBuffer && type == GL20.GL_UNSIGNED_SHORT) {
			ShortBuffer sb = ((ByteBuffer)indices).asShortBuffer();
			int position = sb.position();
			int oldLimit = sb.limit();
			sb.limit(position + count);
			GL.glDrawElements(mode, sb);
			sb.limit(oldLimit);
		} else if (indices is ByteBuffer && type == GL20.GL_UNSIGNED_BYTE) {
			ByteBuffer bb = (ByteBuffer)indices;
			int position = bb.position();
			int oldLimit = bb.limit();
			bb.limit(position + count);
			GL.glDrawElements(mode, bb);
			bb.limit(oldLimit);
		} else
			throw new GdxRuntimeException("Can't use " + indices.GetType().Name
				+ " with this method. Use ShortBuffer or ByteBuffer instead. Blame LWJGL");
	}

	public void glEnable(int cap)
	{
		GL.glEnable(cap);
	}

	public void glEnableVertexAttribArray(int index)
	{
		throw new NotImplementedException();
		//	GL.glEnableVertexAttribArray(index);
		}

		public void glFinish()
	{
		throw new NotImplementedException();
		//GL.glFinish();
		}

		public void glFlush()
	{
		throw new NotImplementedException();
		//	GL.glFlush();
		}

		public void glFramebufferRenderbuffer(int target, int attachment, int renderbuffertarget, int renderbuffer)
	{
		throw new NotImplementedException();
		//	EXTFramebufferObject.glFramebufferRenderbufferEXT(target, attachment, renderbuffertarget, renderbuffer);
		}

		public void glFramebufferTexture2D(int target, int attachment, int textarget, int texture, int level)
	{
		throw new NotImplementedException();
		//	EXTFramebufferObject.glFramebufferTexture2DEXT(target, attachment, textarget, texture, level);
		}

		public void glFrontFace(int mode)
	{
		throw new NotImplementedException();
		//	GL.glFrontFace(mode);
		}

		public void glGenBuffers(int n, IntBuffer buffers)
	{
		throw new NotImplementedException();
		//GL.glGenBuffers(buffers);
		}

		public int glGenBuffer()
	{
		throw new NotImplementedException();
		//return GL.glGenBuffers();
		}

		public void glGenFramebuffers(int n, IntBuffer framebuffers)
	{
		throw new NotImplementedException();
		//EXTFramebufferObject.glGenFramebuffersEXT(framebuffers);
		}

		public int glGenFramebuffer()
	{
		throw new NotImplementedException();
		//return EXTFramebufferObject.glGenFramebuffersEXT();
		}

		public void glGenRenderbuffers(int n, IntBuffer renderbuffers)
	{
		throw new NotImplementedException();
		//EXTFramebufferObject.glGenRenderbuffersEXT(renderbuffers);
		}

		public int glGenRenderbuffer()
	{
		throw new NotImplementedException();
		//return EXTFramebufferObject.glGenRenderbuffersEXT();
		}

		public void glGenTextures(int n, IntBuffer textures)
	{
			GL.glGenTextures(textures);
		}

		public int glGenTexture()
	{
		return GL.glGenTextures();
		}

		public void glGenerateMipmap(int target)
	{
		throw new NotImplementedException();
		//EXTFramebufferObject.glGenerateMipmapEXT(target);
		}

		public String glGetActiveAttrib(int program, int index, IntBuffer size, IntBuffer type)
	{
		throw new NotImplementedException();
		//return GL.glGetActiveAttrib(program, index, 256, size, type);
		}

		public String glGetActiveUniform(int program, int index, IntBuffer size, IntBuffer type)
	{
		throw new NotImplementedException();
		//return GL.glGetActiveUniform(program, index, 256, size, type);
		}

		public void glGetAttachedShaders(int program, int maxcount, Buffer count, IntBuffer shaders)
	{
		throw new NotImplementedException();
		//GL.glGetAttachedShaders(program, (IntBuffer)count, shaders);
		}

		public int glGetAttribLocation(int program, String name)
	{
		throw new NotImplementedException();
		//return GL.glGetAttribLocation(program, name);
		}

		public void glGetBooleanv(int pname, Buffer @params)
	{
		throw new NotImplementedException();
		//GL.glGetBooleanv(pname, (ByteBuffer)@params);
		}

		public void glGetBufferParameteriv(int target, int pname, IntBuffer @params)
	{
		throw new NotImplementedException();
		//GL.glGetBufferParameteriv(target, pname, @params);
		}

		public int glGetError()
	{
		return GL.glGetError();
	}

	public void glGetFloatv(int pname, FloatBuffer @params)
	{
		throw new NotImplementedException();
		//GL.glGetFloatv(pname, @params);
		}

		public void glGetFramebufferAttachmentParameteriv(int target, int attachment, int pname, IntBuffer @params)
	{
		throw new NotImplementedException();
		//EXTFramebufferObject.glGetFramebufferAttachmentParameterivEXT(target, attachment, pname, @params);
		}

		public void glGetIntegerv(int pname, IntBuffer @params)
	{
		throw new NotImplementedException();
		//GL.glGetIntegerv(pname, @params);
		}

		public String glGetProgramInfoLog(int program)
	{
		throw new NotImplementedException();
		// //ByteBuffer buffer = ByteBuffer.allocateDirect(1024 * 10);
		//buffer.order(ByteOrder.nativeOrder());
		//ByteBuffer tmp = ByteBuffer.allocateDirect(4);
		//tmp.order(ByteOrder.nativeOrder());
		//IntBuffer intBuffer = tmp.asIntBuffer();

		//GL.glGetProgramInfoLog(program, intBuffer, buffer);
		//int numBytes = intBuffer.get(0);
		//byte[] bytes = new byte[numBytes];
		//buffer.get(bytes);
		//return new String(bytes);
	}

	public void glGetProgramiv(int program, int pname, IntBuffer @params)
	{
		throw new NotImplementedException();
		//GL.glGetProgramiv(program, pname, @params);
		}

		public void glGetRenderbufferParameteriv(int target, int pname, IntBuffer @params)
	{
		throw new NotImplementedException();
		//EXTFramebufferObject.glGetRenderbufferParameterivEXT(target, pname, @params);
		}

		public String glGetShaderInfoLog(int shader)
	{
		throw new NotImplementedException();
		// //ByteBuffer buffer = ByteBuffer.allocateDirect(1024 * 10);
		//buffer.order(ByteOrder.nativeOrder());
		//ByteBuffer tmp = ByteBuffer.allocateDirect(4);
		//tmp.order(ByteOrder.nativeOrder());
		//IntBuffer intBuffer = tmp.asIntBuffer();

		//GL.glGetShaderInfoLog(shader, intBuffer, buffer);
		//int numBytes = intBuffer.get(0);
		//byte[] bytes = new byte[numBytes];
		//buffer.get(bytes);
		//return new String(bytes);
	}

	public void glGetShaderPrecisionFormat(int shadertype, int precisiontype, IntBuffer range, IntBuffer precision)
	{
		throw new UnsupportedOperationException("unsupported, won't implement");
	}

	public void glGetShaderiv(int shader, int pname, IntBuffer @params)
	{
		throw new NotImplementedException();
		//GL.glGetShaderiv(shader, pname, @params);
		}

		public String glGetString(int name)
	{
		return GL.glGetString(name);
	}

	public void glGetTexParameterfv(int target, int pname, FloatBuffer @params)
	{
		throw new NotImplementedException();
		//GL.glGetTexParameterfv(target, pname, @params);
	}

	public void glGetTexParameteriv(int target, int pname, IntBuffer @params)
	{
		throw new NotImplementedException();
		//GL.glGetTexParameteriv(target, pname, @params);
		}

		public int glGetUniformLocation(int program, String name)
	{
		throw new NotImplementedException();
		//return GL.glGetUniformLocation(program, name);
		}

		public void glGetUniformfv(int program, int location, FloatBuffer @params)
	{
		throw new NotImplementedException();
		//GL.glGetUniformfv(program, location, @params);
		}

		public void glGetUniformiv(int program, int location, IntBuffer @params)
	{
		throw new NotImplementedException();
		//GL.glGetUniformiv(program, location, @params);
		}

		public void glGetVertexAttribPointerv(int index, int pname, Buffer pointer)
	{
		throw new UnsupportedOperationException("unsupported, won't implement");
	}

	public void glGetVertexAttribfv(int index, int pname, FloatBuffer @params)
	{
		throw new NotImplementedException();
		//GL.glGetVertexAttribfv(index, pname, @params);
		}

		public void glGetVertexAttribiv(int index, int pname, IntBuffer @params)
	{
		throw new NotImplementedException();
		//GL.glGetVertexAttribiv(index, pname, @params);
		}

		public void glHint(int target, int mode)
	{
		throw new NotImplementedException();
		//	GL.glHint(target, mode);
		}

		public bool glIsBuffer(int buffer)
	{
		throw new NotImplementedException();
		//return GL.glIsBuffer(buffer);
		}

		public bool glIsEnabled(int cap)
	{
		throw new NotImplementedException();
		//return GL.glIsEnabled(cap);
		}

		public bool glIsFramebuffer(int framebuffer)
	{
		throw new NotImplementedException();
		//return EXTFramebufferObject.glIsFramebufferEXT(framebuffer);
		}

		public bool glIsProgram(int program)
	{
		throw new NotImplementedException();
		//return GL.glIsProgram(program);
		}

		public bool glIsRenderbuffer(int renderbuffer)
	{
		throw new NotImplementedException();
		//return EXTFramebufferObject.glIsRenderbufferEXT(renderbuffer);
		}

		public bool glIsShader(int shader)
	{
		throw new NotImplementedException();
		//return GL.glIsShader(shader);
		}

		public bool glIsTexture(int texture)
	{
		throw new NotImplementedException();
		//return GL.glIsTexture(texture);
		}

		public void glLineWidth(float width)
	{
		throw new NotImplementedException();
		//GL.glLineWidth(width);
		}

		public void glLinkProgram(int program)
	{
		throw new NotImplementedException();
		//GL.glLinkProgram(program);
		}

		public void glPixelStorei(int pname, int param)
	{
		GL.glPixelStorei(pname, param);
		}

		public void glPolygonOffset(float factor, float units)
	{
		GL.glPolygonOffset(factor, units);
	}

	public void glReadPixels(int x, int y, int width, int height, int format, int type, Buffer pixels)
	{
		throw new NotImplementedException();
		//if (pixels is ByteBuffer)
			//	GL.glReadPixels(x, y, width, height, format, type, (ByteBuffer)pixels);
			//else if (pixels is ShortBuffer)
			//	GL.glReadPixels(x, y, width, height, format, type, (ShortBuffer)pixels);
			//else if (pixels is IntBuffer)
			//	GL.glReadPixels(x, y, width, height, format, type, (IntBuffer)pixels);
			//else if (pixels is FloatBuffer)
			//	GL.glReadPixels(x, y, width, height, format, type, (FloatBuffer)pixels);
			//else
			//	throw new GdxRuntimeException("Can't use " + pixels.GetType().Name
			//		+ " with this method. Use ByteBuffer, ShortBuffer, IntBuffer or FloatBuffer instead. Blame LWJGL");
		}

		public void glReleaseShaderCompiler()
	{
		// nothing to do here
	}

	public void glRenderbufferStorage(int target, int internalformat, int width, int height)
	{
		throw new NotImplementedException();
		//EXTFramebufferObject.glRenderbufferStorageEXT(target, internalformat, width, height);
		}

		public void glSampleCoverage(float value, bool invert)
	{
		throw new NotImplementedException();
		//GL.glSampleCoverage(value, invert);
		}

		public void glScissor(int x, int y, int width, int height)
	{
		throw new NotImplementedException();
		//GL.glScissor(x, y, width, height);
		}

		public void glShaderBinary(int n, IntBuffer shaders, int binaryformat, Buffer binary, int length)
	{
		throw new UnsupportedOperationException("unsupported, won't implement");
	}

		public void glShaderSource(int shader, String @string)
		{
			GL.glShaderSource(shader, @string);
		}

		public void glStencilFunc(int func, int @ref, int mask)
	{
		throw new NotImplementedException();
		//	GL.glStencilFunc(func, @ref, mask);
		}

		public void glStencilFuncSeparate(int face, int func, int @ref, int mask)
	{
		throw new NotImplementedException();
		//GL.glStencilFuncSeparate(face, func, @ref, mask);
		}

		public void glStencilMask(int mask)
	{
		throw new NotImplementedException();
		//	GL.glStencilMask(mask);
		}

		public void glStencilMaskSeparate(int face, int mask)
	{
		throw new NotImplementedException();
		//GL20.glStencilMaskSeparate(face, mask);
		}

		public void glStencilOp(int fail, int zfail, int zpass)
	{
		throw new NotImplementedException();
		//GL.glStencilOp(fail, zfail, zpass);
		}

		public void glStencilOpSeparate(int face, int fail, int zfail, int zpass)
	{
		throw new NotImplementedException();
		//	GL.glStencilOpSeparate(face, fail, zfail, zpass);
		}

		public void glTexImage2D(int target, int level, int internalformat, int width, int height, int border, int format, int type,
		Buffer pixels)
	{
		if (pixels == null)
				GL.glTexImage2D(target, level, internalformat, width, height, border, format, type, (ByteBuffer)null);
			else if (pixels is ByteBuffer)
				GL.glTexImage2D(target, level, internalformat, width, height, border, format, type, (ByteBuffer)pixels);
			else if (pixels is ShortBuffer)
				GL.glTexImage2D(target, level, internalformat, width, height, border, format, type, (ShortBuffer)pixels);
			else if (pixels is IntBuffer)
				GL.glTexImage2D(target, level, internalformat, width, height, border, format, type, (IntBuffer)pixels);
			else if (pixels is FloatBuffer)
				GL.glTexImage2D(target, level, internalformat, width, height, border, format, type, (FloatBuffer)pixels);
			else if (pixels is DoubleBuffer)
				GL.glTexImage2D(target, level, internalformat, width, height, border, format, type, (DoubleBuffer)pixels);
			else
				throw new GdxRuntimeException("Can't use " + pixels.GetType().Name
					+ " with this method. Use ByteBuffer, ShortBuffer, IntBuffer, FloatBuffer or DoubleBuffer instead. Blame LWJGL");
		}

		public void glTexParameterf(int target, int pname, float param)
	{
		throw new NotImplementedException();
		//GL.glTexParameterf(target, pname, param);
		}

		public void glTexParameterfv(int target, int pname, FloatBuffer @params)
	{
		throw new NotImplementedException();
		//GL.glTexParameterfv(target, pname, @params);
		}

		public void glTexParameteri(int target, int pname, int param)
	{
		GL.glTexParameteri(target, pname, param);
		}

		public void glTexParameteriv(int target, int pname, IntBuffer @params)
	{
		throw new NotImplementedException();
		//GL.glTexParameteriv(target, pname, @params);
		}

		public void glTexSubImage2D(int target, int level, int xoffset, int yoffset, int width, int height, int format, int type,
		Buffer pixels)
	{
		throw new NotImplementedException();
		//if (pixels is ByteBuffer)
		//	GL.glTexSubImage2D(target, level, xoffset, yoffset, width, height, format, type, (ByteBuffer)pixels);
		//else if (pixels is ShortBuffer)
		//	GL.glTexSubImage2D(target, level, xoffset, yoffset, width, height, format, type, (ShortBuffer)pixels);
		//else if (pixels is IntBuffer)
		//	GL.glTexSubImage2D(target, level, xoffset, yoffset, width, height, format, type, (IntBuffer)pixels);
		//else if (pixels is FloatBuffer)
		//	GL.glTexSubImage2D(target, level, xoffset, yoffset, width, height, format, type, (FloatBuffer)pixels);
		//else if (pixels is DoubleBuffer)
		//	GL.glTexSubImage2D(target, level, xoffset, yoffset, width, height, format, type, (DoubleBuffer)pixels);
		//else
		//	throw new GdxRuntimeException("Can't use " + pixels.GetType().Name
		//		+ " with this method. Use ByteBuffer, ShortBuffer, IntBuffer, FloatBuffer or DoubleBuffer instead. Blame LWJGL");
	}

	public void glUniform1f(int location, float x)
	{
		throw new NotImplementedException();
		//	GL.glUniform1f(location, x);
		}

		public void glUniform1fv(int location, int count, FloatBuffer v)
	{
		throw new NotImplementedException();
		//GL.glUniform1fv(location, v);
		}

		public void glUniform1fv(int location, int count, float[] v, int offset)
	{
		throw new NotImplementedException();
		//GL.glUniform1fv(location, toFloatBuffer(v, offset, count));
		}

		public void glUniform1i(int location, int x)
	{
		throw new NotImplementedException();
		//GL.glUniform1i(location, x);
		}

		public void glUniform1iv(int location, int count, IntBuffer v)
	{
		throw new NotImplementedException();
		//GL.glUniform1iv(location, v);
		}

		public void glUniform1iv(int location, int count, int[] v, int offset)
	{
		throw new NotImplementedException();
		//GL.glUniform1iv(location, toIntBuffer(v, offset, count));
		}

		public void glUniform2f(int location, float x, float y)
	{
		throw new NotImplementedException();
		//GL.glUniform2f(location, x, y);
		}

		public void glUniform2fv(int location, int count, FloatBuffer v)
	{
		throw new NotImplementedException();
		//GL.glUniform2fv(location, v);
		}

		public void glUniform2fv(int location, int count, float[] v, int offset)
	{
		throw new NotImplementedException();
		//GL.glUniform2fv(location, toFloatBuffer(v, offset, count << 1));
		}

		public void glUniform2i(int location, int x, int y)
	{
		throw new NotImplementedException();
		//GL.glUniform2i(location, x, y);
		}

		public void glUniform2iv(int location, int count, IntBuffer v)
	{
		throw new NotImplementedException();
		//	GL.glUniform2iv(location, v);
		}

		public void glUniform2iv(int location, int count, int[] v, int offset)
	{
		throw new NotImplementedException();
		//GL.glUniform2iv(location, toIntBuffer(v, offset, count << 1));
		}

		public void glUniform3f(int location, float x, float y, float z)
	{
		throw new NotImplementedException();
		//GL.glUniform3f(location, x, y, z);
		}

		public void glUniform3fv(int location, int count, FloatBuffer v)
	{
		throw new NotImplementedException();
		//GL20.glUniform3fv(location, v);
		}

		public void glUniform3fv(int location, int count, float[] v, int offset)
	{
		throw new NotImplementedException();
		//GL20.glUniform3fv(location, toFloatBuffer(v, offset, count * 3));
		}

		public void glUniform3i(int location, int x, int y, int z)
	{
		throw new NotImplementedException();
		//	GL20.glUniform3i(location, x, y, z);
		}

		public void glUniform3iv(int location, int count, IntBuffer v)
	{
		throw new NotImplementedException();
		//GL20.glUniform3iv(location, v);
		}

		public void glUniform3iv(int location, int count, int[] v, int offset)
	{
		throw new NotImplementedException();
		//GL20.glUniform3iv(location, toIntBuffer(v, offset, count * 3));
		}

		public void glUniform4f(int location, float x, float y, float z, float w)
	{
		throw new NotImplementedException();
		//GL20.glUniform4f(location, x, y, z, w);
		}

		public void glUniform4fv(int location, int count, FloatBuffer v)
	{
		throw new NotImplementedException();
		//GL20.glUniform4fv(location, v);
		}

		public void glUniform4fv(int location, int count, float[] v, int offset)
	{
		throw new NotImplementedException();
		//GL.glUniform4fv(location, toFloatBuffer(v, offset, count << 2));
		}

		public void glUniform4i(int location, int x, int y, int z, int w)
	{
		throw new NotImplementedException();
		//GL.glUniform4i(location, x, y, z, w);
		}

		public void glUniform4iv(int location, int count, IntBuffer v)
	{
		throw new NotImplementedException();
		//GL.glUniform4iv(location, v);
		}

		public void glUniform4iv(int location, int count, int[] v, int offset)
	{
		throw new NotImplementedException();
		//GL.glUniform4iv(location, toIntBuffer(v, offset, count << 2));
		}

		public void glUniformMatrix2fv(int location, int count, bool transpose, FloatBuffer value)
	{
		throw new NotImplementedException();
		//GL.glUniformMatrix2fv(location, transpose, value);
		}

		public void glUniformMatrix2fv(int location, int count, bool transpose, float[] value, int offset)
	{
		throw new NotImplementedException();
		//GL.glUniformMatrix2fv(location, transpose, toFloatBuffer(value, offset, count << 2));
		}

		public void glUniformMatrix3fv(int location, int count, bool transpose, FloatBuffer value)
	{
		throw new NotImplementedException();
		//GL.glUniformMatrix3fv(location, transpose, value);
		}

		public void glUniformMatrix3fv(int location, int count, bool transpose, float[] value, int offset)
	{
		throw new NotImplementedException();
		//GL.glUniformMatrix3fv(location, transpose, toFloatBuffer(value, offset, count * 9));
		}

		public void glUniformMatrix4fv(int location, int count, bool transpose, FloatBuffer value)
	{
		throw new NotImplementedException();
		//GL.glUniformMatrix4fv(location, transpose, value);
		}

		public void glUniformMatrix4fv(int location, int count, bool transpose, float[] value, int offset)
	{
		throw new NotImplementedException();
		//	GL.glUniformMatrix4fv(location, transpose, toFloatBuffer(value, offset, count << 4));
		}

		public void glUseProgram(int program)
	{
		throw new NotImplementedException();
		//GL.glUseProgram(program);
		}

		public void glValidateProgram(int program)
	{
		throw new NotImplementedException();
		//GL.glValidateProgram(program);
		}

		public void glVertexAttrib1f(int indx, float x)
	{
		throw new NotImplementedException();
		//GL.glVertexAttrib1f(indx, x);
		}

		public void glVertexAttrib1fv(int indx, FloatBuffer values)
	{
		throw new NotImplementedException();
		//GL.glVertexAttrib1f(indx, values.get());
		}

		public void glVertexAttrib2f(int indx, float x, float y)
	{
		throw new NotImplementedException();
		//GL.glVertexAttrib2f(indx, x, y);
		}

		public void glVertexAttrib2fv(int indx, FloatBuffer values)
	{
		throw new NotImplementedException();
		//GL.glVertexAttrib2f(indx, values.get(), values.get());
		}

		public void glVertexAttrib3f(int indx, float x, float y, float z)
	{
		throw new NotImplementedException();
		//GL.glVertexAttrib3f(indx, x, y, z);
		}

		public void glVertexAttrib3fv(int indx, FloatBuffer values)
	{
		throw new NotImplementedException();
		//GL.glVertexAttrib3f(indx, values.get(), values.get(), values.get());
		}

		public void glVertexAttrib4f(int indx, float x, float y, float z, float w)
	{
		throw new NotImplementedException();
		//GL.glVertexAttrib4f(indx, x, y, z, w);
		}

		public void glVertexAttrib4fv(int indx, FloatBuffer values)
	{
		throw new NotImplementedException();
		//GL.glVertexAttrib4f(indx, values.get(), values.get(), values.get(), values.get());
		}

		public void glVertexAttribPointer(int indx, int size, int type, bool normalized, int stride, Buffer buffer)
	{
		throw new NotImplementedException();
		// //if (buffer is ByteBuffer) {
		//	if (type == GL20.GL_BYTE)
		//		GL.glVertexAttribPointer(indx, size, type, normalized, stride, (ByteBuffer)buffer);
		//	else if (type == GL20.GL_UNSIGNED_BYTE)
		//		GL.glVertexAttribPointer(indx, size, type, normalized, stride, (ByteBuffer)buffer);
		//	else if (type == GL20.GL_SHORT)
		//		GL.glVertexAttribPointer(indx, size, type, normalized, stride, ((ByteBuffer)buffer).asShortBuffer());
		//	else if (type == GL20.GL_UNSIGNED_SHORT)
		//		GL.glVertexAttribPointer(indx, size, type, normalized, stride, ((ByteBuffer)buffer).asShortBuffer());
		//	else if (type == GL20.GL_FLOAT)
		//		GL.glVertexAttribPointer(indx, size, type, normalized, stride, ((ByteBuffer)buffer).asFloatBuffer());
		//	else
		//		throw new GdxRuntimeException("Can't use " + buffer.GetType().Name + " with type " + type
		//			+ " with this method. Use ByteBuffer and one of GL_BYTE, GL_UNSIGNED_BYTE, GL_SHORT, GL_UNSIGNED_SHORT or GL_FLOAT for type. Blame LWJGL");
		//} else if (buffer is FloatBuffer) {
		//	if (type == GL20.GL_FLOAT)
		//		GL.glVertexAttribPointer(indx, size, type, normalized, stride, (FloatBuffer)buffer);
		//	else
		//		throw new GdxRuntimeException(
		//			"Can't use " + buffer.GetType().Name + " with type " + type + " with this method.");
		//} else
		//	throw new GdxRuntimeException(
		//		"Can't use " + buffer.GetType().Name + " with this method. Use ByteBuffer instead. Blame LWJGL");
	}

	public void glViewport(int x, int y, int width, int height)
	{
		GL.glViewport(x, y, width, height);
	}

	public void glDrawElements(int mode, int count, int type, int indices)
	{
		GL.glDrawElements(mode, count, type, indices);
	}

	public void glVertexAttribPointer(int indx, int size, int type, bool normalized, int stride, int ptr)
	{
		GL.glVertexAttribPointer(indx, size, type, normalized, stride, ptr);
	}
}
}
