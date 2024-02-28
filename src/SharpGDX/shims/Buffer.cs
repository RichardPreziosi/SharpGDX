namespace SharpGDX.shims;

public class Buffer
{
	/** <code>UNSET_MARK</code> means the mark has not been set. */
	readonly static int UNSET_MARK = -1;

	/** The capacity of this buffer, which never change. */
	readonly int _capacity;

	/** <code>limit - 1</code> is the last element that can be read or written. Limit must be no less than zero and no greater than
	 * <code>capacity</code>. */
	int _limit;

	/** Mark is where position will be set when <code>reset()</code> is called. Mark is not set by default. Mark is always no less
	 * than zero and no greater than <code>position</code>. */
	int mark = UNSET_MARK;

	/** The current position of this buffer. Position is always no less than zero and no greater than <code>limit</code>. */
	int _position = 0;

	public  int limit()
	{
		return _limit;
	}

	public Buffer limit(int newLimit)
	{
		if (newLimit < 0 || newLimit > _capacity)
		{
			throw new ArgumentException();
		}

		_limit = newLimit;

		if (_position > newLimit)
		{
			_position = newLimit;
		}
		if ((mark != UNSET_MARK) && (mark > newLimit))
		{
			mark = UNSET_MARK;
		}
		return this;
	}

	public Buffer flip()
	{
		_limit = _position;
		_position = 0;
		mark = UNSET_MARK;
		return this;
	}

	public int remaining()
	{
		return _limit - _position;
	}

	public Buffer clear()
	{
		_position = 0;
		mark = UNSET_MARK;
		_limit = _capacity;
		return this;
	}

	public int capacity()
	{
		throw new NotImplementedException();
	}
	
	public void position(int i)
	{
		throw new NotImplementedException();
	}

	public int position()
	{
		throw new NotImplementedException();
	}
}