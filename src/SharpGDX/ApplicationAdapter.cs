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
	public virtual void create()
	{
	}

		public virtual void resize(int width, int height)
	{
	}

		public virtual void render()
	{
	}

		public virtual void pause()
	{
	}

		public virtual void resume()
	{
	}

		public virtual void dispose()
	{
	}
	}
}
