using SharpGDX.graphics;
using SharpGDX.graphics.glutils;
using SharpGDX.utils;
using static SharpGDX.graphics.Cursor;
using static SharpGDX.Graphics;
using Monitor = SharpGDX.Graphics.Monitor;

namespace SharpGDX.Headless.mock.graphics;

/**
 * The headless backend does its best to mock elements. This is intended to make code-sharing between server and client as simple
 * as possible.
 */
public class MockGraphics : AbstractGraphics
{
	private float deltaTime;
	private int fps;
	private long frameId = -1;
	private int frames;
	private long frameStart;
	private readonly GLVersion glVersion = new(Application.ApplicationType.HeadlessDesktop, "", "", "");
	private long lastTime = TimeUtils.nanoTime();
	private long targetRenderInterval;

	public override int getBackBufferHeight()
	{
		return 0;
	}

	public override int getBackBufferWidth()
	{
		return 0;
	}

	public override BufferFormat getBufferFormat()
	{
		return null;
	}

	public override float getDeltaTime()
	{
		return deltaTime;
	}

	public override DisplayMode getDisplayMode()
	{
		return null;
	}

	public override DisplayMode getDisplayMode(Monitor monitor)
	{
		return null;
	}

	public override DisplayMode[] getDisplayModes()
	{
		return new DisplayMode[0];
	}

	public override DisplayMode[] getDisplayModes(Monitor monitor)
	{
		return null;
	}

	public override long getFrameId()
	{
		return frameId;
	}

	public override int getFramesPerSecond()
	{
		return fps;
	}

	public override GL20 getGL20()
	{
		return null;
	}

	public override GL30 getGL30()
	{
		return null;
	}

	public override GL31 getGL31()
	{
		return null;
	}

	public override GL32 getGL32()
	{
		return null;
	}

	public override GLVersion getGLVersion()
	{
		return glVersion;
	}

	public override int getHeight()
	{
		return 0;
	}

	public override Monitor getMonitor()
	{
		return null;
	}

	public override Monitor[] getMonitors()
	{
		return null;
	}

	public override float getPpcX()
	{
		return 0;
	}

	public override float getPpcY()
	{
		return 0;
	}

	public override float getPpiX()
	{
		return 0;
	}

	public override float getPpiY()
	{
		return 0;
	}

	public override Monitor getPrimaryMonitor()
	{
		return null;
	}

	public override int getSafeInsetBottom()
	{
		return 0;
	}

	public override int getSafeInsetLeft()
	{
		return 0;
	}

	public override int getSafeInsetRight()
	{
		return 0;
	}

	public override int getSafeInsetTop()
	{
		return 0;
	}

	public long getTargetRenderInterval()
	{
		return targetRenderInterval;
	}

	public override GraphicsType getType()
	{
		return GraphicsType.Mock;
	}

	public override int getWidth()
	{
		return 0;
	}

	public void incrementFrameId()
	{
		frameId++;
	}

	public override bool isContinuousRendering()
	{
		return false;
	}

	public override bool isFullscreen()
	{
		return false;
	}

	public override bool isGL30Available()
	{
		return false;
	}

	public override bool isGL31Available()
	{
		return false;
	}

	public override bool isGL32Available()
	{
		return false;
	}

	public override Cursor newCursor(Pixmap pixmap, int xHotspot, int yHotspot)
	{
		return null;
	}

	public override void requestRendering()
	{
	}

	public override void setContinuousRendering(bool isContinuous)
	{
	}

	public override void setCursor(Cursor cursor)
	{
	}

	/**
	 * Sets the target framerate for the application. Use 0 to never sleep; negative to not call the render method at all. Default
	 * is 60.
	 * 
	 * @param fps fps
	 */
	public override void setForegroundFPS(int fps)
	{
		targetRenderInterval = (long)(fps <= 0 ? fps == 0 ? 0 : -1 : 1F / fps * 1000000000F);
	}

	public override bool setFullscreenMode(DisplayMode displayMode)
	{
		return false;
	}

	public override void setGL20(GL20 gl20)
	{
	}

	public override void setGL30(GL30 gl30)
	{
	}

	public override void setGL31(GL31 gl31)
	{
	}

	public override void setGL32(GL32 gl32)
	{
	}

	public override void setResizable(bool resizable)
	{
	}

	public override void setSystemCursor(SystemCursor systemCursor)
	{
	}

	public override void setTitle(string title)
	{
	}

	public override void setUndecorated(bool undecorated)
	{
	}

	public override void setVSync(bool vsync)
	{
	}

	public override bool setWindowedMode(int width, int height)
	{
		return false;
	}

	public override bool supportsDisplayModeChange()
	{
		return false;
	}

	public override bool supportsExtension(string extension)
	{
		return false;
	}

	public void updateTime()
	{
		var time = TimeUtils.nanoTime();
		deltaTime = (time - lastTime) / 1000000000.0f;
		lastTime = time;

		if (time - frameStart >= 1000000000)
		{
			fps = frames;
			frames = 0;
			frameStart = time;
		}

		frames++;
	}
}