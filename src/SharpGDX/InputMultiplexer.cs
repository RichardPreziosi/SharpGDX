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
	public class InputMultiplexer : IInputProcessor
	{
	private SnapshotArray<IInputProcessor> processors = new (4);

	public InputMultiplexer()
	{
	}

	public InputMultiplexer(IInputProcessor[]processors)
	{
		this.processors.addAll(processors);
	}

	public void addProcessor(int index, IInputProcessor processor)
	{
		if (processor == null) throw new NullPointerException("processor cannot be null");
		processors.insert(index, processor);
	}

	public void removeProcessor(int index)
	{
		processors.removeIndex(index);
	}

	public void addProcessor(IInputProcessor processor)
	{
		if (processor == null) throw new NullPointerException("processor cannot be null");
		processors.add(processor);
	}

	public void removeProcessor(IInputProcessor processor)
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

	public void setProcessors(IInputProcessor[]processors)
	{
		this.processors.clear();
		this.processors.addAll(processors);
	}

	public void setProcessors(Array<IInputProcessor> processors)
	{
		this.processors.clear();
		this.processors.addAll(processors);
	}

	public SnapshotArray<IInputProcessor> getProcessors()
	{
		return processors;
	}

	public bool keyDown(int keycode)
	{
		Object[] items = processors.begin();
		try
		{
			for (int i = 0, n = processors.size; i < n; i++)
				if (((IInputProcessor)items[i]).keyDown(keycode)) return true;
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
				if (((IInputProcessor)items[i]).keyUp(keycode)) return true;
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
				if (((IInputProcessor)items[i]).keyTyped(character)) return true;
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
				if (((IInputProcessor)items[i]).touchDown(screenX, screenY, pointer, button)) return true;
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
				if (((IInputProcessor)items[i]).touchUp(screenX, screenY, pointer, button)) return true;
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
				if (((IInputProcessor)items[i]).touchCancelled(screenX, screenY, pointer, button)) return true;
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
				if (((IInputProcessor)items[i]).touchDragged(screenX, screenY, pointer)) return true;
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
				if (((IInputProcessor)items[i]).mouseMoved(screenX, screenY)) return true;
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
				if (((IInputProcessor)items[i]).scrolled(amountX, amountY)) return true;
		}
		finally
		{
			processors.end();
		}
		return false;
	}
}
}
