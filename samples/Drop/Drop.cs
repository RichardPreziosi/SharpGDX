using SharpGDX;
using SharpGDX.audio;

namespace Drop;

internal class Drop : ApplicationAdapter
{
	//private Texture dropImage;
	//private Texture bucketImage;
	private Sound dropSound;
	private Music rainMusic;

	public override void create()
	{
		// load the images for the droplet and the bucket, 64x64 pixels each
		//dropImage = new Texture(Gdx.files.internal ("droplet.png"));
		//bucketImage = new Texture(Gdx.files.internal ("bucket.png"));

		// load the drop sound effect and the rain background "music"
		//dropSound = Gdx.audio.newSound(Gdx.files.Internal("drop.wav"));
		rainMusic = Gdx.audio.newMusic(Gdx.files.Internal("rain.wav"));

		// start the playback of the background music immediately
		rainMusic.setLooping(true);
		rainMusic.play();
	}

	private float sec = 0;

	public override void render()
	{
		//if ((sec += Gdx.graphics.getDeltaTime()) >= 1)
		//{
		//	sec = 0;
		//	dropSound.play();
		//}
	}
}