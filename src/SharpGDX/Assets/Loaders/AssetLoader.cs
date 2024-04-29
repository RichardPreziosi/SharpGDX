using SharpGDX.Shims;
using SharpGDX.Utils;
using SharpGDX.Mathematics;

namespace SharpGDX.Assets.Loaders;

// TODO: Split into two files

public interface AssetLoader
{
	/** Returns the assets this asset requires to be loaded first. This method may be called on a thread other than the GL thread.
 * @param fileName name of the asset to load
 * @param file the resolved file to load
 * @param parameter parameters for loading the asset
 * @return other assets that the asset depends on and need to be loaded first or null if there are no dependencies. */
	Array<AssetDescriptor>
		getDependencies(String fileName, FileHandle file, AssetLoaderParameters parameter);

	/** @param fileName file name to resolve
	 * @return handle to the file, as resolved by the {@link FileHandleResolver} set on the loader */
	FileHandle resolve(String fileName);
}

/** Abstract base class for asset loaders.
 * @author mzechner
 * 
 * @param <T> the class of the asset the loader supports
 * @param <P> the class of the loading parameters the loader supports. */
public abstract class AssetLoader<T, P > : AssetLoader
where P: AssetLoaderParameters<T>
{
	/** {@link FileHandleResolver} used to map from plain asset names to {@link FileHandle} instances **/
	private FileHandleResolver resolver;

	/** Constructor, sets the {@link FileHandleResolver} to use to resolve the file associated with the asset name.
	 * @param resolver */
	public AssetLoader (FileHandleResolver resolver) {
		this.resolver = resolver;
	}

	/** @param fileName file name to resolve
	 * @return handle to the file, as resolved by the {@link FileHandleResolver} set on the loader */
	public FileHandle resolve (String fileName) {
		return resolver.resolve(fileName);
	}

	/** Returns the assets this asset requires to be loaded first. This method may be called on a thread other than the GL thread.
	 * @param fileName name of the asset to load
	 * @param file the resolved file to load
	 * @param parameter parameters for loading the asset
	 * @return other assets that the asset depends on and need to be loaded first or null if there are no dependencies. */
	public abstract Array<AssetDescriptor<T>> getDependencies (String fileName, FileHandle file, P parameter);
	
	Array<AssetDescriptor> AssetLoader.getDependencies(string fileName, FileHandle file,
		AssetLoaderParameters parameter)
	{
		// TODO: Better way to do this while still using Array<T>?
		return new Array<AssetDescriptor>(getDependencies(fileName, file, (P)parameter).OfType<AssetDescriptor>().ToArray());
	}
}
