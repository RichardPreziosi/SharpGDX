﻿using SharpGDX.Headless.mock.audio;
using SharpGDX.Headless.mock.graphics;
using SharpGDX.Headless.mock.input;
using SharpGDX.Headless;
using SharpGDX.shims;
using SharpGDX.utils;
using SharpGDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SharpGDX.Application;

namespace SharpGDX.Headless
{
	/** a headless implementation of a GDX Application primarily intended to be used in servers
 * @author Jon Renner */
	public class HeadlessApplication : Application
	{
		protected readonly ApplicationListener listener;
		protected Thread mainLoopThread;
		protected readonly HeadlessFiles files;
		protected readonly HeadlessNet net;
		protected readonly MockAudio audio;
		protected readonly MockInput input;
		protected readonly MockGraphics graphics;
		protected bool running = true;
		protected readonly Array<Action> runnables = new Array<Action>();
		protected readonly Array<Action> executedRunnables = new Array<Action>();
		protected readonly Array<LifecycleListener> lifecycleListeners = new Array<LifecycleListener>();
		protected int logLevel = LOG_INFO;
		protected ApplicationLogger applicationLogger;
		private String preferencesdir;

		public HeadlessApplication(ApplicationListener listener)
			: this(listener, null)
		{

		}

		public HeadlessApplication(ApplicationListener listener, HeadlessApplicationConfiguration config)
		{
			if (config == null) config = new HeadlessApplicationConfiguration();

			HeadlessNativesLoader.load();
			setApplicationLogger(new HeadlessApplicationLogger());
			this.listener = listener;
			this.files = new HeadlessFiles();
			this.net = new HeadlessNet(config);
			// the following elements are not applicable for headless applications
			// they are only implemented as mock objects
			this.graphics = new MockGraphics();
			this.graphics.setForegroundFPS(config.updatesPerSecond);
			this.audio = new MockAudio();
			this.input = new MockInput();

			this.preferencesdir = config.preferencesDirectory;

			Gdx.app = this;
			Gdx.files = files;
			Gdx.net = net;
			Gdx.audio = audio;
			Gdx.graphics = graphics;
			Gdx.input = input;

			initialize();
		}

		private void initialize()
		{
			mainLoopThread = new Thread(() =>
			{

				try
				{
					this.mainLoop();
				}
				catch (Exception t)
				{
					if (t is SystemException)
						throw (SystemException)t;
					else
						throw new GdxRuntimeException(t);
				}

			});
			mainLoopThread.Name = "HeadlessApplication";
			mainLoopThread.Start();
		}

		protected void mainLoop()
		{
			Array<LifecycleListener> lifecycleListeners = this.lifecycleListeners;

			listener.create();

			// unlike LwjglApplication, a headless application will eat up CPU in this while loop
			// it is up to the implementation to call Thread.sleep as necessary
			long t = TimeUtils.nanoTime() + graphics.getTargetRenderInterval();
			if (graphics.getTargetRenderInterval() >= 0f)
			{
				while (running)
				{
					long n = TimeUtils.nanoTime();
					if (t > n)
					{
						try
						{
							long sleep = t - n;

							// TODO: The original call is Thread.sleep(long, int). C# can't sleep that precisely, and I doubt Java can either.
							Thread.Sleep((int)(sleep / 1000000)); //, (int)(sleep % 1000000));
						}
						catch (ThreadInterruptedException e)
						{
						}

						t = t + graphics.getTargetRenderInterval();
					}
					else
						t = n + graphics.getTargetRenderInterval();

					executeRunnables();
					graphics.incrementFrameId();
					listener.render();
					graphics.updateTime();

					// If one of the runnables set running to false, for example after an exit().
					if (!running) break;
				}
			}

			lock (lifecycleListeners)
			{
				foreach (LifecycleListener listener in lifecycleListeners)
				{
					listener.pause();
					listener.dispose();
				}
			}

			listener.pause();
			listener.dispose();
		}

		public bool executeRunnables()
		{
			lock (runnables)
			{
				for (int i = runnables.size - 1; i >= 0; i--)
					executedRunnables.add(runnables.get(i));
				runnables.clear();
			}

			if (executedRunnables.size == 0) return false;
			for (int i = executedRunnables.size - 1; i >= 0; i--)
				executedRunnables.removeIndex(i).Invoke();
			return true;
		}

		public ApplicationListener getApplicationListener()
		{
			return listener;
		}

		public Graphics getGraphics()
		{
			return graphics;
		}

		public Audio getAudio()
		{
			return audio;
		}

		public Input getInput()
		{
			return input;
		}

		public Files getFiles()
		{
			return files;
		}

		public Net getNet()
		{
			return net;
		}

		public ApplicationType getType()
		{
			return ApplicationType.HeadlessDesktop;
		}

		public int getVersion()
		{
			return 0;
		}

		public long getJavaHeap()
		{
			return GC.GetTotalMemory(false);
		}

		public long getNativeHeap()
		{
			return getJavaHeap();
		}

		ObjectMap<String, Preferences> preferences = new ObjectMap<String, Preferences>();

		public Preferences getPreferences(String name)
		{
			if (preferences.containsKey(name))
			{
				return preferences.get(name);
			}
			else
			{
				Preferences prefs = new HeadlessPreferences(name, this.preferencesdir);
				preferences.put(name, prefs);
				return prefs;
			}
		}

		public Clipboard getClipboard()
		{
			// no clipboards for headless apps
			return null;
		}

		public void postRunnable(Action runnable)
		{
			lock (runnables)
			{
				runnables.add(runnable);
			}
		}

		public void debug(String tag, String message)
		{
			if (logLevel >= LOG_DEBUG) getApplicationLogger().debug(tag, message);
		}

		public void debug(String tag, String message, Exception exception)
		{
			if (logLevel >= LOG_DEBUG) getApplicationLogger().debug(tag, message, exception);
		}

		public void log(String tag, String message)
		{
			if (logLevel >= LOG_INFO) getApplicationLogger().log(tag, message);
		}

		public void log(String tag, String message, Exception exception)
		{
			if (logLevel >= LOG_INFO) getApplicationLogger().log(tag, message, exception);
		}

		public void error(String tag, String message)
		{
			if (logLevel >= LOG_ERROR) getApplicationLogger().error(tag, message);
		}

		public void error(String tag, String message, Exception exception)
		{
			if (logLevel >= LOG_ERROR) getApplicationLogger().error(tag, message, exception);
		}

		public void setLogLevel(int logLevel)
		{
			this.logLevel = logLevel;
		}

		public int getLogLevel()
		{
			return logLevel;
		}

		public void setApplicationLogger(ApplicationLogger applicationLogger)
		{
			this.applicationLogger = applicationLogger;
		}

		public ApplicationLogger getApplicationLogger()
		{
			return applicationLogger;
		}

		public void exit()
		{
			postRunnable(() => { running = false; });
		}

		public void addLifecycleListener(LifecycleListener listener)
		{
			lock (lifecycleListeners)
			{
				lifecycleListeners.add(listener);
			}
		}

		public void removeLifecycleListener(LifecycleListener listener)
		{
			lock (lifecycleListeners)
			{
				lifecycleListeners.removeValue(listener, true);
			}
		}
	}
}