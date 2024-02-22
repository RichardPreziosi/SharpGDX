using SharpGDX.utils;

namespace SharpGDX.shims;

public class Map<TKey, TValue>
	where TKey : notnull
{
	private readonly Dictionary<TKey, TValue> _dictionary = new();

	public void clear()
	{
	}

	public bool containsKey(TKey key)
	{
		return _dictionary.ContainsKey(key);
	}

	public IEnumerable<Entry<TKey, TValue>> entrySet()
	{
		foreach (var entry in _dictionary)
		{
			yield return new Entry<TKey, TValue> { key = entry.Key, value = entry.Value };
		}
	}

	public TValue get(TKey key, TValue defaultValue)
	{
		return _dictionary.GetValueOrDefault(key, defaultValue);
	}

	public void put(TKey key, TValue value)
	{
	}

	public void remove(TKey key)
	{
		_dictionary.Remove(key);
	}

	public class Entry<TKey, TValue>
	{
		internal TKey key;

		[Null] internal TValue value;

		public TKey getKey()
		{
			return key;
		}

		public TValue getValue()
		{
			return value;
		}
	}
}