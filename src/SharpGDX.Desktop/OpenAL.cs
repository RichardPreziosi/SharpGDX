using System.Runtime.InteropServices;
using SharpGDX.Shims;

namespace SharpGDX.Desktop;

internal static class OpenAL
{
	private const string Library = "openal32";

	public static readonly int
		AL_BUFFERS_QUEUED = 0x1015,
		AL_BUFFERS_PROCESSED = 0x1016;

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

	public static readonly int
		AL_FORMAT_MONO8 = 0x1100,
		AL_FORMAT_MONO16 = 0x1101,
		AL_FORMAT_STEREO8 = 0x1102,
		AL_FORMAT_STEREO16 = 0x1103;

	public static readonly int
		AL_INITIAL = 0x1011,
		AL_PLAYING = 0x1012,
		AL_PAUSED = 0x1013,
		AL_STOPPED = 0x1014;

	/**
	 * General tokens.
	 */
	public static readonly int
		AL_INVALID = unchecked((int)0xFFFFFFFF),
		AL_NONE = 0x0,
		AL_FALSE = 0x0,
		AL_TRUE = 0x1;

	public static readonly int
		AL_NO_ERROR = 0x0,
		AL_INVALID_NAME = 0xA001,
		AL_INVALID_ENUM = 0xA002,
		AL_INVALID_VALUE = 0xA003,
		AL_INVALID_OPERATION = 0xA004,
		AL_OUT_OF_MEMORY = 0xA005;

	public static readonly int AL_ORIENTATION = 0x100F;

	public static readonly int
		AL_POSITION = 0x1004,
		AL_VELOCITY = 0x1006,
		AL_GAIN = 0x100A;

	public static void alBufferData(int bufferName, int format, ShortBuffer data, int frequency)
	{
		var bufferHandle = GCHandle.Alloc(data.array(), GCHandleType.Pinned);

		alBufferData(bufferName, format, bufferHandle.AddrOfPinnedObject(), data.remaining() << 1, frequency);

		bufferHandle.Free();
	}

	[DllImport(Library)]
	static extern void alBufferData(int bufferName, int format, long data, int size, int frequency);

	public static void alBufferData(int bufferName, int format, ByteBuffer data, int frequency)
	{
		var bufferHandle = GCHandle.Alloc(data.array(), GCHandleType.Pinned);

		alBufferData(bufferName, format, bufferHandle.AddrOfPinnedObject(), data.remaining(), frequency);

		bufferHandle.Free();
	}

	[DllImport(Library)]
	static extern void alDeleteBuffers(int n, long bufferNames);

	public static void alDeleteBuffers(IntBuffer buffer)
	{
		var bufferHandle = GCHandle.Alloc(buffer.array(), GCHandleType.Pinned);

		alDeleteBuffers(buffer.remaining(), bufferHandle.AddrOfPinnedObject());

		bufferHandle.Free();
	}

	public static void alDeleteBuffers(int bufferName)
	{
		var buffer = IntBuffer.allocate(1);
		buffer.put(bufferName);
		var bufferHandle = GCHandle.Alloc(buffer.array(), GCHandleType.Pinned);

		alDeleteBuffers(1, bufferHandle.AddrOfPinnedObject());

		bufferHandle.Free();
	}

	public static void alDeleteSources(int source)
	{
		IntBuffer buffer = IntBuffer.allocate(1);

		buffer.put(source);

		var bufferHandle = GCHandle.Alloc(buffer.array(), GCHandleType.Pinned);

		alDeleteSources(1, bufferHandle.AddrOfPinnedObject());

		bufferHandle.Free();

		[DllImport(Library)]
		static extern void alDeleteSources(int n, long sources);
	}

	[DllImport(Library)]
	public static extern void alDisable(int target);
	
	[DllImport(Library)]
	static extern void alGenBuffers(int n, long bufferNames);

	public static void alGenBuffers(IntBuffer bufferNames)
	{
		var bufferNamesHandle = GCHandle.Alloc(bufferNames.array(), GCHandleType.Pinned);
		alGenBuffers(bufferNames.remaining(), bufferNamesHandle.AddrOfPinnedObject());

		bufferNamesHandle.Free();
	}

	public static int alGenBuffers()
	{
		var bufferNames = IntBuffer.allocate(1);
		var bufferNamesHandle = GCHandle.Alloc(bufferNames.array(), GCHandleType.Pinned);

		alGenBuffers(bufferNames.remaining(), bufferNamesHandle.AddrOfPinnedObject());

		bufferNamesHandle.Free();

		return bufferNames.get(0);
	}

	public static void alGenSources(IntBuffer sourceNames)
	{
		var bufferNamesHandle = GCHandle.Alloc(sourceNames.array(), GCHandleType.Pinned);
		alGenSources(1, bufferNamesHandle.AddrOfPinnedObject());

		bufferNamesHandle.Free();
	}

	[DllImport(Library)]
	static extern void alGenSources(int n, long bufferNames);

	public static int alGenSources()
	{
		var bufferNames = IntBuffer.allocate(1);
		var bufferNamesHandle = GCHandle.Alloc(bufferNames.array(), GCHandleType.Pinned);

		alGenSources(bufferNames.remaining(), bufferNamesHandle.AddrOfPinnedObject());

		bufferNamesHandle.Free();

		return bufferNames.get(0);
	}

	[DllImport(Library)]
	public static extern int alGetError();
	
	public static float alGetSourcef(int source, int param)
	{
		var buffer = FloatBuffer.allocate(1);

		var bufferHandle = GCHandle.Alloc(buffer.array(), GCHandleType.Pinned);

		alGetSourcef(source, param, bufferHandle.AddrOfPinnedObject());

		bufferHandle.Free();

		return buffer.get(0);

		[DllImport(Library)]
		static extern void alGetSourcef(int source, int param, long value);
	}

	public static int alGetSourcei(int source, int param)
	{
		var buffer = IntBuffer.allocate(1);

		var bufferHandle = GCHandle.Alloc(buffer.array(), GCHandleType.Pinned);

		alGetSourcei(source, param, bufferHandle.AddrOfPinnedObject());

		bufferHandle.Free();

		return buffer.get(0);

		[DllImport(Library)]
		static extern void alGetSourcei(int source, int param, long value);
	}

	public static void alListenerfv(int paramName, FloatBuffer values)
	{
		var bufferHandle = GCHandle.Alloc(values.array(), GCHandleType.Pinned);

		alListenerfv(paramName, bufferHandle.AddrOfPinnedObject());

		bufferHandle.Free();
	}

	[DllImport(Library)]
	static extern void alListenerfv(int paramName, long values);

	[DllImport(Library)]
	public static extern void alSource3f(int source, int param, float value1, float value2, float value3);

	[DllImport(Library)]
	public static extern void alSourcef(int source, int param, float value);

	[DllImport(Library)]
	public static extern void alSourcei(int source, int param, int value);

	// TODO: Not sure about this
	[DllImport(Library)]
	public static extern void alSourcePause(int source);

	// TODO: Not sure about this
	[DllImport(Library)]
	public static extern void alSourcePlay(int source);

	public static void alSourceQueueBuffers(int sourceName, int bufferName)
	{
		var bufferNames = IntBuffer.allocate(1);
		bufferNames.put(bufferName);
		var bufferNamesHandle = GCHandle.Alloc(bufferNames.array(), GCHandleType.Pinned);

		alSourceQueueBuffers(sourceName, 1, bufferNamesHandle.AddrOfPinnedObject());

		bufferNamesHandle.Free();

		[DllImport(Library)]
		static extern void alSourceQueueBuffers(int sourceName, int numBuffers, long bufferNames);
	}

	// TODO: Not sure about this
	[DllImport(Library)]
	public static extern void alSourceStop(int id);

	[DllImport(Library)]
	public static extern void alSourceUnqueueBuffers(int sourceName, int numEntries, long bufferNames);

	public static void alSourceUnqueueBuffers(int sourceName, IntBuffer bufferNames)
	{
		var bufferNamesHandle = GCHandle.Alloc(bufferNames.array(), GCHandleType.Pinned);
		alSourceUnqueueBuffers(sourceName, bufferNames.remaining(), bufferNamesHandle.AddrOfPinnedObject());
		bufferNamesHandle.Free();
	}

	public static int alSourceUnqueueBuffers(int sourceName)
	{
		var bufferNames = IntBuffer.allocate(1);
		var bufferNamesHandle = GCHandle.Alloc(bufferNames.array(), GCHandleType.Pinned);

		alSourceUnqueueBuffers(sourceName, 1, bufferNamesHandle.AddrOfPinnedObject());

		bufferNamesHandle.Free();

		return bufferNames.get(0);
	}
}