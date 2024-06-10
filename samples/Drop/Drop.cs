using SharpGDX.Graphics;
using SharpGDX.Graphics.GLUtils;
using SharpGDX.Utils.Viewports;
using SharpGDX.Utils;
using SharpGDX;
using SharpGDX.Audio;
using SharpGDX.Desktop;
using SharpGDX.Graphics.G2D;
using SharpGDX.Mathematics;

namespace Drop
{
	//public class Drop : ApplicationAdapter
	//{
	//	private Texture dropImage;
	//	private Texture bucketImage;
	//	private ISound dropSound;
	//	private IMusic rainMusic;
	//	private SpriteBatch batch;
	//	private OrthographicCamera camera;
	//	private Rectangle bucket;
	//	private List<Rectangle> raindrops;
	//	private long lastDropTime;

	//	public override void Create()
	//	{
	//		// load the images for the droplet and the bucket, 64x64 pixels each
	//		dropImage = new Texture(Gdx.files.@internal("droplet.png"));
	//		bucketImage = new Texture(Gdx.files.@internal("bucket.png"));

	//		// load the drop sound effect and the rain background "music"
	//		dropSound = Gdx.audio.newSound(Gdx.files.@internal("drop.wav"));
	//		rainMusic = Gdx.audio.newMusic(Gdx.files.@internal("rain.wav"));

	//		// start the playback of the background music immediately
	//		rainMusic.setLooping(true);
	//		rainMusic.play();

	//		// create the camera and the SpriteBatch
	//		camera = new OrthographicCamera();
	//		camera.setToOrtho(false, 800, 480);
	//		batch = new SpriteBatch();

	//		// create a Rectangle to logically represent the bucket
	//		bucket = new Rectangle();
	//		bucket.x = 800 / 2 - 64 / 2; // center the bucket horizontally
	//		bucket.y = 20; // bottom left corner of the bucket is 20 pixels above the bottom screen edge
	//		bucket.width = 64;
	//		bucket.height = 64;

	//		// create the raindrops array and spawn the first raindrop
	//		raindrops = new();
	//		spawnRaindrop();
	//	}

	//	private void spawnRaindrop()
	//	{
	//		Rectangle raindrop = new Rectangle();
	//		raindrop.x = MathUtils.random(0, 800 - 64);
	//		raindrop.y = 480;
	//		raindrop.width = 64;
	//		raindrop.height = 64;
	//		raindrops.Add(raindrop);
	//		lastDropTime = TimeUtils.nanoTime();
	//	}

	//	public override void Render()
	//	{
	//		// clear the screen with a dark blue color. The
	//		// arguments to clear are the red, green
	//		// blue and alpha component in the range [0,1]
	//		// of the color to be used to clear the screen.
	//		ScreenUtils.clear(0, 0, 0.2f, 1);

	//		// tell the camera to update its matrices.
	//		camera.update();

	//		// tell the SpriteBatch to render in the
	//		// coordinate system specified by the camera.
	//		batch.setProjectionMatrix(camera.combined);

	//		// begin a new batch and draw the bucket and
	//		// all drops
	//		batch.begin();

	//		batch.draw(bucketImage, bucket.x, bucket.y);
	//		foreach (Rectangle raindrop in raindrops)
	//		{
	//			batch.draw(dropImage, raindrop.x, raindrop.y);
	//		}
	//		batch.end();

	//		// process user input
	//		if (Gdx.input.isTouched())
	//		{
	//			Vector3 touchPos = new Vector3();
	//			touchPos.set(Gdx.input.getX(), Gdx.input.getY(), 0);
	//			//camera.unproject(touchPos);
	//			bucket.x = touchPos.x - 64 / 2;
	//		}
	//		if (Gdx.input.isKeyPressed(IInput.Keys.LEFT)) bucket.x -= 200 * Gdx.graphics.getDeltaTime();
	//		if (Gdx.input.isKeyPressed(IInput.Keys.RIGHT)) bucket.x += 200 * Gdx.graphics.getDeltaTime();

	//		// make sure the bucket stays within the screen bounds
	//		if (bucket.x < 0) bucket.x = 0;
	//		if (bucket.x > 800 - 64) bucket.x = 800 - 64;

	//		// check if we need to create a new raindrop
	//		if (TimeUtils.nanoTime() - lastDropTime > 1000000000) spawnRaindrop();

	//		// move the raindrops, remove any that are beneath the bottom edge of
	//		// the screen or that hit the bucket. In the latter case we play back
	//		// a sound effect as well.
	//		for (var i = raindrops.Count() - 1; i >= 0; i--)
	//		{
	//			Rectangle raindrop = raindrops[i];
	//			raindrop.y -= 200 * Gdx.graphics.getDeltaTime();
	//			if (raindrop.y + 64 < 0)
	//			{
	//				raindrops.RemoveAt(i);
	//			}

	//			if (raindrop.overlaps(bucket))
	//			{
	//				dropSound.play();
	//				raindrops.RemoveAt(i);
	//			}
	//		}
	//	}

	//	public override void Dispose()
	//	{
	//		// dispose of all the native resources
	//		dropImage.dispose();
	//		bucketImage.dispose();
	//		dropSound.dispose();
	//		rainMusic.dispose();
	//		batch.dispose();
	//	}
	//}

	public class Drop : ApplicationAdapter
	{
		SpriteBatch batch;
		Texture img;

		public override void Create()
		{
			batch = new SpriteBatch();
			img = new Texture("badlogic.png");
		}

		public override void Render()
		{
			ScreenUtils.clear(1, 0, 0, 1);
			batch.begin();
			batch.draw(img, 0, 0);
			batch.end();
		}

		public override void Dispose()
		{
			batch.dispose();
			img.dispose();
		}
	}

	//	public class Drop : Game
	//	{

	//	public  static float WIDTH = 100;
	//	public  static float HEIGHT = 16 * WIDTH / 9;

	//	FitViewport viewport;
	//	internal OrthographicCamera camera;
	//	internal ShapeRenderer shape;

	//		public override void Create()
	//	{
	//		shape = new ShapeRenderer();
	//		camera = new OrthographicCamera();
	//		viewport = new FitViewport(WIDTH, HEIGHT, camera);

	//		SetScreen(new ScreenPlay(){g = this});

	//		Gdx.app.getPreferences("test").putString("hi", "hello").flush();
	//	}

	//	public override void Resize(int width, int height)
	//	{
	//		viewport.update(width, height);
	//	}

	//	}

	//	public class Box
	//	{
	//		public float x;
	//		public float y;
	//		public float width;
	//		public float height;
	//}

	//public class ScreenPlay : IScreen
	//{
	//	internal Drop g;
	//	Array<Box> boxes;

	//		public void Show()
	//	{
	//		boxes = new Array<Box>();
	//		boxes.add(new Box(){x = 10, y = 10, height = 10, width = 10});
	//	}

	//	public void Render(float delta)
	//	{
	//		ScreenUtils.clear(0.2f, 0,0,1);
	//		foreach (Box box in boxes) {
	//			g.shape.setProjectionMatrix(g.camera.combined);
	//			g.shape.begin(ShapeRenderer.ShapeType.Line);
	//			g.shape.setColor(Color.RED);
	//			g.shape.rect(box.x, box.y, box.width, box.height);
	//			g.shape.end();

	//			g.shape.setProjectionMatrix(g.camera.combined);
	//			g.shape.begin(ShapeRenderer.ShapeType.Filled);
	//			g.shape.setColor(Color.BLUE);
	//			g.shape.ellipse(box.x, box.y, box.width, box.height);
	//			g.shape.end();
	//		}	
	//	}

	//	public void Resize(int width, int height)
	//	{
	//	}

	//	public void Pause()
	//	{
	//	}

	//	public void Resume()
	//	{
	//	}

	//	public void Hide()
	//	{
	//	}

	//	public void Dispose()
	//	{
	//	}
	//}
}