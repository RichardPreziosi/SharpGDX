using OpenTK.Windowing.GraphicsLibraryFramework;
using Monitor = SharpGDX.Graphics.Monitor;
using GLFWVidMode = OpenTK.Windowing.GraphicsLibraryFramework.VideoMode;
using GLFWMonitor = OpenTK.Windowing.GraphicsLibraryFramework.Monitor;
using SharpGDX.shims;
using SharpGDX.graphics.glutils;
using SharpGDX.math;
using SharpGDX.utils;
using static SharpGDX.Files;
using static SharpGDX.Graphics;

namespace SharpGDX.Desktop
{
	public unsafe class DesktopApplicationConfiguration : DesktopWindowConfiguration
	{
	public static StreamWriter errorStream = new(Console.OpenStandardOutput());

	internal bool _disableAudio = false;

	/** The maximum number of threads to use for network requests. Default is {@link Integer#MAX_VALUE}. */
	int maxNetThreads = int.MaxValue;

	internal int audioDeviceSimultaneousSources = 16;
	internal int audioDeviceBufferSize = 512;
	internal int audioDeviceBufferCount = 9;

	public enum GLEmulation
	{
		ANGLE_GLES20, GL20, GL30, GL31, GL32
	}

	internal GLEmulation glEmulation = GLEmulation.GL20;
	internal int gles30ContextMajorVersion = 3;
	internal int gles30ContextMinorVersion = 2;

	internal int r = 8, g = 8, b = 8, a = 8;
	internal int depth = 16, stencil = 0;
	internal int samples = 0;
	internal bool transparentFramebuffer;

	internal int idleFPS = 60;
	internal int foregroundFPS = 0;

	internal String preferencesDirectory = ".prefs/";
	internal Files.FileType preferencesFileType = FileType.External;

	internal HdpiMode hdpiMode = HdpiMode.Logical;

	internal bool debug = false;
	StreamWriter debugStream = new(Console.OpenStandardOutput());

	internal static DesktopApplicationConfiguration copy(DesktopApplicationConfiguration config)
	{
		DesktopApplicationConfiguration copy = new DesktopApplicationConfiguration();
		copy.set(config);
		return copy;
	}

	void set(DesktopApplicationConfiguration config)
	{
		base.setWindowConfiguration(config);
		_disableAudio = config._disableAudio;
		audioDeviceSimultaneousSources = config.audioDeviceSimultaneousSources;
		audioDeviceBufferSize = config.audioDeviceBufferSize;
		audioDeviceBufferCount = config.audioDeviceBufferCount;
		glEmulation = config.glEmulation;
		gles30ContextMajorVersion = config.gles30ContextMajorVersion;
		gles30ContextMinorVersion = config.gles30ContextMinorVersion;
		r = config.r;
		g = config.g;
		b = config.b;
		a = config.a;
		depth = config.depth;
		stencil = config.stencil;
		samples = config.samples;
		transparentFramebuffer = config.transparentFramebuffer;
		idleFPS = config.idleFPS;
		foregroundFPS = config.foregroundFPS;
		preferencesDirectory = config.preferencesDirectory;
		preferencesFileType = config.preferencesFileType;
		hdpiMode = config.hdpiMode;
		debug = config.debug;
		debugStream = config.debugStream;
	}

	/** @param visibility whether the window will be visible on creation. (default true) */
	public void setInitialVisible(bool visibility)
	{
		this.initialVisible = visibility;
	}

	/** Whether to disable audio or not. If set to true, the returned audio class instances like {@link Audio} or {@link Music}
	 * will be mock implementations. */
	public void disableAudio(bool disableAudio)
	{
		this._disableAudio = disableAudio;
	}

	/** Sets the maximum number of threads to use for network requests. */
	public void setMaxNetThreads(int maxNetThreads)
	{
		this.maxNetThreads = maxNetThreads;
	}

	/** Sets the audio device configuration.
	 * 
	 * @param simultaneousSources the maximum number of sources that can be played simultaniously (default 16)
	 * @param bufferSize the audio device buffer size in samples (default 512)
	 * @param bufferCount the audio device buffer count (default 9) */
	public void setAudioConfig(int simultaneousSources, int bufferSize, int bufferCount)
	{
		this.audioDeviceSimultaneousSources = simultaneousSources;
		this.audioDeviceBufferSize = bufferSize;
		this.audioDeviceBufferCount = bufferCount;
	}

	/** Sets which OpenGL version to use to emulate OpenGL ES. If the given major/minor version is not supported, the backend falls
	 * back to OpenGL ES 2.0 emulation through OpenGL 2.0. The default parameters for major and minor should be 3 and 2
	 * respectively to be compatible with Mac OS X. Specifying major version 4 and minor version 2 will ensure that all OpenGL ES
	 * 3.0 features are supported. Note however that Mac OS X does only support 3.2.
	 * 
	 * @see <a href= "http://legacy.lwjgl.org/javadoc/org/lwjgl/opengl/ContextAttribs.html"> LWJGL OSX ContextAttribs note</a>
	 * 
	 * @param glVersion which OpenGL ES emulation version to use
	 * @param gles3MajorVersion OpenGL ES major version, use 3 as default
	 * @param gles3MinorVersion OpenGL ES minor version, use 2 as default */
	public void setOpenGLEmulation(GLEmulation glVersion, int gles3MajorVersion, int gles3MinorVersion)
	{
		this.glEmulation = glVersion;
		this.gles30ContextMajorVersion = gles3MajorVersion;
		this.gles30ContextMinorVersion = gles3MinorVersion;
	}

	/** Sets the bit depth of the color, depth and stencil buffer as well as multi-sampling.
	 * 
	 * @param r red bits (default 8)
	 * @param g green bits (default 8)
	 * @param b blue bits (default 8)
	 * @param a alpha bits (default 8)
	 * @param depth depth bits (default 16)
	 * @param stencil stencil bits (default 0)
	 * @param samples MSAA samples (default 0) */
	public void setBackBufferConfig(int r, int g, int b, int a, int depth, int stencil, int samples)
	{
		this.r = r;
		this.g = g;
		this.b = b;
		this.a = a;
		this.depth = depth;
		this.stencil = stencil;
		this.samples = samples;
	}

	/** Set transparent window hint. Results may vary on different OS and GPUs. Usage with the ANGLE backend is less consistent.
	 * @param transparentFramebuffer */
	public void setTransparentFramebuffer(bool transparentFramebuffer)
	{
		this.transparentFramebuffer = transparentFramebuffer;
	}

	/** Sets the polling rate during idle time in non-continuous rendering mode. Must be positive. Default is 60. */
	public void setIdleFPS(int fps)
	{
		this.idleFPS = fps;
	}

	/** Sets the target framerate for the application. The CPU sleeps as needed. Must be positive. Use 0 to never sleep. Default is
	 * 0. */
	public void setForegroundFPS(int fps)
	{
		this.foregroundFPS = fps;
	}

	/** Sets the directory where {@link Preferences} will be stored, as well as the file type to be used to store them. Defaults to
	 * "$USER_HOME/.prefs/" and {@link FileType#External}. */
	public void setPreferencesConfig(String preferencesDirectory, Files.FileType preferencesFileType)
	{
		this.preferencesDirectory = preferencesDirectory;
		this.preferencesFileType = preferencesFileType;
	}

	/** Defines how HDPI monitors are handled. Operating systems may have a per-monitor HDPI scale setting. The operating system
	 * may report window width/height and mouse coordinates in a logical coordinate system at a lower resolution than the actual
	 * physical resolution. This setting allows you to specify whether you want to work in logical or raw pixel units. See
	 * {@link HdpiMode} for more information. Note that some OpenGL functions like {@link GL20#glViewport(int, int, int, int)} and
	 * {@link GL20#glScissor(int, int, int, int)} require raw pixel units. Use {@link HdpiUtils} to help with the conversion if
	 * HdpiMode is set to {@link HdpiMode#Logical}. Defaults to {@link HdpiMode#Logical}. */
	public void setHdpiMode(HdpiMode mode)
	{
		this.hdpiMode = mode;
	}

	/** Enables use of OpenGL debug message callbacks. If not supported by the core GL driver (since GL 4.3), this uses the
	 * KHR_debug, ARB_debug_output or AMD_debug_output extension if available. By default, debug messages with NOTIFICATION
	 * severity are disabled to avoid log spam.
	 *
	 * You can call with {@link System#err} to output to the "standard" error output stream.
	 *
	 * Use {@link DesktopApplication#setGLDebugMessageControl(DesktopApplication.GLDebugMessageSeverity, bool)} to enable or
	 * disable other severity debug levels. */
	public void enableGLDebugOutput(bool enable, StreamWriter debugOutputStream)
	{
		debug = enable;
		debugStream = debugOutputStream;
	}

	/** @return the currently active {@link DisplayMode} of the primary monitor */
	public static DisplayMode getDisplayMode()
	{
			DesktopApplication.initializeGlfw();
			var videoMode = GLFW.GetVideoMode(GLFW.GetPrimaryMonitor());
			return new DesktopGraphics.DesktopDisplayMode(GLFW.GetPrimaryMonitor(), videoMode->Width, videoMode->Height,
				videoMode->RefreshRate, videoMode->RedBits + videoMode->GreenBits + videoMode->BlueBits);
	}

	/** @return the currently active {@link DisplayMode} of the given monitor */
	public static DisplayMode getDisplayMode(Monitor monitor)
	{
		DesktopApplication.initializeGlfw();
		var videoMode = GLFW.GetVideoMode(((DesktopGraphics.DesktopMonitor)monitor).getMonitorHandle());
		return new DesktopGraphics.DesktopDisplayMode(((DesktopGraphics.DesktopMonitor)monitor).getMonitorHandle(), videoMode->Width, videoMode->Height,
			videoMode->RefreshRate, videoMode->RedBits + videoMode->GreenBits + videoMode->BlueBits);
	}

	/** @return the available {@link DisplayMode}s of the primary monitor */
	public static DisplayMode[] getDisplayModes()
	{
		DesktopApplication.initializeGlfw();
		var videoModes = GLFW.GetVideoModes(GLFW.GetPrimaryMonitor());
		DisplayMode[] result = new DisplayMode[videoModes.Length];
		for (int i = 0; i < result.Length; i++)
		{
			GLFWVidMode videoMode = videoModes[i];
			result[i] = new DesktopGraphics.DesktopDisplayMode(GLFW.GetPrimaryMonitor(), videoMode.Width, videoMode.Height,
				videoMode.RefreshRate, videoMode.RedBits + videoMode.GreenBits + videoMode.BlueBits);
		}
		return result;
	}

	/** @return the available {@link DisplayMode}s of the given {@link Monitor} */
	public static DisplayMode[] getDisplayModes(Monitor monitor)
	{
		DesktopApplication.initializeGlfw();
		var videoModes = GLFW.GetVideoModes(((DesktopGraphics.DesktopMonitor)monitor).getMonitorHandle());
		DisplayMode[] result = new DisplayMode[videoModes.Length];
		for (int i = 0; i < result.Length; i++)
		{
			GLFWVidMode videoMode = videoModes[i];
			result[i] = new DesktopGraphics.DesktopDisplayMode(((DesktopGraphics.DesktopMonitor)monitor).getMonitorHandle(), videoMode.Width,
				videoMode.Height, videoMode.RefreshRate, videoMode.RedBits + videoMode.GreenBits + videoMode.BlueBits);
		}
		return result;
	}

	/** @return the primary {@link Monitor} */
	public static Monitor getPrimaryMonitor()
	{
		DesktopApplication.initializeGlfw();
		return toDesktopMonitor(GLFW.GetPrimaryMonitor());
	}

	/** @return the connected {@link Monitor}s */
	public static Monitor[] getMonitors()
	{
		DesktopApplication.initializeGlfw();
		var glfwMonitors = GLFW.GetMonitors();
		Monitor[] monitors = new Monitor[glfwMonitors.Length];
		for (int i = 0; i < glfwMonitors.Length; i++)
		{
			monitors[i] = toDesktopMonitor(glfwMonitors[i]);
		}
		return monitors;
	}

	internal static DesktopGraphics.DesktopMonitor toDesktopMonitor(GLFWMonitor* glfwMonitor)
	{
		GLFW.GetMonitorPos(glfwMonitor, out var virtualX, out var virtualY);
		String name = GLFW.GetMonitorName(glfwMonitor);
		return new DesktopGraphics.DesktopMonitor(glfwMonitor, virtualX, virtualY, name);
	}

	internal static GridPoint2 calculateCenteredWindowPosition(DesktopGraphics.DesktopMonitor monitor, int newWidth, int newHeight)
	{
		

		DisplayMode displayMode = getDisplayMode(monitor);

		GLFW.GetMonitorWorkarea(monitor.getMonitorHandle(), out var x, out var y, out var width, out var height);
		int workareaWidth = width;
		int workareaHeight = height;

		int minX, minY, maxX, maxY;

		// If the new width is greater than the working area, we have to ignore stuff like the taskbar for centering and use the
		// whole monitor's size
		if (newWidth > workareaWidth)
		{
			minX = monitor.virtualX;
			maxX = displayMode.width;
		}
		else
		{
			minX = x;
			maxX = workareaWidth;
		}
		// The same is true for height
		if (newHeight > workareaHeight)
		{
			minY = monitor.virtualY;
			maxY = displayMode.height;
		}
		else
		{
			minY = y;
			maxY = workareaHeight;
		}

		return new GridPoint2(Math.Max(minX, minX + (maxX - newWidth) / 2), Math.Max(minY, minY + (maxY - newHeight) / 2));
	}
}
}
