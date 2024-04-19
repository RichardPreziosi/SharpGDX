using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGDX.Utils;

namespace SharpGDX.Desktop
{
	public static class Lwjgl3NativesLoader
	{

		static Lwjgl3NativesLoader(){
			// TODO: System.setProperty("org.lwjgl.input.Mouse.allowNegativeMouseCoords", "true");
		}

		static public void load()
		{
			GdxNativesLoader.load();
		}
	}
}
