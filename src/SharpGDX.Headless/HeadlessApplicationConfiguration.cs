using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX.Headless
{
	public class HeadlessApplicationConfiguration
	{
		/** The amount of updates targeted per second. Use 0 to never sleep; negative to not call the render method at all. Default is
		 * 60. */
		public int updatesPerSecond = 60;
		/** Preferences directory for headless. Default is ".prefs/". */
		public String preferencesDirectory = ".prefs/";

		/** The maximum number of threads to use for network requests. Default is {@link Integer#MAX_VALUE}. */
		public int maxNetThreads = int.MaxValue;
	}
}
