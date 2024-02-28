using OpenTK.Windowing.GraphicsLibraryFramework;
using SharpGDX.Desktop;
using SharpGDX.graphics;
using SharpGDX.shims;
using SharpGDX.utils;
using System.Runtime.InteropServices;
using GLFWImage = OpenTK.Windowing.GraphicsLibraryFramework.Image;
using GLFWWindow = OpenTK.Windowing.GraphicsLibraryFramework.Window;

namespace SharpGDX.Desktop
{
	public unsafe class DesktopWindow : Disposable
	{
		private GLFWWindow* windowHandle;
		readonly ApplicationListener listener;
		readonly DesktopApplicationBase application;
		private bool listenerInitialized = false;
		DesktopWindowListener windowListener;
		private DesktopGraphics graphics;
		private DesktopInput input;
		private readonly DesktopApplicationConfiguration config;
		private readonly Array<Runnable> runnables = new Array<Runnable>();
		private readonly Array<Runnable> executedRunnables = new Array<Runnable>();
		private readonly IntBuffer tmpBuffer;
		private readonly IntBuffer tmpBuffer2;
		bool iconified = false;
		bool focused = false;
		private bool _requestRendering = false;

		private void focusCallback(GLFWWindow* windowHandle, bool focused)
		{
			postRunnable(() =>
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

			});
		}

		private void iconifyCallback(GLFWWindow* windowHandle, bool iconified)
		{
			postRunnable(() =>
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


			});
		}


		private void maximizeCallback(GLFWWindow* windowHandle, bool maximized)
			{
				postRunnable(() =>
				{
					if (windowListener != null)
					{
					windowListener.maximized(maximized);
				}
				});
			}
		
		private void closeCallback(GLFWWindow* windowHandle)
			{
				postRunnable(() =>
				{
					if (windowListener != null)
					{
					if (!windowListener.closeRequested())
					{
					GLFW.SetWindowShouldClose(windowHandle, false);
				}
				}
				
				});
			}

		private void dropCallback(GLFWWindow* windowHandle, int count, byte** names)
		{
			String[] files = new String[count];
			for (int i = 0; i < count; i++)
			{
				files[i] = Marshal.PtrToStringUTF8((IntPtr)names[i])!;
			}

			postRunnable(() =>
			{
				if (windowListener != null)
				{
					windowListener.filesDropped(files);
				}

			});
		}

		private void refreshCallback(GLFWWindow* windowHandle)
		{
			postRunnable(() =>
			{
				if (windowListener != null)
				{
					windowListener.refreshRequested();
				}
			});
		}

internal 		DesktopWindow(ApplicationListener listener, DesktopApplicationConfiguration config,
			DesktopApplicationBase application)
		{
			this.listener = listener;
			this.windowListener = config.windowListener;
			this.config = config;
			this.application = application;
			this.tmpBuffer = BufferUtils.createIntBuffer(1);
			this.tmpBuffer2 = BufferUtils.createIntBuffer(1);
		}

		internal void create(GLFWWindow* windowHandle)
		{
			this.windowHandle = windowHandle;
			this.input = application.createInput(this);
			this.graphics = new DesktopGraphics(this);

			GLFW.SetWindowFocusCallback(windowHandle, focusCallback);
			GLFW.SetWindowIconifyCallback(windowHandle, iconifyCallback);
			GLFW.SetWindowMaximizeCallback(windowHandle, maximizeCallback);
			GLFW.SetWindowCloseCallback(windowHandle, closeCallback);
			GLFW.SetDropCallback(windowHandle, dropCallback);
			GLFW.SetWindowRefreshCallback(windowHandle, refreshCallback);

			if (windowListener != null)
			{
				windowListener.created(this);
			}
		}

		/** @return the {@link ApplicationListener} associated with this window **/
		public ApplicationListener getListener()
		{
			return listener;
		}

		/** @return the {@link DesktopWindowListener} set on this window **/
		public DesktopWindowListener getWindowListener()
		{
			return windowListener;
		}

		public void setWindowListener(DesktopWindowListener listener)
		{
			this.windowListener = listener;
		}

		/** Post a {@link Runnable} to this window's event queue. Use this if you access statics like {@link Gdx#graphics} in your
		 * runnable instead of {@link Application#postRunnable(Runnable)}. */
		public void postRunnable(Runnable runnable)
		{
			lock (runnables)
			{
				runnables.add(runnable);
			}
		}

		/** Sets the position of the window in logical coordinates. All monitors span a virtual surface together. The coordinates are
		 * relative to the first monitor in the virtual surface. **/
		public void setPosition(int x, int y)
		{
			GLFW.SetWindowPos(windowHandle, x, y);
		}

		/** @return the window position in logical coordinates. All monitors span a virtual surface together. The coordinates are
		 *         relative to the first monitor in the virtual surface. **/
		public int getPositionX()
		{
			GLFW.GetWindowPos(windowHandle, out tmpBuffer[0], out tmpBuffer2[0]);
			return tmpBuffer.get(0);
		}

		/** @return the window position in logical coordinates. All monitors span a virtual surface together. The coordinates are
		 *         relative to the first monitor in the virtual surface. **/
		public int getPositionY()
		{
			GLFW.GetWindowPos(windowHandle, out tmpBuffer[0], out tmpBuffer2[0]);
			return tmpBuffer2.get(0);
		}

		/** Sets the visibility of the window. Invisible windows will still call their {@link ApplicationListener} */
		public void setVisible(bool visible)
		{
			if (visible)
			{
				GLFW.ShowWindow(windowHandle);
			}
			else
			{
				GLFW.HideWindow(windowHandle);
			}
		}

		/** Closes this window and pauses and disposes the associated {@link ApplicationListener}. */
		public void closeWindow()
		{
			GLFW.SetWindowShouldClose(windowHandle, true);
		}

		/** Minimizes (iconifies) the window. Iconified windows do not call their {@link ApplicationListener} until the window is
		 * restored. */
		public void iconifyWindow()
		{
			GLFW.IconifyWindow(windowHandle);
		}

		/** Whether the window is iconfieid */
		public bool isIconified()
		{
			return iconified;
		}

		/** De-minimizes (de-iconifies) and de-maximizes the window. */
		public void restoreWindow()
		{
			GLFW.RestoreWindow(windowHandle);
		}

		/** Maximizes the window. */
		public void maximizeWindow()
		{
			GLFW.MaximizeWindow(windowHandle);
		}

		/** Brings the window to front and sets input focus. The window should already be visible and not iconified. */
		public void focusWindow()
		{
			GLFW.FocusWindow(windowHandle);
		}

		public bool isFocused()
		{
			return focused;
		}

		/** Sets the icon that will be used in the window's title bar. Has no effect in macOS, which doesn't use window icons.
		 * @param image One or more images. The one closest to the system's desired size will be scaled. Good sizes include 16x16,
		 *           32x32 and 48x48. Pixmap format {@link com.badlogic.gdx.graphics.Pixmap.Format#RGBA8888 RGBA8888} is preferred so
		 *           the images will not have to be copied and converted. The chosen image is copied, and the provided Pixmaps are not
		 *           disposed. */
		public void setIcon(Pixmap[] image)
		{
			setIcon(windowHandle, image);
		}

		internal static void setIcon(GLFWWindow* windowHandle, String[] imagePaths, Files.FileType imageFileType)
		{
			// TODO: 
			throw new NotImplementedException();
			//if (SharedLibraryLoader.isMac) return;

			//Pixmap[] pixmaps = new Pixmap[imagePaths.Length];
			//for (int i = 0; i < imagePaths.Length; i++)
			//{
			//	pixmaps[i] = new Pixmap(Gdx.files.getFileHandle(imagePaths[i], imageFileType));
			//}

			//setIcon(windowHandle, pixmaps);

			//foreach (Pixmap pixmap in pixmaps)
			//{
			//	pixmap.dispose();
			//}
		}

		static void setIcon(GLFWWindow* windowHandle, Pixmap[] images)
		{
			// TODO: 
			throw new NotImplementedException();
			//if (SharedLibraryLoader.isMac) return;

			//GLFWImage.Buffer buffer = GLFWImage.malloc(images.Length);
			//Pixmap[] tmpPixmaps = new Pixmap[images.Length];

			//for (int i = 0; i < images.Length; i++)
			//{
			//	Pixmap pixmap = images[i];

			//	if (pixmap.getFormat() != Pixmap.Format.RGBA8888)
			//	{
			//		Pixmap rgba = new Pixmap(pixmap.getWidth(), pixmap.getHeight(), Pixmap.Format.RGBA8888);
			//		rgba.setBlending(Pixmap.Blending.None);
			//		rgba.drawPixmap(pixmap, 0, 0);
			//		tmpPixmaps[i] = rgba;
			//		pixmap = rgba;
			//	}

			//	GLFWImage icon = new GLFWImage();
			//	icon.set(pixmap.getWidth(), pixmap.getHeight(), pixmap.getPixels());
			//	buffer.put(icon);

			//	icon.free();
			//}

			//buffer.position(0);
			//GLFW.SetWindowIcon(windowHandle, buffer);

			//buffer.free();
			//foreach (Pixmap pixmap in tmpPixmaps)
			//{
			//	if (pixmap != null)
			//	{
			//		pixmap.dispose();
			//	}
			//}

		}

		public void setTitle(string title)
		{
			GLFW.SetWindowTitle(windowHandle, title);
		}

		/** Sets minimum and maximum size limits for the window. If the window is full screen or not resizable, these limits are
		 * ignored. Use -1 to indicate an unrestricted dimension. */
		public void setSizeLimits(int minWidth, int minHeight, int maxWidth, int maxHeight)
		{
			setSizeLimits(windowHandle, minWidth, minHeight, maxWidth, maxHeight);
		}

		internal static void setSizeLimits(GLFWWindow* windowHandle, int minWidth, int minHeight, int maxWidth, int maxHeight)
		{
			GLFW.SetWindowSizeLimits(windowHandle, minWidth > -1 ? minWidth : GLFW.DontCare,
				minHeight > -1 ? minHeight : GLFW.DontCare, maxWidth > -1 ? maxWidth : GLFW.DontCare,
				maxHeight > -1 ? maxHeight : GLFW.DontCare);
		}

		internal DesktopGraphics getGraphics()
		{
			return graphics;
		}

		internal DesktopInput getInput()
		{
			return input;
		}

		public GLFWWindow* getWindowHandle()
		{
			return windowHandle;
		}

		void windowHandleChanged(GLFWWindow* windowHandle)
		{
			this.windowHandle = windowHandle;
			input.windowHandleChanged(windowHandle);
		}

		internal bool update()
		{
			if (!listenerInitialized)
			{
				initializeListener();
			}

			lock (runnables)
			{
				executedRunnables.addAll(runnables);
				runnables.clear();
			}

			foreach (Runnable runnable in executedRunnables)
			{
				runnable.Invoke();
			}

			bool shouldRender = executedRunnables.size > 0 || graphics.isContinuousRendering();
			executedRunnables.clear();

			if (!iconified) input.update();

			lock (this)
			{
				shouldRender |= _requestRendering && !iconified;
				_requestRendering = false;
			}

			if (shouldRender)
			{
				graphics.update();
				listener.render();
				GLFW.SwapBuffers(windowHandle);
			}

			if (!iconified) input.prepareNext();

			return shouldRender;
		}

		internal void requestRendering()
		{
			lock (this)
			{
				this._requestRendering = true;
			}
		}

		internal bool shouldClose()
		{
			return GLFW.WindowShouldClose(windowHandle);
		}

		internal DesktopApplicationConfiguration getConfig()
		{
			return config;
		}

		internal bool isListenerInitialized()
		{
			return listenerInitialized;
		}

		void initializeListener()
		{
			if (!listenerInitialized)
			{
				listener.create();
				listener.resize(graphics.getWidth(), graphics.getHeight());
				listenerInitialized = true;
			}
		}

		internal void makeCurrent()
		{
			Gdx.graphics = graphics;
			Gdx.gl32 = graphics.getGL32();
			Gdx.gl31 = Gdx.gl32 != null ? Gdx.gl32 : graphics.getGL31();
			Gdx.gl30 = Gdx.gl31 != null ? Gdx.gl31 : graphics.getGL30();
			Gdx.gl20 = Gdx.gl30 != null ? Gdx.gl30 : graphics.getGL20();
			Gdx.gl = Gdx.gl20;
			Gdx.input = input;

			GLFW.MakeContextCurrent(windowHandle);
		}

		public void dispose()
		{
			listener.pause();
			listener.dispose();
			DesktopCursor.dispose(this);
			graphics.dispose();
			input.dispose();

			GLFW.SetWindowFocusCallback(windowHandle, null);
			GLFW.SetWindowIconifyCallback(windowHandle, null);
			GLFW.SetWindowMaximizeCallback(windowHandle, null);
			GLFW.SetWindowCloseCallback(windowHandle, null);
			GLFW.SetDropCallback(windowHandle, null);
			GLFW.SetWindowRefreshCallback(windowHandle, null);

			GLFW.DestroyWindow(windowHandle);
		}

		public override int GetHashCode()
		{
			int prime = 31;
			int result = 1;
			result = prime * result + (int)((IntPtr)windowHandle ^ ((IntPtr)windowHandle >>> 32));
			return result;
		}

		public override bool Equals(Object? obj)
		{
			if (this == obj) return true;
			if (obj == null) return false;
			if (GetType() != obj.GetType()) return false;
			DesktopWindow other = (DesktopWindow)obj;
			if (windowHandle != other.windowHandle) return false;
			return true;
		}

		public void flash()
		{
			GLFW.RequestWindowAttention(windowHandle);
		}
	}
}