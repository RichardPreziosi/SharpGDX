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
	public abstract class Game : ApplicationListener
	{
	protected Screen screen;

	
	public void dispose()
	{
		if (screen != null) screen.hide();
	}

	
	public void pause()
	{
		if (screen != null) screen.pause();
	}

	
	public void resume()
	{
		if (screen != null) screen.resume();
	}

	
	public void render()
	{
		if (screen != null) screen.render(Gdx.graphics.getDeltaTime());
	}


	public abstract void create();

	public void resize(int width, int height)
	{
		if (screen != null) screen.resize(width, height);
	}

	/** Sets the current screen. {@link Screen#hide()} is called on any old screen, and {@link Screen#show()} is called on the new
	 * screen, if any.
	 * @param screen may be {@code null} */
	public void setScreen(Screen screen)
	{
		if (this.screen != null) this.screen.hide();
		this.screen = screen;
		if (this.screen != null)
		{
			this.screen.show();
			this.screen.resize(Gdx.graphics.getWidth(), Gdx.graphics.getHeight());
		}
	}

	/** @return the currently active {@link Screen}. */
	public Screen getScreen()
	{
		return screen;
	}
	}
}
