﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX.utils.async
{
	/** Task to be submitted to an {@link AsyncExecutor}, returning a result of type T.
	 * @author badlogic */
	public interface AsyncTask<T>
	{
		public T call(); // TODO: throws Exception;
	}
}
