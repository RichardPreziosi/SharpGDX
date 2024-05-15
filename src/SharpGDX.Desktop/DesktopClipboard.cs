using OpenTK.Windowing.GraphicsLibraryFramework;
using SharpGDX.Utils;

namespace SharpGDX.Desktop;

/// <summary>
///     Clipboard implementation for desktop that uses the system clipboard via GLFW.
/// </summary>
public class Lwjgl3Clipboard : Clipboard
{
	public unsafe string getContents()
	{
		return GLFW.GetClipboardString(((Lwjgl3Graphics)Gdx.graphics).getWindow().getWindowPtr());
	}

	public bool hasContents()
	{
		return !string.IsNullOrEmpty(getContents());
	}

	public unsafe void setContents(string content)
	{
		GLFW.SetClipboardString(((Lwjgl3Graphics)Gdx.graphics).getWindow().getWindowPtr(), content);
	}
}