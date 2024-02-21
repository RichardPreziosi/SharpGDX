namespace SharpGDX;

/**
 * Convenience implementation of {@link ApplicationListener}. Derive from this and only override what you need.
 * @author mzechner
 */
public abstract class ApplicationAdapter : ApplicationListener
{
	public virtual void create()
	{
	}

	public virtual void dispose()
	{
	}

	public virtual void pause()
	{
	}

	public virtual void render()
	{
	}

	public virtual void resize(int width, int height)
	{
	}

	public virtual void resume()
	{
	}
}