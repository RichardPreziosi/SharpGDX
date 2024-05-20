using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGDX.Utils;

namespace SharpGDX.Graphics
{
	/**
 * <p>
 * Represents a mouse cursor. Create a cursor via {@link Graphics#newCursor(Pixmap, int, int)}. To set the cursor use
 * {@link Graphics#setCursor(Cursor)}. To use one of the system cursors, call Graphics#setSystemCursor
 * </p>
 **/
	public interface Cursor : Disposable
	{

	public enum SystemCursor
	{
		Arrow, Ibeam, Crosshair, Hand, HorizontalResize, VerticalResize, NWSEResize, NESWResize, AllResize, NotAllowed, None
	}
	}
}
