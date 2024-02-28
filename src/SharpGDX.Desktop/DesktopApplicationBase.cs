using SharpGDX.Desktop.audio;

namespace SharpGDX.Desktop;

public interface DesktopApplicationBase : Application
{
	DesktopAudio createAudio(DesktopApplicationConfiguration config);

	DesktopInput createInput(DesktopWindow window);
}