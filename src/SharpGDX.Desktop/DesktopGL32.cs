﻿using SharpGDX.graphics;

namespace SharpGDX.Desktop
{
	public class DesktopGL32 : DesktopGL31, GL32
	{
		public void glBlendBarrier()
		{
			throw new NotImplementedException();
		}

		public void glCopyImageSubData(int srcName, int srcTarget, int srcLevel, int srcX, int srcY, int srcZ, int dstName,
			int dstTarget, int dstLevel, int dstX, int dstY, int dstZ, int srcWidth, int srcHeight, int srcDepth)
		{
			throw new NotImplementedException();
		}

		public void glDebugMessageControl(int source, int type, int severity, int[] ids, bool enabled)
		{
			throw new NotImplementedException();
		}

		public void glDebugMessageInsert(int source, int type, int id, int severity, string buf)
		{
			throw new NotImplementedException();
		}

		public void glDebugMessageCallback(GL32.DebugProc callback)
		{
			throw new NotImplementedException();
		}

		public int glGetDebugMessageLog(int count, int[] sources, int[] types, int[] ids, int[] severities,
			int[] lengths, byte[] messageLog)
		{
			throw new NotImplementedException();
		}

		public void glPushDebugGroup(int source, int id, string message)
		{
			throw new NotImplementedException();
		}

		public void glPopDebugGroup()
		{
			throw new NotImplementedException();
		}

		public void glObjectLabel(int identifier, int name, string label)
		{
			throw new NotImplementedException();
		}

		public string glGetObjectLabel(int identifier, int name)
		{
			throw new NotImplementedException();
		}

		public long glGetPointerv(int pname)
		{
			throw new NotImplementedException();
		}

		public void glEnablei(int target, int index)
		{
			throw new NotImplementedException();
		}

		public void glDisablei(int target, int index)
		{
			throw new NotImplementedException();
		}

		public void glBlendEquationi(int buf, int mode)
		{
			throw new NotImplementedException();
		}

		public void glBlendEquationSeparatei(int buf, int modeRGB, int modeAlpha)
		{
			throw new NotImplementedException();
		}

		public void glBlendFunci(int buf, int src, int dst)
		{
			throw new NotImplementedException();
		}

		public void glBlendFuncSeparatei(int buf, int srcRGB, int dstRGB, int srcAlpha, int dstAlpha)
		{
			throw new NotImplementedException();
		}

		public void glColorMaski(int index, bool r, bool g, bool b, bool a)
		{
			throw new NotImplementedException();
		}

		public bool glIsEnabledi(int target, int index)
		{
			throw new NotImplementedException();
		}

		public void glDrawElementsBaseVertex<T>(int mode, int count, int type, T[] indices, int basevertex)
			where T : struct
		{
			throw new NotImplementedException();
		}

		public void glDrawRangeElementsBaseVertex<T>(int mode, int start, int end, int count, int type, T[] indices, int basevertex)
			where T : struct
		{
			throw new NotImplementedException();
		}

		public void glDrawElementsInstancedBaseVertex<T>(int mode, int count, int type, T[] indices, int instanceCount,
			int basevertex)
			where T : struct
		{
			throw new NotImplementedException();
		}

		public void glDrawElementsInstancedBaseVertex(int mode, int count, int type, int indicesOffset, int instanceCount,
			int basevertex)
		{
			throw new NotImplementedException();
		}

		public void glFramebufferTexture(int target, int attachment, int texture, int level)
		{
			throw new NotImplementedException();
		}

		public int glGetGraphicsResetStatus()
		{
			throw new NotImplementedException();
		}

		public void glReadnPixels<T>(int x, int y, int width, int height, int format, int type, int bufSize, T[] data)
			where T : struct
		{
			throw new NotImplementedException();
		}

		public void glGetnUniformfv(int program, int location, float[] @params)
		{
			throw new NotImplementedException();
		}

		public void glGetnUniformiv(int program, int location, int[] @params)
		{
			throw new NotImplementedException();
		}

		public void glGetnUniformuiv(int program, int location, int[] @params)
		{
			throw new NotImplementedException();
		}

		public void glMinSampleShading(float value)
		{
			throw new NotImplementedException();
		}

		public void glPatchParameteri(int pname, int value)
		{
			throw new NotImplementedException();
		}

		public void glTexParameterIiv(int target, int pname, int[] @params)
		{
			throw new NotImplementedException();
		}

		public void glTexParameterIuiv(int target, int pname, int[] @params)
		{
			throw new NotImplementedException();
		}

		public void glGetTexParameterIiv(int target, int pname, int[] @params)
		{
			throw new NotImplementedException();
		}

		public void glGetTexParameterIuiv(int target, int pname, int[] @params)
		{
			throw new NotImplementedException();
		}

		public void glSamplerParameterIiv(int sampler, int pname, int[] param)
		{
			throw new NotImplementedException();
		}

		public void glSamplerParameterIuiv(int sampler, int pname, int[] param)
		{
			throw new NotImplementedException();
		}

		public void glGetSamplerParameterIiv(int sampler, int pname, int[] @params)
		{
			throw new NotImplementedException();
		}

		public void glGetSamplerParameterIuiv(int sampler, int pname, int[] @params)
		{
			throw new NotImplementedException();
		}

		public void glTexBuffer(int target, int internalformat, int buffer)
		{
			throw new NotImplementedException();
		}

		public void glTexBufferRange(int target, int internalformat, int buffer, int offset, int size)
		{
			throw new NotImplementedException();
		}

		public void glTexStorage3DMultisample(int target, int samples, int internalformat, int width, int height, int depth,
			bool fixedsamplelocations)
		{
			throw new NotImplementedException();
		}
	}
}
