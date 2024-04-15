using StringWriter = SharpGDX.Shims.StringWriter;

namespace Drop
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var writer = new StringWriter(12);

			writer.append("hi");

			var s = writer.ToString();
		}
	}
}
