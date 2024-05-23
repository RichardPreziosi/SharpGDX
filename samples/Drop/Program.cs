using SharpGDX;
using SharpGDX.Desktop;
using SharpGDX.Shims;
using StringWriter = SharpGDX.Shims.StringWriter;

namespace Drop
{
	internal class Program
	{
		static void Main(string[] args)
		{
			DesktopApplicationConfiguration config = new DesktopApplicationConfiguration();
			config.setTitle("Drop");
			config.setWindowedMode(800, 480);
			config.useVsync(true);
			config.setForegroundFPS(60);
			config.enableGLDebugOutput(true, new PrintStream());
			new DesktopApplication(new Drop(), config);
		}
	}
}
