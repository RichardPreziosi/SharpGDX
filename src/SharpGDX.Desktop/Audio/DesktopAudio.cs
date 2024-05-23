using SharpGDX.Utils;

namespace SharpGDX.Desktop.Audio;

public interface IDesktopAudio : SharpGDX.IAudio, Disposable
{
	void Update();
}