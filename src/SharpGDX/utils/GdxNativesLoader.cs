namespace SharpGDX.utils
{
	public class GdxNativesLoader
	{
		static public bool disableNativesLoading = false;

		static private bool nativesLoaded;
		static private object lockObject;

		/** Loads the libgdx native libraries if they have not already been loaded. */
		static public void load()
		{
			lock (lockObject)
			{
				if (nativesLoaded) return;

				if (disableNativesLoading) return;

				// TODO: new SharedLibraryLoader().load("gdx");
				nativesLoaded = true;
			}
		}
	}
}