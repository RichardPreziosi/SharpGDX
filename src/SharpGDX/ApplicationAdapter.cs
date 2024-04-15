using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX
{
	/** Convenience implementation of {@link ApplicationListener}. Derive from this and only override what you need.
 * @author mzechner */
	public abstract class ApplicationAdapter : ApplicationListener
	{
	public void create()
	{
	}

	public void resize(int width, int height)
	{
	}

	public void render()
	{
	}

	public void pause()
	{
	}

	public void resume()
	{
	}

	public void dispose()
	{
	}
	}
}
