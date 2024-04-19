﻿using SharpGDX.Mathematics;
using SharpGDX.Shims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX.Utils
{
	/** A resizable, ordered or unordered long array. Avoids the boxing that occurs with ArrayList<Long>. If unordered, this class
 * avoids a memory copy when removing elements (the last element is moved to the removed element's position).
 * @author Nathan Sweet */
	public class LongArray
	{
		public long[] items;
		public int size;
		public bool ordered;

		/** Creates an ordered array with a capacity of 16. */
		public LongArray()
			:this(true, 16)
		{
		}

		/** Creates an ordered array with the specified capacity. */
		public LongArray(int capacity)
		: this(true, capacity)
		{
		}

		/** @param ordered If false, methods that remove elements may change the order of other elements in the array, which avoids a
		 *           memory copy.
		 * @param capacity Any elements added beyond this will cause the backing array to be grown. */
		public LongArray(bool ordered, int capacity)
		{
			this.ordered = ordered;
			items = new long[capacity];
		}

		/** Creates a new array containing the elements in the specific array. The new array will be ordered if the specific array is
		 * ordered. The capacity is set to the number of elements, so any subsequent elements added will cause the backing array to be
		 * grown. */
		public LongArray(LongArray array)
		{
			this.ordered = array.ordered;
			size = array.size;
			items = new long[size];
			Array.Copy(array.items, 0, items, 0, size);
		}

		/** Creates a new ordered array containing the elements in the specified array. The capacity is set to the number of elements,
		 * so any subsequent elements added will cause the backing array to be grown. */
		public LongArray(long[] array)
		: this(true, array, 0, array.Length)
		{
		}

		/** Creates a new array containing the elements in the specified array. The capacity is set to the number of elements, so any
		 * subsequent elements added will cause the backing array to be grown.
		 * @param ordered If false, methods that remove elements may change the order of other elements in the array, which avoids a
		 *           memory copy. */
		public LongArray(bool ordered, long[] array, int startIndex, int count)
		: this(ordered, count)
		{
			size = count;
			Array.Copy(array, startIndex, items, 0, count);
		}

		public void add(long value)
		{
			long[] items = this.items;
			if (size == items.Length) items = resize(Math.Max(8, (int)(size * 1.75f)));
			items[size++] = value;
		}

		public void add(long value1, long value2)
		{
			long[] items = this.items;
			if (size + 1 >= items.Length) items = resize(Math.Max(8, (int)(size * 1.75f)));
			items[size] = value1;
			items[size + 1] = value2;
			size += 2;
		}

		public void add(long value1, long value2, long value3)
		{
			long[] items = this.items;
			if (size + 2 >= items.Length) items = resize(Math.Max(8, (int)(size * 1.75f)));
			items[size] = value1;
			items[size + 1] = value2;
			items[size + 2] = value3;
			size += 3;
		}

		public void add(long value1, long value2, long value3, long value4)
		{
			long[] items = this.items;
			if (size + 3 >= items.Length) items = resize(Math.Max(8, (int)(size * 1.8f))); // 1.75 isn't enough when size=5.
			items[size] = value1;
			items[size + 1] = value2;
			items[size + 2] = value3;
			items[size + 3] = value4;
			size += 4;
		}

		public void addAll(LongArray array)
		{
			addAll(array.items, 0, array.size);
		}

		public void addAll(LongArray array, int offset, int length)
		{
			if (offset + length > array.size)
				throw new IllegalArgumentException("offset + length must be <= size: " + offset + " + " + length + " <= " + array.size);
			addAll(array.items, offset, length);
		}

		public void addAll(long[] array)
		{
			addAll(array, 0, array.Length);
		}

		public void addAll(long[] array, int offset, int length)
		{
			long[] items = this.items;
			int sizeNeeded = size + length;
			if (sizeNeeded > items.Length) items = resize(Math.Max(Math.Max(8, sizeNeeded), (int)(size * 1.75f)));
			Array.Copy(array, offset, items, size, length);
			size += length;
		}

		public long get(int index)
		{
			if (index >= size) throw new IndexOutOfBoundsException("index can't be >= size: " + index + " >= " + size);
			return items[index];
		}

		public void set(int index, long value)
		{
			if (index >= size) throw new IndexOutOfBoundsException("index can't be >= size: " + index + " >= " + size);
			items[index] = value;
		}

		public void incr(int index, long value)
		{
			if (index >= size) throw new IndexOutOfBoundsException("index can't be >= size: " + index + " >= " + size);
			items[index] += value;
		}

		public void incr(long value)
		{
			long[] items = this.items;
			for (int i = 0, n = size; i < n; i++)
				items[i] += value;
		}

		public void mul(int index, long value)
		{
			if (index >= size) throw new IndexOutOfBoundsException("index can't be >= size: " + index + " >= " + size);
			items[index] *= value;
		}

		public void mul(long value)
		{
			long[] items = this.items;
			for (int i = 0, n = size; i < n; i++)
				items[i] *= value;
		}

		public void insert(int index, long value)
		{
			if (index > size) throw new IndexOutOfBoundsException("index can't be > size: " + index + " > " + size);
			long[] items = this.items;
			if (size == items.Length) items = resize(Math.Max(8, (int)(size * 1.75f)));
			if (ordered)
				Array.Copy(items, index, items, index + 1, size - index);
			else
				items[size] = items[index];
			size++;
			items[index] = value;
		}

		/** Inserts the specified number of items at the specified index. The new items will have values equal to the values at those
		 * indices before the insertion. */
		public void insertRange(int index, int count)
		{
			if (index > size) throw new IndexOutOfBoundsException("index can't be > size: " + index + " > " + size);
			int sizeNeeded = size + count;
			if (sizeNeeded > items.Length) items = resize(Math.Max(Math.Max(8, sizeNeeded), (int)(size * 1.75f)));
			Array.Copy(items, index, items, index + count, size - index);
			size = sizeNeeded;
		}

		public void swap(int first, int second)
		{
			if (first >= size) throw new IndexOutOfBoundsException("first can't be >= size: " + first + " >= " + size);
			if (second >= size) throw new IndexOutOfBoundsException("second can't be >= size: " + second + " >= " + size);
			long[] items = this.items;
			long firstValue = items[first];
			items[first] = items[second];
			items[second] = firstValue;
		}

		public bool contains(long value)
		{
			int i = size - 1;
			long[] items = this.items;
			while (i >= 0)
				if (items[i--] == value) return true;
			return false;
		}

		public int indexOf(long value)
		{
			long[] items = this.items;
			for (int i = 0, n = size; i < n; i++)
				if (items[i] == value) return i;
			return -1;
		}

		public int lastIndexOf(char value)
		{
			long[] items = this.items;
			for (int i = size - 1; i >= 0; i--)
				if (items[i] == value) return i;
			return -1;
		}

		public bool removeValue(long value)
		{
			long[] items = this.items;
			for (int i = 0, n = size; i < n; i++)
			{
				if (items[i] == value)
				{
					removeIndex(i);
					return true;
				}
			}
			return false;
		}

		/** Removes and returns the item at the specified index. */
		public long removeIndex(int index)
		{
			if (index >= size) throw new IndexOutOfBoundsException("index can't be >= size: " + index + " >= " + size);
			long[] items = this.items;
			long value = items[index];
			size--;
			if (ordered)
				Array.Copy(items, index + 1, items, index, size - index);
			else
				items[index] = items[size];
			return value;
		}

		/** Removes the items between the specified indices, inclusive. */
		public void removeRange(int start, int end)
		{
			int n = size;
			if (end >= n) throw new IndexOutOfBoundsException("end can't be >= size: " + end + " >= " + size);
			if (start > end) throw new IndexOutOfBoundsException("start can't be > end: " + start + " > " + end);
			int count = end - start + 1, lastIndex = n - count;
			if (ordered)
				Array.Copy(items, start + count, items, start, n - (start + count));
			else
			{
				int i = Math.Max(lastIndex, end + 1);
				Array.Copy(items, i, items, start, n - i);
			}
			size = n - count;
		}

		/** Removes from this array all of elements contained in the specified array.
		 * @return true if this array was modified. */
		public bool removeAll(LongArray array)
		{
			int size = this.size;
			int startSize = size;
			long[] items = this.items;
			for (int i = 0, n = array.size; i < n; i++)
			{
				long item = array.get(i);
				for (int ii = 0; ii < size; ii++)
				{
					if (item == items[ii])
					{
						removeIndex(ii);
						size--;
						break;
					}
				}
			}
			return size != startSize;
		}

		/** Removes and returns the last item. */
		public long pop()
		{
			return items[--size];
		}

		/** Returns the last item. */
		public long peek()
		{
			return items[size - 1];
		}

		/** Returns the first item. */
		public long first()
		{
			if (size == 0) throw new IllegalStateException("Array is empty.");
			return items[0];
		}

		/** Returns true if the array has one or more items. */
		public bool notEmpty()
		{
			return size > 0;
		}

		/** Returns true if the array is empty. */
		public bool isEmpty()
		{
			return size == 0;
		}

		public void clear()
		{
			size = 0;
		}

		/** Reduces the size of the backing array to the size of the actual items. This is useful to release memory when many items
		 * have been removed, or if it is known that more items will not be added.
		 * @return {@link #items} */
		public long[] shrink()
		{
			if (items.Length != size) resize(size);
			return items;
		}

		/** Increases the size of the backing array to accommodate the specified number of additional items. Useful before adding many
		 * items to avoid multiple backing array resizes.
		 * @return {@link #items} */
		public long[] ensureCapacity(int additionalCapacity)
		{
			if (additionalCapacity < 0) throw new IllegalArgumentException("additionalCapacity must be >= 0: " + additionalCapacity);
			int sizeNeeded = size + additionalCapacity;
			if (sizeNeeded > items.Length) resize(Math.Max(Math.Max(8, sizeNeeded), (int)(size * 1.75f)));
			return items;
		}

		/** Sets the array size, leaving any values beyond the current size undefined.
		 * @return {@link #items} */
		public long[] setSize(int newSize)
		{
			if (newSize < 0) throw new IllegalArgumentException("newSize must be >= 0: " + newSize);
			if (newSize > items.Length) resize(Math.Max(8, newSize));
			size = newSize;
			return items;
		}

		protected long[] resize(int newSize)
		{
			long[] newItems = new long[newSize];
			long[] items = this.items;
			Array.Copy(items, 0, newItems, 0, Math.Min(size, newItems.Length));
			this.items = newItems;
			return newItems;
		}

		public void sort()
		{
			Array.Sort(items, 0, size);
		}

		public void reverse()
		{
			long[] items = this.items;
			for (int i = 0, lastIndex = size - 1, n = size / 2; i < n; i++)
			{
				int ii = lastIndex - i;
				long temp = items[i];
				items[i] = items[ii];
				items[ii] = temp;
			}
		}

		public void shuffle()
		{
			long[] items = this.items;
			for (int i = size - 1; i >= 0; i--)
			{
				int ii = MathUtils.random(i);
				long temp = items[i];
				items[i] = items[ii];
				items[ii] = temp;
			}
		}

		/** Reduces the size of the array to the specified size. If the array is already smaller than the specified size, no action is
		 * taken. */
		public void truncate(int newSize)
		{
			if (size > newSize) size = newSize;
		}

		/** Returns a random item from the array, or zero if the array is empty. */
		public long random()
		{
			if (size == 0) return 0;
			return items[MathUtils.random(0, size - 1)];
		}

		public long[] toArray()
		{
			long[] array = new long[size];
			Array.Copy(items, 0, array, 0, size);
			return array;
		}

		public override int GetHashCode()
		{
			if (!ordered) return base.GetHashCode();
			long[] items = this.items;
			int h = 1;
			for (int i = 0, n = size; i < n; i++)
			{
				long item = items[i];
				h = h * 31 + (int)(item ^ (item >>> 32));
			}
			return h;
		}

		public override bool Equals(Object? obj)
		{
			if (obj == this) return true;
			if (!ordered) return false;
			if (!(obj is LongArray)) return false;
			LongArray array = (LongArray)obj;
			if (!array.ordered) return false;
			int n = size;
			if (n != array.size) return false;
			long[] items1 = this.items, items2 = array.items;
			for (int i = 0; i < n; i++)
				if (items1[i] != items2[i]) return false;
			return true;
		}

		public override String ToString()
		{
			if (size == 0) return "[]";
			long[] items = this.items;
			StringBuilder buffer = new StringBuilder(32);
			buffer.Append('[');
			buffer.Append(items[0]);
			for (int i = 1; i < size; i++)
			{
				buffer.Append(", ");
				buffer.Append(items[i]);
			}
			buffer.Append(']');
			return buffer.ToString();
		}

		public String toString(String separator)
		{
			if (size == 0) return "";
			long[] items = this.items;
			StringBuilder buffer = new StringBuilder(32);
			buffer.Append(items[0]);
			for (int i = 1; i < size; i++)
			{
				buffer.Append(separator);
				buffer.Append(items[i]);
			}
			return buffer.ToString();
		}

		/** @see #LongArray(long[]) */
		static public LongArray with(long[] array)
		{
			return new LongArray(array);
		}
	}
}
