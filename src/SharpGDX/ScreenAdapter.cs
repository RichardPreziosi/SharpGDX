namespace SharpGDX;

/**
 * Convenience implementation of {@link Screen}. Derive from this and only override what you need.
 * @author semtiko
 */
public class ScreenAdapter : Screen
{
	public virtual void dispose()
	{
	}

	public virtual void hide()
	{
	}

	public virtual void pause()
	{
	}

	public virtual void render(float delta)
	{
	}

	public virtual void resize(int width, int height)
	{
	}

	public virtual void resume()
	{
	}

	public virtual void show()
	{
	}
}