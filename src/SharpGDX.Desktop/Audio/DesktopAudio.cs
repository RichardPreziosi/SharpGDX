using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGDX.Utils;

namespace SharpGDX.Desktop.Audio
{
	public interface Lwjgl3Audio :SharpGDX.Audio, Disposable {

		void update();
	}
}
