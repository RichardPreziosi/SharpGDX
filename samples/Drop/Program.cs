using System.Collections;
using SharpGDX.math;
using SharpGDX.utils;

namespace Drop;

internal class Program
{
	private static void Main()
	{
		LongMap<string> l = new();

		l.put(2, "Hi");

		foreach (var f in l)
		{
			Console.WriteLine(f.value);
		}

		Console.Read();
	}
}