using static SharpGDX.assets.AssetLoaderParameters;

namespace SharpGDX.assets;

// TODO: Split to two files.

public interface AssetLoaderParameters
{
	/**
	 * Callback interface that will be invoked when the {@link AssetManager} loaded an asset.
	 * @author mzechner
	 */
	public interface LoadedCallback
	{
		public void finishedLoading(AssetManager assetManager, string fileName, Type type);
	}

	public LoadedCallback loadedCallback { get; }
}

public class AssetLoaderParameters<T> : AssetLoaderParameters
{
	public LoadedCallback loadedCallback;
	LoadedCallback AssetLoaderParameters.loadedCallback => loadedCallback;
}