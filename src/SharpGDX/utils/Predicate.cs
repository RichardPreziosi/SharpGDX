using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX.utils
{
	/** Interface used to select items within an iterator against a predicate.
 * @author Xoppa */
	public interface Predicate<T>
	{

		/** @return true if the item matches the criteria and should be included in the iterator's items */
		bool evaluate(T arg0);

		public class PredicateIterator<T> : IEnumerator<T>
		{
		public IEnumerator<T> iterator;
		public Predicate<T> predicate;
		public bool end = false;
		public bool peeked = false;
		public T? next = default;

		public PredicateIterator( IEnumerable<T> iterable,  Predicate<T> predicate)
		: this(iterable.GetEnumerator(), predicate)
			{
			
		}

		public PredicateIterator(IEnumerator<T> iterator,  Predicate<T> predicate)
		{
			set(iterator, predicate);
		}

		public void set(IEnumerable<T> iterable,  Predicate<T> predicate)
		{
			set(iterable.GetEnumerator(), predicate);
		}

		public void set(IEnumerator<T> iterator,  Predicate<T> predicate)
		{
			this.iterator = iterator;
			this.predicate = predicate;
			end = peeked = false;
			next = default;
		}

		public bool MoveNext()
		{
			if (end) return false;
			if (next != null) return true;
			peeked = true;
			while (iterator.MoveNext())
			{
				T n = iterator.Current;
				if (predicate.evaluate(n))
				{
					next = n;
					return true;
				}
			}
			end = true;
			return false;
		}
			
		public void remove()
		{
			if (peeked) throw new GdxRuntimeException("Cannot remove between a call to hasNext() and next().");
			// TODO: iterator.remove();
			throw new NotImplementedException();
		}

		public void Reset(){ throw new NotImplementedException(); }

		public T? Current
		{
			get
			{
				if (next == null && !MoveNext()) return default;
				T result = next;
				next = default;
				peeked = false;
				return result;
				}
		}

		object? IEnumerator.Current => Current;

		public void Dispose()
		{

			var s = new List<string>();
		}
		}

	public class PredicateIterable<T> : IEnumerable<T> 
	{
		public IEnumerable<T> iterable;
	public Predicate<T> predicate;
	public PredicateIterator<T> iterator = null;

	public PredicateIterable(IEnumerable<T> iterable, Predicate<T> predicate)
	{
		Set(iterable, predicate);
	}

	public void Set(IEnumerable<T> iterable, Predicate<T> predicate)
	{
		this.iterable = iterable;
		this.predicate = predicate;
	}

	/** Returns an iterator. Remove is supported.
	 * <p>
	 * If {@link Collections#allocateIterators} is false, the same iterator instance is returned each time this method is
	 * called. Use the {@link Predicate.PredicateIterator} constructor for nested or multithreaded iteration. */
	public IEnumerator<T> GetEnumerator()
	{
		if (Collections.allocateIterators) return new PredicateIterator<T>(iterable.GetEnumerator(), predicate);
		if (iterator == null)
			iterator = new PredicateIterator<T>(iterable.GetEnumerator(), predicate);
		else
			iterator.set(iterable.GetEnumerator(), predicate);
		return iterator;
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
	}
}
}
