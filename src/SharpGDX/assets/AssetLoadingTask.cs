﻿using SharpGDX.assets.loaders;
using SharpGDX.files;
using SharpGDX.utils.async;
using SharpGDX.utils;

namespace SharpGDX.assets
{
	/** Responsible for loading an asset through an {@link AssetLoader} based on an {@link AssetDescriptor}. Please don't forget to
 * update the overriding emu file on GWT backend when changing this file!
 * 
 * @author mzechner */
	class AssetLoadingTask : AsyncTask<object> {
		AssetManager manager;
		internal readonly AssetDescriptor assetDesc;
		readonly AssetLoader loader;
		readonly AsyncExecutor executor;
		internal readonly long startTime;

	volatile bool asyncDone;
	internal volatile bool dependenciesLoaded;
	internal volatile Array<AssetDescriptor> dependencies;
	volatile AsyncResult<object> depsFuture;
	volatile AsyncResult<object> loadFuture;
	internal volatile Object asset;

	internal volatile bool cancel;

	public AssetLoadingTask(AssetManager manager, AssetDescriptor assetDesc, AssetLoader loader, AsyncExecutor threadPool)
	{
		this.manager = manager;
		this.assetDesc = assetDesc;
		this.loader = loader;
		this.executor = threadPool;
		startTime = manager.log.getLevel() == Logger.DEBUG ? TimeUtils.nanoTime() : 0;
	}

	/** Loads parts of the asset asynchronously if the loader is an {@link AsynchronousAssetLoader}. */
	public object? call() // TODO: throws Exception
	{
		if (cancel) return null;
		AsynchronousAssetLoader asyncLoader = (AsynchronousAssetLoader)loader;
		if (!dependenciesLoaded) {
			dependencies = asyncLoader.getDependencies(assetDesc.fileName, resolve(loader, assetDesc), assetDesc.@params);
			if (dependencies != null)
			{
				removeDuplicates(dependencies);
				manager.injectDependencies(assetDesc.fileName, dependencies);
			}
			else
			{
				// if we have no dependencies, we load the async part of the task immediately.
				asyncLoader.loadAsync(manager, assetDesc.fileName, resolve(loader, assetDesc), assetDesc.@params);
				asyncDone = true;
			}
		} else {
			asyncLoader.loadAsync(manager, assetDesc.fileName, resolve(loader, assetDesc), assetDesc.@params);
			asyncDone = true;
		}
		return null;
	}

	/** Updates the loading of the asset. In case the asset is loaded with an {@link AsynchronousAssetLoader}, the loaders
	 * {@link AsynchronousAssetLoader#loadAsync(AssetManager, String, FileHandle, AssetLoaderParameters)} method is first called on
	 * a worker thread. Once this method returns, the rest of the asset is loaded on the rendering thread via
	 * {@link AsynchronousAssetLoader#loadSync(AssetManager, String, FileHandle, AssetLoaderParameters)}.
	 * @return true in case the asset was fully loaded, false otherwise
	 * @throws GdxRuntimeException */
	public bool update()
	{
		if (loader is SynchronousAssetLoader)
			handleSyncLoader();
		else
			handleAsyncLoader();
		return asset != null;
	}

	private void handleSyncLoader()
	{
		SynchronousAssetLoader syncLoader = (SynchronousAssetLoader)loader;
		if (!dependenciesLoaded)
		{
			dependenciesLoaded = true;
			dependencies = syncLoader.getDependencies(assetDesc.fileName, resolve(loader, assetDesc), assetDesc.@params);
			if (dependencies == null)
			{
				asset = syncLoader.load(manager, assetDesc.fileName, resolve(loader, assetDesc), assetDesc.@params);
				return;
			}
			removeDuplicates(dependencies);
			manager.injectDependencies(assetDesc.fileName, dependencies);
		}
		else
			asset = syncLoader.load(manager, assetDesc.fileName, resolve(loader, assetDesc), assetDesc.@params);
	}

	private void handleAsyncLoader()
	{
		AsynchronousAssetLoader asyncLoader = (AsynchronousAssetLoader)loader;
		if (!dependenciesLoaded)
		{
			if (depsFuture == null)
				depsFuture = executor.submit(this);
			else if (depsFuture.isDone())
			{
				try
				{
					depsFuture.get();
				}
				catch (Exception e)
				{
					throw new GdxRuntimeException("Couldn't load dependencies of asset: " + assetDesc.fileName, e);
				}
				dependenciesLoaded = true;
				if (asyncDone)
					asset = asyncLoader.loadSync(manager, assetDesc.fileName, resolve(loader, assetDesc), assetDesc.@params);
			}
		}
		else if (loadFuture == null && !asyncDone)
			loadFuture = executor.submit(this);
		else if (asyncDone)
			asset = asyncLoader.loadSync(manager, assetDesc.fileName, resolve(loader, assetDesc), assetDesc.@params);
		else if (loadFuture.isDone())
		{
			try
			{
				loadFuture.get();
			}
			catch (Exception e)
			{
				throw new GdxRuntimeException("Couldn't load asset: " + assetDesc.fileName, e);
			}
			asset = asyncLoader.loadSync(manager, assetDesc.fileName, resolve(loader, assetDesc), assetDesc.@params);
		}
	}

	/** Called when this task is the task that is currently being processed and it is unloaded. */
	public void unload()
	{
		if (loader is AsynchronousAssetLoader)
			((AsynchronousAssetLoader)loader).unloadAsync(manager, assetDesc.fileName, resolve(loader, assetDesc), assetDesc.@params);
	}

	private FileHandle resolve(AssetLoader loader, AssetDescriptor assetDesc)
	{
		if (assetDesc.file == null) assetDesc.file = loader.resolve(assetDesc.fileName);
		return assetDesc.file;
	}

	private void removeDuplicates(Array<AssetDescriptor> array)
	{
		bool ordered = array.ordered;
		array.ordered = true;
		for (int i = 0; i < array.size; ++i)
		{
			 String fn = array.get(i).fileName;
			 Type type = array.get(i).type;
			for (int j = array.size - 1; j > i; --j)
				if (type == array.get(j).type && fn.Equals(array.get(j).fileName)) array.removeIndex(j);
		}
		array.ordered = ordered;
	}
}
}
