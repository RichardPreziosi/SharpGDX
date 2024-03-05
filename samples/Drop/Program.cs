using SharpGDX.Desktop;
using SharpGDX.files;

namespace Drop;

internal class Program
{
	private static void Main()
	{
		var config = new DesktopApplicationConfiguration();

		config.setTitle("Drop");
		config.setWindowedMode(800, 480);
		config.useVsync(true);
		config.setForegroundFPS(60);

		_ = new DesktopApplication(new Drop(), config);
	}
}