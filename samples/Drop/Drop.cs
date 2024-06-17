using SharpGDX;
using SharpGDX.Audio;
using SharpGDX.Graphics;
using SharpGDX.Graphics.G2D;
using SharpGDX.Mathematics;
using SharpGDX.Utils;

namespace Drop;

public class Drop : ApplicationAdapter
{
	private SpriteBatch _batch = null!;
	private Rectangle _bucket = null!;
	private Texture _bucketImage = null!;
	private OrthographicCamera _camera = null!;
	private Texture _dropImage = null!;
	private ISound _dropSound = null!;
	private long _lastDropTime;
	private List<Rectangle> _raindrops = null!;
	private IMusic _rainMusic = null!;

	public override void Create()
	{
		// load the images for the droplet and the bucket, 64x64 pixels each
		_dropImage = new Texture(Gdx.files.@internal("droplet.png"));
		_bucketImage = new Texture(Gdx.files.@internal("bucket.png"));

		// load the drop sound effect and the rain background "music"
		_dropSound = Gdx.audio.newSound(Gdx.files.@internal("drop.wav"));
		_rainMusic = Gdx.audio.newMusic(Gdx.files.@internal("rain.wav"));

		// start the playback of the background music immediately
		_rainMusic.setLooping(true);
		_rainMusic.play();

		// create the camera and the SpriteBatch
		_camera = new OrthographicCamera();
		_camera.setToOrtho(false, 800, 480);
		_batch = new SpriteBatch();

		// create a Rectangle to logically represent the bucket
		_bucket = new Rectangle
		{
			x = 800.0f / 2.0f - 64.0f / 2.0f, // center the bucket horizontally
			y = 20, // bottom left corner of the bucket is 20 pixels above the bottom screen edge
			width = 64,
			height = 64
		};

		// create the raindrops array and spawn the first raindrop
		_raindrops = [];
		SpawnRaindrop();
	}

	public override void Dispose()
	{
		// dispose of all the native resources
		_dropImage.dispose();
		_bucketImage.dispose();
		_dropSound.dispose();
		_rainMusic.dispose();
		_batch.dispose();
	}

	public override void Render()
	{
		// clear the screen with a dark blue color. The
		// arguments to clear are the red, green
		// blue and alpha component in the range [0,1]
		// of the color to be used to clear the screen.
		ScreenUtils.clear(0, 0, 0.2f, 1);

		// tell the camera to update its matrices.
		_camera.update();

		// tell the SpriteBatch to render in the
		// coordinate system specified by the camera.
		_batch.setProjectionMatrix(_camera.combined);

		// begin a new batch and draw the bucket and all drops
		_batch.begin();

		_batch.draw(_bucketImage, _bucket.x, _bucket.y);
		foreach (var raindrop in _raindrops)
		{
			_batch.draw(_dropImage, raindrop.x, raindrop.y);
		}

		_batch.end();

		// process user input
		if (Gdx.input.isTouched())
		{
			var touchPos = new Vector3();
			touchPos.set(Gdx.input.getX(), Gdx.input.getY(), 0);
			_camera.unproject(touchPos);
			_bucket.x = touchPos.x - 64.0f / 2.0f;
		}

		if (Gdx.input.isKeyPressed(IInput.Keys.LEFT))
		{
			_bucket.x -= 200 * Gdx.graphics.getDeltaTime();
		}

		if (Gdx.input.isKeyPressed(IInput.Keys.RIGHT))
		{
			_bucket.x += 200 * Gdx.graphics.getDeltaTime();
		}

		// make sure the bucket stays within the screen bounds
		if (_bucket.x < 0)
		{
			_bucket.x = 0;
		}

		if (_bucket.x > 800 - 64)
		{
			_bucket.x = 800 - 64;
		}

		// check if we need to create a new raindrop
		if (TimeUtils.nanoTime() - _lastDropTime > 1000000000)
		{
			SpawnRaindrop();
		}

		// move the raindrops, remove any that are beneath the bottom edge of
		// the screen or that hit the bucket. In the latter case we play back
		// a sound effect as well.
		for (var i = _raindrops.Count - 1; i >= 0; i--)
		{
			var raindrop = _raindrops[i];
			raindrop.y -= 200 * Gdx.graphics.getDeltaTime();
			if (raindrop.y + 64 < 0)
			{
				_raindrops.RemoveAt(i);
			}

			if (raindrop.overlaps(_bucket))
			{
				_dropSound.play();
				_raindrops.RemoveAt(i);
			}
		}
	}

	private void SpawnRaindrop()
	{
		var raindrop = new Rectangle
		{
			x = MathUtils.random(0, 800 - 64),
			y = 480,
			width = 64,
			height = 64
		};

		_raindrops.Add(raindrop);
		_lastDropTime = TimeUtils.nanoTime();
	}
}