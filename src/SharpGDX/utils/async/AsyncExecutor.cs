using SharpGDX.utils.async;
using SharpGDX.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

// TODO: This whole thing really does nothing.
namespace SharpGDX.utils.async
{
	/** Allows asnynchronous execution of {@link AsyncTask} instances on a separate thread. Needs to be disposed via a call to
 * {@link #dispose()} when no longer used, in which case the executor waits for running tasks to finish. Scheduled but not yet
 * running tasks will not be executed.
 * @author badlogic */
	public class AsyncExecutor : Disposable
	{
		//private readonly SynchronizationContext executor;

		/** Creates a new AsynchExecutor with the name "AsynchExecutor-Thread". */
		public AsyncExecutor(int maxConcurrent)
			: this(maxConcurrent, "AsynchExecutor-Thread")
		{

		}

		/** Creates a new AsynchExecutor that allows maxConcurrent {@link Runnable} instances to run in parallel.
		 * @param maxConcurrent
		 * @param name The name of the threads. */
		public AsyncExecutor(int maxConcurrent, String name)
		{
			//ThreadPool.SetMaxThreads(maxConcurrent, )


			//	var pool =

			//		executor = new SynchronizationContext();

			//	executor.MaximumConcurrencyLevel = maxConcurrent;


			//	Executors.newFixedThreadPool(maxConcurrent, new ThreadFactory() {
			//		@Override
			//		public Thread newThread(Runnable r)
			//	{
			//		Thread thread = new Thread(r, name);
			//		thread.setDaemon(true);
			//		return thread;
			//	}
			//});
		}

		/** Submits a {@link Runnable} to be executed asynchronously. If maxConcurrent runnables are already running, the runnable will
		 * be queued.
		 * @param task the task to execute asynchronously */
		public AsyncResult<T> submit<T>(AsyncTask<T> task)
		{
			//if (executor.isShutdown())
			//{
			//	throw new GdxRuntimeException("Cannot run tasks on an executor that has been shutdown (disposed)");
			//}

			//return new AsyncResult(executor.submit(new Callable<T>()
			//{
			//	@Override
			//	public T call() throws Exception {
			//	return task.call();
			//}
			//}));

			// TODO: Should this be the super naughty Task.Run()?
			return new AsyncResult<T>(new Task<T>(task.call));
		}

		/** Waits for running {@link AsyncTask} instances to finish, then destroys any resources like threads. Can not be used after
		 * this method is called. */
		public void dispose()
		{
			//executor.shutdown();
			//try
			//{
			//	executor.awaitTermination(Long.MAX_VALUE, TimeUnit.SECONDS);
			//}
			//catch (ThreadInterruptedException e)
			//{
			//	throw new GdxRuntimeException("Couldn't shutdown loading thread", e);
			//}
		}
	}
}