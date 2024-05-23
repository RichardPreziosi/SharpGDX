using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX
{
	/** An adapter class for {@link InputProcessor}. You can derive from this and only override what you are interested in.
 *
 * @author mzechner */
	public class InputAdapter : IInputProcessor
	{
	public virtual bool keyDown(int keycode)
	{
		return false;
	}

	public virtual bool keyUp(int keycode)
	{
		return false;
	}

	public virtual bool keyTyped(char character)
	{
		return false;
	}

	public virtual bool touchDown(int screenX, int screenY, int pointer, int button)
	{
		return false;
	}

	public virtual bool touchUp(int screenX, int screenY, int pointer, int button)
	{
		return false;
	}

	public virtual bool touchCancelled(int screenX, int screenY, int pointer, int button)
	{
		return false;
	}

	public virtual bool touchDragged(int screenX, int screenY, int pointer)
	{
		return false;
	}

	public virtual bool mouseMoved(int screenX, int screenY)
	{
		return false;
	}

	public virtual bool scrolled(float amountX, float amountY)
	{
		return false;
	}
	}
}
