using System.Net;
using File = SharpGDX.Shims.File;
using SharpGDX.Shims;
using SharpGDX.Utils;
using SharpGDX.Desktop.Audio;
using SharpGDX.Desktop.Audio.Mock;
using SharpGDX.Mathematics;

namespace SharpGDX.Desktop
{
	public class Lwjgl3Application : Lwjgl3ApplicationBase {
	private readonly Lwjgl3ApplicationConfiguration config;
	readonly Array<Lwjgl3Window> windows = new Array<Lwjgl3Window>();
	private volatile Lwjgl3Window currentWindow;
	private Lwjgl3Audio audio;
	private readonly Files files;
	private readonly Net net;
	private readonly ObjectMap<String, Preferences> preferences = new ObjectMap<String, Preferences>();
	private readonly Lwjgl3Clipboard clipboard;
	private int logLevel = Application.LOG_INFO;
	private ApplicationLogger applicationLogger;
	private volatile bool running = true;
	private readonly Array<Runnable> runnables = new Array<Runnable>();
	private readonly Array<Runnable> executedRunnables = new Array<Runnable>();
	private readonly Array<LifecycleListener> lifecycleListeners = new Array<LifecycleListener>();
	private static GLFW.GLFWErrorCallback errorCallback;
	private static GLVersion glVersion;
	private static Action glDebugCallback;
	private readonly Sync sync;

	internal static void initializeGlfw () {
		if (errorCallback == null) {
			if (SharedLibraryLoader.isMac)
			{
					//loadGlfwAwtMacos();
					throw new NotImplementedException();
				}
			Lwjgl3NativesLoader.load();
			errorCallback = (int code, string description) =>
			{
				// TODO: ??? GLFWErrorCallback.createPrint(Lwjgl3ApplicationConfiguration.errorStream);

				Console.WriteLine(description);
			};

			GLFW.glfwSetErrorCallback(errorCallback);
			if (SharedLibraryLoader.isMac) GLFW.glfwInitHint(GLFW.GLFW_ANGLE_PLATFORM_TYPE, GLFW.GLFW_ANGLE_PLATFORM_TYPE_METAL);
			GLFW.glfwInitHint(GLFW.GLFW_JOYSTICK_HAT_BUTTONS, GLFW.GLFW_FALSE);
			if (!GLFW.glfwInit()) {
				throw new GdxRuntimeException("Unable to initialize GLFW");
			}
		}
	}

	static void loadANGLE () {
			//try {
			//	Class angleLoader = Class.forName("com.badlogic.gdx.backends.lwjgl3.angle.ANGLELoader");
			//	Method load = angleLoader.getMethod("load");
			//	load.invoke(angleLoader);
			//} catch (ClassNotFoundException t) {
			//	return;
			//} catch (Throwable t) {
			//	throw new GdxRuntimeException("Couldn't load ANGLE.", t);
			//}
			throw new NotImplementedException();
		}

	static void postLoadANGLE () {
		//try {
		//	Class angleLoader = Class.forName("com.badlogic.gdx.backends.lwjgl3.angle.ANGLELoader");
		//	Method load = angleLoader.getMethod("postGlfwInit");
		//	load.invoke(angleLoader);
		//} catch (ClassNotFoundException t) {
		//	return;
		//} catch (Exception t) {
		//	throw new GdxRuntimeException("Couldn't load ANGLE.", t);
		//}
		throw new NotImplementedException();
	}

	static void loadGlfwAwtMacos () {
		//try {
		//	Class loader = Class.forName("com.badlogic.gdx.backends.lwjgl3.awt.GlfwAWTLoader");
		//	Method load = loader.getMethod("load");
		//	WebRequestMethods.File sharedLib = (WebRequestMethods.File)load.invoke(loader);
		//	Configuration.GLFW_LIBRARY_NAME.set(sharedLib.getAbsolutePath());
		//	Configuration.GLFW_CHECK_THREAD0.set(false);
		//} catch (ClassNotFoundException t) {
		//	return;
		//} catch (Exception t) {
		//	throw new GdxRuntimeException("Couldn't load GLFW AWT for macOS.", t);
		//}
		throw new NotImplementedException();
	}

	public Lwjgl3Application (ApplicationListener listener) 
	: this(listener, new Lwjgl3ApplicationConfiguration())
	{
		
	}

	public Lwjgl3Application (ApplicationListener listener, Lwjgl3ApplicationConfiguration config) {
		if (config.glEmulation == Lwjgl3ApplicationConfiguration.GLEmulation.ANGLE_GLES20) loadANGLE();
		initializeGlfw();
		setApplicationLogger(new Lwjgl3ApplicationLogger());

		this.config = config = Lwjgl3ApplicationConfiguration.copy(config);
		if (config.title == null) config.title = listener.GetType().Name;

		Gdx.app = this;
		if (!config._disableAudio) {
			try
			{
				this.audio = createAudio(config);
				
			} catch (Exception t) {
				log("Lwjgl3Application", "Couldn't initialize audio, disabling audio", t);
				this.audio = new MockAudio();
			}
		} else {
			this.audio = new MockAudio();
		}
		Gdx.audio = audio;
		this.files = Gdx.files = createFiles();
		this.net = Gdx.net = new Lwjgl3Net(config);
		this.clipboard = new Lwjgl3Clipboard();

		this.sync = new Sync();

		Lwjgl3Window window = createWindow(config, listener, 0);
		if (config.glEmulation == Lwjgl3ApplicationConfiguration.GLEmulation.ANGLE_GLES20) postLoadANGLE();
		windows.add(window);
		try {
			loop();
			cleanupWindows();
		} catch (Exception t) {
			if (t is RuntimeException)
				throw (RuntimeException)t;
			else
				throw new GdxRuntimeException(t);
		} finally {
			cleanup();
		}
	}

	protected void loop () {
		Array<Lwjgl3Window> closedWindows = new Array<Lwjgl3Window>();
		while (running && windows.size > 0) {
			// FIXME put it on a separate thread
			audio.update();

			bool haveWindowsRendered = false;
			closedWindows.clear();
			int targetFramerate = -2;
			foreach (Lwjgl3Window window in windows) {
				window.makeCurrent();
				currentWindow = window;
				if (targetFramerate == -2) targetFramerate = window.getConfig().foregroundFPS;
				lock (lifecycleListeners) {
					haveWindowsRendered |= window.update();
				}
				if (window.shouldClose()) {
					closedWindows.add(window);
				}
			}
			GLFW.glfwPollEvents();

			bool shouldRequestRendering;
			lock (runnables) {
				shouldRequestRendering = runnables.size > 0;
				executedRunnables.clear();
				executedRunnables.addAll(runnables);
				runnables.clear();
			}
			foreach (Runnable runnable in executedRunnables) {
				runnable.Invoke();
			}
			if (shouldRequestRendering) {
				// Must follow Runnables execution so changes done by Runnables are reflected
				// in the following render.
				foreach (Lwjgl3Window window in windows) {
					if (!window.getGraphics().isContinuousRendering()) window.requestRendering();
				}
			}

			foreach (Lwjgl3Window closedWindow in closedWindows) {
				if (windows.size == 1) {
					// Lifecycle listener methods have to be called before ApplicationListener methods. The
					// application will be disposed when _all_ windows have been disposed, which is the case,
					// when there is only 1 window left, which is in the process of being disposed.
					for (int i = lifecycleListeners.size - 1; i >= 0; i--) {
						LifecycleListener l = lifecycleListeners.get(i);
						l.pause();
						l.dispose();
					}
					lifecycleListeners.clear();
				}
				closedWindow.dispose();

				windows.removeValue(closedWindow, false);
			}

			if (!haveWindowsRendered) {
				// Sleep a few milliseconds in case no rendering was requested
				// with continuous rendering disabled.
				try {
					Thread.Sleep(1000 / config.idleFPS);
				} catch (ThreadInterruptedException e) {
					// ignore
				}
			} else if (targetFramerate > 0) {
				sync.sync(targetFramerate); // sleep as needed to meet the target framerate
			}
		}
	}

	protected void cleanupWindows () {
		lock (lifecycleListeners) {
			foreach (LifecycleListener lifecycleListener in lifecycleListeners) {
				lifecycleListener.pause();
				lifecycleListener.dispose();
			}
		}
		foreach (Lwjgl3Window window in windows) {
			window.dispose();
		}
		windows.clear();
	}

	protected void cleanup () {
		Lwjgl3Cursor.disposeSystemCursors();
		audio.dispose();
		// TODO: errorCallback.free();
		errorCallback = null;
		if (glDebugCallback != null) {
			// TODO: 	glDebugCallback.free();
				glDebugCallback = null;
		}
		GLFW.glfwTerminate();
	}

	public ApplicationListener getApplicationListener () {
		return currentWindow.getListener();
	}

	public Graphics getGraphics () {
		return currentWindow.getGraphics();
	}

	public SharpGDX.Audio getAudio () {
		return audio;
	}

	public Input getInput () {
		return currentWindow.getInput();
	}

	public Files getFiles () {
		return files;
	}

	public Net getNet () {
		return net;
	}

	public void debug (String tag, String message) {
		if (logLevel >= Application.LOG_DEBUG) getApplicationLogger().debug(tag, message);
	}

	public void debug (String tag, String message, Exception exception) {
		if (logLevel >= Application.LOG_DEBUG) getApplicationLogger().debug(tag, message, exception);
	}

	public void log (String tag, String message) {
		if (logLevel >= Application.LOG_INFO) getApplicationLogger().log(tag, message);
	}

	public void log (String tag, String message, Exception exception) {
		if (logLevel >= Application.LOG_INFO) getApplicationLogger().log(tag, message, exception);
	}

	public void error (String tag, String message) {
		if (logLevel >= Application.LOG_ERROR) getApplicationLogger().error(tag, message);
	}

	public void error (String tag, String message, Exception exception) {
		if (logLevel >= Application.LOG_ERROR) getApplicationLogger().error(tag, message, exception);
	}

	public void setLogLevel (int logLevel) {
		this.logLevel = logLevel;
	}

	public int getLogLevel () {
		return logLevel;
	}

	public void setApplicationLogger (ApplicationLogger applicationLogger) {
		this.applicationLogger = applicationLogger;
	}

	public ApplicationLogger getApplicationLogger () {
		return applicationLogger;
	}

	public Application.ApplicationType getType () {
		return Application.ApplicationType.Desktop;
	}

	public int getVersion () {
		return 0;
	}

	public long getJavaHeap () {
		return GC.GetTotalMemory(false);
	}

	public long getNativeHeap () {
		return getJavaHeap();
	}

	public Preferences getPreferences (String name) {
		if (preferences.containsKey(name)) {
			return preferences.get(name);
		} else {
			Preferences prefs = new Lwjgl3Preferences(
				new Lwjgl3FileHandle(new File(config.preferencesDirectory, name), config.preferencesFileType));
			preferences.put(name, prefs);
			return prefs;
		}
	}

	public Clipboard getClipboard () {
		return clipboard;
	}

	public void postRunnable (Runnable runnable) {
		lock (runnables) {
			runnables.add(runnable);
		}
	}

	public void exit () {
		running = false;
	}

	public void addLifecycleListener (LifecycleListener listener) {
		lock (lifecycleListeners) {
			lifecycleListeners.add(listener);
		}
	}

	public void removeLifecycleListener (LifecycleListener listener) {
		lock (lifecycleListeners) {
			lifecycleListeners.removeValue(listener, true);
		}
	}

	public Lwjgl3Audio createAudio (Lwjgl3ApplicationConfiguration config) {
		return new OpenALLwjgl3Audio(config.audioDeviceSimultaneousSources, config.audioDeviceBufferCount,
			config.audioDeviceBufferSize);
	}

	public Lwjgl3Input createInput (Lwjgl3Window window) {
		return new DefaultLwjgl3Input(window);
	}

	protected Files createFiles () {
		return new Lwjgl3Files();
	}

	/** Creates a new {@link Lwjgl3Window} using the provided listener and {@link Lwjgl3WindowConfiguration}.
	 *
	 * This function only just instantiates a {@link Lwjgl3Window} and returns immediately. The actual window creation is postponed
	 * with {@link Application#postRunnable(Runnable)} until after all existing windows are updated. */
	public Lwjgl3Window newWindow (ApplicationListener listener, Lwjgl3WindowConfiguration config) {
		Lwjgl3ApplicationConfiguration appConfig = Lwjgl3ApplicationConfiguration.copy(this.config);
		appConfig.setWindowConfiguration(config);
		if (appConfig.title == null) appConfig.title = listener.GetType().Name;
		return createWindow(appConfig, listener, windows.get(0).getWindowHandle());
	}

	private Lwjgl3Window createWindow (Lwjgl3ApplicationConfiguration config, ApplicationListener listener,
		long sharedContext) {
		Lwjgl3Window window = new Lwjgl3Window(listener, config, this);
		if (sharedContext == 0) {
			// the main window is created immediately
			createWindow(window, config, sharedContext);
		} else {
			// creation of additional windows is deferred to avoid GL context trouble
			postRunnable(() => {
				createWindow(window, config, sharedContext);
					windows.add(window);
			});
		}
		return window;
	}

	void createWindow (Lwjgl3Window window, Lwjgl3ApplicationConfiguration config, long sharedContext) {
		long windowHandle = createGlfwWindow(config, sharedContext);
		window.create(windowHandle);
		window.setVisible(config.initialVisible);

		for (int i = 0; i < 2; i++) {
			// TODO:
			//GL11.glClearColor(config.initialBackgroundColor.r, config.initialBackgroundColor.g, config.initialBackgroundColor.b,
			//	config.initialBackgroundColor.a);
			//GL11.glClear(GL11.GL_COLOR_BUFFER_BIT);
			GLFW.glfwSwapBuffers(windowHandle);
		}
	}

	static long createGlfwWindow (Lwjgl3ApplicationConfiguration config, long sharedContextWindow) {
		GLFW.glfwDefaultWindowHints();
		GLFW.glfwWindowHint(GLFW.GLFW_VISIBLE, GLFW.GLFW_FALSE);
		GLFW.glfwWindowHint(GLFW.GLFW_RESIZABLE, config.windowResizable ? GLFW.GLFW_TRUE : GLFW.GLFW_FALSE);
		GLFW.glfwWindowHint(GLFW.GLFW_MAXIMIZED, config.windowMaximized ? GLFW.GLFW_TRUE : GLFW.GLFW_FALSE);
		GLFW.glfwWindowHint(GLFW.GLFW_AUTO_ICONIFY, config.autoIconify ? GLFW.GLFW_TRUE : GLFW.GLFW_FALSE);

		GLFW.glfwWindowHint(GLFW.GLFW_RED_BITS, config.r);
		GLFW.glfwWindowHint(GLFW.GLFW_GREEN_BITS, config.g);
		GLFW.glfwWindowHint(GLFW.GLFW_BLUE_BITS, config.b);
		GLFW.glfwWindowHint(GLFW.GLFW_ALPHA_BITS, config.a);
		GLFW.glfwWindowHint(GLFW.GLFW_STENCIL_BITS, config.stencil);
		GLFW.glfwWindowHint(GLFW.GLFW_DEPTH_BITS, config.depth);
		GLFW.glfwWindowHint(GLFW.GLFW_SAMPLES, config.samples);

		if (config.glEmulation == Lwjgl3ApplicationConfiguration.GLEmulation.GL30
			|| config.glEmulation == Lwjgl3ApplicationConfiguration.GLEmulation.GL31
			|| config.glEmulation == Lwjgl3ApplicationConfiguration.GLEmulation.GL32) {
			GLFW.glfwWindowHint(GLFW.GLFW_CONTEXT_VERSION_MAJOR, config.gles30ContextMajorVersion);
			GLFW.glfwWindowHint(GLFW.GLFW_CONTEXT_VERSION_MINOR, config.gles30ContextMinorVersion);
			if (SharedLibraryLoader.isMac) {
				// hints mandatory on OS X for GL 3.2+ context creation, but fail on Windows if the
				// WGL_ARB_create_context extension is not available
				// see: http://www.glfw.org/docs/latest/compat.html
				GLFW.glfwWindowHint(GLFW.GLFW_OPENGL_FORWARD_COMPAT, GLFW.GLFW_TRUE);
				GLFW.glfwWindowHint(GLFW.GLFW_OPENGL_PROFILE, GLFW.GLFW_OPENGL_CORE_PROFILE);
			}
		} else {
			if (config.glEmulation == Lwjgl3ApplicationConfiguration.GLEmulation.ANGLE_GLES20) {
				GLFW.glfwWindowHint(GLFW.GLFW_CONTEXT_CREATION_API, GLFW.GLFW_EGL_CONTEXT_API);
				GLFW.glfwWindowHint(GLFW.GLFW_CLIENT_API, GLFW.GLFW_OPENGL_ES_API);
				GLFW.glfwWindowHint(GLFW.GLFW_CONTEXT_VERSION_MAJOR, 2);
				GLFW.glfwWindowHint(GLFW.GLFW_CONTEXT_VERSION_MINOR, 0);
			}
		}

		if (config.transparentFramebuffer) {
			GLFW.glfwWindowHint(GLFW.GLFW_TRANSPARENT_FRAMEBUFFER, GLFW.GLFW_TRUE);
		}

		if (config.debug) {
			GLFW.glfwWindowHint(GLFW.GLFW_OPENGL_DEBUG_CONTEXT, GLFW.GLFW_TRUE);
		}

		long windowHandle = 0;

		if (config.fullscreenMode != null) {
			GLFW.glfwWindowHint(GLFW.GLFW_REFRESH_RATE, config.fullscreenMode.refreshRate);
			windowHandle = GLFW.glfwCreateWindow(config.fullscreenMode.width, config.fullscreenMode.height, config.title,
				config.fullscreenMode.getMonitor(), sharedContextWindow);
		} else {
			GLFW.glfwWindowHint(GLFW.GLFW_DECORATED, config.windowDecorated ? GLFW.GLFW_TRUE : GLFW.GLFW_FALSE);
			windowHandle = GLFW.glfwCreateWindow(config.windowWidth, config.windowHeight, config.title, 0, sharedContextWindow);
		}
		if (windowHandle == 0) {
			throw new GdxRuntimeException("Couldn't create window");
		}
		Lwjgl3Window.setSizeLimits(windowHandle, config.windowMinWidth, config.windowMinHeight, config.windowMaxWidth,
			config.windowMaxHeight);
		if (config.fullscreenMode == null) {
			if (config.windowX == -1 && config.windowY == -1) { // i.e., center the window
				int windowWidth = Math.Max(config.windowWidth, config.windowMinWidth);
				int windowHeight = Math.Max(config.windowHeight, config.windowMinHeight);
				if (config.windowMaxWidth > -1) windowWidth = Math.Min(windowWidth, config.windowMaxWidth);
				if (config.windowMaxHeight > -1) windowHeight = Math.Min(windowHeight, config.windowMaxHeight);

				long monitorHandle = GLFW.glfwGetPrimaryMonitor();
				if (config.windowMaximized && config.maximizedMonitor != null) {
					monitorHandle = config.maximizedMonitor.monitorHandle;
				}

				GridPoint2 newPos = Lwjgl3ApplicationConfiguration.calculateCenteredWindowPosition(
					Lwjgl3ApplicationConfiguration.toLwjgl3Monitor(monitorHandle), windowWidth, windowHeight);
				GLFW.glfwSetWindowPos(windowHandle, newPos.x, newPos.y);
			} else {
				GLFW.glfwSetWindowPos(windowHandle, config.windowX, config.windowY);
			}

			if (config.windowMaximized) {
				GLFW.glfwMaximizeWindow(windowHandle);
			}
		}
		if (config.windowIconPaths != null) {
			//Lwjgl3Window.setIcon(windowHandle, config.windowIconPaths, config.windowIconFileType);
			throw new NotImplementedException();
		}
		GLFW.glfwMakeContextCurrent(windowHandle);
		GLFW.glfwSwapInterval(config.vSyncEnabled ? 1 : 0);
		if (config.glEmulation == Lwjgl3ApplicationConfiguration.GLEmulation.ANGLE_GLES20) {
			try {
				//Class gles = Class.forName("org.lwjgl.opengles.GLES");
				//gles.getMethod("createCapabilities").invoke(gles);
				//// TODO: Remove once https://github.com/LWJGL/lwjgl3/issues/931 is fixed
				//ThreadLocalUtil.setFunctionMissingAddresses(0);
				throw new NotImplementedException();
			} catch (Exception e) {
				throw new GdxRuntimeException("Couldn't initialize GLES", e);
			}
		} else {
			GL.createCapabilities();
		}

		initiateGL(config.glEmulation == Lwjgl3ApplicationConfiguration.GLEmulation.ANGLE_GLES20);
		if (!glVersion.IsVersionEqualToOrHigher(2, 0))
		{
			throw new NotImplementedException();
			//throw new GdxRuntimeException("OpenGL 2.0 or higher with the FBO extension is required. OpenGL version: "
		 //                              + GL11.glGetString(GL11.GL_VERSION) + "\n" + glVersion.getDebugVersionString());

		}

		if (config.glEmulation != Lwjgl3ApplicationConfiguration.GLEmulation.ANGLE_GLES20 && !supportsFBO())
		{
			throw new NotImplementedException();
			//throw new GdxRuntimeException("OpenGL 2.0 or higher with the FBO extension is required. OpenGL version: "
			//	+ GL11.glGetString(GL11.GL_VERSION) + ", FBO extension: false\n" + glVersion.getDebugVersionString());
		}

		if (config.debug)
		{
			throw new NotImplementedException();
			//glDebugCallback = GLUtil.setupDebugMessageCallback(config.debugStream);
			//setGLDebugMessageControl(GLDebugMessageSeverity.NOTIFICATION, false);
		}

		return windowHandle;
	}

	private static void initiateGL (bool useGLES20) {
		if (!useGLES20) {
				String versionString = GL.glGetString(GL11.GL_VERSION);
				String vendorString = GL.glGetString(GL11.GL_VENDOR);
				String rendererString = GL.glGetString(GL11.GL_RENDERER);
				glVersion = new GLVersion(Application.ApplicationType.Desktop, versionString, vendorString, rendererString);
			} else {
			try {
				//Class gles = Class.forName("org.lwjgl.opengles.GLES20");
				//Method getString = gles.getMethod("glGetString", int.class);
				//String versionString = (String)getString.invoke(gles, GL11.GL_VERSION);
				//String vendorString = (String)getString.invoke(gles, GL11.GL_VENDOR);
				//String rendererString = (String)getString.invoke(gles, GL11.GL_RENDERER);
				//glVersion = new GLVersion(Application.ApplicationType.Desktop, versionString, vendorString, rendererString);
				throw new NotImplementedException();
			} catch (Exception e) {
				throw new GdxRuntimeException("Couldn't get GLES version string.", e);
			}
		}
	}

	private static bool supportsFBO () {
		// FBO is in core since OpenGL 3.0, see https://www.opengl.org/wiki/Framebuffer_Object
		return glVersion.IsVersionEqualToOrHigher(3, 0) || GLFW.glfwExtensionSupported("GL_EXT_framebuffer_object")
			|| GLFW.glfwExtensionSupported("GL_ARB_framebuffer_object");
	}

	//public enum GLDebugMessageSeverity {
	//	HIGH(GL43.GL_DEBUG_SEVERITY_HIGH, KHRDebug.GL_DEBUG_SEVERITY_HIGH, ARBDebugOutput.GL_DEBUG_SEVERITY_HIGH_ARB,
	//		AMDDebugOutput.GL_DEBUG_SEVERITY_HIGH_AMD), MEDIUM(GL43.GL_DEBUG_SEVERITY_MEDIUM, KHRDebug.GL_DEBUG_SEVERITY_MEDIUM,
	//			ARBDebugOutput.GL_DEBUG_SEVERITY_MEDIUM_ARB, AMDDebugOutput.GL_DEBUG_SEVERITY_MEDIUM_AMD), LOW(
	//				GL43.GL_DEBUG_SEVERITY_LOW, KHRDebug.GL_DEBUG_SEVERITY_LOW, ARBDebugOutput.GL_DEBUG_SEVERITY_LOW_ARB,
	//				AMDDebugOutput.GL_DEBUG_SEVERITY_LOW_AMD), NOTIFICATION(GL43.GL_DEBUG_SEVERITY_NOTIFICATION,
	//					KHRDebug.GL_DEBUG_SEVERITY_NOTIFICATION, -1, -1);

	//	final int gl43, khr, arb, amd;

	//	GLDebugMessageSeverity (int gl43, int khr, int arb, int amd) {
	//		this.gl43 = gl43;
	//		this.khr = khr;
	//		this.arb = arb;
	//		this.amd = amd;
	//	}
	//}

	///** Enables or disables GL debug messages for the specified severity level. Returns false if the severity level could not be
	// * set (e.g. the NOTIFICATION level is not supported by the ARB and AMD extensions).
	// *
	// * See {@link Lwjgl3ApplicationConfiguration#enableGLDebugOutput(bool, PrintStream)} */
	//public static bool setGLDebugMessageControl (Lwjgl3Application.GLDebugMessageSeverity severity, bool enabled) {
	//	GLCapabilities caps = GL.getCapabilities();
	//	final int GL_DONT_CARE = 0x1100; // not defined anywhere yet

	//	if (caps.OpenGL43) {
	//		GL43.glDebugMessageControl(GL_DONT_CARE, GL_DONT_CARE, severity.gl43, (IntBuffer)null, enabled);
	//		return true;
	//	}

	//	if (caps.GL_KHR_debug) {
	//		KHRDebug.glDebugMessageControl(GL_DONT_CARE, GL_DONT_CARE, severity.khr, (IntBuffer)null, enabled);
	//		return true;
	//	}

	//	if (caps.GL_ARB_debug_output && severity.arb != -1) {
	//		ARBDebugOutput.glDebugMessageControlARB(GL_DONT_CARE, GL_DONT_CARE, severity.arb, (IntBuffer)null, enabled);
	//		return true;
	//	}

	//	if (caps.GL_AMD_debug_output && severity.amd != -1) {
	//		AMDDebugOutput.glDebugMessageEnableAMD(GL_DONT_CARE, severity.amd, (IntBuffer)null, enabled);
	//		return true;
	//	}

	//	return false;
	//}

}
}
