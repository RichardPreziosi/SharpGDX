namespace SharpGDX;

/// <summary>
///     Convenience implementation of <see cref="IApplicationListener" />.
/// </summary>
/// <remarks>
///     Derive from this and only override what you need.
/// </remarks>
public abstract class ApplicationAdapter : IApplicationListener
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