using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGDX.Utils;

namespace SharpGDX.Headless
{
	public class HeadlessNativesLoader
	{

		public static void load()
		{
			GdxNativesLoader.load();
		}
	}
}
