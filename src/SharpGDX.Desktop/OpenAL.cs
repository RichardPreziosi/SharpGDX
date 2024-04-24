using SharpGDX.Shims;
using System;
using System.Runtime.InteropServices;
using static SharpGDX.Pixmap;

namespace SharpGDX.Desktop
{
	internal static class OpenAL
	{
		private const string Library = "openal32";

		public static readonly int
			AL_CONE_INNER_ANGLE = 0x1001,
			AL_CONE_OUTER_ANGLE = 0x1002,
			AL_PITCH = 0x1003,
			AL_DIRECTION = 0x1005,
			AL_LOOPING = 0x1007,
			AL_BUFFER = 0x1009,
			AL_SOURCE_STATE = 0x1010,
			AL_CONE_OUTER_GAIN = 0x1022,
			AL_SOURCE_TYPE = 0x1027;

		/** General tokens. */
		public static readonly int
			AL_INVALID = unchecked((int)0xFFFFFFFF),
			AL_NONE = 0x0,
			AL_FALSE = 0x0,
			AL_TRUE = 0x1;

		public static readonly int
			AL_POSITION = 0x1004,
			AL_VELOCITY = 0x1006,
			AL_GAIN = 0x100A;

		public static int alGenBuffers(int n, IntBuffer bufferNames)
		{
			var bufferNamesHandle = GCHandle.Alloc(bufferNames, GCHandleType.Pinned);
			var result = alGenBuffers(n, bufferNamesHandle.AddrOfPinnedObject());

			bufferNamesHandle.Free();

			return result;
		}

		[DllImport(Library)]
		public static extern void alSourceUnqueueBuffers(int sourceName, int numEntries, long bufferNames);

		[DllImport(Library)]
		public static extern void alSourceQueueBuffers(int id, int buffer);

		public static void alSourceUnqueueBuffers(int sourceName, IntBuffer bufferNames)
		{
			var bufferNamesHandle = GCHandle.Alloc(bufferNames, GCHandleType.Pinned);
			alSourceUnqueueBuffers(sourceName, bufferNames.remaining(), bufferNamesHandle.AddrOfPinnedObject());
			bufferNamesHandle.Free();
		}

		public static int alSourceUnqueueBuffers(int sourceName)
		{
			IntBuffer bufferNames = BufferUtils.newIntBuffer(1);
			var bufferNamesHandle = GCHandle.Alloc(bufferNames, GCHandleType.Pinned);

			alSourceUnqueueBuffers(sourceName, 1, bufferNamesHandle.AddrOfPinnedObject());

			bufferNamesHandle.Free();

			return bufferNames.get(0);
		}

		[DllImport(Library)]
		public static extern int alGenBuffers(int n, long bufferNames);

		public static int alGenBuffers(IntBuffer bufferNames)
		{
			var bufferNamesHandle = GCHandle.Alloc(bufferNames, GCHandleType.Pinned);
			var result = alGenBuffers(bufferNames.remaining(), bufferNamesHandle.AddrOfPinnedObject());

			bufferNamesHandle.Free();

			return result;
		}

		[DllImport(Library)]
		public static extern void alSourcef(int id, int param, float value);

		[DllImport(Library)]
		public static extern float alGetSourcef(int id, int param); 

		[DllImport(Library)]
		public static extern void alSourcei(int id, int param, int value);

		[DllImport(Library)]
		public static extern int alGetError(); 

		public static readonly int
			AL_INITIAL = 0x1011,
			AL_PLAYING = 0x1012,
			AL_PAUSED = 0x1013,
			AL_STOPPED = 0x1014;

		[DllImport(Library)]
		public static extern void alDisable(int capability);

		[DllImport(Library)]
		public static extern void alDeleteSources(int id);

		public static int alGenSources(int n, IntBuffer sourceNames)
		{
			var bufferNamesHandle = GCHandle.Alloc(sourceNames.array(), GCHandleType.Pinned);
			var result = alGenSources(n, bufferNamesHandle.AddrOfPinnedObject());

			bufferNamesHandle.Free();

			return result;

			[DllImport(Library)]
			static extern int alGenSources(int n, long bufferNames);
		}

		public static int alGenSources()
		{
			IntBuffer umm = BufferUtils.newIntBuffer(1);

			return alGenSources(1, umm);
		}

		public static void alBufferData(int id, int format, ShortBuffer buffer, int sampleRate)
		{
			var bufferHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);

			alBufferData(id, format, bufferHandle.AddrOfPinnedObject(), sampleRate);

			bufferHandle.Free();

			[DllImport(Library)]
			static extern void alBufferData(int id, int format, long buffer, int sampleRate);
		}

		public static void alBufferData(int id, int format, ByteBuffer buffer, int sampleRate)
		{
			var bufferHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);

			alBufferData(id, format, bufferHandle.AddrOfPinnedObject(), sampleRate);

			bufferHandle.Free();

			[DllImport(Library)]
			static extern void alBufferData(int id, int format, long buffer, int sampleRate);
		}

		[DllImport(Library)]
		public static extern void alDeleteBuffers(int n, long bufferNames);

		public static void alDeleteBuffers(IntBuffer buffer)
		{
			var bufferHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);

			alDeleteBuffers(buffer.remaining(), bufferHandle.AddrOfPinnedObject());

			bufferHandle.Free();
		}

		public static readonly int
			AL_BUFFERS_QUEUED = 0x1015,
			AL_BUFFERS_PROCESSED = 0x1016;

		public static void alDeleteBuffers(int bufferName)
		{
			IntBuffer buffer = IntBuffer.allocate(1);
			buffer.put(bufferName);
			var bufferHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);

			alDeleteBuffers(1, bufferHandle.AddrOfPinnedObject());

			bufferHandle.Free();
		}

		[DllImport(Library)]
		public static extern void alSourceStop(int id);

		[DllImport(Library)]
		public static extern void alSourcePause(int id);

		[DllImport(Library)]
		public static extern void alSourcePlay(int id); 

		[DllImport(Library)]
		public static extern int alGetSourcei(int id, int param); 

		[DllImport(Library)]
		public static extern void alSource3f(int id, int param, float value1, float value2, float value3);

		public static void alListenerfv(int param, FloatBuffer values)
		{
			var bufferHandle = GCHandle.Alloc(values.array(), GCHandleType.Pinned);

			alListenerfv(param, bufferHandle.AddrOfPinnedObject());

			bufferHandle.Free();

			[DllImport(Library)]
			static extern void alListenerfv(int param, long values);
		}

		public static readonly int
			AL_NO_ERROR = 0x0,
			AL_INVALID_NAME = 0xA001,
			AL_INVALID_ENUM = 0xA002,
			AL_INVALID_VALUE = 0xA003,
			AL_INVALID_OPERATION = 0xA004,
			AL_OUT_OF_MEMORY = 0xA005;

		public static readonly int AL_ORIENTATION = 0x100F;

		public static int alGenBuffers()
		{
			IntBuffer umm = BufferUtils.newIntBuffer(1);

			return alGenBuffers(1, umm);
		}

		public static readonly int
			AL_FORMAT_MONO8 = 0x1100,
			AL_FORMAT_MONO16 = 0x1101,
			AL_FORMAT_STEREO8 = 0x1102,
			AL_FORMAT_STEREO16 = 0x1103;
	}
}
