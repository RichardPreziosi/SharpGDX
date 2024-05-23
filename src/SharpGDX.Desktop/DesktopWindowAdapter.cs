using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX.Desktop
{
	/** Convenience implementation of {@link DesktopWindowListener}. Derive from this class and only overwrite the methods you are
 * interested in.
 * @author badlogic */
	public class DesktopAdapter : IDesktopWindowListener
	{
	public void Created(DesktopWindow window)
	{
	}

	public void Iconified(bool isIconified)
	{
	}

	public void Maximized(bool isMaximized)
	{
	}

	public void FocusLost()
	{
	}

	public void FocusGained()
	{
	}

	public bool CloseRequested()
	{
		return true;
	}

	public void FilesDropped(String[] files)
	{
	}

	public void RefreshRequested()
	{
	}
	}
}
