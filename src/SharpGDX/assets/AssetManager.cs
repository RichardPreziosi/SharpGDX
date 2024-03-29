﻿using SharpGDX.assets.loaders.resolvers;
using SharpGDX.assets.loaders;
using SharpGDX.audio;
using SharpGDX.graphics;
using SharpGDX.utils.async;
using SharpGDX.utils;
using SharpGDX;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;
using SharpGDX.assets;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SharpGDX.assets
{
	/** Loads and stores assets like textures, bitmapfonts, tile maps, sounds, music and so on.
 * @author mzechner */
	public class AssetManager : Disposable
	{
		readonly ObjectMap<Type, ObjectMap<String, RefCountedContainer>> assets = new ();
		readonly ObjectMap<String, Type> assetTypes = new ();
		readonly ObjectMap<String, Array<String>> assetDependencies = new ();
	readonly ObjectSet<String> injected = new ();

	readonly ObjectMap<Type, ObjectMap<String, AssetLoader>> loaders = new ();
	readonly Array<AssetDescriptor> loadQueue = new ();
	readonly AsyncExecutor executor;

	readonly Array<AssetLoadingTask> tasks = new ();
	AssetErrorListener listener;
	int loaded;
	int toLoad;
	int peakTasks;

	readonly FileHandleResolver resolver;

	internal Logger log = new Logger("AssetManager", Application.LOG_NONE);

	/** Creates a new AssetManager with all default loaders. */
	public AssetManager()
	: this(new InternalFileHandleResolver())
		{
		
	}

	/** Creates a new AssetManager with all default loaders. */
	public AssetManager(FileHandleResolver resolver)
	: this(resolver, true)
		{
	}

	/** Creates a new AssetManager with optionally all default loaders. If you don't add the default loaders then you do have to
	 * manually add the loaders you need, including any loaders they might depend on.
	 * @param defaultLoaders whether to add the default loaders */
	public AssetManager(FileHandleResolver resolver, bool defaultLoaders)
	{
		this.resolver = resolver;
		if (defaultLoaders)
		{
			//setLoader(BitmapFont.class, new BitmapFontLoader(resolver));
			//setLoader(Music.class, new MusicLoader(resolver));
			//setLoader(Pixmap.class, new PixmapLoader(resolver));
			setLoader(typeof(Sound), new SoundLoader(resolver));
			//setLoader(TextureAtlas.class, new TextureAtlasLoader(resolver));
			//setLoader(Texture.class, new TextureLoader(resolver));
			//setLoader(Skin.class, new SkinLoader(resolver));
			//setLoader(ParticleEffect.class, new ParticleEffectLoader(resolver));
			//setLoader(com.badlogic.gdx.graphics.g3d.particles.ParticleEffect.class,
			//	new com.badlogic.gdx.graphics.g3d.particles.ParticleEffectLoader(resolver));
			//setLoader(PolygonRegion.class, new PolygonRegionLoader(resolver));
			//setLoader(I18NBundle.class, new I18NBundleLoader(resolver));
			//setLoader(Model.class, ".g3dj", new G3dModelLoader(new JsonReader(), resolver));
			//setLoader(Model.class, ".g3db", new G3dModelLoader(new UBJsonReader(), resolver));
			//setLoader(Model.class, ".obj", new ObjLoader(resolver));
			//setLoader(ShaderProgram.class, new ShaderProgramLoader(resolver));
			//setLoader(Cubemap.class, new CubemapLoader(resolver));
		}
executor = new AsyncExecutor(1, "AssetManager");
	}

	/** Returns the {@link FileHandleResolver} for which this AssetManager was loaded with.
	 * @return the file handle resolver which this AssetManager uses */
	public FileHandleResolver getFileHandleResolver()
{
	return resolver;
}

/** @param fileName the asset file name
 * @return the asset
 * @throws GdxRuntimeException if the asset is not loaded */
public  T get<T>(String fileName)
{
	lock (this)
	{
		return get<T>(fileName, true);
	}
}

/** @param fileName the asset file name
 * @param type the asset type
 * @return the asset
 * @throws GdxRuntimeException if the asset is not loaded */
public T get<T>(String fileName, Type type)
{
	lock (this)
	{
		return get<T>(fileName, type, true);
	}
}

		/** @param fileName the asset file name
		 * @param required true to throw GdxRuntimeException if the asset is not loaded, else null is returned
		 * @return the asset or null if it is not loaded and required is false */
		[Null]
public T get<T>(String fileName, bool required)
{
	lock (this)
	{
		Type type = assetTypes.get(fileName);
		if (type != null)
		{
			ObjectMap<String, RefCountedContainer> assetsByType = assets.get(type);
			if (assetsByType != null)
			{
				RefCountedContainer assetContainer = assetsByType.get(fileName);
				if (assetContainer != null) return (T)assetContainer.
				@object;
			}
		}

		if (required) throw new GdxRuntimeException("Asset not loaded: " + fileName);
		return default;
	}
}

/** @param fileName the asset file name
 * @param type the asset type
 * @param required true to throw GdxRuntimeException if the asset is not loaded, else null is returned
 * @return the asset or null if it is not loaded and required is false */
		[Null]
		public T get<T>(String fileName, Type type, bool required)
	{
		lock (this)
		{
			ObjectMap<String, RefCountedContainer> assetsByType = assets.get(type);
			if (assetsByType != null)
			{
				RefCountedContainer assetContainer = assetsByType.get(fileName);
				if (assetContainer != null) return (T)assetContainer.@object;
			}

			if (required) throw new GdxRuntimeException("Asset not loaded: " + fileName);
			return default;
		}
	}

	/** @param assetDescriptor the asset descriptor
	 * @return the asset
	 * @throws GdxRuntimeException if the asset is not loaded */
	public  T get<T>(AssetDescriptor < T > assetDescriptor) {
		lock (this)
		{
			return get<T>(assetDescriptor.fileName, assetDescriptor.type, true);
		}
}

/** @param type the asset type
 * @return all the assets matching the specified type */
public  Array<T> getAll<T>(Type type, Array<T> @out)
{
	lock (this)
	{
		ObjectMap<String, RefCountedContainer> assetsByType = assets.get(type);
		if (assetsByType != null)
		{
			foreach (RefCountedContainer assetRef in assetsByType.values())
				@out.add((T)assetRef.@object);
		}

		return @out;
	}
}

/** Returns true if an asset with the specified name is loading, queued to be loaded, or has been loaded. */
	public bool contains(String fileName)
	{
		lock (this)
		{
			if (tasks.size > 0 && tasks.first().assetDesc.fileName.Equals(fileName)) return true;

			for (int i = 0; i < loadQueue.size; i++)
				if (loadQueue.get(i).fileName.Equals(fileName))
					return true;
			return isLoaded(fileName);
		}
	}

	/** Returns true if an asset with the specified name and type is loading, queued to be loaded, or has been loaded. */
	public bool contains(String fileName, Type type)
	{
		lock (this)
		{
			if (tasks.size > 0)
			{
				AssetDescriptor assetDesc = tasks.first().assetDesc;
				if (assetDesc.type == type && assetDesc.fileName.Equals(fileName)) return true;
			}

			for (int i = 0; i < loadQueue.size; i++)
			{
				AssetDescriptor assetDesc = loadQueue.get(i);
				if (assetDesc.type == type && assetDesc.fileName.Equals(fileName)) return true;
			}

			return isLoaded(fileName, type);
		}
	}

	/** Removes the asset and all its dependencies, if they are not used by other assets.
	 * @param fileName the file name */
public void unload(String fileName)
{
	lock (this)
	{
		// check if it's currently processed (and the first element in the stack, thus not a dependency) and cancel if necessary
		if (tasks.size > 0)
		{
			AssetLoadingTask currentTask = tasks.first();
			if (currentTask.assetDesc.fileName.Equals(fileName))
			{
				log.info("Unload (from tasks): " + fileName);
				currentTask.cancel = true;
				currentTask.unload();
				return;
			}
		}

		var type = assetTypes.get(fileName);

		// check if it's in the queue
		int foundIndex = -1;
		for (int i = 0; i < loadQueue.size; i++)
		{
			if (loadQueue.get(i).fileName.Equals(fileName))
			{
				foundIndex = i;
				break;
			}
		}

		if (foundIndex != -1)
		{
			toLoad--;
			AssetDescriptor desc = loadQueue.removeIndex(foundIndex);
			log.info("Unload (from queue): " + fileName);

			// if the queued asset was already loaded, let the callback know it is available.
			if (type != null && desc.@params != null && desc.@params.loadedCallback != null)
			desc.@params.loadedCallback.finishedLoading(this, desc.fileName, desc.type);
			return;
		}

		if (type == null) throw new GdxRuntimeException("Asset not loaded: " + fileName);

		RefCountedContainer assetRef = assets.get(type).get(fileName);

		// if it is reference counted, decrement ref count and check if we can really get rid of it.
		assetRef.refCount--;
		if (assetRef.refCount <= 0)
		{
			log.info("Unload (dispose): " + fileName);

			// if it is disposable dispose it
			if (assetRef.@object is Disposable) ((Disposable)assetRef.@object).dispose();

			// remove the asset from the manager.
			assetTypes.remove(fileName);
			assets.get(type).remove(fileName);
		}
		else
			log.info("Unload (decrement): " + fileName);

		// remove any dependencies (or just decrement their ref count).
		Array<String> dependencies = assetDependencies.get(fileName);
		if (dependencies != null)
		{
			foreach (String dependency in dependencies)
			if (isLoaded(dependency)) unload(dependency);
		}

		// remove dependencies if ref count < 0
		if (assetRef.refCount <= 0) assetDependencies.remove(fileName);
	}
}

/** @param asset the asset
 * @return whether the asset is contained in this manager */
public bool containsAsset<T>(T asset)
{
	lock (this)
	{
		ObjectMap<String, RefCountedContainer> assetsByType = assets.get(asset.GetType());
		if (assetsByType == null) return false;
		foreach (RefCountedContainer assetRef in assetsByType.values())
			// TODO: Is this right?
		if (ReferenceEquals(assetRef.@object , asset) || asset.Equals(assetRef.@object)) return true;
		return false;
	}
}

/** @param asset the asset
 * @return the filename of the asset or null */
public String getAssetFileName<T>(T asset)
{
	lock (this)
	{
		foreach (var assetType in assets.keys())
		{
			ObjectMap<String, RefCountedContainer> assetsByType = assets.get(assetType);
			foreach (var entry in assetsByType)
			{
				Object @object = entry.value.@object;
				// TODO: Is this right?
				if (ReferenceEquals(@object , asset) || asset.Equals(@object)) return entry.key;
			}
		}
		return null;
	}
}

/** @param assetDesc the AssetDescriptor of the asset
 * @return whether the asset is loaded */
public bool isLoaded(AssetDescriptor assetDesc) {
	lock (this)
	{
		return isLoaded(assetDesc.fileName);
	}
}

/** @param fileName the file name of the asset
 * @return whether the asset is loaded */
public bool isLoaded(String fileName)
{
	lock (this)
	{
		if (fileName == null) return false;
		return assetTypes.containsKey(fileName);
	}
}

/** @param fileName the file name of the asset
 * @return whether the asset is loaded */
public bool isLoaded(String fileName, Type type) {
	lock (this)
	{
		ObjectMap<String, RefCountedContainer> assetsByType = assets.get(type);
		if (assetsByType == null) return false;
		return assetsByType.get(fileName) != null;
	}
}

/** Returns the default loader for the given type.
 * @param type The type of the loader to get
 * @return The loader capable of loading the type, or null if none exists */
public AssetLoader getLoader(Type type) {
	return getLoader(type, null);
}

/** Returns the loader for the given type and the specified filename. If no loader exists for the specific filename, the
 * default loader for that type is returned.
 * @param type The type of the loader to get
 * @param fileName The filename of the asset to get a loader for, or null to get the default loader
 * @return The loader capable of loading the type and filename, or null if none exists */
public AssetLoader getLoader(Type type, String fileName) {
	ObjectMap<String, AssetLoader> loaders = this.loaders.get(type);
	if (loaders == null || loaders.size < 1) return null;
	if (fileName == null) return loaders.get("");
	AssetLoader result = null;
	int length = -1;
	foreach (var entry in loaders.entries())
	{
		if (entry.key.Length > length && fileName.EndsWith(entry.key))
		{
			result = entry.value;
			length = entry.key.Length;
		}
	}
	return result;
}

/** Adds the given asset to the loading queue of the AssetManager.
 * @param fileName the file name (interpretation depends on {@link AssetLoader})
 * @param type the type of the asset. */
public void load<T>(String fileName, Type type)
{
	lock (this)
	{
		load<T>(fileName, type, null);
	}
}

/** Adds the given asset to the loading queue of the AssetManager.
 * @param fileName the file name (interpretation depends on {@link AssetLoader})
 * @param type the type of the asset.
 * @param parameter parameters for the AssetLoader. */
public void load<T>(String fileName, Type type, AssetLoaderParameters<T> parameter)
{
	lock (this)
	{
		AssetLoader loader = getLoader(type, fileName);
		if (loader == null) throw new GdxRuntimeException("No loader for type: " + (type).Name);

		// reset stats
		if (loadQueue.size == 0)
		{
			loaded = 0;
			toLoad = 0;
			peakTasks = 0;
		}

		// check if an asset with the same name but a different type has already been added.

		// check preload queue
		for (int i = 0; i < loadQueue.size; i++)
		{
			AssetDescriptor desc = loadQueue.get(i);
			if (desc.fileName.Equals(fileName) && !desc.type.Equals(type))
				throw new GdxRuntimeException(
					"Asset with name '" + fileName + "' already in preload queue, but has different type (expected: "
					+ (type).Name + ", found: " + (desc.type).Name +
					")");
		}

		// check task list
		for (int i = 0; i < tasks.size; i++)
		{
			AssetDescriptor desc = tasks.get(i).assetDesc;
			if (desc.fileName.Equals(fileName) && !desc.type.Equals(type))
				throw new GdxRuntimeException(
					"Asset with name '" + fileName + "' already in task list, but has different type (expected: "
					+ (type).Name + ", found: " + (desc.type).Name +
					")");
		}

		// check loaded assets
		Type otherType = assetTypes.get(fileName);
		if (otherType != null && !otherType.Equals(type))
			throw new GdxRuntimeException("Asset with name '" + fileName +
			                              "' already loaded, but has different type (expected: "
			                              + (type).Name + ", found: " +
			                              (otherType).Name + ")");

		toLoad++;
		AssetDescriptor assetDesc = new AssetDescriptor<T>(fileName, type, parameter);
		loadQueue.add(assetDesc);
		log.debug("Queued: " + assetDesc);
	}
}

/** Adds the given asset to the loading queue of the AssetManager.
 * @param desc the {@link AssetDescriptor} */
public void load<T>(AssetDescriptor<T> desc)
{
	lock (this)
	{
		load(desc.fileName, desc.type, desc.@params);
	}
}

/** Updates the AssetManager for a single task. Returns if the current task is still being processed or there are no tasks,
 * otherwise it finishes the current task and starts the next task.
 * @return true if all loading is finished. */
public bool update()
{
	lock (this)
	{
		try
		{
			if (tasks.size == 0)
			{
				// loop until we have a new task ready to be processed
				while (loadQueue.size != 0 && tasks.size == 0)
					nextTask();
				// have we not found a task? We are done!
				if (tasks.size == 0) return true;
			}

			return updateTask() && loadQueue.size == 0 && tasks.size == 0;
		}
		catch (Exception t)
		{
			handleTaskError(t);
			return loadQueue.size == 0;
		}
	}
}

/** Updates the AssetManager continuously for the specified number of milliseconds, yielding the CPU to the loading thread
 * between updates. This may block for less time if all loading tasks are complete. This may block for more time if the portion
 * of a single task that happens in the GL thread takes a long time. On GWT, updates for a single task instead (see
 * {@link #update()}).
 * @return true if all loading is finished. */
public bool update(int millis)
{
	if (Gdx.app.getType() == Application.ApplicationType.WebGL) return update();
	long endTime = TimeUtils.millis() + millis;
	while (true)
	{
		bool done = update();
		if (done || TimeUtils.millis() > endTime) return done;
		Thread.Yield();
	}
}

/** Returns true when all assets are loaded. Can be called from any thread but note {@link #update()} or related methods must
 * be called to process tasks. */
public bool isFinished()
{
	lock (this)
	{
		return loadQueue.size == 0 && tasks.size == 0;
	}
}

/** Blocks until all assets are loaded. */
public void finishLoading()
{
	log.debug("Waiting for loading to complete...");
	while (!update())
		Thread.Yield();
	log.debug("Loading complete.");
}

/** Blocks until the specified asset is loaded.
 * @param assetDesc the AssetDescriptor of the asset */
public T finishLoadingAsset<T>(AssetDescriptor assetDesc) {
	return finishLoadingAsset<T>(assetDesc.fileName);
}

/** Blocks until the specified asset is loaded.
 * @param fileName the file name (interpretation depends on {@link AssetLoader}) */
public T finishLoadingAsset<T>(String fileName) {
	log.debug("Waiting for asset to be loaded: " + fileName);
	while (true)
	{
		lock(this) {
			Type type = assetTypes.get(fileName);
			if (type != null)
			{
				ObjectMap<String, RefCountedContainer> assetsByType = assets.get(type);
				if (assetsByType != null)
				{
					RefCountedContainer assetContainer = assetsByType.get(fileName);
					if (assetContainer != null)
					{
						log.debug("Asset loaded: " + fileName);
						return (T)assetContainer.@object;
					}
				}
			}
			update();
		}
		Thread.Yield();
	}
}

internal void injectDependencies(String parentAssetFilename, Array<AssetDescriptor> dependendAssetDescs)
{
	lock (this)
	{
		ObjectSet<String> injected = this.injected;
		foreach (AssetDescriptor desc in dependendAssetDescs)
		{
			if (injected.contains(desc.fileName)) continue; // Ignore subsequent dependencies if there are duplicates.
			injected.add(desc.fileName);
			injectDependency(parentAssetFilename, desc);
		}
		injected.clear(32);
	}
}

private void injectDependency(String parentAssetFilename, AssetDescriptor dependendAssetDesc)
{
	lock (this)
	{
		// add the asset as a dependency of the parent asset
		Array<String> dependencies = assetDependencies.get(parentAssetFilename);
		if (dependencies == null)
		{
			dependencies = new ();
			assetDependencies.put(parentAssetFilename, dependencies);
		}

		dependencies.add(dependendAssetDesc.fileName);

		// if the asset is already loaded, increase its reference count.
		if (isLoaded(dependendAssetDesc.fileName))
		{
			log.debug("Dependency already loaded: " + dependendAssetDesc);
			var type = assetTypes.get(dependendAssetDesc.fileName);
			RefCountedContainer assetRef = assets.get(type).get(dependendAssetDesc.fileName);
			assetRef.refCount++;
			incrementRefCountedDependencies(dependendAssetDesc.fileName);
		}
		else
		{
			// else add a new task for the asset.
			log.info("Loading dependency: " + dependendAssetDesc);
			addTask(dependendAssetDesc);
		}
	}
}

/** Removes a task from the loadQueue and adds it to the task stack. If the asset is already loaded (which can happen if it was
 * a dependency of a previously loaded asset) its reference count will be increased. */
private void nextTask()
{
	AssetDescriptor assetDesc = loadQueue.removeIndex(0);

	// if the asset not meant to be reloaded and is already loaded, increase its reference count
	if (isLoaded(assetDesc.fileName))
	{
		log.debug("Already loaded: " + assetDesc);
		var type = assetTypes.get(assetDesc.fileName);
		RefCountedContainer assetRef = assets.get(type).get(assetDesc.fileName);
		assetRef.refCount++;
		incrementRefCountedDependencies(assetDesc.fileName);
		if (assetDesc.@params != null && assetDesc.@params.loadedCallback != null)
				assetDesc.@params.loadedCallback.finishedLoading(this, assetDesc.fileName, assetDesc.type);
		loaded++;
	}
	else
	{
		// else add a new task for the asset.
		log.info("Loading: " + assetDesc);
		addTask(assetDesc);
	}
}

/** Adds a {@link AssetLoadingTask} to the task stack for the given asset. */
private void addTask(AssetDescriptor assetDesc)
{
	AssetLoader loader = getLoader(assetDesc.type, assetDesc.fileName);
	if (loader == null) throw new GdxRuntimeException("No loader for type: " + (assetDesc.type).Name);
	tasks.add(new AssetLoadingTask(this, assetDesc, loader, executor));
	peakTasks++;
}

/** Adds an asset to this AssetManager */
protected void addAsset<T>(String fileName, Type type, T asset) {
	// add the asset to the filename lookup
	assetTypes.put(fileName, type);

	// add the asset to the type lookup
	ObjectMap<String, RefCountedContainer> typeToAssets = assets.get(type);
	if (typeToAssets == null)
	{
		typeToAssets = new ObjectMap<String, RefCountedContainer>();
		assets.put(type, typeToAssets);
	}
	RefCountedContainer assetRef = new RefCountedContainer();
	assetRef.@object = asset;
	typeToAssets.put(fileName, assetRef);
}

/** Updates the current task on the top of the task stack.
 * @return true if the asset is loaded or the task was cancelled. */
private bool updateTask()
{
	AssetLoadingTask task = tasks.peek();

	bool complete = true;
	try
	{
		complete = task.cancel || task.update();
	}
	catch (SystemException ex)
	{
		task.cancel = true;
		taskFailed(task.assetDesc, ex);
	}

	// if the task has been cancelled or has finished loading
	if (complete)
	{
		// increase the number of loaded assets and pop the task from the stack
		if (tasks.size == 1)
		{
			loaded++;
			peakTasks = 0;
		}
		tasks.pop();

		if (task.cancel) return true;

		addAsset(task.assetDesc.fileName, task.assetDesc.type, task.asset);

		// otherwise, if a listener was found in the parameter invoke it
		if (task.assetDesc.@params != null && task.assetDesc.@params.loadedCallback != null)
				task.assetDesc.@params.loadedCallback.finishedLoading(this, task.assetDesc.fileName, task.assetDesc.type);

		long endTime = TimeUtils.nanoTime();
		log.debug("Loaded: " + (endTime - task.startTime) / 1000000f + "ms " + task.assetDesc);

		return true;
	}
	return false;
}

/** Called when a task throws an exception during loading. The default implementation rethrows the exception. A subclass may
 * supress the default implementation when loading assets where loading failure is recoverable. */
protected void taskFailed(AssetDescriptor assetDesc, SystemException ex)
{
	throw ex;
}

private void incrementRefCountedDependencies(String parent)
{
	Array<String> dependencies = assetDependencies.get(parent);
	if (dependencies == null) return;

	foreach (String dependency in dependencies) {
			var type = assetTypes.get(dependency);
RefCountedContainer assetRef = assets.get(type).get(dependency);
assetRef.refCount++;
incrementRefCountedDependencies(dependency);
		}
	}

	/** Handles a runtime/loading error in {@link #update()} by optionally invoking the {@link AssetErrorListener}.
	 * @param t */
	private void handleTaskError(Exception t)
{
	log.error("Error loading asset.", t);

	if (tasks.isEmpty()) throw new GdxRuntimeException(t);

	// pop the faulty task from the stack
	AssetLoadingTask task = tasks.pop();
	AssetDescriptor assetDesc = task.assetDesc;

	// remove all dependencies
	if (task.dependenciesLoaded && task.dependencies != null)
	{
		foreach (AssetDescriptor desc in task.dependencies)
				unload(desc.fileName);
		}

		// clear the rest of the stack
		tasks.clear();

// inform the listener that something bad happened
if (listener != null)
	listener.error(assetDesc, t);
else
	throw new GdxRuntimeException(t);
	}

	/** Sets a new {@link AssetLoader} for the given type.
	 * @param type the type of the asset
	 * @param loader the loader */
	public  void setLoader<T,P>(Type type, AssetLoader<T, P> loader)
		where P : AssetLoaderParameters<T>
{
	lock (this)
	{
		setLoader(type, null, loader);
	}
}

/** Sets a new {@link AssetLoader} for the given type.
 * @param type the type of the asset
 * @param suffix the suffix the filename must have for this loader to be used or null to specify the default loader.
 * @param loader the loader */
public void setLoader<T, P>(Type type, String suffix, AssetLoader<T, P> loader)
	where P : AssetLoaderParameters<T>
{
	lock (this)
	{
		if (type == null) throw new ArgumentException("type cannot be null.");
		if (loader == null) throw new ArgumentException("loader cannot be null.");
		log.debug("Loader set: " + (type).Name + " -> " +
		          (loader.GetType()).Name);
		ObjectMap<String, AssetLoader> loaders = this.loaders.get(type);
		if (loaders == null) this.loaders.put(type, loaders = new ObjectMap<String, AssetLoader>());
		loaders.put(suffix == null ? "" : suffix, loader);
	}
}

/** @return the number of loaded assets */
public int getLoadedAssets()
{
	lock (this)
	{
		return assetTypes.size;
	}
}

/** @return the number of currently queued assets */
public int getQueuedAssets()
{
	lock (this)
	{
		return loadQueue.size + tasks.size;
	}
}

/** @return the progress in percent of completion. */
public float getProgress()
{
	lock (this)
	{
		if (toLoad == 0) return 1;
		float fractionalLoaded = loaded;
		if (peakTasks > 0)
		{
			fractionalLoaded += ((peakTasks - tasks.size) / (float)peakTasks);
		}

		return Math.Min(1, fractionalLoaded / toLoad);
	}
}

/** Sets an {@link AssetErrorListener} to be invoked in case loading an asset failed.
 * @param listener the listener or null */
public void setErrorListener(AssetErrorListener listener)
{
	lock (this)
	{
		this.listener = listener;
	}
}

/** Disposes all assets in the manager and stops all asynchronous loading. */
	public void dispose()
{
	log.debug("Disposing.");
	clear();
	executor.dispose();
}

/** Clears and disposes all assets and the preloading queue. */
public void clear()
{
	lock(this) {
		loadQueue.clear();
	}

	// Lock is temporarily released to yield to blocked executor threads
	// A pending async task can cause a deadlock if we do not release

	finishLoading();

	lock(this) {
		ObjectIntMap<String> dependencyCount = new ObjectIntMap<String>();
		while (assetTypes.size > 0)
		{
			// for each asset, figure out how often it was referenced
			dependencyCount.clear(51);
			Array<String> assets = assetTypes.keys().toArray();
			foreach (String asset in assets) {
					Array<String> dependencies = assetDependencies.get(asset);
if (dependencies == null) continue;
foreach (String dependency in dependencies)
	dependencyCount.getAndIncrement(dependency, 0, 1);
				}

				// only dispose of assets that are root assets (not referenced)
				foreach (String asset in assets)
	if (dependencyCount.get(asset, 0) == 0) unload(asset);
			}

			this.assets.clear(51);
this.assetTypes.clear(51);
this.assetDependencies.clear(51);
this.loaded = 0;
this.toLoad = 0;
this.peakTasks = 0;
this.loadQueue.clear();
this.tasks.clear();
		}
	}

	/** @return the {@link Logger} used by the {@link AssetManager} */
	public Logger getLogger()
{
	return log;
}

public void setLogger(Logger logger)
{
	log = logger;
}

/** Returns the reference count of an asset.
 * @param fileName */
public int getReferenceCount(String fileName)
{
	lock (this)
	{
		var type = assetTypes.get(fileName);
		if (type == null) throw new GdxRuntimeException("Asset not loaded: " + fileName);
		return assets.get(type).get(fileName).refCount;
	}
}

/** Sets the reference count of an asset.
 * @param fileName */
public void setReferenceCount(String fileName, int refCount)
{
	lock (this)
	{
		var type = assetTypes.get(fileName);
		if (type == null) throw new GdxRuntimeException("Asset not loaded: " + fileName);
		assets.get(type).get(fileName).refCount = refCount;
	}
}

/** @return a string containing ref count and dependency information for all assets. */
public String getDiagnostics()
{
	lock (this)
	{
		StringBuilder buffer = new StringBuilder(256);
		foreach (var entry in assetTypes)
		{
			String fileName = entry.key;
			Type type = entry.value;

			if (buffer.Length > 0) buffer.Append('\n');
			buffer.Append(fileName);
			buffer.Append(", ");
			buffer.Append((type).Name);
			buffer.Append(", refs: ");
			buffer.Append(assets.get(type).get(fileName).refCount);

			Array<String> dependencies = assetDependencies.get(fileName);
			if (dependencies != null)
			{
				buffer.Append(", deps: [");
				foreach (String dep in dependencies)
				{
					buffer.Append(dep);
					buffer.Append(',');
				}

				buffer.Append(']');
			}
		}

		return buffer.ToString();
	}
}

/** @return the file names of all loaded assets. */
public Array<String> getAssetNames()
{
	lock (this)
	{
		return assetTypes.keys().toArray();
	}
}

/** @return the dependencies of an asset or null if the asset has no dependencies. */
	public  Array<String> getDependencies (String fileName) {
		lock (this)
		{
			return assetDependencies.get(fileName);
		}
	}

	/** @return the type of a loaded asset. */
	public Type getAssetType(String fileName)
	{
		lock (this)
		{
			return assetTypes.get(fileName);
		}
	}

	class RefCountedContainer
{
	internal Object @object;
	internal int refCount = 1;
}
}
}