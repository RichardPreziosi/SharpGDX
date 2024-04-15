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
	public class InputAdapter : InputProcessor
	{
	public bool keyDown(int keycode)
	{
		return false;
	}

	public bool keyUp(int keycode)
	{
		return false;
	}

	public bool keyTyped(char character)
	{
		return false;
	}

	public bool touchDown(int screenX, int screenY, int pointer, int button)
	{
		return false;
	}

	public bool touchUp(int screenX, int screenY, int pointer, int button)
	{
		return false;
	}

	public bool touchCancelled(int screenX, int screenY, int pointer, int button)
	{
		return false;
	}

	public bool touchDragged(int screenX, int screenY, int pointer)
	{
		return false;
	}

	public bool mouseMoved(int screenX, int screenY)
	{
		return false;
	}

	public bool scrolled(float amountX, float amountY)
	{
		return false;
	}
	}
}
