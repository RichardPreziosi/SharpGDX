using SharpGDX;
using SharpGDX.Desktop;
using StringWriter = SharpGDX.Shims.StringWriter;

namespace Drop
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Lwjgl3ApplicationConfiguration config = new Lwjgl3ApplicationConfiguration();
			config.setTitle("Drop");
			config.setWindowedMode(800, 480);
			config.useVsync(true);
			config.setForegroundFPS(60);
			new Lwjgl3Application(new Drop(), config);
		}
	}

	internal class Drop : ApplicationAdapter
	{

	}
}
