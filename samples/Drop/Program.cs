using SharpGDX.math;
using SharpGDX.utils;

namespace Drop;

internal class Program
{
	private static void Main()
	{
		var array = new Array<Vector2>();

		array.add(new Vector2(1, 1));
		array.add(new Vector2(2, 2));
		array.add(new Vector2(3, 3));

		Console.WriteLine(array.size);

		foreach (var x in array)
		{
			Console.WriteLine(x);
		}

		Console.Read();
	}
}