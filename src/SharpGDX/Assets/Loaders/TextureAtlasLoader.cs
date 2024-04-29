//using SharpGDX.Shims;
//using SharpGDX.Utils;
//using SharpGDX.Mathematics;

//namespace SharpGDX.Assets.Loaders;

///** {@link AssetLoader} to load {@link TextureAtlas} instances. Passing a {@link TextureAtlasParameter} to
// * {@link AssetManager#load(String, Class, AssetLoaderParameters)} allows to specify whether the atlas regions should be flipped
// * on the y-axis or not.
// * @author mzechner */
//public class TextureAtlasLoader : SynchronousAssetLoader<TextureAtlas, TextureAtlasLoader.TextureAtlasParameter> {
//	public TextureAtlasLoader (FileHandleResolver resolver) {
//		super(resolver);
//	}

//	TextureAtlasData data;

//	@Override
//	public TextureAtlas load (AssetManager assetManager, String fileName, FileHandle file, TextureAtlasParameter parameter) {
//		for (Page page : data.getPages()) {
//			Texture texture = assetManager.get(page.textureFile.path().replaceAll("\\\\", "/"), Texture.class);
//			page.texture = texture;
//		}

//		TextureAtlas atlas = new TextureAtlas(data);
//		data = null;
//		return atlas;
//	}

//	@Override
//	public Array<AssetDescriptor> getDependencies (String fileName, FileHandle atlasFile, TextureAtlasParameter parameter) {
//		FileHandle imgDir = atlasFile.parent();

//		if (parameter != null)
//			data = new TextureAtlasData(atlasFile, imgDir, parameter.flip);
//		else {
//			data = new TextureAtlasData(atlasFile, imgDir, false);
//		}

//		Array<AssetDescriptor> dependencies = new Array();
//		for (Page page : data.getPages()) {
//			TextureParameter params = new TextureParameter();
//			params.format = page.format;
//			params.genMipMaps = page.useMipMaps;
//			params.minFilter = page.minFilter;
//			params.magFilter = page.magFilter;
//			dependencies.add(new AssetDescriptor(page.textureFile, Texture.class, params));
//		}
//		return dependencies;
//	}

//	static public class TextureAtlasParameter extends AssetLoaderParameters<TextureAtlas> {
//		/** whether to flip the texture atlas vertically **/
//		public boolean flip = false;

//		public TextureAtlasParameter () {
//		}

//		public TextureAtlasParameter (boolean flip) {
//			this.flip = flip;
//		}
//	}
//}
