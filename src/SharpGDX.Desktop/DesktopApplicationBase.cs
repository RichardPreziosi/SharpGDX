using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGDX.Desktop.Audio;

namespace SharpGDX.Desktop
{
	public interface Lwjgl3ApplicationBase : Application
	{

	Lwjgl3Audio createAudio (Lwjgl3ApplicationConfiguration config);

	Lwjgl3Input createInput (Lwjgl3Window window);
	}
}
