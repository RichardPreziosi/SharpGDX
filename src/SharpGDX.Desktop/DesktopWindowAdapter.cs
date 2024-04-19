using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX.Desktop
{
	/** Convenience implementation of {@link Lwjgl3WindowListener}. Derive from this class and only overwrite the methods you are
 * interested in.
 * @author badlogic */
	public class Lwjgl3WindowAdapter : Lwjgl3WindowListener
	{
	public void created(Lwjgl3Window window)
	{
	}

	public void iconified(bool isIconified)
	{
	}

	public void maximized(bool isMaximized)
	{
	}

	public void focusLost()
	{
	}

	public void focusGained()
	{
	}

	public bool closeRequested()
	{
		return true;
	}

	public void filesDropped(String[] files)
	{
	}

	public void refreshRequested()
	{
	}
	}
}
