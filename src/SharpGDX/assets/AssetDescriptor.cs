using System.Text;
using SharpGDX.files;

namespace SharpGDX.assets;

// TODO: Split to two files
public interface AssetDescriptor
{
	public string fileName { get; }
	public AssetLoaderParameters @params { get; }
	public Type type { get; }

	/**
	 * The resolved file. May be null if the fileName has not been resolved yet.
	 */
	public FileHandle file { get; set; }
}

/**
 * Describes an asset to be loaded by its filename, type and {@link AssetLoaderParameters}. Instances of this are used in
 * {@link AssetLoadingTask} to load the actual asset.
 * @author mzechner
 */
public class AssetDescriptor<T> : AssetDescriptor
{
	public  string fileName { get; }
	AssetLoaderParameters AssetDescriptor.@params => @params;

	public AssetLoaderParameters<T> @params { get; }
	public  Type type { get; }

	/**
	 * The resolved file. May be null if the fileName has not been resolved yet.
	 */
	public FileHandle file { get; set; }

	public AssetDescriptor(string fileName, Type assetType)
		: this(fileName, assetType, null)
	{
	}

	/**
	 * Creates an AssetDescriptor with an already resolved name.
	 */
	public AssetDescriptor(FileHandle file, Type assetType)
		: this(file, assetType, null)
	{
	}

	public AssetDescriptor(string fileName, Type assetType, AssetLoaderParameters<T> @params)
	{
		this.fileName = fileName;
		type = assetType;
		this.@params = @params;
	}

	/**
	 * Creates an AssetDescriptor with an already resolved name.
	 */
	public AssetDescriptor(FileHandle file, Type assetType, AssetLoaderParameters<T> @params)
	{
		fileName = file.path();
		this.file = file;
		type = assetType;
		this.@params = @params;
	}

	public override string ToString()
	{
		var sb = new StringBuilder();
		sb.Append(fileName);
		sb.Append(", ");
		sb.Append(type.Name);
		return sb.ToString();
	}
}