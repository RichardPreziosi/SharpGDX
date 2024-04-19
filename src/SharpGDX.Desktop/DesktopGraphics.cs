using SharpGDX.Desktop;
using Monitor = SharpGDX.Graphics.Monitor;
using SharpGDX.Mathematics;
using SharpGDX.Shims;
using SharpGDX.Utils;
using SharpGDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static SharpGDX.Cursor;
using static SharpGDX.Graphics;

namespace SharpGDX.Desktop
{
	public class Lwjgl3Graphics : AbstractGraphics , Disposable
	{
		readonly Lwjgl3Window window;
		GL20 gl20;
	private GL30 gl30;
	private GL31 gl31;
	private GL32 gl32;
	private GLVersion glVersion;
	private volatile int backBufferWidth;
	private volatile int backBufferHeight;
	private volatile int logicalWidth;
	private volatile int logicalHeight;
	private volatile bool isContinuous = true;
	private BufferFormat bufferFormat;
	private long lastFrameTime = -1;
	private float deltaTime;
	private bool _resetDeltaTime = false;
	private long frameId;
	private long frameCounterStart = 0;
	private int frames;
	private int fps;
	private int windowPosXBeforeFullscreen;
	private int windowPosYBeforeFullscreen;
	private int windowWidthBeforeFullscreen;
	private int windowHeightBeforeFullscreen;
	private DisplayMode displayModeBeforeFullscreen = null;

		// TODO: this was originally BufferUtils.createIntBuffer, not sure if this works
		IntBuffer tmpBuffer = IntBuffer.allocate(1);
	IntBuffer tmpBuffer2 = IntBuffer.allocate(1);

	private volatile bool posted;

	private void resizeCallback(long windowHandle,  int width,  int height)
	{
		// TODO: Implement, this might really only be a Java(lwjgl) thing.
		// TODO: This was used to ensure that glfwInit was called on the first thread of the JVM process.
		//if (Configuration.GLFW_CHECK_THREAD0.get(true))
		//{
		//	renderWindow(windowHandle, width, height);
		//}
		//else
		{
			if (posted) return;
			posted = true;
			Gdx.app.postRunnable(() => {
					
				posted = false;
				renderWindow(windowHandle, width, height);
			
		});
	}
}

private void renderWindow(long windowHandle,  int width,  int height)
{
	updateFramebufferInfo();
	if (!window.isListenerInitialized())
	{
		return;
	}
	window.makeCurrent();
	gl20.glViewport(0, 0, backBufferWidth, backBufferHeight);
	window.getListener().resize(getWidth(), getHeight());
	window.getListener().render();
	GLFW.glfwSwapBuffers(windowHandle);
}

public Lwjgl3Graphics (Lwjgl3Window window)
{
	this.window = window;
	if (window.getConfig().glEmulation == Lwjgl3ApplicationConfiguration.GLEmulation.GL32)
	{
		this.gl20 = this.gl30 = this.gl31 = this.gl32 = new Lwjgl3GL32();
	}
	else if (window.getConfig().glEmulation == Lwjgl3ApplicationConfiguration.GLEmulation.GL31)
	{
		this.gl20 = this.gl30 = this.gl31 = new Lwjgl3GL31();
	}
	else if (window.getConfig().glEmulation == Lwjgl3ApplicationConfiguration.GLEmulation.GL30)
	{
		this.gl20 = this.gl30 = new Lwjgl3GL30();
	}
	else
	{
		try
		{
			this.gl20 = window.getConfig().glEmulation == Lwjgl3ApplicationConfiguration.GLEmulation.GL20
				? new Lwjgl3GL20()
				: throw new NotImplementedException();// TODO: (GL20)Class.forName("com.badlogic.gdx.backends.lwjgl3.angle.Lwjgl3GLES20").newInstance();
		}
		catch (Exception t)
		{
			throw new GdxRuntimeException("Couldn't instantiate GLES20.", t);
		}
		this.gl30 = null;
	}
	updateFramebufferInfo();
	initiateGL();
	GLFW.glfwSetFramebufferSizeCallback(window.getWindowHandle(), resizeCallback);
}

private void initiateGL()
{
	String versionString = gl20.glGetString(GL11.GL_VERSION);
	String vendorString = gl20.glGetString(GL11.GL_VENDOR);
	String rendererString = gl20.glGetString(GL11.GL_RENDERER);
	glVersion = new GLVersion(Application.ApplicationType.Desktop, versionString, vendorString, rendererString);
	if (supportsCubeMapSeamless())
	{
		enableCubeMapSeamless(true);
	}
}

/** @return whether cubemap seamless feature is supported. */
public bool supportsCubeMapSeamless()
{
	return glVersion.IsVersionEqualToOrHigher(3, 2) || supportsExtension("GL_ARB_seamless_cube_map");
}

/** Enable or disable cubemap seamless feature. Default is true if supported. Should only be called if this feature is
 * supported. (see {@link #supportsCubeMapSeamless()})
 * @param enable */
public void enableCubeMapSeamless(bool enable)
{
	if (enable)
	{
		gl20.glEnable(GL32.GL_TEXTURE_CUBE_MAP_SEAMLESS);
	}
	else
	{
		gl20.glDisable(GL32.GL_TEXTURE_CUBE_MAP_SEAMLESS);
	}
}

public Lwjgl3Window getWindow()
{
	return window;
}

void updateFramebufferInfo()
{
	GLFW.glfwGetFramebufferSize(window.getWindowHandle(), tmpBuffer, tmpBuffer2);
	this.backBufferWidth = tmpBuffer.get(0);
	this.backBufferHeight = tmpBuffer2.get(0);
	GLFW.glfwGetWindowSize(window.getWindowHandle(), tmpBuffer, tmpBuffer2);
	this.logicalWidth = tmpBuffer.get(0);
	this.logicalHeight = tmpBuffer2.get(0);
	Lwjgl3ApplicationConfiguration config = window.getConfig();
	bufferFormat = new BufferFormat(config.r, config.g, config.b, config.a, config.depth, config.stencil, config.samples,
		false);
}

internal void update()
{
	long time = TimeUtils.nanoTime();
	if (lastFrameTime == -1) lastFrameTime = time;
	if (_resetDeltaTime)
	{
		_resetDeltaTime = false;
		deltaTime = 0;
	}
	else
		deltaTime = (time - lastFrameTime) / 1000000000.0f;
	lastFrameTime = time;

	if (time - frameCounterStart >= 1000000000)
	{
		fps = frames;
		frames = 0;
		frameCounterStart = time;
	}
	frames++;
	frameId++;
}

	public override bool isGL30Available()
{
	return gl30 != null;
}

public override bool isGL31Available()
{
	return gl31 != null;
}

public override bool isGL32Available()
{
	return gl32 != null;
}

public override GL20 getGL20()
{
	return gl20;
}

public override GL30 getGL30()
{
	return gl30;
}

public override GL31 getGL31()
{
	return gl31;
}

public override GL32 getGL32()
{
	return gl32;
}

public override void setGL20(GL20 gl20)
{
	this.gl20 = gl20;
}

public override void setGL30(GL30 gl30)
{
	this.gl30 = gl30;
}

public override void setGL31(GL31 gl31)
{
	this.gl31 = gl31;
}

public override void setGL32(GL32 gl32)
{
	this.gl32 = gl32;
}

public override int getWidth()
{
	if (window.getConfig().hdpiMode == HdpiMode.Pixels)
	{
		return backBufferWidth;
	}
	else
	{
		return logicalWidth;
	}
}

public override int getHeight()
{
	if (window.getConfig().hdpiMode == HdpiMode.Pixels)
	{
		return backBufferHeight;
	}
	else
	{
		return logicalHeight;
	}
}

public override int getBackBufferWidth()
{
	return backBufferWidth;
}

public override int getBackBufferHeight()
{
	return backBufferHeight;
}

public int getLogicalWidth()
{
	return logicalWidth;
}

public int getLogicalHeight()
{
	return logicalHeight;
}

public override long getFrameId()
{
	return frameId;
}

public override float getDeltaTime()
{
	return deltaTime;
}

public void resetDeltaTime()
{
	_resetDeltaTime = true;
}

public override int getFramesPerSecond()
{
	return fps;
}

public override GraphicsType getType()
{
	return GraphicsType.LWJGL3;
}

public override GLVersion getGLVersion()
{
	return glVersion;
}

public override float getPpiX()
{
	return getPpcX() * 2.54f;
}

public override float getPpiY()
{
	return getPpcY() * 2.54f;
}

public override float getPpcX()
{
	Lwjgl3Monitor monitor = (Lwjgl3Monitor)getMonitor();
	GLFW.glfwGetMonitorPhysicalSize(monitor.monitorHandle, tmpBuffer, tmpBuffer2);
	int sizeX = tmpBuffer.get(0);
	DisplayMode mode = getDisplayMode();
	return mode.width / (float)sizeX * 10;
}

public override float getPpcY()
{
	Lwjgl3Monitor monitor = (Lwjgl3Monitor)getMonitor();
	GLFW.glfwGetMonitorPhysicalSize(monitor.monitorHandle, tmpBuffer, tmpBuffer2);
	int sizeY = tmpBuffer2.get(0);
	DisplayMode mode = getDisplayMode();
	return mode.height / (float)sizeY * 10;
}

public override bool supportsDisplayModeChange()
{
	return true;
}

public override Monitor getPrimaryMonitor()
{
	return Lwjgl3ApplicationConfiguration.toLwjgl3Monitor(GLFW.glfwGetPrimaryMonitor());
}

public override Monitor getMonitor()
{
	Monitor[] monitors = getMonitors();
	Monitor result = monitors[0];

	GLFW.glfwGetWindowPos(window.getWindowHandle(), tmpBuffer, tmpBuffer2);
	int windowX = tmpBuffer.get(0);
	int windowY = tmpBuffer2.get(0);
	GLFW.glfwGetWindowSize(window.getWindowHandle(), tmpBuffer, tmpBuffer2);
	int windowWidth = tmpBuffer.get(0);
	int windowHeight = tmpBuffer2.get(0);
	int overlap;
	int bestOverlap = 0;

	foreach (Monitor monitor in monitors) {
			DisplayMode mode = getDisplayMode(monitor);

overlap = Math.Max(0,
	Math.Min(windowX + windowWidth, monitor.virtualX + mode.width) - Math.Max(windowX, monitor.virtualX))
	* Math.Max(0, Math.Min(windowY + windowHeight, monitor.virtualY + mode.height) - Math.Max(windowY, monitor.virtualY));

if (bestOverlap < overlap)
{
	bestOverlap = overlap;
	result = monitor;
}
		}
		return result;
	}

	public override Monitor[] getMonitors()
	{
		throw new NotImplementedException();
		//PointerBuffer glfwMonitors = GLFW.glfwGetMonitors();
		//Monitor[] monitors = new Monitor[glfwMonitors.limit()];
		//for (int i = 0; i < glfwMonitors.limit(); i++)
		//{
		//	monitors[i] = Lwjgl3ApplicationConfiguration.toLwjgl3Monitor(glfwMonitors.get(i));
		//}
		//return monitors;
	}

public override DisplayMode[] getDisplayModes()
{
	return Lwjgl3ApplicationConfiguration.getDisplayModes(getMonitor());
}

public override DisplayMode[] getDisplayModes(Monitor monitor)
{
	return Lwjgl3ApplicationConfiguration.getDisplayModes(monitor);
}

public override DisplayMode getDisplayMode()
{
	return Lwjgl3ApplicationConfiguration.getDisplayMode(getMonitor());
}

public override DisplayMode getDisplayMode(Monitor monitor)
{
	return Lwjgl3ApplicationConfiguration.getDisplayMode(monitor);
}
public override int getSafeInsetLeft()
{
	return 0;
}

public override int getSafeInsetTop()
{
	return 0;
}

public override int getSafeInsetBottom()
{
	return 0;
}

public override int getSafeInsetRight()
{
	return 0;
}

public override bool setFullscreenMode(DisplayMode displayMode)
{
	window.getInput().resetPollingStates();
	Lwjgl3DisplayMode newMode = (Lwjgl3DisplayMode)displayMode;
	if (isFullscreen())
	{
		Lwjgl3DisplayMode currentMode = (Lwjgl3DisplayMode)getDisplayMode();
		if (currentMode.getMonitor() == newMode.getMonitor() && currentMode.refreshRate == newMode.refreshRate)
		{
			// same monitor and refresh rate
			GLFW.glfwSetWindowSize(window.getWindowHandle(), newMode.width, newMode.height);
		}
		else
		{
			// different monitor and/or refresh rate
			GLFW.glfwSetWindowMonitor(window.getWindowHandle(), newMode.getMonitor(), 0, 0, newMode.width, newMode.height,
				newMode.refreshRate);
		}
	}
	else
	{
		// store window position so we can restore it when switching from fullscreen to windowed later
		storeCurrentWindowPositionAndDisplayMode();

		// switch from windowed to fullscreen
		GLFW.glfwSetWindowMonitor(window.getWindowHandle(), newMode.getMonitor(), 0, 0, newMode.width, newMode.height,
			newMode.refreshRate);
	}
	updateFramebufferInfo();

	setVSync(window.getConfig().vSyncEnabled);

	return true;
}

private void storeCurrentWindowPositionAndDisplayMode()
{
	windowPosXBeforeFullscreen = window.getPositionX();
	windowPosYBeforeFullscreen = window.getPositionY();
	windowWidthBeforeFullscreen = logicalWidth;
	windowHeightBeforeFullscreen = logicalHeight;
	displayModeBeforeFullscreen = getDisplayMode();
}

public override bool setWindowedMode(int width, int height)
{
	window.getInput().resetPollingStates();
	if (!isFullscreen())
	{
		GridPoint2 newPos = null;
		bool centerWindow = false;
		if (width != logicalWidth || height != logicalHeight)
		{
			centerWindow = true; // recenter the window since its size changed
			newPos = Lwjgl3ApplicationConfiguration.calculateCenteredWindowPosition((Lwjgl3Monitor)getMonitor(), width, height);
		}
		GLFW.glfwSetWindowSize(window.getWindowHandle(), width, height);
		if (centerWindow)
		{
			window.setPosition(newPos.x, newPos.y); // on macOS the centering has to happen _after_ the new window size was set
		}
	}
	else
	{ // if we were in fullscreen mode, we should consider restoring a previous display mode
		if (displayModeBeforeFullscreen == null)
		{
			storeCurrentWindowPositionAndDisplayMode();
		}
		if (width != windowWidthBeforeFullscreen || height != windowHeightBeforeFullscreen)
		{ // center the window since its size
		  // changed
			GridPoint2 newPos = Lwjgl3ApplicationConfiguration.calculateCenteredWindowPosition((Lwjgl3Monitor)getMonitor(), width,
				height);
			GLFW.glfwSetWindowMonitor(window.getWindowHandle(), 0, newPos.x, newPos.y, width, height,
				displayModeBeforeFullscreen.refreshRate);
		}
		else
		{ // restore previous position
			GLFW.glfwSetWindowMonitor(window.getWindowHandle(), 0, windowPosXBeforeFullscreen, windowPosYBeforeFullscreen, width,
				height, displayModeBeforeFullscreen.refreshRate);
		}
	}
	updateFramebufferInfo();
	return true;
}

public override void setTitle(String title)
{
	if (title == null)
	{
		title = "";
	}
	GLFW.glfwSetWindowTitle(window.getWindowHandle(), title);
}
public override void setUndecorated(bool undecorated)
{
	getWindow().getConfig().setDecorated(!undecorated);
	GLFW.glfwSetWindowAttrib(window.getWindowHandle(), GLFW.GLFW_DECORATED, undecorated ? GLFW.GLFW_FALSE : GLFW.GLFW_TRUE);
}
public override void setResizable(bool resizable)
{
	getWindow().getConfig().setResizable(resizable);
	GLFW.glfwSetWindowAttrib(window.getWindowHandle(), GLFW.GLFW_RESIZABLE, resizable ? GLFW.GLFW_TRUE : GLFW.GLFW_FALSE);
}

public override void setVSync(bool vsync)
{
	getWindow().getConfig().vSyncEnabled = vsync;
	GLFW.glfwSwapInterval(vsync ? 1 : 0);
}

/** Sets the target framerate for the application, when using continuous rendering. Must be positive. The cpu sleeps as needed.
 * Use 0 to never sleep. If there are multiple windows, the value for the first window created is used for all. Default is 0.
 *
 * @param fps fps */
public override void setForegroundFPS(int fps)
{
	getWindow().getConfig().foregroundFPS = fps;
}

public override BufferFormat getBufferFormat()
{
	return bufferFormat;
}
public override bool supportsExtension(String extension)
{
	return GLFW.glfwExtensionSupported(extension);
}

public override void setContinuousRendering(bool isContinuous)
{
	this.isContinuous = isContinuous;
}

public override bool isContinuousRendering()
{
	return isContinuous;
}

public override void requestRendering()
{
	window.requestRendering();
}

public override bool isFullscreen()
{
	return GLFW.glfwGetWindowMonitor(window.getWindowHandle()) != 0;
}

public override Cursor newCursor(Pixmap pixmap, int xHotspot, int yHotspot)
{
	return new Lwjgl3Cursor(getWindow(), pixmap, xHotspot, yHotspot);
}

public override void setCursor(Cursor cursor)
{
	GLFW.glfwSetCursor(getWindow().getWindowHandle(), ((Lwjgl3Cursor)cursor).glfwCursor);
}

public override void setSystemCursor(SystemCursor systemCursor)
{
	Lwjgl3Cursor.setSystemCursor(getWindow().getWindowHandle(), systemCursor);
}

public void dispose()
{
	// TODO: Set to null
	//this.resizeCallback.free();
}

public  class Lwjgl3DisplayMode : DisplayMode
{
	readonly long monitorHandle;

internal 	Lwjgl3DisplayMode (long monitor, int width, int height, int refreshRate, int bitsPerPixel) 
	: base(width, height, refreshRate, bitsPerPixel)
	{
		
		this.monitorHandle = monitor;
	}

		public long getMonitor()
{
	return monitorHandle;
}
	}

	public  class Lwjgl3Monitor : Monitor
{
	internal readonly long monitorHandle;

internal 	Lwjgl3Monitor (long monitor, int virtualX, int virtualY, String name) 
	: base(virtualX, virtualY, name)
	{
		
		this.monitorHandle = monitor;
	}

		public long getMonitorHandle()
{
	return monitorHandle;
}
	}
}
}
