using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX.shims
{
	/** A <code>BufferOverflowException</code> is thrown when elements are written to a buffer but there is not enough remaining space
 * in the buffer.
 *
 * @since Android 1.0 */
	public class BufferOverflowException : SystemException
	{

		private static readonly long serialVersionUID = -5484897634319144535L;

		/** Constructs a <code>BufferOverflowException</code>.
		 *
		 * @since Android 1.0 */
		public BufferOverflowException()
		{
		}
	}
}