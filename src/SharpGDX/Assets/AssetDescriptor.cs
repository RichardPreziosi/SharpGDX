using SharpGDX.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGDX.Shims;
using SharpGDX.Utils;
using SharpGDX.Mathematics;

namespace SharpGDX.Assets
{
	// TODO: Split to two files
	public interface IAssetDescriptor
	{
		public string fileName { get; }
		public IAssetLoaderParameters @params { get; }
		public Type type { get; }

		/**
		 * The resolved file. May be null if the fileName has not been resolved yet.
		 */
		public FileHandle file { get; set; }
	}

	/** Describes an asset to be loaded by its filename, type and {@link AssetLoaderParameters}. Instances of this are used in
 * {@link AssetLoadingTask} to load the actual asset.
 * @author mzechner */
	public class AssetDescriptor<T> : IAssetDescriptor {
	public  String fileName { get; }
	IAssetLoaderParameters IAssetDescriptor.@params => @params;
	public Type type { get; }
		public AssetLoaderParameters<T> @params { get; }
	/** The resolved file. May be null if the fileName has not been resolved yet. */
	public FileHandle file { get; set; }

	public AssetDescriptor (String fileName, Type assetType) 
	: this(fileName, assetType, null)
	{
		
	}

	/** Creates an AssetDescriptor with an already resolved name. */
	public AssetDescriptor (FileHandle file, Type assetType) 
	: this(file, assetType, null)
	{
		
	}

	public AssetDescriptor (String fileName, Type assetType, AssetLoaderParameters<T> @params) {
		this.fileName = fileName;
		this.type = assetType;
		this.@params = @params;
	}

	/** Creates an AssetDescriptor with an already resolved name. */
	public AssetDescriptor (FileHandle file, Type assetType, AssetLoaderParameters<T> @params) {
		this.fileName = file.path();
		this.file = file;
		this.type = assetType;
		this.@params = @params;
	}

	public override String ToString () {
		StringBuilder sb = new StringBuilder();
		sb.Append(fileName);
		sb.Append(", ");
		sb.Append(type.Name);
		return sb.ToString();
	}
}
}
