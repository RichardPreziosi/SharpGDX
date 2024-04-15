using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SharpGDX.Utils
{
	public class GdxNativesLoader
	{
		static public bool disableNativesLoading = false;

		static private bool nativesLoaded;

		private static object _lock = new object();

		/** Loads the libgdx native libraries if they have not already been loaded. */
		static public void load()
		{
			lock (_lock)
			{
				if (nativesLoaded) return;

				if (disableNativesLoading) return;

				// TODO: ??? new SharedLibraryLoader().load("gdx");
				nativesLoaded = true;
			}
		}
	}
}