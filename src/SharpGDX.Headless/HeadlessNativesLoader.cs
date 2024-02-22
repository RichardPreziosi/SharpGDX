using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGDX.utils;

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
