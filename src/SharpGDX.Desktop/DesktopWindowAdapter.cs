namespace SharpGDX.Desktop
{
	/** Convenience implementation of {@link DesktopWindowListener}. Derive from this class and only overwrite the methods you are
 * interested in.
 * @author badlogic */
	public class DesktopWindowAdapter : DesktopWindowListener
	{
	public virtual void created(DesktopWindow window)
	{
	}

	public virtual void iconified(bool isIconified)
	{
	}

	public virtual void maximized(bool isMaximized)
	{
	}

	public virtual void focusLost()
	{
	}

	public virtual void focusGained()
	{
	}

	public virtual bool closeRequested()
	{
		return true;
	}

	public virtual void filesDropped(String[] files)
	{
	}

	public virtual void refreshRequested()
	{
	}
	}
}
