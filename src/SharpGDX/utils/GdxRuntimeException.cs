using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX.utils
{
	/** Typed runtime exception used throughout libGDX
 *
 * @author mzechner */
	public class GdxRuntimeException : SystemException
	{
		private static readonly long serialVersionUID = 6735854402467673117L;

		public GdxRuntimeException(String message)
			: base(message)
		{
		}

		public GdxRuntimeException(Exception t)
			: base(t.Message)
		{
		}

		public GdxRuntimeException(String message, Exception t)
			: base(message, t)
		{
		}
	}
}