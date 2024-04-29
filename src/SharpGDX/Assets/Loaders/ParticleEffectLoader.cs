//using SharpGDX.Shims;
//using SharpGDX.Utils;
//using SharpGDX.Mathematics;

//namespace SharpGDX.Assets.Loaders;

///** {@link AssetLoader} to load {@link ParticleEffect} instances. Passing a {@link ParticleEffectParameter} to
// * {@link AssetManager#load(String, Class, AssetLoaderParameters)} allows to specify an atlas file or an image directory to be
// * used for the effect's images. Per default images are loaded from the directory in which the effect file is found. */
//public class ParticleEffectLoader : SynchronousAssetLoader<ParticleEffect, ParticleEffectLoader.ParticleEffectParameter> {
//	public ParticleEffectLoader (FileHandleResolver resolver) {
//		super(resolver);
//	}

//	@Override
//	public ParticleEffect load (AssetManager am, String fileName, FileHandle file, ParticleEffectParameter param) {
//		ParticleEffect effect = new ParticleEffect();
//		if (param != null && param.atlasFile != null)
//			effect.load(file, am.get(param.atlasFile, TextureAtlas.class), param.atlasPrefix);
//		else if (param != null && param.imagesDir != null)
//			effect.load(file, param.imagesDir);
//		else
//			effect.load(file, file.parent());
//		return effect;
//	}

//	@Override
//	public Array<AssetDescriptor> getDependencies (String fileName, FileHandle file, ParticleEffectParameter param) {
//		Array<AssetDescriptor> deps = null;
//		if (param != null && param.atlasFile != null) {
//			deps = new Array();
//			deps.add(new AssetDescriptor<TextureAtlas>(param.atlasFile, TextureAtlas.class));
//		}
//		return deps;
//	}

//	/** Parameter to be passed to {@link AssetManager#load(String, Class, AssetLoaderParameters)} if additional configuration is
//	 * necessary for the {@link ParticleEffect}. */
//	public static class ParticleEffectParameter extends AssetLoaderParameters<ParticleEffect> {
//		/** Atlas file name. */
//		public String atlasFile;
//		/** Optional prefix to image names **/
//		public String atlasPrefix;
//		/** Image directory. */
//		public FileHandle imagesDir;
//	}
//}
