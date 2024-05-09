using SharpGDX.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SharpGDX.GLFW3;
using SharpGDX.Shims;
using static SharpGDX.Cursor;

namespace SharpGDX.Desktop
{
	public class Lwjgl3Cursor : Cursor
	{
	static readonly Array<Lwjgl3Cursor> cursors = new Array<Lwjgl3Cursor>();
	static readonly Map<SystemCursor, long> systemCursors = new Map<SystemCursor, long>();

	private static int inputModeBeforeNoneCursor = -1;

	readonly Lwjgl3Window window;
	Pixmap pixmapCopy;
	GLFWImage? glfwImage;
	internal readonly long glfwCursor;

internal 	Lwjgl3Cursor(Lwjgl3Window window, Pixmap pixmap, int xHotspot, int yHotspot)
	{
		this.window = window;
		if (pixmap.getFormat() != Pixmap.Format.RGBA8888)
		{
			throw new GdxRuntimeException("Cursor image pixmap is not in RGBA8888 format.");
		}

		if ((pixmap.getWidth() & (pixmap.getWidth() - 1)) != 0)
		{
			throw new GdxRuntimeException(
				"Cursor image pixmap width of " + pixmap.getWidth() + " is not a power-of-two greater than zero.");
		}

		if ((pixmap.getHeight() & (pixmap.getHeight() - 1)) != 0)
		{
			throw new GdxRuntimeException(
				"Cursor image pixmap height of " + pixmap.getHeight() + " is not a power-of-two greater than zero.");
		}

		if (xHotspot < 0 || xHotspot >= pixmap.getWidth())
		{
			throw new GdxRuntimeException(
				"xHotspot coordinate of " + xHotspot + " is not within image width bounds: [0, " + pixmap.getWidth() + ").");
		}

		if (yHotspot < 0 || yHotspot >= pixmap.getHeight())
		{
			throw new GdxRuntimeException(
				"yHotspot coordinate of " + yHotspot + " is not within image height bounds: [0, " + pixmap.getHeight() + ").");
		}

		this.pixmapCopy = new Pixmap(pixmap.getWidth(), pixmap.getHeight(), Pixmap.Format.RGBA8888);
		this.pixmapCopy.setBlending(Pixmap.Blending.None);
		this.pixmapCopy.drawPixmap(pixmap, 0, 0);

		glfwImage = new GLFWImage()
		{
			Width = (pixmapCopy.getWidth()),
			Height = (pixmapCopy.getHeight()),
			Pixels = (pixmapCopy.getPixels().array())
		};

		glfwCursor = GLFW.glfwCreateCursor(glfwImage.Value, xHotspot, yHotspot);
		cursors.add(this);
	}

	public void dispose()
	{
		if (pixmapCopy == null)
		{
			throw new GdxRuntimeException("Cursor already disposed");
		}
		cursors.removeValue(this, true);
		pixmapCopy.dispose();
		pixmapCopy = null;

		glfwImage = null;
		GLFW.glfwDestroyCursor(glfwCursor);
	}

	internal static void dispose(Lwjgl3Window window)
	{
		for (int i = cursors.size - 1; i >= 0; i--)
		{
			Lwjgl3Cursor cursor = cursors.get(i);
			if (cursor.window.Equals(window))
			{
				cursors.removeIndex(i).dispose();
			}
		}
	}

	internal static void disposeSystemCursors()
	{
		foreach (long systemCursor in systemCursors.values())
		{
			GLFW.glfwDestroyCursor(systemCursor);
		}
		systemCursors.clear();
	}

	internal static void setSystemCursor(long windowHandle, SystemCursor systemCursor)
	{
		if (systemCursor == SystemCursor.None)
		{
			if (inputModeBeforeNoneCursor == -1) inputModeBeforeNoneCursor = GLFW.glfwGetInputMode(windowHandle, GLFW.GLFW_CURSOR);
			GLFW.glfwSetInputMode(windowHandle, GLFW.GLFW_CURSOR, GLFW.GLFW_CURSOR_HIDDEN);
			return;
		}
		else if (inputModeBeforeNoneCursor != -1)
		{
			GLFW.glfwSetInputMode(windowHandle, GLFW.GLFW_CURSOR, inputModeBeforeNoneCursor);
			inputModeBeforeNoneCursor = -1;
		}
		long glfwCursor = systemCursors.get(systemCursor);
		if (glfwCursor == null)
		{
			long handle = 0;
			if (systemCursor == SystemCursor.Arrow)
			{
				handle = GLFW.glfwCreateStandardCursor(GLFW.GLFW_ARROW_CURSOR);
			}
			else if (systemCursor == SystemCursor.Crosshair)
			{
				handle = GLFW.glfwCreateStandardCursor(GLFW.GLFW_CROSSHAIR_CURSOR);
			}
			else if (systemCursor == SystemCursor.Hand)
			{
				handle = GLFW.glfwCreateStandardCursor(GLFW.GLFW_HAND_CURSOR);
			}
			else if (systemCursor == SystemCursor.HorizontalResize)
			{
				handle = GLFW.glfwCreateStandardCursor(GLFW.GLFW_HRESIZE_CURSOR);
			}
			else if (systemCursor == SystemCursor.VerticalResize)
			{
				handle = GLFW.glfwCreateStandardCursor(GLFW.GLFW_VRESIZE_CURSOR);
			}
			else if (systemCursor == SystemCursor.Ibeam)
			{
				handle = GLFW.glfwCreateStandardCursor(GLFW.GLFW_IBEAM_CURSOR);
			}
			else if (systemCursor == SystemCursor.NWSEResize)
			{
				handle = GLFW.glfwCreateStandardCursor(GLFW.GLFW_RESIZE_NWSE_CURSOR);
			}
			else if (systemCursor == SystemCursor.NESWResize)
			{
				handle = GLFW.glfwCreateStandardCursor(GLFW.GLFW_RESIZE_NESW_CURSOR);
			}
			else if (systemCursor == SystemCursor.AllResize)
			{
				handle = GLFW.glfwCreateStandardCursor(GLFW.GLFW_RESIZE_ALL_CURSOR);
			}
			else if (systemCursor == SystemCursor.NotAllowed)
			{
				handle = GLFW.glfwCreateStandardCursor(GLFW.GLFW_NOT_ALLOWED_CURSOR);
			}
			else
			{
				throw new GdxRuntimeException("Unknown system cursor " + systemCursor);
			}

			if (handle == 0)
			{
				return;
			}
			glfwCursor = handle;
			systemCursors.put(systemCursor, glfwCursor);
		}
		GLFW.glfwSetCursor(windowHandle, glfwCursor);
	}
}
}
