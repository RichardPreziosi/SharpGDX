using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SharpGDX.GLFW3;
using SharpGDX;
using SharpGDX.Desktop;
using SharpGDX.Shims;
using SharpGDX.Utils;

namespace SharpGDX.Desktop
{
	public class Lwjgl3Window : Disposable {
	private long windowHandle;
	readonly ApplicationListener listener;
	readonly Lwjgl3ApplicationBase application;
	private bool listenerInitialized = false;
	Lwjgl3WindowListener windowListener;
	private Lwjgl3Graphics graphics;
	private Lwjgl3Input input;
	private readonly Lwjgl3ApplicationConfiguration config;
	private readonly Array<Runnable> runnables = new Array<Runnable>();
	private readonly Array<Runnable> executedRunnables = new Array<Runnable>();
	private readonly IntBuffer tmpBuffer;
	private readonly IntBuffer tmpBuffer2;
	bool iconified = false;
	bool focused = false;
	private bool _requestRendering = false;

	private GLFWWindowFocusCallback _focusCallback;

	private GLFWWindowIconifyCallback iconifyCallback;

	private void maximizeCallback(long windowHandle,  bool maximized) {
			postRunnable(() => {
				if (windowListener != null) {
						windowListener.maximized(maximized);
					}
				
			});
		}


		private GLFWWindowCloseCallback closeCallback;
	

	private void dropCallback (long windowHandle, int count, long names) {
			String[] files = new String[count];
			for (int i = 0; i < count; i++) {
				// TODO: Do these need to be freed?
				files[i] = Marshal.PtrToStringUTF8(Marshal.ReadIntPtr(names, i * IntPtr.Size));
			}
			postRunnable(() => {
				
					if (windowListener != null) {
						windowListener.filesDropped(files);
					}
			});
		}

		private GLFWWindowRefreshCallback refreshCallback;

	internal Lwjgl3Window (ApplicationListener listener, Lwjgl3ApplicationConfiguration config, Lwjgl3ApplicationBase application) {
		this.listener = listener;
		this.windowListener = config.windowListener;
		this.config = config;
		this.application = application;

			// TODO: This was originally BufferUtils.createIntBuffer, not sure if this is right.
			this.tmpBuffer = IntBuffer.allocate(1);
		this.tmpBuffer2 = IntBuffer.allocate(1);
	}

	internal void create (long windowHandle) {
		this.windowHandle = windowHandle;
		this.input = application.createInput(this);
		this.graphics = new Lwjgl3Graphics(this);

		GLFW.glfwSetWindowFocusCallback
		(
			windowHandle,
			_focusCallback = (_, focused) => postRunnable(() =>
			{

				if (windowListener != null)
				{
					if (focused)
					{
						windowListener.focusGained();
					}
					else
					{
						windowListener.focusLost();
					}

					this.focused = focused;
				}
			})
		);

		GLFW.glfwSetWindowIconifyCallback
		(
			windowHandle,
			iconifyCallback = (_, iconified) => postRunnable(() =>
			{

				if (windowListener != null)
				{
					windowListener.iconified(iconified);
				}

				this.iconified = iconified;
				if (iconified)
				{
					listener.pause();
				}
				else
				{
					listener.resume();
				}
			})
		);

		GLFW.glfwSetWindowMaximizeCallback(windowHandle, maximizeCallback);

		GLFW.glfwSetWindowCloseCallback
		(
			windowHandle,
			closeCallback = (_) => postRunnable(() =>
			{

				if (windowListener != null)
				{
					if (!windowListener.closeRequested())
					{
						GLFW.glfwSetWindowShouldClose(windowHandle, false);
					}
				}

			})
		);

		GLFW.glfwSetDropCallback(windowHandle, dropCallback);

		GLFW.glfwSetWindowRefreshCallback
		(
			windowHandle,
			refreshCallback = (_) => postRunnable(() =>
			{
				if (windowListener != null)
				{
					windowListener.refreshRequested();
				}

			})
		);

		if (windowListener != null) {
			windowListener.created(this);
		}
	}

	/** @return the {@link ApplicationListener} associated with this window **/
	public ApplicationListener getListener () {
		return listener;
	}

	/** @return the {@link Lwjgl3WindowListener} set on this window **/
	public Lwjgl3WindowListener getWindowListener () {
		return windowListener;
	}

	public void setWindowListener (Lwjgl3WindowListener listener) {
		this.windowListener = listener;
	}

	/** Post a {@link Runnable} to this window's event queue. Use this if you access statics like {@link Gdx#graphics} in your
	 * runnable instead of {@link Application#postRunnable(Runnable)}. */
	public void postRunnable (Runnable runnable) {
		lock (runnables) {
			runnables.add(runnable);
		}
	}

	/** Sets the position of the window in logical coordinates. All monitors span a virtual surface together. The coordinates are
	 * relative to the first monitor in the virtual surface. **/
	public void setPosition (int x, int y) {
		GLFW.glfwSetWindowPos(windowHandle, x, y);
	}

	/** @return the window position in logical coordinates. All monitors span a virtual surface together. The coordinates are
	 *         relative to the first monitor in the virtual surface. **/
	public int getPositionX () {
		GLFW.glfwGetWindowPos(windowHandle, out var x, out var y);
		return x;
	}

	/** @return the window position in logical coordinates. All monitors span a virtual surface together. The coordinates are
	 *         relative to the first monitor in the virtual surface. **/
	public int getPositionY () {
		GLFW.glfwGetWindowPos(windowHandle, out var x, out var y);
		return y;
	}

	/** Sets the visibility of the window. Invisible windows will still call their {@link ApplicationListener} */
	public void setVisible (bool visible) {
		if (visible) {
			GLFW.glfwShowWindow(windowHandle);
		} else {
			GLFW.glfwHideWindow(windowHandle);
		}
	}

	/** Closes this window and pauses and disposes the associated {@link ApplicationListener}. */
	public void closeWindow () {
		GLFW.glfwSetWindowShouldClose(windowHandle, true);
	}

	/** Minimizes (iconifies) the window. Iconified windows do not call their {@link ApplicationListener} until the window is
	 * restored. */
	public void iconifyWindow () {
		GLFW.glfwIconifyWindow(windowHandle);
	}

	/** Whether the window is iconfieid */
	public bool isIconified () {
		return iconified;
	}

	/** De-minimizes (de-iconifies) and de-maximizes the window. */
	public void restoreWindow () {
		GLFW.glfwRestoreWindow(windowHandle);
	}

	/** Maximizes the window. */
	public void maximizeWindow () {
		GLFW.glfwMaximizeWindow(windowHandle);
	}

	/** Brings the window to front and sets input focus. The window should already be visible and not iconified. */
	public void focusWindow () {
		GLFW.glfwFocusWindow(windowHandle);
	}

	public bool isFocused () {
		return focused;
	}

	/** Sets the icon that will be used in the window's title bar. Has no effect in macOS, which doesn't use window icons.
	 * @param image One or more images. The one closest to the system's desired size will be scaled. Good sizes include 16x16,
	 *           32x32 and 48x48. Pixmap format {@link com.badlogic.gdx.graphics.Pixmap.Format#RGBA8888 RGBA8888} is preferred so
	 *           the images will not have to be copied and converted. The chosen image is copied, and the provided Pixmaps are not
	 *           disposed. */
	public void setIcon (Pixmap[] image) {
		setIcon(windowHandle, image);
	}

	static void setIcon (long windowHandle, String[] imagePaths, Files.FileType imageFileType) {
		if (SharedLibraryLoader.isMac) return;

		Pixmap[] pixmaps = new Pixmap[imagePaths.Length];
		for (int i = 0; i < imagePaths.Length; i++) {
			pixmaps[i] = new Pixmap(Gdx.files.getFileHandle(imagePaths[i], imageFileType));
		}

		setIcon(windowHandle, pixmaps);

		foreach (Pixmap pixmap in pixmaps) {
			pixmap.dispose();
		}
	}

	static void setIcon (long windowHandle, Pixmap[] images) {
		//if (SharedLibraryLoader.isMac) return;

		//GLFWImage.Buffer buffer = GLFWImage.malloc(images.Length);
		//Pixmap[] tmpPixmaps = new Pixmap[images.length];

		//for (int i = 0; i < images.length; i++) {
		//	Pixmap pixmap = images[i];

		//	if (pixmap.getFormat() != Pixmap.Format.RGBA8888) {
		//		Pixmap rgba = new Pixmap(pixmap.getWidth(), pixmap.getHeight(), Pixmap.Format.RGBA8888);
		//		rgba.setBlending(Pixmap.Blending.None);
		//		rgba.drawPixmap(pixmap, 0, 0);
		//		tmpPixmaps[i] = rgba;
		//		pixmap = rgba;
		//	}

		//	GLFWImage icon = GLFWImage.malloc();
		//	icon.set(pixmap.getWidth(), pixmap.getHeight(), pixmap.getPixels());
		//	buffer.put(icon);

		//	icon.free();
		//}

		//buffer.position(0);
		//GLFW.glfwSetWindowIcon(windowHandle, buffer);

		//buffer.free();
		//foreach (Pixmap pixmap in tmpPixmaps) {
		//	if (pixmap != null) {
		//		pixmap.dispose();
		//	}
		//}

	}

	public void setTitle (string title) {
		GLFW.glfwSetWindowTitle(windowHandle, title);
	}

	/** Sets minimum and maximum size limits for the window. If the window is full screen or not resizable, these limits are
	 * ignored. Use -1 to indicate an unrestricted dimension. */
	public void setSizeLimits (int minWidth, int minHeight, int maxWidth, int maxHeight) {
		setSizeLimits(windowHandle, minWidth, minHeight, maxWidth, maxHeight);
	}

	internal static void setSizeLimits (long windowHandle, int minWidth, int minHeight, int maxWidth, int maxHeight) {
		GLFW.glfwSetWindowSizeLimits(windowHandle, minWidth > -1 ? minWidth : GLFW.GLFW_DONT_CARE,
			minHeight > -1 ? minHeight : GLFW.GLFW_DONT_CARE, maxWidth > -1 ? maxWidth : GLFW.GLFW_DONT_CARE,
			maxHeight > -1 ? maxHeight : GLFW.GLFW_DONT_CARE);
	}

	internal Lwjgl3Graphics getGraphics () {
		return graphics;
	}

	internal Lwjgl3Input getInput () {
		return input;
	}

	public long getWindowHandle () {
		return windowHandle;
	}

	void windowHandleChanged (long windowHandle) {
		this.windowHandle = windowHandle;
		input.windowHandleChanged(windowHandle);
	}

	internal bool update () {
		if (!listenerInitialized) {
			initializeListener();
		}
		lock (runnables) {
			executedRunnables.addAll(runnables);
			runnables.clear();
		}
		foreach (Runnable runnable in executedRunnables) {
			runnable.Invoke();
		}
		bool shouldRender = executedRunnables.size > 0 || graphics.isContinuousRendering();
		executedRunnables.clear();

		if (!iconified) input.update();

		lock (this) {
			shouldRender |= _requestRendering && !iconified;
			_requestRendering = false;
		}

		if (shouldRender) {
			graphics.update();
			listener.render();
			GLFW.glfwSwapBuffers(windowHandle);
		}

		if (!iconified) input.prepareNext();

		return shouldRender;
	}

	internal void requestRendering () {
		lock (this) {
			this._requestRendering = true;
		}
	}

	internal bool shouldClose () {
		return GLFW.glfwWindowShouldClose(windowHandle) == GLFW.GLFW_TRUE;
	}

	internal Lwjgl3ApplicationConfiguration getConfig () {
		return config;
	}

	internal bool isListenerInitialized () {
		return listenerInitialized;
	}

	void initializeListener () {
		if (!listenerInitialized) {
			listener.create();
			listener.resize(graphics.getWidth(), graphics.getHeight());
			listenerInitialized = true;
		}
	}

	internal void makeCurrent () {
		Gdx.graphics = graphics;
		Gdx.gl32 = graphics.getGL32();
		Gdx.gl31 = Gdx.gl32 != null ? Gdx.gl32 : graphics.getGL31();
		Gdx.gl30 = Gdx.gl31 != null ? Gdx.gl31 : graphics.getGL30();
		Gdx.gl20 = Gdx.gl30 != null ? Gdx.gl30 : graphics.getGL20();
		Gdx.gl = Gdx.gl20;
		Gdx.input = input;

		GLFW.glfwMakeContextCurrent(windowHandle);
	}

	public void dispose () {
		listener.pause();
		listener.dispose();
		Lwjgl3Cursor.dispose(this);
		graphics.dispose();
		input.dispose();

		GLFW.glfwSetWindowFocusCallback(windowHandle, null);
		GLFW.glfwSetWindowIconifyCallback(windowHandle, null);
		GLFW.glfwSetWindowMaximizeCallback(windowHandle, null);
		GLFW.glfwSetWindowCloseCallback(windowHandle, null);
		GLFW.glfwSetDropCallback(windowHandle, null);
		GLFW.glfwSetWindowRefreshCallback(windowHandle, null);

		GLFW.glfwDestroyWindow(windowHandle);
		}

	public override int GetHashCode () {
		 int prime = 31;
		int result = 1;
		result = prime * result + (int)(windowHandle ^ (windowHandle >>> 32));
		return result;
	}

	public override bool Equals (Object? obj) {
		if (this == obj) return true;
		if (obj == null) return false;
		if (GetType() != obj.GetType()) return false;
		Lwjgl3Window other = (Lwjgl3Window)obj;
		if (windowHandle != other.windowHandle) return false;
		return true;
	}

	public void flash () {
		GLFW.glfwRequestWindowAttention(windowHandle);
	}
}
}
