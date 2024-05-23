using SharpGDX.Desktop.Audio;

namespace SharpGDX.Desktop;

public interface IDesktopApplicationBase : IApplication
{
	IDesktopAudio CreateAudio(DesktopApplicationConfiguration config);

	IDesktopInput CreateInput(DesktopWindow window);
}