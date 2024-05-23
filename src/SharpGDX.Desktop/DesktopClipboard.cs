using OpenTK.Windowing.GraphicsLibraryFramework;
using SharpGDX.Utils;

namespace SharpGDX.Desktop;

/// <summary>
///     Clipboard implementation for desktop that uses the system clipboard via GLFW.
/// </summary>
public class DesktopClipboard : IClipboard
{
	public unsafe string getContents()
	{
		return GLFW.GetClipboardString(((DesktopGraphics)Gdx.graphics).getWindow().getWindowPtr());
	}

	public bool hasContents()
	{
		return !string.IsNullOrEmpty(getContents());
	}

	public unsafe void setContents(string content)
	{
		GLFW.SetClipboardString(((DesktopGraphics)Gdx.graphics).getWindow().getWindowPtr(), content);
	}
}