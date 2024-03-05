using OpenTK.Audio.OpenAL;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;
using SharpGDX.graphics.glutils;
using SharpGDX.graphics;
using SharpGDX.math;
using SharpGDX.shims;
using SharpGDX.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SharpGDX.Desktop.audio;
using SharpGDX.Desktop.audio.mock;
using static OpenTK.Windowing.GraphicsLibraryFramework.GLFWCallbacks;
using static SharpGDX.Application;
using GLFWWindow = OpenTK.Windowing.GraphicsLibraryFramework.Window;
using GLFWErrorCallback = OpenTK.Windowing.GraphicsLibraryFramework.GLFWCallbacks.ErrorCallback;

namespace SharpGDX.Desktop
{
	public unsafe class DesktopApplication : DesktopApplicationBase
	{
	private readonly DesktopApplicationConfiguration config;
	readonly Array<DesktopWindow> windows = new Array<DesktopWindow>();
	private volatile DesktopWindow currentWindow;
	private DesktopAudio audio;
	private readonly Files files;
	private readonly Net net;
	private readonly ObjectMap<String, Preferences> preferences = new ObjectMap<String, Preferences>();
	private readonly DesktopClipboard clipboard;
	private int logLevel = LOG_INFO;
	private ApplicationLogger applicationLogger;
	private volatile bool running = true;
	private readonly Array<Action> runnables = new Array<Action>();
	private readonly Array<Action> executedRunnables = new Array<Action>();
	private readonly Array<LifecycleListener> lifecycleListeners = new Array<LifecycleListener>();
	private static GLFWErrorCallback? errorCallback;
	private static GLVersion glVersion;
	private static Action? glDebugCallback;
	private readonly Sync sync;

	internal static void initializeGlfw()
	{
		if (errorCallback == null)
		{
			if (OperatingSystem.IsMacOS() || OperatingSystem.IsMacCatalyst())
			{
				loadGlfwAwtMacos();
			}

			DesktopNativesLoader.load();

			errorCallback = (_, description) => { Console.WriteLine(description); };

			GLFW.SetErrorCallback(errorCallback);
			if (OperatingSystem.IsMacOS() || OperatingSystem.IsMacCatalyst())
			{
				// TODO: GLFW.InitHint(GLFW.GLFW_ANGLE_PLATFORM_TYPE, GLFW.GLFW_ANGLE_PLATFORM_TYPE_METAL);
				throw new NotImplementedException();
			}

			GLFW.InitHint(InitHintBool.JoystickHatButtons, false);
			if (!GLFW.Init())
			{
				throw new GdxRuntimeException("Unable to initialize GLFW");
			}
		}
	}

	static void loadANGLE()
	{
		// TODO:
		throw new NotImplementedException();
			//try
			//{
			//	Class angleLoader = Class.forName("com.badlogic.gdx.backends.lwjgl3.angle.ANGLELoader");
			//	Method load = angleLoader.getMethod("load");
			//	load.invoke(angleLoader);
			//}
			//catch (ClassNotFoundException t)
			//{
			//	return;
			//}
			//catch (Exception t)
			//{
			//	throw new GdxRuntimeException("Couldn't load ANGLE.", t);
			//}
		}

	static void postLoadANGLE()
	{
		// TODO:
		throw new NotImplementedException();
			//try
			//{
			//	Class angleLoader = Class.forName("com.badlogic.gdx.backends.lwjgl3.angle.ANGLELoader");
			//	Method load = angleLoader.getMethod("postGlfwInit");
			//	load.invoke(angleLoader);
			//}
			//catch (ClassNotFoundException t)
			//{
			//	return;
			//}
			//catch (Exception t)
			//{
			//	throw new GdxRuntimeException("Couldn't load ANGLE.", t);
			//}
		}

	static void loadGlfwAwtMacos()
	{
		// TODO:
		throw new NotImplementedException();
		//try
		//{
		//	Class loader = Class.forName("com.badlogic.gdx.backends.lwjgl3.awt.GlfwAWTLoader");
		//	Method load = loader.getMethod("load");
		//	WebRequestMethods.File sharedLib = (WebRequestMethods.File)load.invoke(loader);
		//	Configuration.GLFW_LIBRARY_NAME.set(sharedLib.getAbsolutePath());
		//	Configuration.GLFW_CHECK_THREAD0.set(false);
		//}
		//catch (ClassNotFoundException t)
		//{
		//	return;
		//}
		//catch (Exception t)
		//{
		//	throw new GdxRuntimeException("Couldn't load GLFW AWT for macOS.", t);
		//}
	}

	public DesktopApplication(ApplicationListener listener)
	: this(listener, new DesktopApplicationConfiguration())
		{
	}

	public DesktopApplication(ApplicationListener listener, DesktopApplicationConfiguration config)
	{
		if (config.glEmulation == DesktopApplicationConfiguration.GLEmulation.ANGLE_GLES20) loadANGLE();
		initializeGlfw();
		setApplicationLogger(new DesktopApplicationLogger());

		this.config = config = DesktopApplicationConfiguration.copy(config);
		if (config.title == null) config.title = listener.GetType().Name;

		Gdx.app = this;
		if (!config._disableAudio)
		{
			try
			{
				this.audio = createAudio(config);
			}
			catch (Exception t)
			{
				log("DesktopApplication", "Couldn't initialize audio, disabling audio", t);
				this.audio = new MockAudio();
			}
		}
		else
		{
			this.audio = new MockAudio();
		}
		Gdx.audio = audio;
		this.files = Gdx.files = createFiles();
		this.net = Gdx.net = new DesktopNet(config);
		this.clipboard = new DesktopClipboard();

		this.sync = new Sync();

		DesktopWindow window = createWindow(config, listener, null);
		if (config.glEmulation == DesktopApplicationConfiguration.GLEmulation.ANGLE_GLES20) postLoadANGLE();
		windows.add(window);
		try
		{
			loop();
			cleanupWindows();
		}
		catch (Exception t)
		{
			if (t is SystemException)
				throw (SystemException)t;
			else
				throw new GdxRuntimeException(t);
		}
		finally
		{
			cleanup();
		}
	}

	protected void loop()
	{
		Array<DesktopWindow> closedWindows = new Array<DesktopWindow>();
		while (running && windows.size > 0)
		{
			// FIXME put it on a separate thread
			audio.update();

			bool haveWindowsRendered = false;
			closedWindows.clear();
			int targetFramerate = -2;
			foreach (DesktopWindow window in windows)
			{
				window.makeCurrent();
				currentWindow = window;
				if (targetFramerate == -2) targetFramerate = window.getConfig().foregroundFPS;
				lock(lifecycleListeners) {
					haveWindowsRendered |= window.update();
				}
				if (window.shouldClose())
				{
					closedWindows.add(window);
				}
			}
			GLFW.PollEvents();

			bool shouldRequestRendering;
			lock (runnables) {
				shouldRequestRendering = runnables.size > 0;
				executedRunnables.clear();
				executedRunnables.addAll(runnables);
				runnables.clear();
			}
			foreach (Action runnable in executedRunnables)
			{
				runnable.Invoke();
			}
			if (shouldRequestRendering)
			{
				// Must follow Runnables execution so changes done by Runnables are reflected
				// in the following render.
				foreach (DesktopWindow window in windows)
				{
					if (!window.getGraphics().isContinuousRendering()) window.requestRendering();
				}
			}

			foreach (DesktopWindow closedWindow in closedWindows)
			{
				if (windows.size == 1)
				{
					// Lifecycle listener methods have to be called before ApplicationListener methods. The
					// application will be disposed when _all_ windows have been disposed, which is the case,
					// when there is only 1 window left, which is in the process of being disposed.
					for (int i = lifecycleListeners.size - 1; i >= 0; i--)
					{
						LifecycleListener l = lifecycleListeners.get(i);
						l.pause();
						l.dispose();
					}
					lifecycleListeners.clear();
				}
				closedWindow.dispose();

				windows.removeValue(closedWindow, false);
			}

			if (!haveWindowsRendered)
			{
				// Sleep a few milliseconds in case no rendering was requested
				// with continuous rendering disabled.
				try
				{
					Thread.Sleep(1000 / config.idleFPS);
				}
				catch (ThreadInterruptedException e)
				{
					// ignore
				}
			}
			else if (targetFramerate > 0)
			{
				sync.sync(targetFramerate); // sleep as needed to meet the target framerate
			}
		}
	}

	protected void cleanupWindows()
	{
		lock(lifecycleListeners) {
			foreach (LifecycleListener lifecycleListener in lifecycleListeners)
			{
				lifecycleListener.pause();
				lifecycleListener.dispose();
			}
		}
		foreach (DesktopWindow window in windows)
		{
			window.dispose();
		}
		windows.clear();
	}

	protected void cleanup()
	{
		DesktopCursor.disposeSystemCursors();
		audio.dispose();
		GLFW.SetErrorCallback(null);
		errorCallback = null;
		
		if (glDebugCallback != null)
		{
			glDebugCallback = null;
		}

		GLFW.Terminate();
	}

	public ApplicationListener getApplicationListener()
	{
		return currentWindow.getListener();
	}

	public Graphics getGraphics()
	{
		return currentWindow.getGraphics();
	}

	public Audio getAudio()
	{
		return audio;
	}

	public Input getInput()
	{
		return currentWindow.getInput();
	}

	public Files getFiles()
	{
		return files;
	}

	public Net getNet()
	{
		return net;
	}

	public void debug(String tag, String message)
	{
		if (logLevel >= LOG_DEBUG) getApplicationLogger().debug(tag, message);
	}

	public void debug(String tag, String message, Exception exception)
	{
		if (logLevel >= LOG_DEBUG) getApplicationLogger().debug(tag, message, exception);
	}

	public void log(String tag, String message)
	{
		if (logLevel >= LOG_INFO) getApplicationLogger().log(tag, message);
	}

	public void log(String tag, String message, Exception exception)
	{
		if (logLevel >= LOG_INFO) getApplicationLogger().log(tag, message, exception);
	}

	public void error(String tag, String message)
	{
		if (logLevel >= LOG_ERROR) getApplicationLogger().error(tag, message);
	}

	public void error(String tag, String message, Exception exception)
	{
		if (logLevel >= LOG_ERROR) getApplicationLogger().error(tag, message, exception);
	}

	public void setLogLevel(int logLevel)
	{
		this.logLevel = logLevel;
	}

	public int getLogLevel()
	{
		return logLevel;
	}

	public void setApplicationLogger(ApplicationLogger applicationLogger)
	{
		this.applicationLogger = applicationLogger;
	}

	public ApplicationLogger getApplicationLogger()
	{
		return applicationLogger;
	}

	public ApplicationType getType()
	{
		return ApplicationType.Desktop;
	}

	public int getVersion()
	{
		return 0;
	}

	public long getJavaHeap()
	{
		return GC.GetTotalMemory(false);
		}

	public long getNativeHeap()
	{
		return getJavaHeap();
	}

	public Preferences getPreferences(String name)
	{
		if (preferences.containsKey(name))
		{
			return preferences.get(name);
		}
		else
		{
			Preferences prefs = new DesktopPreferences(
				new DesktopFileHandle(new FileInfo(Path.Combine(config.preferencesDirectory, name)), config.preferencesFileType));
			preferences.put(name, prefs);
			return prefs;
		}
	}

	public Clipboard getClipboard()
	{
		return clipboard;
	}

	public void postRunnable(Action runnable)
	{
		lock(runnables) {
			runnables.add(runnable);
		}
	}

	public void exit()
	{
		running = false;
	}

	public void addLifecycleListener(LifecycleListener listener)
	{
		lock (lifecycleListeners) {
			lifecycleListeners.add(listener);
		}
	}

	public void removeLifecycleListener(LifecycleListener listener)
	{
		lock (lifecycleListeners) {
			lifecycleListeners.removeValue(listener, true);
		}
	}

	public DesktopAudio createAudio(DesktopApplicationConfiguration config)
	{
		return new OpenALDesktopAudio(config.audioDeviceSimultaneousSources, config.audioDeviceBufferCount,
			config.audioDeviceBufferSize);
	}

	public DesktopInput createInput(DesktopWindow window)
	{
		return new DefaultDesktopInput(window);
	}

	protected Files createFiles()
	{
		return new DesktopFiles();
	}

	/** Creates a new {@link DesktopWindow} using the provided listener and {@link DesktopWindowConfiguration}.
	 *
	 * This function only just instantiates a {@link DesktopWindow} and returns immediately. The actual window creation is postponed
	 * with {@link Application#postRunnable(Runnable)} until after all existing windows are updated. */
	public DesktopWindow newWindow(ApplicationListener listener, DesktopWindowConfiguration config)
	{
		DesktopApplicationConfiguration appConfig = DesktopApplicationConfiguration.copy(this.config);
		appConfig.setWindowConfiguration(config);
		if (appConfig.title == null) appConfig.title = listener.GetType().Name;
		return createWindow(appConfig, listener, windows.get(0).getWindowHandle());
	}

	private DesktopWindow createWindow(DesktopApplicationConfiguration config, ApplicationListener listener,
		GLFWWindow* sharedContext)
	{
		DesktopWindow window = new DesktopWindow(listener, config, this);
		if (sharedContext == null)
		{
			// the main window is created immediately
			createWindow(window, config, sharedContext);
		}
		else
		{
			// creation of additional windows is deferred to avoid GL context trouble
			postRunnable(() =>
			{
				createWindow(window, config, sharedContext);
				windows.add(window);
			});
		}

		return window;
	}

	void createWindow(DesktopWindow window, DesktopApplicationConfiguration config, GLFWWindow* sharedContext)
{
	var windowHandle = createGlfwWindow(config, sharedContext);
	window.create(windowHandle);
	window.setVisible(config.initialVisible);

	for (int i = 0; i < 2; i++)
	{
		GL.ClearColor(config.initialBackgroundColor.r, config.initialBackgroundColor.g, config.initialBackgroundColor.b,
			config.initialBackgroundColor.a);
		GL.Clear(ClearBufferMask.ColorBufferBit);
		GLFW.SwapBuffers(windowHandle);
	}
}

static GLFWWindow* createGlfwWindow(DesktopApplicationConfiguration config, GLFWWindow* sharedContextWindow)
{
	GLFW.DefaultWindowHints();
	GLFW.WindowHint(WindowHintBool.Visible, false);
	GLFW.WindowHint(WindowHintBool.Resizable, config.windowResizable ? true : false);
	GLFW.WindowHint(WindowHintBool.Maximized, config.windowMaximized ? true : false);
	GLFW.WindowHint(WindowHintBool.AutoIconify, config.autoIconify ? true : false);

	GLFW.WindowHint(WindowHintInt.RedBits, config.r);
	GLFW.WindowHint(WindowHintInt.GreenBits, config.g);
	GLFW.WindowHint(WindowHintInt.BlueBits, config.b);
	GLFW.WindowHint(WindowHintInt.AlphaBits, config.a);
	GLFW.WindowHint(WindowHintInt.StencilBits, config.stencil);
	GLFW.WindowHint(WindowHintInt.DepthBits, config.depth);
	GLFW.WindowHint(WindowHintInt.Samples, config.samples);

	if (config.glEmulation == DesktopApplicationConfiguration.GLEmulation.GL30
		|| config.glEmulation == DesktopApplicationConfiguration.GLEmulation.GL31
		|| config.glEmulation == DesktopApplicationConfiguration.GLEmulation.GL32)
	{
		GLFW.WindowHint(WindowHintInt.ContextVersionMajor, config.gles30ContextMajorVersion);
		GLFW.WindowHint(WindowHintInt.ContextVersionMinor, config.gles30ContextMinorVersion);
		if (OperatingSystem.IsMacOS() || OperatingSystem.IsMacCatalyst())
		{
			// hints mandatory on OS X for GL 3.2+ context creation, but fail on Windows if the
			// WGL_ARB_create_context extension is not available
			// see: http://www.glfw.org/docs/latest/compat.html
			GLFW.WindowHint(WindowHintBool.OpenGLForwardCompat, true);
			GLFW.WindowHint(WindowHintOpenGlProfile.OpenGlProfile, OpenGlProfile.Core);
		}
	}
	else
	{
		if (config.glEmulation == DesktopApplicationConfiguration.GLEmulation.ANGLE_GLES20)
		{
			GLFW.WindowHint(WindowHintContextApi.ContextCreationApi, ContextApi.EglContextApi);
			GLFW.WindowHint(WindowHintClientApi.ClientApi, ClientApi.OpenGlEsApi);
			GLFW.WindowHint(WindowHintInt.ContextVersionMajor, 2);
			GLFW.WindowHint(WindowHintInt.ContextVersionMinor, 0);
		}
	}

	if (config.transparentFramebuffer)
	{
		GLFW.WindowHint(WindowHintBool.TransparentFramebuffer, true);
	}

	if (config.debug)
	{
		GLFW.WindowHint(WindowHintBool.OpenGLDebugContext, true);
	}

	GLFWWindow* windowHandle = null;

	if (config.fullscreenMode != null)
	{
		GLFW.WindowHint(WindowHintInt.RefreshRate, config.fullscreenMode.refreshRate);
		windowHandle = GLFW.CreateWindow(config.fullscreenMode.width, config.fullscreenMode.height, config.title,
			config.fullscreenMode.getMonitor(), sharedContextWindow);
	}
	else
	{
		GLFW.WindowHint(WindowHintBool.Decorated, config.windowDecorated ? true : false);
		windowHandle = GLFW.CreateWindow(config.windowWidth, config.windowHeight, config.title, null, sharedContextWindow);
	}
	if (windowHandle == null)
	{
		throw new GdxRuntimeException("Couldn't create window");
	}
	DesktopWindow.setSizeLimits(windowHandle, config.windowMinWidth, config.windowMinHeight, config.windowMaxWidth,
		config.windowMaxHeight);
	if (config.fullscreenMode == null)
	{
		if (config.windowX == -1 && config.windowY == -1)
		{ // i.e., center the window
			int windowWidth = Math.Max(config.windowWidth, config.windowMinWidth);
			int windowHeight = Math.Max(config.windowHeight, config.windowMinHeight);
			if (config.windowMaxWidth > -1) windowWidth = Math.Min(windowWidth, config.windowMaxWidth);
			if (config.windowMaxHeight > -1) windowHeight = Math.Min(windowHeight, config.windowMaxHeight);

			var monitorHandle = GLFW.GetPrimaryMonitor();
			if (config.windowMaximized && config.maximizedMonitor != null)
			{
				monitorHandle = config.maximizedMonitor.getMonitorHandle();
			}

			GridPoint2 newPos = DesktopApplicationConfiguration.calculateCenteredWindowPosition(
				DesktopApplicationConfiguration.toDesktopMonitor(monitorHandle), windowWidth, windowHeight);
			GLFW.SetWindowPos(windowHandle, newPos.x, newPos.y);
		}
		else
		{
			GLFW.SetWindowPos(windowHandle, config.windowX, config.windowY);
		}

		if (config.windowMaximized)
		{
			GLFW.MaximizeWindow(windowHandle);
		}
	}
	if (config.windowIconPaths != null)
	{
		DesktopWindow.setIcon(windowHandle, config.windowIconPaths, config.windowIconFileType);
	}
	GLFW.MakeContextCurrent(windowHandle);
	GLFW.SwapInterval(config.vSyncEnabled ? 1 : 0);
	if (config.glEmulation == DesktopApplicationConfiguration.GLEmulation.ANGLE_GLES20)
	{
		try
		{
			// TODO:
			throw new NotImplementedException();
			//Class gles = Class.forName("org.lwjgl.opengles.GLES");
			//gles.getMethod("createCapabilities").invoke(gles);
			//// TODO: Remove once https://github.com/LWJGL/lwjgl3/issues/931 is fixed
			//ThreadLocalUtil.setFunctionMissingAddresses(0);
		}
		catch (Exception e)
		{
			throw new GdxRuntimeException("Couldn't initialize GLES", e);
		}
	}
	else
	{
		GL.LoadBindings(new GLFWBindingsContext());
	}

	initiateGL(config.glEmulation == DesktopApplicationConfiguration.GLEmulation.ANGLE_GLES20);
	if (!glVersion.isVersionEqualToOrHigher(2, 0))
		throw new GdxRuntimeException("OpenGL 2.0 or higher with the FBO extension is required. OpenGL version: "
		+ GL.GetString(StringName.Version) + "\n" + glVersion.getDebugVersionString());

	if (config.glEmulation != DesktopApplicationConfiguration.GLEmulation.ANGLE_GLES20 && !supportsFBO())
	{
		throw new GdxRuntimeException("OpenGL 2.0 or higher with the FBO extension is required. OpenGL version: "
			+ GL.GetString(StringName.Version) + ", FBO extension: false\n" + glVersion.getDebugVersionString());
	}

	if (config.debug)
	{
		// TODO:
		throw new NotImplementedException();
		//glDebugCallback = GLUtil.setupDebugMessageCallback(config.debugStream);
		//setGLDebugMessageControl(GLDebugMessageSeverity.NOTIFICATION, false);
	}
	return windowHandle;
}

private static void initiateGL(bool useGLES20)
{
	if (!useGLES20)
	{
		String versionString = GL.GetString(StringName.Version);
		String vendorString = GL.GetString(StringName.Vendor);
		String rendererString = GL.GetString(StringName.Renderer);
		glVersion = new GLVersion(Application.ApplicationType.Desktop, versionString, vendorString, rendererString);
	}
	else
	{
		// TODO:
		throw new NotImplementedException();
		//try
		//{
		//	Class gles = Class.forName("org.lwjgl.opengles.GLES20");
		//	Method getString = gles.getMethod("glGetString", int.class);
		//	String versionString = (String)getString.invoke(gles, GL11.GL_VERSION);
		//	String vendorString = (String)getString.invoke(gles, GL11.GL_VENDOR);
		//	String rendererString = (String)getString.invoke(gles, GL11.GL_RENDERER);
		//	glVersion = new GLVersion(Application.ApplicationType.Desktop, versionString, vendorString, rendererString);
		//}
		//catch (Exception e)
		//{
		//	throw new GdxRuntimeException("Couldn't get GLES version string.", e);
		//}
	}
}

private static bool supportsFBO()
{
	// FBO is in core since OpenGL 3.0, see https://www.opengl.org/wiki/Framebuffer_Object
	return glVersion.isVersionEqualToOrHigher(3, 0) || GLFW.ExtensionSupported("GL_EXT_framebuffer_object")
		|| GLFW.ExtensionSupported("GL_ARB_framebuffer_object");
}

// TODO: 
//public enum GLDebugMessageSeverity
//{
//	HIGH(GL43.GL_DEBUG_SEVERITY_HIGH, KHRDebug.GL_DEBUG_SEVERITY_HIGH, ARBDebugOutput.GL_DEBUG_SEVERITY_HIGH_ARB,
//	AMDDebugOutput.GL_DEBUG_SEVERITY_HIGH_AMD), MEDIUM(GL43.GL_DEBUG_SEVERITY_MEDIUM, KHRDebug.GL_DEBUG_SEVERITY_MEDIUM,
//	ARBDebugOutput.GL_DEBUG_SEVERITY_MEDIUM_ARB, AMDDebugOutput.GL_DEBUG_SEVERITY_MEDIUM_AMD), LOW(
//					GL43.GL_DEBUG_SEVERITY_LOW, KHRDebug.GL_DEBUG_SEVERITY_LOW, ARBDebugOutput.GL_DEBUG_SEVERITY_LOW_ARB,
//	AMDDebugOutput.GL_DEBUG_SEVERITY_LOW_AMD), NOTIFICATION(GL43.GL_DEBUG_SEVERITY_NOTIFICATION,
//	KHRDebug.GL_DEBUG_SEVERITY_NOTIFICATION, -1, -1);

//	final int gl43, khr, arb, amd;

//	GLDebugMessageSeverity (int gl43, int khr, int arb, int amd) {
//			this.gl43 = gl43;
//			this.khr = khr;
//			this.arb = arb;
//			this.amd = amd;
//}
//	}

//	/** Enables or disables GL debug messages for the specified severity level. Returns false if the severity level could not be
//	 * set (e.g. the NOTIFICATION level is not supported by the ARB and AMD extensions).
//	 *
//	 * See {@link DesktopApplicationConfiguration#enableGLDebugOutput(bool, PrintStream)} */
//	public static bool setGLDebugMessageControl(DesktopApplication.GLDebugMessageSeverity severity, bool enabled)
//{
//	GLCapabilities caps = GL.getCapabilities();
//	final int GL_DONT_CARE = 0x1100; // not defined anywhere yet

//	if (caps.OpenGL43)
//	{
//		GL43.glDebugMessageControl(GL_DONT_CARE, GL_DONT_CARE, severity.gl43, (IntBuffer)null, enabled);
//		return true;
//	}

//	if (caps.GL_KHR_debug)
//	{
//		KHRDebug.glDebugMessageControl(GL_DONT_CARE, GL_DONT_CARE, severity.khr, (IntBuffer)null, enabled);
//		return true;
//	}

//	if (caps.GL_ARB_debug_output && severity.arb != -1)
//	{
//		ARBDebugOutput.glDebugMessageControlARB(GL_DONT_CARE, GL_DONT_CARE, severity.arb, (IntBuffer)null, enabled);
//		return true;
//	}

//	if (caps.GL_AMD_debug_output && severity.amd != -1)
//	{
//		AMDDebugOutput.glDebugMessageEnableAMD(GL_DONT_CARE, severity.amd, (IntBuffer)null, enabled);
//		return true;
//	}

//	return false;
//}

}
}
