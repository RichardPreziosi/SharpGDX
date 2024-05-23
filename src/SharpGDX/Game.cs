using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX
{
	/**
 * <p>
 * An {@link ApplicationListener} that delegates to a {@link Screen}. This allows an application to easily have multiple screens.
 * </p>
 * <p>
 * Screens are not disposed automatically. You must handle whether you want to keep screens around or dispose of them when another
 * screen is set.
 * </p>
 */
	public abstract class Game : IApplicationListener
	{
	protected IScreen screen;

	
	public virtual void dispose()
	{
		if (screen != null) screen.Hide();
	}


		public virtual void pause()
	{
		if (screen != null) screen.Pause();
	}


		public virtual void resume()
	{
		if (screen != null) screen.Resume();
	}


		public virtual void render()
	{
		if (screen != null) screen.Render(Gdx.graphics.getDeltaTime());
	}


	public abstract void create();

		public virtual void resize(int width, int height)
	{
		if (screen != null) screen.Resize(width, height);
	}

		/** Sets the current screen. {@link Screen#hide()} is called on any old screen, and {@link Screen#show()} is called on the new
		 * screen, if any.
		 * @param screen may be {@code null} */
		public void setScreen(IScreen screen)
	{
		if (this.screen != null) this.screen.Hide();
		this.screen = screen;
		if (this.screen != null)
		{
			this.screen.Show();
			this.screen.Resize(Gdx.graphics.getWidth(), Gdx.graphics.getHeight());
		}
	}

	/** @return the currently active {@link Screen}. */
	public IScreen getScreen()
	{
		return screen;
	}
	}
}
