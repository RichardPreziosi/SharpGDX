using SharpGDX.utils;

namespace SharpGDX.Desktop.audio;

public interface DesktopAudio : Audio, Disposable
{
	void update();
}