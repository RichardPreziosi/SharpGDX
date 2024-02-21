// TODO: Verify all of this

namespace SharpGDX.utils.reflect
{
	/** Utilities for Array reflection.
 * @author nexsoftware */
	public sealed class ArrayReflection
	{

		/** Creates a new array with the specified component type and length. */
		static public Object newInstance(Type c, int size)
		{
			return Array.CreateInstance(c, size);
		}

		/** Returns the length of the supplied array. */
		static public int getLength(Object array)
		{
			if (array is not Array)
			{
				throw new InvalidOperationException($"Object of type '{array.GetType().Name}' is not an array.");
			}
			return ((Array)array).Length;
		}

		/** Returns the value of the indexed component in the supplied array. */
		static public Object? get(Object array, int index)
		{
			if (array is not Array)
			{
				throw new InvalidOperationException($"Object of type '{array.GetType().Name}' is not an array.");
			}

			if (((Array)array).Length <= index)
			{
				throw new IndexOutOfRangeException();
			}

			return ((Array)array).GetValue(index);
		}

		/** Sets the value of the indexed component in the supplied array to the supplied value. */
		static public void set(Object array, int index, Object value)
		{
			if (array is not Array)
			{
				throw new InvalidOperationException($"Object of type '{array.GetType().Name}' is not an array.");
			}

			if (((Array)array).Length <= index)
			{
				throw new IndexOutOfRangeException();
			}

			((Array)array).SetValue(value,index);
		}

	}
}
