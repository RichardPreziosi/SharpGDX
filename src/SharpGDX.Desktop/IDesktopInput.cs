using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Windowing.GraphicsLibraryFramework;
using SharpGDX.Utils;

namespace SharpGDX.Desktop
{
	public interface Lwjgl3Input : Input, Disposable {

		// TODO: Really don't want to expose this, marked internal.
		internal unsafe void windowHandleChanged(Window* windowHandle);

		void update();

		void prepareNext();

		void resetPollingStates();
	}
}
