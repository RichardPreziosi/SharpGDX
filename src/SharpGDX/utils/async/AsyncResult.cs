using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SharpGDX.utils.async
{
	/** Returned by {@link AsyncExecutor#submit(AsyncTask)}, allows to poll for the result of the asynch workload.
 * @author badlogic */
	public class AsyncResult<T>
	{
		private readonly Task<T> future;

		internal AsyncResult(Task<T> future)
		{
			this.future = future;
		}

		/** @return whether the {@link AsyncTask} is done */
		public bool isDone()
		{
			// TODO: Should this be IsCompleted or IsCompletedSuccessfully?
			return future.IsCompleted;
		}

		/** @return waits if necessary for the computation to complete and then returns the result
		 * @throws GdxRuntimeException if there was an error */
		public T? get()
		{
			// TODO: This entire method is pretty suspect. Not 100% sure how this did anything in Java, because it's not doing anything in C#.
			try
			{
				return future.Result;
			}
			catch (ThreadInterruptedException ex)
			{
				return default;
			}
			catch (Exception ex)
			{
				throw new GdxRuntimeException(ex);
			}
		}
	}
}
