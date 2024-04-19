using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGDX.Utils;

namespace SharpGDX.Desktop
{
	public interface Lwjgl3Input : Input, Disposable {

		void windowHandleChanged(long windowHandle);

		void update();

		void prepareNext();

		void resetPollingStates();
	}
}
