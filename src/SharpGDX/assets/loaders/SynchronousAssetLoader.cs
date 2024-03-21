using SharpGDX.files;

namespace SharpGDX.assets.loaders;

// TODO: Split into two files

internal interface SynchronousAssetLoader : AssetLoader
{
	public object load(AssetManager assetManager, String fileName, FileHandle file, AssetLoaderParameters parameter);
}

public abstract class SynchronousAssetLoader<T, P > : AssetLoader<T, P> , SynchronousAssetLoader
where P : AssetLoaderParameters<T>
{
public SynchronousAssetLoader(FileHandleResolver resolver)
: base(resolver)
	{
	
}

public abstract T load(AssetManager assetManager, String fileName, FileHandle file, P parameter);

object SynchronousAssetLoader.load(AssetManager assetManager, string fileName, FileHandle file, AssetLoaderParameters parameter)
{
	return load(assetManager, fileName, file, (P)parameter);
}
}