using OpenTK.Windowing.GraphicsLibraryFramework;
using SharpGDX.utils;

namespace SharpGDX.Desktop
{
	/** Clipboard implementation for desktop that uses the system clipboard via GLFW.
 * @author mzechner */
	public unsafe class DesktopClipboard : Clipboard
	{
		public bool hasContents()
		{
			var contents = getContents();
			return contents != null && !string.IsNullOrEmpty(contents);
		}

		public String? getContents()
		{
			return GLFW.GetClipboardString(((DesktopGraphics)Gdx.graphics).getWindow().getWindowHandle());
		}

		public void setContents(String content)
		{
			GLFW.SetClipboardString(((DesktopGraphics)Gdx.graphics).getWindow().getWindowHandle(), content);
		}
	}
}