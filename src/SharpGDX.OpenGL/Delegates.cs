using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace SharpGDX.OpenGL;

internal static class Delegates
{
	[SuppressUnmanagedCodeSecurity]
	internal delegate void Accum(int op, float value);

	internal unsafe delegate void glDebugMessageControlDelegate(uint source, uint type, uint severity, int count, int* ids, bool enabled);

	internal delegate void DebugMessageCallback(DebugProc callback, IntPtr userParam);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ActiveStencilFaceEXT(int face);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ActiveTexture(int texture);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ActiveTextureARB(int texture);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ActiveVaryingNV(uint program, string name);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void AlphaFragmentOp1ATI(int op, uint dst, uint dstMod, uint arg1, uint arg1Rep, uint arg1Mod);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void AlphaFragmentOp2ATI(int op, uint dst, uint dstMod, uint arg1, uint arg1Rep, uint arg1Mod,
		uint arg2, uint arg2Rep, uint arg2Mod);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void AlphaFragmentOp3ATI(int op, uint dst, uint dstMod, uint arg1, uint arg1Rep, uint arg1Mod,
		uint arg2, uint arg2Rep, uint arg2Mod, uint arg3, uint arg3Rep, uint arg3Mod);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void AlphaFunc(int func, float @ref);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ApplyTextureEXT(int mode);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate bool AreProgramsResidentNV(int n, uint* programs, [Out] bool* residences);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate bool AreTexturesResident(int n, uint* textures, [Out] bool* residences);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate bool AreTexturesResidentEXT(int n, uint* textures, [Out] bool* residences);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ArrayElement(int i);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ArrayElementEXT(int i);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ArrayObjectATI(int array, int size, int type, int stride, uint buffer, uint offset);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void AsyncMarkerSGIX(uint marker);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void AttachObjectARB(uint containerObj, uint obj);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void AttachShader(uint program, uint shader);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Begin(int mode);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BeginFragmentShaderATI();

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BeginOcclusionQueryNV(uint id);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BeginQuery(int target, uint id);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BeginQueryARB(int target, uint id);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BeginTransformFeedbackNV(int primitiveMode);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BeginVertexShaderEXT();

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BindAttribLocation(uint program, uint index, string name);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BindAttribLocationARB(uint programObj, uint index, string name);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BindBuffer(int target, uint buffer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BindBufferARB(int target, uint buffer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BindBufferBaseNV(int target, uint index, uint buffer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BindBufferOffsetNV(int target, uint index, uint buffer, IntPtr offset);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BindBufferRangeNV(int target, uint index, uint buffer, IntPtr offset, IntPtr size);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BindFragDataLocationEXT(uint program, uint color, string name);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BindFragmentShaderATI(uint id);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BindFramebufferEXT(int target, uint framebuffer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate int BindLightParameterEXT(int light, int value);

	[SuppressUnmanagedCodeSecurity]
	internal delegate int BindMaterialParameterEXT(int face, int value);

	[SuppressUnmanagedCodeSecurity]
	internal delegate int BindParameterEXT(int value);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BindProgramARB(int target, uint program);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BindProgramNV(int target, uint id);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BindRenderbufferEXT(int target, uint renderbuffer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate int BindTexGenParameterEXT(int unit, int coord, int value);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BindTexture(int target, uint texture);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BindTextureEXT(int target, uint texture);

	[SuppressUnmanagedCodeSecurity]
	internal delegate int BindTextureUnitParameterEXT(int unit, int value);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BindVertexArrayAPPLE(uint array);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BindVertexShaderEXT(uint id);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Binormal3bEXT(sbyte bx, sbyte by, sbyte bz);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Binormal3bvEXT(sbyte* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Binormal3dEXT(double bx, double by, double bz);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Binormal3dvEXT(double* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Binormal3fEXT(float bx, float by, float bz);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Binormal3fvEXT(float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Binormal3iEXT(int bx, int by, int bz);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Binormal3ivEXT(int* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Binormal3sEXT(short bx, short by, short bz);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Binormal3svEXT(short* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BinormalPointerEXT(int type, int stride, IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Bitmap(int width, int height, float xorig, float yorig, float xmove, float ymove,
		byte* bitmap);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BlendColor(float red, float green, float blue, float alpha);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BlendColorEXT(float red, float green, float blue, float alpha);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BlendEquation(int mode);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BlendEquationEXT(int mode);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BlendEquationSeparate(int modeRGB, int modeAlpha);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BlendEquationSeparateEXT(int modeRGB, int modeAlpha);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BlendFunc(int sfactor, int dfactor);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BlendFuncSeparate(int sfactorRGB, int dfactorRGB, int sfactorAlpha, int dfactorAlpha);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BlendFuncSeparateEXT(int sfactorRGB, int dfactorRGB, int sfactorAlpha, int dfactorAlpha);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BlendFuncSeparateINGR(int sfactorRGB, int dfactorRGB, int sfactorAlpha, int dfactorAlpha);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BlitFramebufferEXT(int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0,
		int dstX1, int dstY1, int mask, int filter);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BufferData(int target, IntPtr size, IntPtr data, int usage);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BufferDataARB(int target, IntPtr size, IntPtr data, int usage);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BufferParameteriAPPLE(int target, int pname, int param);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BufferSubData(int target, IntPtr offset, IntPtr size, IntPtr data);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void BufferSubDataARB(int target, IntPtr offset, IntPtr size, IntPtr data);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CallList(uint list);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CallLists(int n, int type, IntPtr lists);

	[SuppressUnmanagedCodeSecurity]
	internal delegate int CheckFramebufferStatusEXT(int target);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ClampColorARB(int target, int clamp);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Clear(int mask);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ClearAccum(float red, float green, float blue, float alpha);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ClearColor(float red, float green, float blue, float alpha);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ClearColorIiEXT(int red, int green, int blue, int alpha);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ClearColorIuiEXT(uint red, uint green, uint blue, uint alpha);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ClearDepth(double depth);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ClearDepthdNV(double depth);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ClearIndex(float c);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ClearStencil(int s);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ClientActiveTexture(int texture);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ClientActiveTextureARB(int texture);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ClientActiveVertexStreamATI(int stream);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ClipPlane(int plane, double* equation);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Color3b(sbyte red, sbyte green, sbyte blue);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Color3bv(sbyte* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Color3d(double red, double green, double blue);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Color3dv(double* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Color3f(float red, float green, float blue);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Color3fv(float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Color3fVertex3fSUN(float r, float g, float b, float x, float y, float z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Color3fVertex3fvSUN(float* c, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Color3hNV(ushort red, ushort green, ushort blue);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Color3hvNV(ushort* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Color3i(int red, int green, int blue);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Color3iv(int* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Color3s(short red, short green, short blue);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Color3sv(short* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Color3ub(byte red, byte green, byte blue);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Color3ubv(byte* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Color3ui(uint red, uint green, uint blue);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Color3uiv(uint* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Color3us(ushort red, ushort green, ushort blue);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Color3usv(ushort* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Color4b(sbyte red, sbyte green, sbyte blue, sbyte alpha);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Color4bv(sbyte* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Color4d(double red, double green, double blue, double alpha);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Color4dv(double* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Color4f(float red, float green, float blue, float alpha);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Color4fNormal3fVertex3fSUN(float r, float g, float b, float a, float nx, float ny, float nz,
		float x, float y, float z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Color4fNormal3fVertex3fvSUN(float* c, float* n, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Color4fv(float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Color4hNV(ushort red, ushort green, ushort blue, ushort alpha);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Color4hvNV(ushort* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Color4i(int red, int green, int blue, int alpha);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Color4iv(int* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Color4s(short red, short green, short blue, short alpha);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Color4sv(short* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Color4ub(byte red, byte green, byte blue, byte alpha);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Color4ubv(byte* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Color4ubVertex2fSUN(byte r, byte g, byte b, byte a, float x, float y);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Color4ubVertex2fvSUN(byte* c, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Color4ubVertex3fSUN(byte r, byte g, byte b, byte a, float x, float y, float z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Color4ubVertex3fvSUN(byte* c, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Color4ui(uint red, uint green, uint blue, uint alpha);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Color4uiv(uint* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Color4us(ushort red, ushort green, ushort blue, ushort alpha);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Color4usv(ushort* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ColorFragmentOp1ATI(int op, uint dst, uint dstMask, uint dstMod, uint arg1, uint arg1Rep,
		uint arg1Mod);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ColorFragmentOp2ATI(int op, uint dst, uint dstMask, uint dstMod, uint arg1, uint arg1Rep,
		uint arg1Mod, uint arg2, uint arg2Rep, uint arg2Mod);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ColorFragmentOp3ATI(int op, uint dst, uint dstMask, uint dstMod, uint arg1, uint arg1Rep,
		uint arg1Mod, uint arg2, uint arg2Rep, uint arg2Mod, uint arg3, uint arg3Rep, uint arg3Mod);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ColorMask(bool red, bool green, bool blue, bool alpha);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ColorMaskIndexedEXT(uint index, bool r, bool g, bool b, bool a);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ColorMaterial(int face, int mode);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ColorPointer(int size, int type, int stride, IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ColorPointerEXT(int size, int type, int stride, int count, IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ColorPointerListIBM(int size, int type, int stride, IntPtr pointer, int ptrstride);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ColorPointervINTEL(int size, int type, IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ColorSubTable(int target, int start, int count, int format, int type, IntPtr data);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ColorSubTableEXT(int target, int start, int count, int format, int type, IntPtr data);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ColorTable(int target, int internalformat, int width, int format, int type, IntPtr table);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ColorTableEXT(int target, int internalFormat, int width, int format, int type, IntPtr table);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ColorTableParameterfv(int target, int pname, float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ColorTableParameterfvSGI(int target, int pname, float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ColorTableParameteriv(int target, int pname, int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ColorTableParameterivSGI(int target, int pname, int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ColorTableSGI(int target, int internalformat, int width, int format, int type, IntPtr table);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CombinerInputNV(int stage, int portion, int variable, int input, int mapping,
		int componentUsage);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CombinerOutputNV(int stage, int portion, int abOutput, int cdOutput, int sumOutput,
		int scale, int bias, bool abDotProduct, bool cdDotProduct, bool muxSum);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CombinerParameterfNV(int pname, float param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void CombinerParameterfvNV(int pname, float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CombinerParameteriNV(int pname, int param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void CombinerParameterivNV(int pname, int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void CombinerStageParameterfvNV(int stage, int pname, float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CompileShader(uint shader);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CompileShaderARB(uint shaderObj);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CompressedTexImage1D(int target, int level, int internalformat, int width, int border,
		int imageSize, IntPtr data);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CompressedTexImage1DARB(int target, int level, int internalformat, int width, int border,
		int imageSize, IntPtr data);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CompressedTexImage2D(int target, int level, int internalformat, int width, int height,
		int border, int imageSize, IntPtr data);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CompressedTexImage2DARB(int target, int level, int internalformat, int width, int height,
		int border, int imageSize, IntPtr data);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CompressedTexImage3D(int target, int level, int internalformat, int width, int height,
		int depth, int border, int imageSize, IntPtr data);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CompressedTexImage3DARB(int target, int level, int internalformat, int width, int height,
		int depth, int border, int imageSize, IntPtr data);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CompressedTexSubImage1D(int target, int level, int xoffset, int width, int format,
		int imageSize, IntPtr data);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CompressedTexSubImage1DARB(int target, int level, int xoffset, int width, int format,
		int imageSize, IntPtr data);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CompressedTexSubImage2D(int target, int level, int xoffset, int yoffset, int width,
		int height, int format, int imageSize, IntPtr data);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CompressedTexSubImage2DARB(int target, int level, int xoffset, int yoffset, int width,
		int height, int format, int imageSize, IntPtr data);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CompressedTexSubImage3D(int target, int level, int xoffset, int yoffset, int zoffset,
		int width, int height, int depth, int format, int imageSize, IntPtr data);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CompressedTexSubImage3DARB(int target, int level, int xoffset, int yoffset, int zoffset,
		int width, int height, int depth, int format, int imageSize, IntPtr data);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ConvolutionFilter1D(int target, int internalformat, int width, int format, int type,
		IntPtr image);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ConvolutionFilter1DEXT(int target, int internalformat, int width, int format, int type,
		IntPtr image);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ConvolutionFilter2D(int target, int internalformat, int width, int height, int format,
		int type, IntPtr image);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ConvolutionFilter2DEXT(int target, int internalformat, int width, int height, int format,
		int type, IntPtr image);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ConvolutionParameterf(int target, int pname, float @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ConvolutionParameterfEXT(int target, int pname, float @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ConvolutionParameterfv(int target, int pname, float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ConvolutionParameterfvEXT(int target, int pname, float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ConvolutionParameteri(int target, int pname, int @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ConvolutionParameteriEXT(int target, int pname, int @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ConvolutionParameteriv(int target, int pname, int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ConvolutionParameterivEXT(int target, int pname, int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CopyColorSubTable(int target, int start, int x, int y, int width);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CopyColorSubTableEXT(int target, int start, int x, int y, int width);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CopyColorTable(int target, int internalformat, int x, int y, int width);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CopyColorTableSGI(int target, int internalformat, int x, int y, int width);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CopyConvolutionFilter1D(int target, int internalformat, int x, int y, int width);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CopyConvolutionFilter1DEXT(int target, int internalformat, int x, int y, int width);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CopyConvolutionFilter2D(int target, int internalformat, int x, int y, int width, int height);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CopyConvolutionFilter2DEXT(int target, int internalformat, int x, int y, int width,
		int height);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CopyPixels(int x, int y, int width, int height, int type);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CopyTexImage1D(int target, int level, int internalformat, int x, int y, int width,
		int border);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CopyTexImage1DEXT(int target, int level, int internalformat, int x, int y, int width,
		int border);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CopyTexImage2D(int target, int level, int internalformat, int x, int y, int width,
		int height, int border);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CopyTexImage2DEXT(int target, int level, int internalformat, int x, int y, int width,
		int height, int border);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CopyTexSubImage1D(int target, int level, int xoffset, int x, int y, int width);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CopyTexSubImage1DEXT(int target, int level, int xoffset, int x, int y, int width);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CopyTexSubImage2D(int target, int level, int xoffset, int yoffset, int x, int y, int width,
		int height);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CopyTexSubImage2DEXT(int target, int level, int xoffset, int yoffset, int x, int y,
		int width, int height);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CopyTexSubImage3D(int target, int level, int xoffset, int yoffset, int zoffset, int x, int y,
		int width, int height);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CopyTexSubImage3DEXT(int target, int level, int xoffset, int yoffset, int zoffset, int x,
		int y, int width, int height);

	[SuppressUnmanagedCodeSecurity]
	internal delegate int CreateProgram();

	[SuppressUnmanagedCodeSecurity]
	internal delegate int CreateProgramObjectARB();

	[SuppressUnmanagedCodeSecurity]
	internal delegate int CreateShader(int type);

	[SuppressUnmanagedCodeSecurity]
	internal delegate int CreateShaderObjectARB(int shaderType);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CullFace(int mode);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void CullParameterdvEXT(int pname, [Out] double* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void CullParameterfvEXT(int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void CurrentPaletteMatrixARB(int index);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void DeformationMap3dSGIX(int target, double u1, double u2, int ustride, int uorder,
		double v1, double v2, int vstride, int vorder, double w1, double w2, int wstride, int worder, double* points);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void DeformationMap3fSGIX(int target, float u1, float u2, int ustride, int uorder,
		float v1, float v2, int vstride, int vorder, float w1, float w2, int wstride, int worder, float* points);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DeformSGIX(int mask);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DeleteAsyncMarkersSGIX(uint marker, int range);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void DeleteBuffers(int n, uint* buffers);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void DeleteBuffersARB(int n, uint* buffers);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void DeleteFencesAPPLE(int n, uint* fences);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void DeleteFencesNV(int n, uint* fences);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DeleteFragmentShaderATI(uint id);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void DeleteFramebuffersEXT(int n, uint* framebuffers);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DeleteLists(uint list, int range);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DeleteObjectARB(uint obj);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void DeleteOcclusionQueriesNV(int n, uint* ids);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DeleteProgram(uint program);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void DeleteProgramsARB(int n, uint* programs);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void DeleteProgramsNV(int n, uint* programs);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void DeleteQueries(int n, uint* ids);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void DeleteQueriesARB(int n, uint* ids);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void DeleteRenderbuffersEXT(int n, uint* renderbuffers);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DeleteShader(uint shader);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void DeleteTextures(int n, uint* textures);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void DeleteTexturesEXT(int n, uint* textures);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void DeleteVertexArraysAPPLE(int n, uint* arrays);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DeleteVertexShaderEXT(uint id);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DepthBoundsdNV(double zmin, double zmax);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DepthBoundsEXT(double zmin, double zmax);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DepthFunc(int func);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DepthMask(bool flag);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DepthRange(double near, double far);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DepthRangedNV(double zNear, double zFar);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DetachObjectARB(uint containerObj, uint attachedObj);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DetachShader(uint program, uint shader);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void DetailTexFuncSGIS(int target, int n, float* points);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Disable(int cap);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DisableClientState(int array);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DisableIndexedEXT(int target, uint index);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DisableVariantClientStateEXT(uint id);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DisableVertexAttribArray(uint index);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DisableVertexAttribArrayARB(uint index);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DrawArrays(int mode, int first, int count);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DrawArraysEXT(int mode, int first, int count);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DrawArraysInstancedEXT(int mode, int start, int count, int primcount);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DrawBuffer(int mode);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void DrawBuffers(int n, int* bufs);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void DrawBuffersARB(int n, int* bufs);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void DrawBuffersATI(int n, int* bufs);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DrawElementArrayAPPLE(int mode, int first, int count);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DrawElementArrayATI(int mode, int count);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DrawElements(int mode, int count, int type, IntPtr indices);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DrawElementsInstancedEXT(int mode, int count, int type, IntPtr indices, int primcount);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DrawMeshArraysSUN(int mode, int first, int count, int width);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DrawPixels(int width, int height, int format, int type, IntPtr pixels);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DrawRangeElementArrayAPPLE(int mode, uint start, uint end, int first, int count);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DrawRangeElementArrayATI(int mode, uint start, uint end, int count);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DrawRangeElements(int mode, uint start, uint end, int count, int type, IntPtr indices);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void DrawRangeElementsEXT(int mode, uint start, uint end, int count, int type, IntPtr indices);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void EdgeFlag(bool flag);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void EdgeFlagPointer(int stride, IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void EdgeFlagPointerEXT(int stride, int count, bool* pointer);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void EdgeFlagPointerListIBM(int stride, bool* pointer, int ptrstride);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void EdgeFlagv(bool* flag);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ElementPointerAPPLE(int type, IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ElementPointerATI(int type, IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Enable(int cap);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void EnableClientState(int array);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void EnableIndexedEXT(int target, uint index);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void EnableVariantClientStateEXT(uint id);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void EnableVertexAttribArray(uint index);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void EnableVertexAttribArrayARB(uint index);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void End();

	[SuppressUnmanagedCodeSecurity]
	internal delegate void EndFragmentShaderATI();

	[SuppressUnmanagedCodeSecurity]
	internal delegate void EndList();

	[SuppressUnmanagedCodeSecurity]
	internal delegate void EndOcclusionQueryNV();

	[SuppressUnmanagedCodeSecurity]
	internal delegate void EndQuery(int target);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void EndQueryARB(int target);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void EndTransformFeedbackNV();

	[SuppressUnmanagedCodeSecurity]
	internal delegate void EndVertexShaderEXT();

	[SuppressUnmanagedCodeSecurity]
	internal delegate void EvalCoord1d(double u);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void EvalCoord1dv(double* u);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void EvalCoord1f(float u);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void EvalCoord1fv(float* u);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void EvalCoord2d(double u, double v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void EvalCoord2dv(double* u);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void EvalCoord2f(float u, float v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void EvalCoord2fv(float* u);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void EvalMapsNV(int target, int mode);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void EvalMesh1(int mode, int i1, int i2);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void EvalMesh2(int mode, int i1, int i2, int j1, int j2);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void EvalPoint1(int i);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void EvalPoint2(int i, int j);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ExecuteProgramNV(int target, uint id, float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ExtractComponentEXT(uint res, uint src, uint num);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void FeedbackBuffer(int size, int type, [Out] float* buffer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FinalCombinerInputNV(int variable, int input, int mapping, int componentUsage);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Finish();

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate int FinishAsyncSGIX([Out] uint* markerp);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FinishFenceAPPLE(uint fence);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FinishFenceNV(uint fence);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FinishObjectAPPLE(int @object, int name);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FinishTextureSUNX();

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Flush();

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FlushMappedBufferRangeAPPLE(int target, IntPtr offset, IntPtr size);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FlushPixelDataRangeNV(int target);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FlushRasterSGIX();

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FlushVertexArrayRangeAPPLE(int length, [Out] IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FlushVertexArrayRangeNV();

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FogCoordd(double coord);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FogCoorddEXT(double coord);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void FogCoorddv(double* coord);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void FogCoorddvEXT(double* coord);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FogCoordf(float coord);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FogCoordfEXT(float coord);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void FogCoordfv(float* coord);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void FogCoordfvEXT(float* coord);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FogCoordhNV(ushort fog);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void FogCoordhvNV(ushort* fog);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FogCoordPointer(int type, int stride, IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FogCoordPointerEXT(int type, int stride, IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FogCoordPointerListIBM(int type, int stride, IntPtr pointer, int ptrstride);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Fogf(int pname, float param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void FogFuncSGIS(int n, float* points);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Fogfv(int pname, float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Fogi(int pname, int param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Fogiv(int pname, int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FragmentColorMaterialSGIX(int face, int mode);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FragmentLightfSGIX(int light, int pname, float param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void FragmentLightfvSGIX(int light, int pname, float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FragmentLightiSGIX(int light, int pname, int param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void FragmentLightivSGIX(int light, int pname, int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FragmentLightModelfSGIX(int pname, float param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void FragmentLightModelfvSGIX(int pname, float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FragmentLightModeliSGIX(int pname, int param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void FragmentLightModelivSGIX(int pname, int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FragmentMaterialfSGIX(int face, int pname, float param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void FragmentMaterialfvSGIX(int face, int pname, float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FragmentMaterialiSGIX(int face, int pname, int param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void FragmentMaterialivSGIX(int face, int pname, int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FramebufferRenderbufferEXT(int target, int attachment, int renderbuffertarget,
		uint renderbuffer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FramebufferTexture1DEXT(int target, int attachment, int textarget, uint texture, int level);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FramebufferTexture2DEXT(int target, int attachment, int textarget, uint texture, int level);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FramebufferTexture3DEXT(int target, int attachment, int textarget, uint texture, int level,
		int zoffset);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FramebufferTextureEXT(int target, int attachment, uint texture, int level);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FramebufferTextureFaceEXT(int target, int attachment, uint texture, int level, int face);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FramebufferTextureLayerEXT(int target, int attachment, uint texture, int level, int layer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FrameTerminatorGREMEDY();

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FrameZoomSGIX(int factor);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FreeObjectBufferATI(uint buffer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void FrontFace(int mode);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Frustum(double left, double right, double bottom, double top, double zNear, double zFar);

	[SuppressUnmanagedCodeSecurity]
	internal delegate int GenAsyncMarkersSGIX(int range);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GenBuffers(int n, [Out] uint* buffers);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GenBuffersARB(int n, [Out] uint* buffers);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GenerateMipmapEXT(int target);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GenFencesAPPLE(int n, [Out] uint* fences);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GenFencesNV(int n, [Out] uint* fences);

	[SuppressUnmanagedCodeSecurity]
	internal delegate int GenFragmentShadersATI(uint range);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GenFramebuffersEXT(int n, [Out] uint* framebuffers);

	[SuppressUnmanagedCodeSecurity]
	internal delegate int GenLists(int range);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GenOcclusionQueriesNV(int n, [Out] uint* ids);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GenProgramsARB(int n, [Out] uint* programs);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GenProgramsNV(int n, [Out] uint* programs);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GenQueries(int n, [Out] uint* ids);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GenQueriesARB(int n, [Out] uint* ids);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GenRenderbuffersEXT(int n, [Out] uint* renderbuffers);

	[SuppressUnmanagedCodeSecurity]
	internal delegate int GenSymbolsEXT(int datatype, int storagetype, int range, uint components);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GenTextures(int n, [Out] uint* textures);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GenTexturesEXT(int n, [Out] uint* textures);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GenVertexArraysAPPLE(int n, [Out] uint* arrays);

	[SuppressUnmanagedCodeSecurity]
	internal delegate int GenVertexShadersEXT(uint range);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetActiveAttrib(uint program, uint index, int bufSize, [Out] int* length,
		[Out] int* size, [Out] int* type, [Out] StringBuilder name);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetActiveAttribARB(uint programObj, uint index, int maxLength, [Out] int* length,
		[Out] int* size, [Out] int* type, [Out] StringBuilder name);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetActiveUniform(uint program, uint index, int bufSize, [Out] int* length,
		[Out] int* size, [Out] int* type, [Out] StringBuilder name);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetActiveUniformARB(uint programObj, uint index, int maxLength, [Out] int* length,
		[Out] int* size, [Out] int* type, [Out] StringBuilder name);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetActiveVaryingNV(uint program, uint index, int bufSize, [Out] int* length,
		[Out] int* size, [Out] int* type, [Out] StringBuilder name);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetArrayObjectfvATI(int array, int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetArrayObjectivATI(int array, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetAttachedObjectsARB(uint containerObj, int maxCount, [Out] int* count,
		[Out] uint* obj);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetAttachedShaders(uint program, int maxCount, [Out] int* count, [Out] uint* obj);

	[SuppressUnmanagedCodeSecurity]
	internal delegate int GetAttribLocation(uint program, string name);

	[SuppressUnmanagedCodeSecurity]
	internal delegate int GetAttribLocationARB(uint programObj, string name);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetBooleanIndexedvEXT(int target, uint index, [Out] bool* data);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetBooleanv(int pname, [Out] bool* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetBufferParameteriv(int target, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetBufferParameterivARB(int target, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GetBufferPointerv(int target, int pname, [Out] IntPtr @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GetBufferPointervARB(int target, int pname, [Out] IntPtr @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GetBufferSubData(int target, IntPtr offset, IntPtr size, [Out] IntPtr data);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GetBufferSubDataARB(int target, IntPtr offset, IntPtr size, [Out] IntPtr data);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetClipPlane(int plane, [Out] double* equation);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GetColorTable(int target, int format, int type, [Out] IntPtr table);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GetColorTableEXT(int target, int format, int type, [Out] IntPtr data);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetColorTableParameterfv(int target, int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetColorTableParameterfvEXT(int target, int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetColorTableParameterfvSGI(int target, int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetColorTableParameteriv(int target, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetColorTableParameterivEXT(int target, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetColorTableParameterivSGI(int target, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GetColorTableSGI(int target, int format, int type, [Out] IntPtr table);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetCombinerInputParameterfvNV(int stage, int portion, int variable, int pname,
		[Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetCombinerInputParameterivNV(int stage, int portion, int variable, int pname,
		[Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetCombinerOutputParameterfvNV(int stage, int portion, int pname,
		[Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetCombinerOutputParameterivNV(int stage, int portion, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetCombinerStageParameterfvNV(int stage, int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GetCompressedTexImage(int target, int level, [Out] IntPtr img);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GetCompressedTexImageARB(int target, int level, [Out] IntPtr img);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GetConvolutionFilter(int target, int format, int type, [Out] IntPtr image);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GetConvolutionFilterEXT(int target, int format, int type, [Out] IntPtr image);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetConvolutionParameterfv(int target, int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetConvolutionParameterfvEXT(int target, int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetConvolutionParameteriv(int target, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetConvolutionParameterivEXT(int target, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetDetailTexFuncSGIS(int target, [Out] float* points);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetDoublev(int pname, [Out] double* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate int GetError();

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetFenceivNV(uint fence, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetFinalCombinerInputParameterfvNV(int variable, int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetFinalCombinerInputParameterivNV(int variable, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetFloatv(int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetFogFuncSGIS([Out] float* points);

	[SuppressUnmanagedCodeSecurity]
	internal delegate int GetFragDataLocationEXT(uint program, string name);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetFragmentLightfvSGIX(int light, int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetFragmentLightivSGIX(int light, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetFragmentMaterialfvSGIX(int face, int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetFragmentMaterialivSGIX(int face, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetFramebufferAttachmentParameterivEXT(int target, int attachment, int pname,
		[Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate int GetHandleARB(int pname);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GetHistogram(int target, bool reset, int format, int type, [Out] IntPtr values);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GetHistogramEXT(int target, bool reset, int format, int type, [Out] IntPtr values);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetHistogramParameterfv(int target, int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetHistogramParameterfvEXT(int target, int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetHistogramParameteriv(int target, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetHistogramParameterivEXT(int target, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetImageTransformParameterfvHP(int target, int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetImageTransformParameterivHP(int target, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void
		GetInfoLogARB(uint obj, int maxLength, [Out] int* length, [Out] StringBuilder infoLog);

	[SuppressUnmanagedCodeSecurity]
	internal delegate int GetInstrumentsSGIX();

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetIntegerIndexedvEXT(int target, uint index, [Out] int* data);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetIntegerv(int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetInvariantBooleanvEXT(uint id, int value, [Out] bool* data);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetInvariantFloatvEXT(uint id, int value, [Out] float* data);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetInvariantIntegervEXT(uint id, int value, [Out] int* data);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetLightfv(int light, int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetLightiv(int light, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetListParameterfvSGIX(uint list, int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetListParameterivSGIX(uint list, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetLocalConstantBooleanvEXT(uint id, int value, [Out] bool* data);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetLocalConstantFloatvEXT(uint id, int value, [Out] float* data);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetLocalConstantIntegervEXT(uint id, int value, [Out] int* data);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetMapAttribParameterfvNV(int target, uint index, int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetMapAttribParameterivNV(int target, uint index, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GetMapControlPointsNV(int target, uint index, int type, int ustride, int vstride,
		bool packed, [Out] IntPtr points);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetMapdv(int target, int query, [Out] double* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetMapfv(int target, int query, [Out] float* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetMapiv(int target, int query, [Out] int* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetMapParameterfvNV(int target, int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetMapParameterivNV(int target, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetMaterialfv(int face, int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetMaterialiv(int face, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GetMinmax(int target, bool reset, int format, int type, [Out] IntPtr values);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GetMinmaxEXT(int target, bool reset, int format, int type, [Out] IntPtr values);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetMinmaxParameterfv(int target, int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetMinmaxParameterfvEXT(int target, int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetMinmaxParameteriv(int target, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetMinmaxParameterivEXT(int target, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetObjectBufferfvATI(uint buffer, int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetObjectBufferivATI(uint buffer, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetObjectParameterfvARB(uint obj, int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetObjectParameterivARB(uint obj, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetOcclusionQueryivNV(uint id, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetOcclusionQueryuivNV(uint id, int pname, [Out] uint* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetPixelMapfv(int map, [Out] float* values);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetPixelMapuiv(int map, [Out] uint* values);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetPixelMapusv(int map, [Out] ushort* values);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetPixelTexGenParameterfvSGIS(int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetPixelTexGenParameterivSGIS(int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GetPointerv(int pname, [Out] IntPtr @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GetPointervEXT(int pname, [Out] IntPtr @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetPolygonStipple([Out] byte* mask);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetProgramEnvParameterdvARB(int target, uint index, [Out] double* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetProgramEnvParameterfvARB(int target, uint index, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetProgramEnvParameterIivNV(int target, uint index, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetProgramEnvParameterIuivNV(int target, uint index, [Out] uint* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetProgramInfoLog(uint program, int bufSize, [Out] int* length,
		[Out] StringBuilder infoLog);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetProgramiv(uint program, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetProgramivARB(int target, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetProgramivNV(uint id, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetProgramLocalParameterdvARB(int target, uint index, [Out] double* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetProgramLocalParameterfvARB(int target, uint index, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetProgramLocalParameterIivNV(int target, uint index, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetProgramLocalParameterIuivNV(int target, uint index, [Out] uint* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetProgramNamedParameterdvNV(uint id, int len, byte* name, [Out] double* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetProgramNamedParameterfvNV(uint id, int len, byte* name, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetProgramParameterdvNV(int target, uint index, int pname, [Out] double* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetProgramParameterfvNV(int target, uint index, int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GetProgramStringARB(int target, int pname, [Out] IntPtr @string);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetProgramStringNV(uint id, int pname, [Out] byte* program);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetQueryiv(int target, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetQueryivARB(int target, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetQueryObjecti64vEXT(uint id, int pname, [Out] long* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetQueryObjectiv(uint id, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetQueryObjectivARB(uint id, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetQueryObjectui64vEXT(uint id, int pname, [Out] ulong* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetQueryObjectuiv(uint id, int pname, [Out] uint* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetQueryObjectuivARB(uint id, int pname, [Out] uint* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetRenderbufferParameterivEXT(int target, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GetSeparableFilter(int target, int format, int type, [Out] IntPtr row, [Out] IntPtr column,
		[Out] IntPtr span);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GetSeparableFilterEXT(int target, int format, int type, [Out] IntPtr row,
		[Out] IntPtr column, [Out] IntPtr span);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetShaderInfoLog(uint shader, int bufSize, [Out] int* length,
		[Out] StringBuilder infoLog);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetShaderiv(uint shader, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetShaderSource(uint shader, int bufSize, [Out] int* length,
		[Out] StringBuilder[] source);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetShaderSourceARB(uint obj, int maxLength, [Out] int* length,
		[Out] StringBuilder[] source);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetSharpenTexFuncSGIS(int target, [Out] float* points);

	[SuppressUnmanagedCodeSecurity]
	internal delegate IntPtr GetString(int name);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetTexBumpParameterfvATI(int pname, [Out] float* param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetTexBumpParameterivATI(int pname, [Out] int* param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetTexEnvfv(int target, int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetTexEnviv(int target, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetTexFilterFuncSGIS(int target, int filter, [Out] float* weights);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetTexGendv(int coord, int pname, [Out] double* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetTexGenfv(int coord, int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetTexGeniv(int coord, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GetTexImage(int target, int level, int format, int type, [Out] IntPtr pixels);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetTexLevelParameterfv(int target, int level, int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetTexLevelParameteriv(int target, int level, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetTexParameterfv(int target, int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetTexParameterIivEXT(int target, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetTexParameterIuivEXT(int target, int pname, [Out] uint* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetTexParameteriv(int target, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetTrackMatrixivNV(int target, uint address, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetTransformFeedbackVaryingNV(uint program, uint index, [Out] int* location);

	[SuppressUnmanagedCodeSecurity]
	internal delegate int GetUniformBufferSizeEXT(uint program, int location);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetUniformfv(uint program, int location, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetUniformfvARB(uint programObj, int location, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetUniformiv(uint program, int location, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetUniformivARB(uint programObj, int location, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate int GetUniformLocation(uint program, string name);

	[SuppressUnmanagedCodeSecurity]
	internal delegate int GetUniformLocationARB(uint programObj, string name);

	[SuppressUnmanagedCodeSecurity]
	internal delegate IntPtr GetUniformOffsetEXT(uint program, int location);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetUniformuivEXT(uint program, int location, [Out] uint* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetVariantArrayObjectfvATI(uint id, int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetVariantArrayObjectivATI(uint id, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetVariantBooleanvEXT(uint id, int value, [Out] bool* data);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetVariantFloatvEXT(uint id, int value, [Out] float* data);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetVariantIntegervEXT(uint id, int value, [Out] int* data);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GetVariantPointervEXT(uint id, int value, [Out] IntPtr data);

	[SuppressUnmanagedCodeSecurity]
	internal delegate int GetVaryingLocationNV(uint program, string name);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetVertexAttribArrayObjectfvATI(uint index, int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetVertexAttribArrayObjectivATI(uint index, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetVertexAttribdv(uint index, int pname, [Out] double* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetVertexAttribdvARB(uint index, int pname, [Out] double* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetVertexAttribdvNV(uint index, int pname, [Out] double* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetVertexAttribfv(uint index, int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetVertexAttribfvARB(uint index, int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetVertexAttribfvNV(uint index, int pname, [Out] float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetVertexAttribIivEXT(uint index, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetVertexAttribIuivEXT(uint index, int pname, [Out] uint* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetVertexAttribiv(uint index, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetVertexAttribivARB(uint index, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void GetVertexAttribivNV(uint index, int pname, [Out] int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GetVertexAttribPointerv(uint index, int pname, [Out] IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GetVertexAttribPointervARB(uint index, int pname, [Out] IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GetVertexAttribPointervNV(uint index, int pname, [Out] IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GlobalAlphaFactorbSUN(sbyte factor);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GlobalAlphaFactordSUN(double factor);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GlobalAlphaFactorfSUN(float factor);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GlobalAlphaFactoriSUN(int factor);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GlobalAlphaFactorsSUN(short factor);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GlobalAlphaFactorubSUN(byte factor);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GlobalAlphaFactoruiSUN(uint factor);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void GlobalAlphaFactorusSUN(ushort factor);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Hint(int target, int mode);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void HintPGI(int target, int mode);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Histogram(int target, int width, int internalformat, bool sink);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void HistogramEXT(int target, int width, int internalformat, bool sink);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void IglooInterfaceSGIX(int pname, IntPtr @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ImageTransformParameterfHP(int target, int pname, float param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ImageTransformParameterfvHP(int target, int pname, float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ImageTransformParameteriHP(int target, int pname, int param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ImageTransformParameterivHP(int target, int pname, int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Indexd(double c);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Indexdv(double* c);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Indexf(float c);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void IndexFuncEXT(int func, float @ref);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Indexfv(float* c);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Indexi(int c);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Indexiv(int* c);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void IndexMask(uint mask);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void IndexMaterialEXT(int face, int mode);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void IndexPointer(int type, int stride, IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void IndexPointerEXT(int type, int stride, int count, IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void IndexPointerListIBM(int type, int stride, IntPtr pointer, int ptrstride);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Indexs(short c);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Indexsv(short* c);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Indexub(byte c);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Indexubv(byte* c);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void InitNames();

	[SuppressUnmanagedCodeSecurity]
	internal delegate void InsertComponentEXT(uint res, uint src, uint num);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void InstrumentsBufferSGIX(int size, [Out] int* buffer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void InterleavedArrays(int format, int stride, IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate bool IsAsyncMarkerSGIX(uint marker);

	[SuppressUnmanagedCodeSecurity]
	internal delegate bool IsBuffer(uint buffer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate bool IsBufferARB(uint buffer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate bool IsEnabled(int cap);

	[SuppressUnmanagedCodeSecurity]
	internal delegate bool IsEnabledIndexedEXT(int target, uint index);

	[SuppressUnmanagedCodeSecurity]
	internal delegate bool IsFenceAPPLE(uint fence);

	[SuppressUnmanagedCodeSecurity]
	internal delegate bool IsFenceNV(uint fence);

	[SuppressUnmanagedCodeSecurity]
	internal delegate bool IsFramebufferEXT(uint framebuffer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate bool IsList(uint list);

	[SuppressUnmanagedCodeSecurity]
	internal delegate bool IsObjectBufferATI(uint buffer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate bool IsOcclusionQueryNV(uint id);

	[SuppressUnmanagedCodeSecurity]
	internal delegate bool IsProgram(uint program);

	[SuppressUnmanagedCodeSecurity]
	internal delegate bool IsProgramARB(uint program);

	[SuppressUnmanagedCodeSecurity]
	internal delegate bool IsProgramNV(uint id);

	[SuppressUnmanagedCodeSecurity]
	internal delegate bool IsQuery(uint id);

	[SuppressUnmanagedCodeSecurity]
	internal delegate bool IsQueryARB(uint id);

	[SuppressUnmanagedCodeSecurity]
	internal delegate bool IsRenderbufferEXT(uint renderbuffer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate bool IsShader(uint shader);

	[SuppressUnmanagedCodeSecurity]
	internal delegate bool IsTexture(uint texture);

	[SuppressUnmanagedCodeSecurity]
	internal delegate bool IsTextureEXT(uint texture);

	[SuppressUnmanagedCodeSecurity]
	internal delegate bool IsVariantEnabledEXT(uint id, int cap);

	[SuppressUnmanagedCodeSecurity]
	internal delegate bool IsVertexArrayAPPLE(uint array);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void LightEnviSGIX(int pname, int param);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Lightf(int light, int pname, float param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Lightfv(int light, int pname, float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Lighti(int light, int pname, int param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Lightiv(int light, int pname, int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void LightModelf(int pname, float param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void LightModelfv(int pname, float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void LightModeli(int pname, int param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void LightModeliv(int pname, int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void LineStipple(int factor, ushort pattern);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void LineWidth(float width);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void LinkProgram(uint program);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void LinkProgramARB(uint programObj);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ListBase(uint @base);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ListParameterfSGIX(uint list, int pname, float param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ListParameterfvSGIX(uint list, int pname, float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ListParameteriSGIX(uint list, int pname, int param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ListParameterivSGIX(uint list, int pname, int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void LoadIdentity();

	[SuppressUnmanagedCodeSecurity]
	internal delegate void LoadIdentityDeformationMapSGIX(int mask);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void LoadMatrixd(double* m);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void LoadMatrixf(float* m);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void LoadName(uint name);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void LoadProgramNV(int target, uint id, int len, byte* program);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void LoadTransposeMatrixd(double* m);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void LoadTransposeMatrixdARB(double* m);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void LoadTransposeMatrixf(float* m);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void LoadTransposeMatrixfARB(float* m);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void LockArraysEXT(int first, int count);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void LogicOp(int opcode);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Map1d(int target, double u1, double u2, int stride, int order, double* points);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Map1f(int target, float u1, float u2, int stride, int order, float* points);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Map2d(int target, double u1, double u2, int ustride, int uorder, double v1, double v2,
		int vstride, int vorder, double* points);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Map2f(int target, float u1, float u2, int ustride, int uorder, float v1, float v2,
		int vstride, int vorder, float* points);

	[SuppressUnmanagedCodeSecurity]
	internal delegate IntPtr MapBuffer(int target, int access);

	[SuppressUnmanagedCodeSecurity]
	internal delegate IntPtr MapBufferARB(int target, int access);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MapControlPointsNV(int target, uint index, int type, int ustride, int vstride, int uorder,
		int vorder, bool packed, IntPtr points);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MapGrid1d(int un, double u1, double u2);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MapGrid1f(int un, float u1, float u2);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MapGrid2d(int un, double u1, double u2, int vn, double v1, double v2);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MapGrid2f(int un, float u1, float u2, int vn, float v1, float v2);

	[SuppressUnmanagedCodeSecurity]
	internal delegate IntPtr MapObjectBufferATI(uint buffer);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MapParameterfvNV(int target, int pname, float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MapParameterivNV(int target, int pname, int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Materialf(int face, int pname, float param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Materialfv(int face, int pname, float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Materiali(int face, int pname, int param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Materialiv(int face, int pname, int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MatrixIndexPointerARB(int size, int type, int stride, IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MatrixIndexubvARB(int size, byte* indices);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MatrixIndexuivARB(int size, uint* indices);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MatrixIndexusvARB(int size, ushort* indices);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MatrixMode(int mode);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Minmax(int target, int internalformat, bool sink);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MinmaxEXT(int target, int internalformat, bool sink);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiDrawArrays(int mode, [Out] int* first, [Out] int* count, int primcount);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiDrawArraysEXT(int mode, [Out] int* first, [Out] int* count, int primcount);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiDrawElementArrayAPPLE(int mode, int* first, int* count, int primcount);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiDrawElements(int mode, int* count, int type, IntPtr indices, int primcount);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiDrawElementsEXT(int mode, int* count, int type, IntPtr indices, int primcount);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiDrawRangeElementArrayAPPLE(int mode, uint start, uint end, int* first,
		int* count, int primcount);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiModeDrawArraysIBM(int* mode, int* first, int* count, int primcount,
		int modestride);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiModeDrawElementsIBM(int* mode, int* count, int type, IntPtr indices,
		int primcount, int modestride);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord1d(int target, double s);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord1dARB(int target, double s);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord1dv(int target, double* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord1dvARB(int target, double* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord1f(int target, float s);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord1fARB(int target, float s);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord1fv(int target, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord1fvARB(int target, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord1hNV(int target, ushort s);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord1hvNV(int target, ushort* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord1i(int target, int s);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord1iARB(int target, int s);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord1iv(int target, int* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord1ivARB(int target, int* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord1s(int target, short s);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord1sARB(int target, short s);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord1sv(int target, short* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord1svARB(int target, short* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord2d(int target, double s, double t);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord2dARB(int target, double s, double t);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord2dv(int target, double* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord2dvARB(int target, double* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord2f(int target, float s, float t);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord2fARB(int target, float s, float t);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord2fv(int target, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord2fvARB(int target, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord2hNV(int target, ushort s, ushort t);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord2hvNV(int target, ushort* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord2i(int target, int s, int t);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord2iARB(int target, int s, int t);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord2iv(int target, int* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord2ivARB(int target, int* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord2s(int target, short s, short t);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord2sARB(int target, short s, short t);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord2sv(int target, short* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord2svARB(int target, short* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord3d(int target, double s, double t, double r);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord3dARB(int target, double s, double t, double r);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord3dv(int target, double* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord3dvARB(int target, double* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord3f(int target, float s, float t, float r);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord3fARB(int target, float s, float t, float r);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord3fv(int target, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord3fvARB(int target, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord3hNV(int target, ushort s, ushort t, ushort r);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord3hvNV(int target, ushort* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord3i(int target, int s, int t, int r);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord3iARB(int target, int s, int t, int r);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord3iv(int target, int* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord3ivARB(int target, int* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord3s(int target, short s, short t, short r);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord3sARB(int target, short s, short t, short r);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord3sv(int target, short* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord3svARB(int target, short* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord4d(int target, double s, double t, double r, double q);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord4dARB(int target, double s, double t, double r, double q);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord4dv(int target, double* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord4dvARB(int target, double* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord4f(int target, float s, float t, float r, float q);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord4fARB(int target, float s, float t, float r, float q);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord4fv(int target, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord4fvARB(int target, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord4hNV(int target, ushort s, ushort t, ushort r, ushort q);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord4hvNV(int target, ushort* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord4i(int target, int s, int t, int r, int q);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord4iARB(int target, int s, int t, int r, int q);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord4iv(int target, int* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord4ivARB(int target, int* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord4s(int target, short s, short t, short r, short q);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void MultiTexCoord4sARB(int target, short s, short t, short r, short q);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord4sv(int target, short* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultiTexCoord4svARB(int target, short* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultMatrixd(double* m);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultMatrixf(float* m);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultTransposeMatrixd(double* m);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultTransposeMatrixdARB(double* m);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultTransposeMatrixf(float* m);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void MultTransposeMatrixfARB(float* m);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void NewList(uint list, int mode);

	[SuppressUnmanagedCodeSecurity]
	internal delegate int NewObjectBufferATI(int size, IntPtr pointer, int usage);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Normal3b(sbyte nx, sbyte ny, sbyte nz);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Normal3bv(sbyte* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Normal3d(double nx, double ny, double nz);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Normal3dv(double* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Normal3f(float nx, float ny, float nz);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Normal3fv(float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Normal3fVertex3fSUN(float nx, float ny, float nz, float x, float y, float z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Normal3fVertex3fvSUN(float* n, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Normal3hNV(ushort nx, ushort ny, ushort nz);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Normal3hvNV(ushort* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Normal3i(int nx, int ny, int nz);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Normal3iv(int* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Normal3s(short nx, short ny, short nz);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Normal3sv(short* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void NormalPointer(int type, int stride, IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void NormalPointerEXT(int type, int stride, int count, IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void NormalPointerListIBM(int type, int stride, IntPtr pointer, int ptrstride);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void NormalPointervINTEL(int type, IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void NormalStream3bATI(int stream, sbyte nx, sbyte ny, sbyte nz);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void NormalStream3bvATI(int stream, sbyte* coords);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void NormalStream3dATI(int stream, double nx, double ny, double nz);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void NormalStream3dvATI(int stream, double* coords);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void NormalStream3fATI(int stream, float nx, float ny, float nz);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void NormalStream3fvATI(int stream, float* coords);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void NormalStream3iATI(int stream, int nx, int ny, int nz);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void NormalStream3ivATI(int stream, int* coords);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void NormalStream3sATI(int stream, short nx, short ny, short nz);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void NormalStream3svATI(int stream, short* coords);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Ortho(double left, double right, double bottom, double top, double zNear, double zFar);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PassTexCoordATI(uint dst, uint coord, int swizzle);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PassThrough(float token);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PixelDataRangeNV(int target, int length, [Out] IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void PixelMapfv(int map, int mapsize, float* values);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void PixelMapuiv(int map, int mapsize, uint* values);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void PixelMapusv(int map, int mapsize, ushort* values);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PixelStoref(int pname, float param);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PixelStorei(int pname, int param);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PixelTexGenParameterfSGIS(int pname, float param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void PixelTexGenParameterfvSGIS(int pname, float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PixelTexGenParameteriSGIS(int pname, int param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void PixelTexGenParameterivSGIS(int pname, int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PixelTexGenSGIX(int mode);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PixelTransferf(int pname, float param);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PixelTransferi(int pname, int param);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PixelTransformParameterfEXT(int target, int pname, float param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void PixelTransformParameterfvEXT(int target, int pname, float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PixelTransformParameteriEXT(int target, int pname, int param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void PixelTransformParameterivEXT(int target, int pname, int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PixelZoom(float xfactor, float yfactor);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PNTrianglesfATI(int pname, float param);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PNTrianglesiATI(int pname, int param);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PointParameterf(int pname, float param);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PointParameterfARB(int pname, float param);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PointParameterfEXT(int pname, float param);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PointParameterfSGIS(int pname, float param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void PointParameterfv(int pname, float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void PointParameterfvARB(int pname, float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void PointParameterfvEXT(int pname, float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void PointParameterfvSGIS(int pname, float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PointParameteri(int pname, int param);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PointParameteriNV(int pname, int param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void PointParameteriv(int pname, int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void PointParameterivNV(int pname, int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PointSize(float size);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate int PollAsyncSGIX([Out] uint* markerp);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate int PollInstrumentsSGIX([Out] int* marker_p);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PolygonMode(int face, int mode);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PolygonOffset(float factor, float units);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PolygonOffsetEXT(float factor, float bias);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void PolygonStipple(byte* mask);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PopAttrib();

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PopClientAttrib();

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PopMatrix();

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PopName();

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PrimitiveRestartIndexNV(uint index);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PrimitiveRestartNV();

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void PrioritizeTextures(int n, uint* textures, float* priorities);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void PrioritizeTexturesEXT(int n, uint* textures, float* priorities);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ProgramBufferParametersfvNV(int target, uint buffer, uint index, int count,
		float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ProgramBufferParametersIivNV(int target, uint buffer, uint index, int count,
		int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ProgramBufferParametersIuivNV(int target, uint buffer, uint index, int count,
		uint* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ProgramEnvParameter4dARB(int target, uint index, double x, double y, double z, double w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ProgramEnvParameter4dvARB(int target, uint index, double* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ProgramEnvParameter4fARB(int target, uint index, float x, float y, float z, float w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ProgramEnvParameter4fvARB(int target, uint index, float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ProgramEnvParameterI4iNV(int target, uint index, int x, int y, int z, int w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ProgramEnvParameterI4ivNV(int target, uint index, int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ProgramEnvParameterI4uiNV(int target, uint index, uint x, uint y, uint z, uint w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ProgramEnvParameterI4uivNV(int target, uint index, uint* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ProgramEnvParameters4fvEXT(int target, uint index, int count, float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ProgramEnvParametersI4ivNV(int target, uint index, int count, int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ProgramEnvParametersI4uivNV(int target, uint index, int count, uint* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ProgramLocalParameter4dARB(int target, uint index, double x, double y, double z, double w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ProgramLocalParameter4dvARB(int target, uint index, double* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ProgramLocalParameter4fARB(int target, uint index, float x, float y, float z, float w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ProgramLocalParameter4fvARB(int target, uint index, float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ProgramLocalParameterI4iNV(int target, uint index, int x, int y, int z, int w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ProgramLocalParameterI4ivNV(int target, uint index, int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ProgramLocalParameterI4uiNV(int target, uint index, uint x, uint y, uint z, uint w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ProgramLocalParameterI4uivNV(int target, uint index, uint* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ProgramLocalParameters4fvEXT(int target, uint index, int count, float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ProgramLocalParametersI4ivNV(int target, uint index, int count, int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ProgramLocalParametersI4uivNV(int target, uint index, int count, uint* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ProgramNamedParameter4dNV(uint id, int len, byte* name, double x, double y, double z,
		double w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ProgramNamedParameter4dvNV(uint id, int len, byte* name, double* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ProgramNamedParameter4fNV(uint id, int len, byte* name, float x, float y, float z,
		float w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ProgramNamedParameter4fvNV(uint id, int len, byte* name, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ProgramParameter4dNV(int target, uint index, double x, double y, double z, double w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ProgramParameter4dvNV(int target, uint index, double* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ProgramParameter4fNV(int target, uint index, float x, float y, float z, float w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ProgramParameter4fvNV(int target, uint index, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ProgramParameteriEXT(uint program, int pname, int value);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ProgramParameters4dvNV(int target, uint index, uint count, double* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ProgramParameters4fvNV(int target, uint index, uint count, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ProgramStringARB(int target, int format, int len, IntPtr @string);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ProgramVertexLimitNV(int target, int limit);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PushAttrib(int mask);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PushClientAttrib(int mask);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PushMatrix();

	[SuppressUnmanagedCodeSecurity]
	internal delegate void PushName(uint name);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void RasterPos2d(double x, double y);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void RasterPos2dv(double* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void RasterPos2f(float x, float y);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void RasterPos2fv(float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void RasterPos2i(int x, int y);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void RasterPos2iv(int* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void RasterPos2s(short x, short y);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void RasterPos2sv(short* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void RasterPos3d(double x, double y, double z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void RasterPos3dv(double* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void RasterPos3f(float x, float y, float z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void RasterPos3fv(float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void RasterPos3i(int x, int y, int z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void RasterPos3iv(int* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void RasterPos3s(short x, short y, short z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void RasterPos3sv(short* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void RasterPos4d(double x, double y, double z, double w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void RasterPos4dv(double* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void RasterPos4f(float x, float y, float z, float w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void RasterPos4fv(float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void RasterPos4i(int x, int y, int z, int w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void RasterPos4iv(int* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void RasterPos4s(short x, short y, short z, short w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void RasterPos4sv(short* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ReadBuffer(int mode);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ReadInstrumentsSGIX(int marker);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ReadPixels(int x, int y, int width, int height, int format, int type, [Out] IntPtr pixels);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Rectd(double x1, double y1, double x2, double y2);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Rectdv(double* v1, double* v2);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Rectf(float x1, float y1, float x2, float y2);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Rectfv(float* v1, float* v2);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Recti(int x1, int y1, int x2, int y2);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Rectiv(int* v1, int* v2);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Rects(short x1, short y1, short x2, short y2);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Rectsv(short* v1, short* v2);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ReferencePlaneSGIX(double* equation);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void RenderbufferStorageEXT(int target, int internalformat, int width, int height);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void RenderbufferStorageMultisampleCoverageNV(int target, int coverageSamples, int colorSamples,
		int internalformat, int width, int height);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void RenderbufferStorageMultisampleEXT(int target, int samples, int internalformat, int width,
		int height);

	[SuppressUnmanagedCodeSecurity]
	internal delegate int RenderMode(int mode);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ReplacementCodePointerSUN(int type, int stride, IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ReplacementCodeubSUN(byte code);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ReplacementCodeubvSUN(byte* code);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ReplacementCodeuiColor3fVertex3fSUN(uint rc, float r, float g, float b, float x, float y,
		float z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ReplacementCodeuiColor3fVertex3fvSUN(uint* rc, float* c, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ReplacementCodeuiColor4fNormal3fVertex3fSUN(uint rc, float r, float g, float b, float a,
		float nx, float ny, float nz, float x, float y, float z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ReplacementCodeuiColor4fNormal3fVertex3fvSUN(uint* rc, float* c, float* n, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ReplacementCodeuiColor4ubVertex3fSUN(uint rc, byte r, byte g, byte b, byte a, float x,
		float y, float z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ReplacementCodeuiColor4ubVertex3fvSUN(uint* rc, byte* c, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ReplacementCodeuiNormal3fVertex3fSUN(uint rc, float nx, float ny, float nz, float x, float y,
		float z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ReplacementCodeuiNormal3fVertex3fvSUN(uint* rc, float* n, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ReplacementCodeuiSUN(uint code);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ReplacementCodeuiTexCoord2fColor4fNormal3fVertex3fSUN(uint rc, float s, float t, float r,
		float g, float b, float a, float nx, float ny, float nz, float x, float y, float z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ReplacementCodeuiTexCoord2fColor4fNormal3fVertex3fvSUN(uint* rc, float* tc, float* c,
		float* n, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ReplacementCodeuiTexCoord2fNormal3fVertex3fSUN(uint rc, float s, float t, float nx, float ny,
		float nz, float x, float y, float z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ReplacementCodeuiTexCoord2fNormal3fVertex3fvSUN(uint* rc, float* tc, float* n,
		float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ReplacementCodeuiTexCoord2fVertex3fSUN(uint rc, float s, float t, float x, float y, float z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ReplacementCodeuiTexCoord2fVertex3fvSUN(uint* rc, float* tc, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ReplacementCodeuiVertex3fSUN(uint rc, float x, float y, float z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ReplacementCodeuiVertex3fvSUN(uint* rc, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ReplacementCodeuivSUN(uint* code);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ReplacementCodeusSUN(ushort code);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ReplacementCodeusvSUN(ushort* code);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void RequestResidentProgramsNV(int n, uint* programs);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ResetHistogram(int target);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ResetHistogramEXT(int target);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ResetMinmax(int target);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ResetMinmaxEXT(int target);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ResizeBuffersMESA();

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Rotated(double angle, double x, double y, double z);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Rotatef(float angle, float x, float y, float z);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SampleCoverage(float value, bool invert);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SampleCoverageARB(float value, bool invert);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SampleMapATI(uint dst, uint interp, int swizzle);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SampleMaskEXT(float value, bool invert);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SampleMaskSGIS(float value, bool invert);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SamplePatternEXT(int pattern);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SamplePatternSGIS(int pattern);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Scaled(double x, double y, double z);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Scalef(float x, float y, float z);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Scissor(int x, int y, int width, int height);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SecondaryColor3b(sbyte red, sbyte green, sbyte blue);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SecondaryColor3bEXT(sbyte red, sbyte green, sbyte blue);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void SecondaryColor3bv(sbyte* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void SecondaryColor3bvEXT(sbyte* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SecondaryColor3d(double red, double green, double blue);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SecondaryColor3dEXT(double red, double green, double blue);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void SecondaryColor3dv(double* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void SecondaryColor3dvEXT(double* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SecondaryColor3f(float red, float green, float blue);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SecondaryColor3fEXT(float red, float green, float blue);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void SecondaryColor3fv(float* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void SecondaryColor3fvEXT(float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SecondaryColor3hNV(ushort red, ushort green, ushort blue);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void SecondaryColor3hvNV(ushort* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SecondaryColor3i(int red, int green, int blue);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SecondaryColor3iEXT(int red, int green, int blue);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void SecondaryColor3iv(int* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void SecondaryColor3ivEXT(int* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SecondaryColor3s(short red, short green, short blue);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SecondaryColor3sEXT(short red, short green, short blue);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void SecondaryColor3sv(short* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void SecondaryColor3svEXT(short* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SecondaryColor3ub(byte red, byte green, byte blue);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SecondaryColor3ubEXT(byte red, byte green, byte blue);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void SecondaryColor3ubv(byte* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void SecondaryColor3ubvEXT(byte* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SecondaryColor3ui(uint red, uint green, uint blue);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SecondaryColor3uiEXT(uint red, uint green, uint blue);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void SecondaryColor3uiv(uint* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void SecondaryColor3uivEXT(uint* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SecondaryColor3us(ushort red, ushort green, ushort blue);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SecondaryColor3usEXT(ushort red, ushort green, ushort blue);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void SecondaryColor3usv(ushort* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void SecondaryColor3usvEXT(ushort* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SecondaryColorPointer(int size, int type, int stride, IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SecondaryColorPointerEXT(int size, int type, int stride, IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SecondaryColorPointerListIBM(int size, int type, int stride, IntPtr pointer, int ptrstride);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void SelectBuffer(int size, [Out] uint* buffer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SeparableFilter2D(int target, int internalformat, int width, int height, int format,
		int type, IntPtr row, IntPtr column);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SeparableFilter2DEXT(int target, int internalformat, int width, int height, int format,
		int type, IntPtr row, IntPtr column);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SetFenceAPPLE(uint fence);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SetFenceNV(uint fence, int condition);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void SetFragmentShaderConstantATI(uint dst, float* value);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SetInvariantEXT(uint id, int type, IntPtr addr);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SetLocalConstantEXT(uint id, int type, IntPtr addr);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ShadeModel(int mode);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ShaderOp1EXT(int op, uint res, uint arg1);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ShaderOp2EXT(int op, uint res, uint arg1, uint arg2);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ShaderOp3EXT(int op, uint res, uint arg1, uint arg2, uint arg3);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ShaderSource(uint shader, int count, string[] @string, int* length);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void ShaderSourceARB(uint shaderObj, int count, string[] @string, int* length);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void SharpenTexFuncSGIS(int target, int n, float* points);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SpriteParameterfSGIX(int pname, float param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void SpriteParameterfvSGIX(int pname, float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SpriteParameteriSGIX(int pname, int param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void SpriteParameterivSGIX(int pname, int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void StartInstrumentsSGIX();

	[SuppressUnmanagedCodeSecurity]
	internal delegate void StencilClearTagEXT(int stencilTagBits, uint stencilClearTag);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void StencilFunc(int func, int @ref, uint mask);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void StencilFuncSeparate(int frontfunc, int backfunc, int @ref, uint mask);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void StencilFuncSeparateATI(int frontfunc, int backfunc, int @ref, uint mask);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void StencilMask(uint mask);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void StencilMaskSeparate(int face, uint mask);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void StencilOp(int fail, int zfail, int zpass);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void StencilOpSeparate(int face, int sfail, int dpfail, int dppass);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void StencilOpSeparateATI(int face, int sfail, int dpfail, int dppass);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void StopInstrumentsSGIX(int marker);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void StringMarkerGREMEDY(int len, IntPtr @string);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void SwizzleEXT(uint res, uint @in, int outX, int outY, int outZ, int outW);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TagSampleBufferSGIX();

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Tangent3bEXT(sbyte tx, sbyte ty, sbyte tz);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Tangent3bvEXT(sbyte* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Tangent3dEXT(double tx, double ty, double tz);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Tangent3dvEXT(double* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Tangent3fEXT(float tx, float ty, float tz);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Tangent3fvEXT(float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Tangent3iEXT(int tx, int ty, int tz);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Tangent3ivEXT(int* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Tangent3sEXT(short tx, short ty, short tz);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Tangent3svEXT(short* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TangentPointerEXT(int type, int stride, IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TbufferMask3DFX(uint mask);

	[SuppressUnmanagedCodeSecurity]
	internal delegate bool TestFenceAPPLE(uint fence);

	[SuppressUnmanagedCodeSecurity]
	internal delegate bool TestFenceNV(uint fence);

	[SuppressUnmanagedCodeSecurity]
	internal delegate bool TestObjectAPPLE(int @object, uint name);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexBufferEXT(int target, int internalformat, uint buffer);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexBumpParameterfvATI(int pname, float* param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexBumpParameterivATI(int pname, int* param);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexCoord1d(double s);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexCoord1dv(double* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexCoord1f(float s);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexCoord1fv(float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexCoord1hNV(ushort s);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexCoord1hvNV(ushort* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexCoord1i(int s);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexCoord1iv(int* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexCoord1s(short s);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexCoord1sv(short* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexCoord2d(double s, double t);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexCoord2dv(double* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexCoord2f(float s, float t);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexCoord2fColor3fVertex3fSUN(float s, float t, float r, float g, float b, float x, float y,
		float z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexCoord2fColor3fVertex3fvSUN(float* tc, float* c, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexCoord2fColor4fNormal3fVertex3fSUN(float s, float t, float r, float g, float b, float a,
		float nx, float ny, float nz, float x, float y, float z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexCoord2fColor4fNormal3fVertex3fvSUN(float* tc, float* c, float* n, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexCoord2fColor4ubVertex3fSUN(float s, float t, byte r, byte g, byte b, byte a, float x,
		float y, float z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexCoord2fColor4ubVertex3fvSUN(float* tc, byte* c, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexCoord2fNormal3fVertex3fSUN(float s, float t, float nx, float ny, float nz, float x,
		float y, float z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexCoord2fNormal3fVertex3fvSUN(float* tc, float* n, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexCoord2fv(float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexCoord2fVertex3fSUN(float s, float t, float x, float y, float z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexCoord2fVertex3fvSUN(float* tc, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexCoord2hNV(ushort s, ushort t);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexCoord2hvNV(ushort* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexCoord2i(int s, int t);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexCoord2iv(int* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexCoord2s(short s, short t);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexCoord2sv(short* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexCoord3d(double s, double t, double r);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexCoord3dv(double* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexCoord3f(float s, float t, float r);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexCoord3fv(float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexCoord3hNV(ushort s, ushort t, ushort r);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexCoord3hvNV(ushort* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexCoord3i(int s, int t, int r);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexCoord3iv(int* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexCoord3s(short s, short t, short r);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexCoord3sv(short* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexCoord4d(double s, double t, double r, double q);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexCoord4dv(double* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexCoord4f(float s, float t, float r, float q);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexCoord4fColor4fNormal3fVertex4fSUN(float s, float t, float p, float q, float r, float g,
		float b, float a, float nx, float ny, float nz, float x, float y, float z, float w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexCoord4fColor4fNormal3fVertex4fvSUN(float* tc, float* c, float* n, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexCoord4fv(float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void
		TexCoord4fVertex4fSUN(float s, float t, float p, float q, float x, float y, float z, float w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexCoord4fVertex4fvSUN(float* tc, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexCoord4hNV(ushort s, ushort t, ushort r, ushort q);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexCoord4hvNV(ushort* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexCoord4i(int s, int t, int r, int q);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexCoord4iv(int* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexCoord4s(short s, short t, short r, short q);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexCoord4sv(short* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexCoordPointer(int size, int type, int stride, IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexCoordPointerEXT(int size, int type, int stride, int count, IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexCoordPointerListIBM(int size, int type, int stride, IntPtr pointer, int ptrstride);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexCoordPointervINTEL(int size, int type, IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexEnvf(int target, int pname, float param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexEnvfv(int target, int pname, float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexEnvi(int target, int pname, int param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexEnviv(int target, int pname, int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexFilterFuncSGIS(int target, int filter, int n, float* weights);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexGend(int coord, int pname, double param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexGendv(int coord, int pname, double* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexGenf(int coord, int pname, float param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexGenfv(int coord, int pname, float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexGeni(int coord, int pname, int param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexGeniv(int coord, int pname, int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexImage1D(int target, int level, int internalformat, int width, int border, int format,
		int type, IntPtr pixels);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexImage2D(int target, int level, int internalformat, int width, int height, int border,
		int format, int type, IntPtr pixels);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexImage3D(int target, int level, int internalformat, int width, int height, int depth,
		int border, int format, int type, IntPtr pixels);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexImage3DEXT(int target, int level, int internalformat, int width, int height, int depth,
		int border, int format, int type, IntPtr pixels);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexImage4DSGIS(int target, int level, int internalformat, int width, int height, int depth,
		int size4d, int border, int format, int type, IntPtr pixels);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexParameterf(int target, int pname, float param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexParameterfv(int target, int pname, float* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexParameteri(int target, int pname, int param);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexParameterIivEXT(int target, int pname, int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexParameterIuivEXT(int target, int pname, uint* @params);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TexParameteriv(int target, int pname, int* @params);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexSubImage1D(int target, int level, int xoffset, int width, int format, int type,
		IntPtr pixels);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexSubImage1DEXT(int target, int level, int xoffset, int width, int format, int type,
		IntPtr pixels);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexSubImage2D(int target, int level, int xoffset, int yoffset, int width, int height,
		int format, int type, IntPtr pixels);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexSubImage2DEXT(int target, int level, int xoffset, int yoffset, int width, int height,
		int format, int type, IntPtr pixels);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexSubImage3D(int target, int level, int xoffset, int yoffset, int zoffset, int width,
		int height, int depth, int format, int type, IntPtr pixels);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexSubImage3DEXT(int target, int level, int xoffset, int yoffset, int zoffset, int width,
		int height, int depth, int format, int type, IntPtr pixels);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TexSubImage4DSGIS(int target, int level, int xoffset, int yoffset, int zoffset, int woffset,
		int width, int height, int depth, int size4d, int format, int type, IntPtr pixels);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TextureColorMaskSGIS(bool red, bool green, bool blue, bool alpha);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TextureLightEXT(int pname);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TextureMaterialEXT(int face, int mode);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TextureNormalEXT(int mode);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void TrackMatrixNV(int target, uint address, int matrix, int transform);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TransformFeedbackAttribsNV(uint count, int* attribs, int bufferMode);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void TransformFeedbackVaryingsNV(uint program, int count, int* locations, int bufferMode);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Translated(double x, double y, double z);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Translatef(float x, float y, float z);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Uniform1f(int location, float v0);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Uniform1fARB(int location, float v0);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Uniform1fv(int location, int count, float* value);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Uniform1fvARB(int location, int count, float* value);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Uniform1i(int location, int v0);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Uniform1iARB(int location, int v0);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Uniform1iv(int location, int count, int* value);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Uniform1ivARB(int location, int count, int* value);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Uniform1uiEXT(int location, uint v0);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Uniform1uivEXT(int location, int count, uint* value);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Uniform2f(int location, float v0, float v1);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Uniform2fARB(int location, float v0, float v1);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Uniform2fv(int location, int count, float* value);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Uniform2fvARB(int location, int count, float* value);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Uniform2i(int location, int v0, int v1);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Uniform2iARB(int location, int v0, int v1);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Uniform2iv(int location, int count, int* value);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Uniform2ivARB(int location, int count, int* value);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Uniform2uiEXT(int location, uint v0, uint v1);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Uniform2uivEXT(int location, int count, uint* value);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Uniform3f(int location, float v0, float v1, float v2);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Uniform3fARB(int location, float v0, float v1, float v2);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Uniform3fv(int location, int count, float* value);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Uniform3fvARB(int location, int count, float* value);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Uniform3i(int location, int v0, int v1, int v2);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Uniform3iARB(int location, int v0, int v1, int v2);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Uniform3iv(int location, int count, int* value);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Uniform3ivARB(int location, int count, int* value);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Uniform3uiEXT(int location, uint v0, uint v1, uint v2);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Uniform3uivEXT(int location, int count, uint* value);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Uniform4f(int location, float v0, float v1, float v2, float v3);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Uniform4fARB(int location, float v0, float v1, float v2, float v3);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Uniform4fv(int location, int count, float* value);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Uniform4fvARB(int location, int count, float* value);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Uniform4i(int location, int v0, int v1, int v2, int v3);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Uniform4iARB(int location, int v0, int v1, int v2, int v3);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Uniform4iv(int location, int count, int* value);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Uniform4ivARB(int location, int count, int* value);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Uniform4uiEXT(int location, uint v0, uint v1, uint v2, uint v3);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Uniform4uivEXT(int location, int count, uint* value);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void UniformBufferEXT(uint program, int location, uint buffer);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void UniformMatrix2fv(int location, int count, bool transpose, float* value);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void UniformMatrix2fvARB(int location, int count, bool transpose, float* value);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void UniformMatrix2x3fv(int location, int count, bool transpose, float* value);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void UniformMatrix2x4fv(int location, int count, bool transpose, float* value);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void UniformMatrix3fv(int location, int count, bool transpose, float* value);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void UniformMatrix3fvARB(int location, int count, bool transpose, float* value);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void UniformMatrix3x2fv(int location, int count, bool transpose, float* value);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void UniformMatrix3x4fv(int location, int count, bool transpose, float* value);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void UniformMatrix4fv(int location, int count, bool transpose, float* value);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void UniformMatrix4fvARB(int location, int count, bool transpose, float* value);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void UniformMatrix4x2fv(int location, int count, bool transpose, float* value);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void UniformMatrix4x3fv(int location, int count, bool transpose, float* value);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void UnlockArraysEXT();

	[SuppressUnmanagedCodeSecurity]
	internal delegate bool UnmapBuffer(int target);

	[SuppressUnmanagedCodeSecurity]
	internal delegate bool UnmapBufferARB(int target);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void UnmapObjectBufferATI(uint buffer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void UpdateObjectBufferATI(uint buffer, uint offset, int size, IntPtr pointer, int preserve);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void UseProgram(uint program);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void UseProgramObjectARB(uint programObj);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ValidateProgram(uint program);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void ValidateProgramARB(uint programObj);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VariantArrayObjectATI(uint id, int type, int stride, uint buffer, uint offset);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VariantbvEXT(uint id, sbyte* addr);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VariantdvEXT(uint id, double* addr);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VariantfvEXT(uint id, float* addr);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VariantivEXT(uint id, int* addr);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VariantPointerEXT(uint id, int type, uint stride, IntPtr addr);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VariantsvEXT(uint id, short* addr);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VariantubvEXT(uint id, byte* addr);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VariantuivEXT(uint id, uint* addr);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VariantusvEXT(uint id, ushort* addr);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Vertex2d(double x, double y);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Vertex2dv(double* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Vertex2f(float x, float y);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Vertex2fv(float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Vertex2hNV(ushort x, ushort y);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Vertex2hvNV(ushort* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Vertex2i(int x, int y);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Vertex2iv(int* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Vertex2s(short x, short y);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Vertex2sv(short* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Vertex3d(double x, double y, double z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Vertex3dv(double* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Vertex3f(float x, float y, float z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Vertex3fv(float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Vertex3hNV(ushort x, ushort y, ushort z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Vertex3hvNV(ushort* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Vertex3i(int x, int y, int z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Vertex3iv(int* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Vertex3s(short x, short y, short z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Vertex3sv(short* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Vertex4d(double x, double y, double z, double w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Vertex4dv(double* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Vertex4f(float x, float y, float z, float w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Vertex4fv(float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Vertex4hNV(ushort x, ushort y, ushort z, ushort w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Vertex4hvNV(ushort* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Vertex4i(int x, int y, int z, int w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Vertex4iv(int* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Vertex4s(short x, short y, short z, short w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void Vertex4sv(short* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexArrayParameteriAPPLE(int pname, int param);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexArrayRangeAPPLE(int length, [Out] IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexArrayRangeNV(int length, IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib1d(uint index, double x);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib1dARB(uint index, double x);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib1dNV(uint index, double x);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib1dv(uint index, double* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib1dvARB(uint index, double* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib1dvNV(uint index, double* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib1f(uint index, float x);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib1fARB(uint index, float x);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib1fNV(uint index, float x);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib1fv(uint index, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib1fvARB(uint index, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib1fvNV(uint index, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib1hNV(uint index, ushort x);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib1hvNV(uint index, ushort* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib1s(uint index, short x);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib1sARB(uint index, short x);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib1sNV(uint index, short x);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib1sv(uint index, short* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib1svARB(uint index, short* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib1svNV(uint index, short* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib2d(uint index, double x, double y);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib2dARB(uint index, double x, double y);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib2dNV(uint index, double x, double y);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib2dv(uint index, double* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib2dvARB(uint index, double* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib2dvNV(uint index, double* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib2f(uint index, float x, float y);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib2fARB(uint index, float x, float y);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib2fNV(uint index, float x, float y);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib2fv(uint index, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib2fvARB(uint index, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib2fvNV(uint index, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib2hNV(uint index, ushort x, ushort y);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib2hvNV(uint index, ushort* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib2s(uint index, short x, short y);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib2sARB(uint index, short x, short y);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib2sNV(uint index, short x, short y);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib2sv(uint index, short* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib2svARB(uint index, short* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib2svNV(uint index, short* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib3d(uint index, double x, double y, double z);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib3dARB(uint index, double x, double y, double z);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib3dNV(uint index, double x, double y, double z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib3dv(uint index, double* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib3dvARB(uint index, double* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib3dvNV(uint index, double* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib3f(uint index, float x, float y, float z);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib3fARB(uint index, float x, float y, float z);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib3fNV(uint index, float x, float y, float z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib3fv(uint index, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib3fvARB(uint index, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib3fvNV(uint index, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib3hNV(uint index, ushort x, ushort y, ushort z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib3hvNV(uint index, ushort* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib3s(uint index, short x, short y, short z);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib3sARB(uint index, short x, short y, short z);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib3sNV(uint index, short x, short y, short z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib3sv(uint index, short* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib3svARB(uint index, short* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib3svNV(uint index, short* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib4bv(uint index, sbyte* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib4bvARB(uint index, sbyte* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib4d(uint index, double x, double y, double z, double w);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib4dARB(uint index, double x, double y, double z, double w);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib4dNV(uint index, double x, double y, double z, double w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib4dv(uint index, double* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib4dvARB(uint index, double* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib4dvNV(uint index, double* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib4f(uint index, float x, float y, float z, float w);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib4fARB(uint index, float x, float y, float z, float w);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib4fNV(uint index, float x, float y, float z, float w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib4fv(uint index, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib4fvARB(uint index, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib4fvNV(uint index, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib4hNV(uint index, ushort x, ushort y, ushort z, ushort w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib4hvNV(uint index, ushort* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib4iv(uint index, int* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib4ivARB(uint index, int* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib4Nbv(uint index, sbyte* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib4NbvARB(uint index, sbyte* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib4Niv(uint index, int* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib4NivARB(uint index, int* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib4Nsv(uint index, short* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib4NsvARB(uint index, short* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib4Nub(uint index, byte x, byte y, byte z, byte w);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib4NubARB(uint index, byte x, byte y, byte z, byte w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib4Nubv(uint index, byte* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib4NubvARB(uint index, byte* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib4Nuiv(uint index, uint* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib4NuivARB(uint index, uint* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib4Nusv(uint index, ushort* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib4NusvARB(uint index, ushort* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib4s(uint index, short x, short y, short z, short w);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib4sARB(uint index, short x, short y, short z, short w);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib4sNV(uint index, short x, short y, short z, short w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib4sv(uint index, short* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib4svARB(uint index, short* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib4svNV(uint index, short* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttrib4ubNV(uint index, byte x, byte y, byte z, byte w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib4ubv(uint index, byte* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib4ubvARB(uint index, byte* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib4ubvNV(uint index, byte* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib4uiv(uint index, uint* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib4uivARB(uint index, uint* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib4usv(uint index, ushort* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttrib4usvARB(uint index, ushort* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttribArrayObjectATI(uint index, int size, int type, bool normalized, int stride,
		uint buffer, uint offset);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttribI1iEXT(uint index, int x);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttribI1ivEXT(uint index, int* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttribI1uiEXT(uint index, uint x);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttribI1uivEXT(uint index, uint* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttribI2iEXT(uint index, int x, int y);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttribI2ivEXT(uint index, int* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttribI2uiEXT(uint index, uint x, uint y);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttribI2uivEXT(uint index, uint* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttribI3iEXT(uint index, int x, int y, int z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttribI3ivEXT(uint index, int* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttribI3uiEXT(uint index, uint x, uint y, uint z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttribI3uivEXT(uint index, uint* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttribI4bvEXT(uint index, sbyte* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttribI4iEXT(uint index, int x, int y, int z, int w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttribI4ivEXT(uint index, int* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttribI4svEXT(uint index, short* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttribI4ubvEXT(uint index, byte* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttribI4uiEXT(uint index, uint x, uint y, uint z, uint w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttribI4uivEXT(uint index, uint* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttribI4usvEXT(uint index, ushort* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttribIPointerEXT(uint index, int size, int type, int stride, IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttribPointer(uint index, int size, int type, bool normalized, int stride,
		IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttribPointerARB(uint index, int size, int type, bool normalized, int stride,
		IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexAttribPointerNV(uint index, int fsize, int type, int stride, IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttribs1dvNV(uint index, int count, double* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttribs1fvNV(uint index, int count, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttribs1hvNV(uint index, int n, ushort* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttribs1svNV(uint index, int count, short* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttribs2dvNV(uint index, int count, double* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttribs2fvNV(uint index, int count, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttribs2hvNV(uint index, int n, ushort* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttribs2svNV(uint index, int count, short* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttribs3dvNV(uint index, int count, double* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttribs3fvNV(uint index, int count, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttribs3hvNV(uint index, int n, ushort* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttribs3svNV(uint index, int count, short* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttribs4dvNV(uint index, int count, double* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttribs4fvNV(uint index, int count, float* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttribs4hvNV(uint index, int n, ushort* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttribs4svNV(uint index, int count, short* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexAttribs4ubvNV(uint index, int count, byte* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexBlendARB(int count);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexBlendEnvfATI(int pname, float param);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexBlendEnviATI(int pname, int param);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexPointer(int size, int type, int stride, IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexPointerEXT(int size, int type, int stride, int count, IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexPointerListIBM(int size, int type, int stride, IntPtr pointer, int ptrstride);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexPointervINTEL(int size, int type, IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexStream1dATI(int stream, double x);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexStream1dvATI(int stream, double* coords);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexStream1fATI(int stream, float x);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexStream1fvATI(int stream, float* coords);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexStream1iATI(int stream, int x);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexStream1ivATI(int stream, int* coords);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexStream1sATI(int stream, short x);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexStream1svATI(int stream, short* coords);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexStream2dATI(int stream, double x, double y);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexStream2dvATI(int stream, double* coords);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexStream2fATI(int stream, float x, float y);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexStream2fvATI(int stream, float* coords);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexStream2iATI(int stream, int x, int y);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexStream2ivATI(int stream, int* coords);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexStream2sATI(int stream, short x, short y);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexStream2svATI(int stream, short* coords);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexStream3dATI(int stream, double x, double y, double z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexStream3dvATI(int stream, double* coords);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexStream3fATI(int stream, float x, float y, float z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexStream3fvATI(int stream, float* coords);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexStream3iATI(int stream, int x, int y, int z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexStream3ivATI(int stream, int* coords);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexStream3sATI(int stream, short x, short y, short z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexStream3svATI(int stream, short* coords);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexStream4dATI(int stream, double x, double y, double z, double w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexStream4dvATI(int stream, double* coords);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexStream4fATI(int stream, float x, float y, float z, float w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexStream4fvATI(int stream, float* coords);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexStream4iATI(int stream, int x, int y, int z, int w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexStream4ivATI(int stream, int* coords);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexStream4sATI(int stream, short x, short y, short z, short w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexStream4svATI(int stream, short* coords);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexWeightfEXT(float weight);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexWeightfvEXT(float* weight);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexWeighthNV(ushort weight);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void VertexWeighthvNV(ushort* weight);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void VertexWeightPointerEXT(int size, int type, int stride, IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void Viewport(int x, int y, int width, int height);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WeightbvARB(int size, sbyte* weights);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WeightdvARB(int size, double* weights);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WeightfvARB(int size, float* weights);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WeightivARB(int size, int* weights);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void WeightPointerARB(int size, int type, int stride, IntPtr pointer);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WeightsvARB(int size, short* weights);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WeightubvARB(int size, byte* weights);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WeightuivARB(int size, uint* weights);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WeightusvARB(int size, ushort* weights);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void WindowPos2d(double x, double y);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void WindowPos2dARB(double x, double y);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void WindowPos2dMESA(double x, double y);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WindowPos2dv(double* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WindowPos2dvARB(double* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WindowPos2dvMESA(double* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void WindowPos2f(float x, float y);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void WindowPos2fARB(float x, float y);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void WindowPos2fMESA(float x, float y);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WindowPos2fv(float* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WindowPos2fvARB(float* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WindowPos2fvMESA(float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void WindowPos2i(int x, int y);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void WindowPos2iARB(int x, int y);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void WindowPos2iMESA(int x, int y);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WindowPos2iv(int* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WindowPos2ivARB(int* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WindowPos2ivMESA(int* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void WindowPos2s(short x, short y);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void WindowPos2sARB(short x, short y);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void WindowPos2sMESA(short x, short y);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WindowPos2sv(short* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WindowPos2svARB(short* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WindowPos2svMESA(short* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void WindowPos3d(double x, double y, double z);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void WindowPos3dARB(double x, double y, double z);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void WindowPos3dMESA(double x, double y, double z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WindowPos3dv(double* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WindowPos3dvARB(double* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WindowPos3dvMESA(double* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void WindowPos3f(float x, float y, float z);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void WindowPos3fARB(float x, float y, float z);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void WindowPos3fMESA(float x, float y, float z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WindowPos3fv(float* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WindowPos3fvARB(float* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WindowPos3fvMESA(float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void WindowPos3i(int x, int y, int z);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void WindowPos3iARB(int x, int y, int z);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void WindowPos3iMESA(int x, int y, int z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WindowPos3iv(int* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WindowPos3ivARB(int* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WindowPos3ivMESA(int* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void WindowPos3s(short x, short y, short z);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void WindowPos3sARB(short x, short y, short z);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void WindowPos3sMESA(short x, short y, short z);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WindowPos3sv(short* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WindowPos3svARB(short* v);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WindowPos3svMESA(short* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void WindowPos4dMESA(double x, double y, double z, double w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WindowPos4dvMESA(double* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void WindowPos4fMESA(float x, float y, float z, float w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WindowPos4fvMESA(float* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void WindowPos4iMESA(int x, int y, int z, int w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WindowPos4ivMESA(int* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void WindowPos4sMESA(short x, short y, short z, short w);

	[SuppressUnmanagedCodeSecurity]
	internal unsafe delegate void WindowPos4svMESA(short* v);

	[SuppressUnmanagedCodeSecurity]
	internal delegate void WriteMaskEXT(uint res, uint @in, int outX, int outY, int outZ, int outW);
}