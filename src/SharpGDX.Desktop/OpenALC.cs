using Buffer = SharpGDX.Shims.Buffer;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SharpGDX.Shims;

namespace SharpGDX.Desktop
{
	internal static class OpenALC
	{
		private const string Library = "openal32";

		public static readonly int ALC_CONNECTED = 0x313;

		public static readonly int
			ALC_DEFAULT_ALL_DEVICES_SPECIFIER = 0x1012,
			ALC_ALL_DEVICES_SPECIFIER = 0x1013,
			ALC_CAPTURE_DEVICE_SPECIFIER = 0x310,
			ALC_CAPTURE_DEFAULT_DEVICE_SPECIFIER = 0x311;

		public static int alcGetInteger(long device, int token)
		{
			var buffer = IntBuffer.allocate(1);
			var bufferNamesHandle = GCHandle.Alloc(device, GCHandleType.Pinned);
			var bufferHandle = GCHandle.Alloc(buffer.array(), GCHandleType.Pinned);
			var result = alcGetIntegerv(bufferNamesHandle.AddrOfPinnedObject(), token, 1, bufferHandle.AddrOfPinnedObject());

			bufferNamesHandle.Free();
			bufferHandle.Free();

			return buffer.get(0);

			[DllImport(Library)]
			static extern int alcGetIntegerv(long device, int param, int size, long dest);
		}

		[DllImport(Library)]
		public static extern bool alcMakeContextCurrent(long context);

		[DllImport(Library)]
		public static extern void alcCloseDevice(long device);

		[DllImport(Library)]
		public static extern void alcDestroyContext(long context); 

		public static long alcOpenDevice(ByteBuffer? deviceSpecifier)
		{
			var bufferNamesHandle = GCHandle.Alloc(deviceSpecifier, GCHandleType.Pinned);
			var result = alcOpenDevice(bufferNamesHandle.AddrOfPinnedObject());

			bufferNamesHandle.Free();

			return result;

			[DllImport(Library)]
			static extern long alcOpenDevice(long deviceSpecifier);
		}

		public static string? alcGetString(long device, int param)
		{
			return Marshal.PtrToStringUTF8(alcGetString(device, param));

			[DllImport(Library)]
			static extern IntPtr alcGetString(long device, int param);
		}

		
		public static long alcCreateContext(long device, IntBuffer? buffer)
		{
			var bufferHandle = GCHandle.Alloc(buffer?.array(), GCHandleType.Pinned);
			var result = alcCreateContext(device, bufferHandle.AddrOfPinnedObject());

			bufferHandle.Free();

			return result;

			[DllImport(Library)]
			static extern long alcCreateContext(long device, long buffer);
		}

		public static List<string> getStringList(long device, int param)
		{
			// TODO: 
			//long __result = alcGetString(device, param);
			//if (__result == null)
			//{
			//	return null;
			//}

			//ByteBuffer buffer = bytebuff(__result, Integer.MAX_VALUE);

			//[DllImport(Library)]
			//static extern long alcGetString(long device, int param);

			return new List<string>();
		}
	}
}
