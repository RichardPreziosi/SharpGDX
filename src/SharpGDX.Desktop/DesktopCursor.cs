using OpenTK.Windowing.GraphicsLibraryFramework;
using SharpGDX.graphics;
using SharpGDX.shims;
using SharpGDX.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using GLFWWindow = OpenTK.Windowing.GraphicsLibraryFramework.Window;
using GLFWImage = OpenTK.Windowing.GraphicsLibraryFramework.Image;
using GLFWCursor = OpenTK.Windowing.GraphicsLibraryFramework.Cursor;
using Cursor = SharpGDX.graphics.Cursor;

namespace SharpGDX.Desktop
{
	public unsafe class DesktopCursor : Cursor
	{
	static readonly Array<DesktopCursor> cursors = new Array<DesktopCursor>();
	static readonly Map<Cursor.SystemCursor, IntPtr> systemCursors = new HashMap<Cursor.SystemCursor, IntPtr>();

	private static int inputModeBeforeNoneCursor = -1;

	readonly DesktopWindow window;
	Pixmap pixmapCopy;
	GLFWImage glfwImage;
	internal readonly GLFWCursor* glfwCursor;

internal 	DesktopCursor(DesktopWindow window, Pixmap pixmap, int xHotspot, int yHotspot)
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

		glfwImage = new GLFWImage();
		glfwImage.Width =(pixmapCopy.getWidth());
		glfwImage.Height =(pixmapCopy.getHeight());
		// TODO: glfwImage.Pixels =(pixmapCopy.getPixels());
		glfwCursor = GLFW.CreateCursor(glfwImage, xHotspot, yHotspot);
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

		// TODO: glfwImage.free();
		
		GLFW.DestroyCursor(glfwCursor);
	}

	internal static void dispose(DesktopWindow window)
	{
		for (int i = cursors.size - 1; i >= 0; i--)
		{
			DesktopCursor cursor = cursors.get(i);
			if (cursor.window.Equals(window))
			{
				cursors.removeIndex(i).dispose();
			}
		}
	}

	internal static void disposeSystemCursors()
	{
		foreach (GLFWCursor* systemCursor in systemCursors.values())
		{
			GLFW.DestroyCursor(systemCursor);
		}
		systemCursors.clear();
	}

	internal static void setSystemCursor(GLFWWindow* windowHandle, Cursor.SystemCursor systemCursor)
	{
		if (systemCursor == Cursor.SystemCursor.None)
		{
			if (inputModeBeforeNoneCursor == -1) inputModeBeforeNoneCursor = (int)GLFW.GetInputMode(windowHandle, CursorStateAttribute.Cursor);
			GLFW.SetInputMode(windowHandle, CursorStateAttribute.Cursor, CursorModeValue.CursorHidden);
			return;
		}
		else if (inputModeBeforeNoneCursor != -1)
		{
			GLFW.SetInputMode(windowHandle, CursorStateAttribute.Cursor, (CursorModeValue)inputModeBeforeNoneCursor);
			inputModeBeforeNoneCursor = -1;
		}
		var glfwCursor = (GLFWCursor*)systemCursors.get(systemCursor);
		if (glfwCursor == null)
		{
			GLFWCursor* handle;

			if (systemCursor == Cursor.SystemCursor.Arrow)
			{
				handle = GLFW.CreateStandardCursor(CursorShape.Arrow);
			}
			else if (systemCursor == Cursor.SystemCursor.Crosshair)
			{
				handle = GLFW.CreateStandardCursor(CursorShape.Crosshair);
			}
			else if (systemCursor == Cursor.SystemCursor.Hand)
			{
				handle = GLFW.CreateStandardCursor(CursorShape.Hand);
			}
			else if (systemCursor == Cursor.SystemCursor.HorizontalResize)
			{
				handle = GLFW.CreateStandardCursor(CursorShape.HResize);
			}
			else if (systemCursor == Cursor.SystemCursor.VerticalResize)
			{
				handle = GLFW.CreateStandardCursor(CursorShape.VResize);
			}
			else if (systemCursor == Cursor.SystemCursor.Ibeam)
			{
				handle = GLFW.CreateStandardCursor(CursorShape.IBeam);
			}
			// TODO: Add back when OpenTK upgrades to GLFW 3.4
			//else if (systemCursor == Cursor.SystemCursor.NWSEResize)
			//{
			//	handle = GLFW.CreateStandardCursor(GLFW.GLFW_RESIZE_NWSE_CURSOR);
			//}
			//else if (systemCursor == Cursor.SystemCursor.NESWResize)
			//{
			//	handle = GLFW.CreateStandardCursor(GLFW.GLFW_RESIZE_NESW_CURSOR);
			//}
			//else if (systemCursor == Cursor.SystemCursor.AllResize)
			//{
			//	handle = GLFW.CreateStandardCursor(GLFW.GLFW_RESIZE_ALL_CURSOR);
			//}
			//else if (systemCursor == Cursor.SystemCursor.NotAllowed)
			//{
			//	handle = GLFW.CreateStandardCursor( GLFW.GLFW_NOT_ALLOWED_CURSOR);
			//}
			else
			{
				throw new GdxRuntimeException("Unknown system cursor " + systemCursor);
			}

			if (handle == null)
			{
				return;
			}
			glfwCursor = handle;
			systemCursors.put(systemCursor, (IntPtr)glfwCursor);
		}
		GLFW.SetCursor(windowHandle, glfwCursor);
	}
}
}
