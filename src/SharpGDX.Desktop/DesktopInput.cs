using SharpGDX.utils;
using GLFWWindow = OpenTK.Windowing.GraphicsLibraryFramework.Window;

namespace SharpGDX.Desktop;

public interface DesktopInput : Input, Disposable
{
	void prepareNext();

	void resetPollingStates();

	void update();

	unsafe void windowHandleChanged(GLFWWindow* windowHandle);
}