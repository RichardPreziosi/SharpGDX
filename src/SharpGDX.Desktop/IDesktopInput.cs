using OpenTK.Windowing.GraphicsLibraryFramework;
using SharpGDX.Utils;

namespace SharpGDX.Desktop;

public interface IDesktopInput : Input, Disposable
{
	public void prepareNext();

	public void resetPollingStates();

	public void update();

	// TODO: Really don't want to expose this, marked internal for now. -RP
	internal unsafe void windowHandleChanged(Window* windowHandle);
}