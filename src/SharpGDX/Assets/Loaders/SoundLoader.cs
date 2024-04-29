using SharpGDX.Shims;
using SharpGDX.Utils;
using SharpGDX.Mathematics;

namespace SharpGDX.Assets.Loaders;

/** {@link AssetLoader} to load {@link Sound} instances.
 * @author mzechner */
public class SoundLoader : AsynchronousAssetLoader<Sound, SoundLoader.SoundParameter> {

	private Sound sound;

	public SoundLoader (FileHandleResolver resolver) 
	: base(resolver)
	{
		
	}

	/** Returns the {@link Sound} instance currently loaded by this {@link SoundLoader}.
	 * 
	 * @return the currently loaded {@link Sound}, otherwise {@code null} if no {@link Sound} has been loaded yet. */
	protected Sound getLoadedSound () {
		return sound;
	}

	public override void loadAsync (AssetManager manager, String fileName, FileHandle file, SoundParameter parameter) {
		sound = Gdx.audio.newSound(file);
	}

	public override Sound loadSync (AssetManager manager, String fileName, FileHandle file, SoundParameter parameter) {
		Sound sound = this.sound;
		this.sound = null;
		return sound;
	}

	public override Array<AssetDescriptor<Sound>> getDependencies (String fileName, FileHandle file, SoundParameter parameter) {
		return null;
	}

	public class SoundParameter : AssetLoaderParameters<Sound> {
	}

}
