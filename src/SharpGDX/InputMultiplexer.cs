using SharpGDX.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGDX.Shims;

namespace SharpGDX
{
	/** An {@link InputProcessor} that delegates to an ordered list of other InputProcessors. Delegation for an event stops if a
 * processor returns true, which indicates that the event was handled.
 * @author Nathan Sweet */
	public class InputMultiplexer : InputProcessor
	{
	private SnapshotArray<InputProcessor> processors = new (4);

	public InputMultiplexer()
	{
	}

	public InputMultiplexer(InputProcessor[]processors)
	{
		this.processors.addAll(processors);
	}

	public void addProcessor(int index, InputProcessor processor)
	{
		if (processor == null) throw new NullPointerException("processor cannot be null");
		processors.insert(index, processor);
	}

	public void removeProcessor(int index)
	{
		processors.removeIndex(index);
	}

	public void addProcessor(InputProcessor processor)
	{
		if (processor == null) throw new NullPointerException("processor cannot be null");
		processors.add(processor);
	}

	public void removeProcessor(InputProcessor processor)
	{
		processors.removeValue(processor, true);
	}

	/** @return the number of processors in this multiplexer */
	public int size()
	{
		return processors.size;
	}

	public void clear()
	{
		processors.clear();
	}

	public void setProcessors(InputProcessor[]processors)
	{
		this.processors.clear();
		this.processors.addAll(processors);
	}

	public void setProcessors(Array<InputProcessor> processors)
	{
		this.processors.clear();
		this.processors.addAll(processors);
	}

	public SnapshotArray<InputProcessor> getProcessors()
	{
		return processors;
	}

	public bool keyDown(int keycode)
	{
		Object[] items = processors.begin();
		try
		{
			for (int i = 0, n = processors.size; i < n; i++)
				if (((InputProcessor)items[i]).keyDown(keycode)) return true;
		}
		finally
		{
			processors.end();
		}
		return false;
	}

	public bool keyUp(int keycode)
	{
		Object[] items = processors.begin();
		try
		{
			for (int i = 0, n = processors.size; i < n; i++)
				if (((InputProcessor)items[i]).keyUp(keycode)) return true;
		}
		finally
		{
			processors.end();
		}
		return false;
	}

	public bool keyTyped(char character)
	{
		Object[] items = processors.begin();
		try
		{
			for (int i = 0, n = processors.size; i < n; i++)
				if (((InputProcessor)items[i]).keyTyped(character)) return true;
		}
		finally
		{
			processors.end();
		}
		return false;
	}

	public bool touchDown(int screenX, int screenY, int pointer, int button)
	{
		Object[] items = processors.begin();
		try
		{
			for (int i = 0, n = processors.size; i < n; i++)
				if (((InputProcessor)items[i]).touchDown(screenX, screenY, pointer, button)) return true;
		}
		finally
		{
			processors.end();
		}
		return false;
	}

	public bool touchUp(int screenX, int screenY, int pointer, int button)
	{
		Object[] items = processors.begin();
		try
		{
			for (int i = 0, n = processors.size; i < n; i++)
				if (((InputProcessor)items[i]).touchUp(screenX, screenY, pointer, button)) return true;
		}
		finally
		{
			processors.end();
		}
		return false;
	}

	public bool touchCancelled(int screenX, int screenY, int pointer, int button)
	{
		Object[] items = processors.begin();
		try
		{
			for (int i = 0, n = processors.size; i < n; i++)
				if (((InputProcessor)items[i]).touchCancelled(screenX, screenY, pointer, button)) return true;
		}
		finally
		{
			processors.end();
		}
		return false;
	}

	public bool touchDragged(int screenX, int screenY, int pointer)
	{
		Object[] items = processors.begin();
		try
		{
			for (int i = 0, n = processors.size; i < n; i++)
				if (((InputProcessor)items[i]).touchDragged(screenX, screenY, pointer)) return true;
		}
		finally
		{
			processors.end();
		}
		return false;
	}

	public bool mouseMoved(int screenX, int screenY)
	{
		Object[] items = processors.begin();
		try
		{
			for (int i = 0, n = processors.size; i < n; i++)
				if (((InputProcessor)items[i]).mouseMoved(screenX, screenY)) return true;
		}
		finally
		{
			processors.end();
		}
		return false;
	}

	public bool scrolled(float amountX, float amountY)
	{
		Object[] items = processors.begin();
		try
		{
			for (int i = 0, n = processors.size; i < n; i++)
				if (((InputProcessor)items[i]).scrolled(amountX, amountY)) return true;
		}
		finally
		{
			processors.end();
		}
		return false;
	}
}
}
