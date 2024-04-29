using SharpGDX.Shims;
using SharpGDX.Utils;
using SharpGDX.Mathematics;

namespace SharpGDX.Assets.Loaders.Resolvers;

/** This {@link FileHandleResolver} uses a given list of {@link Resolution}s to determine the best match based on the current back
 * buffer size. An example of how this resolver works:
 * 
 * <p>
 * Let's assume that we have only a single {@link Resolution} added to this resolver. This resolution has the following
 * properties:
 * </p>
 * 
 * <ul>
 * <li>{@code portraitWidth = 1920}</li>
 * <li>{@code portraitHeight = 1080}</li>
 * <li>{@code folder = "1920x1080"}</li>
 * </ul>
 * 
 * <p>
 * One would now supply a file to be found to the resolver. For this example, we assume it is "{@code textures/walls/brick.png}".
 * Since there is only a single {@link Resolution}, this will be the best match for any screen size. The resolver will now try to
 * find the file in the following ways:
 * </p>
 * 
 * <ul>
 * <li>{@code "textures/walls/1920x1080/brick.png"}</li>
 * <li>{@code "textures/walls/brick.png"}</li>
 * </ul>
 * 
 * <p>
 * The files are ultimately resolved via the given {{@link #baseResolver}. In case the first version cannot be resolved, the
 * fallback will try to search for the file without the resolution folder.
 * </p>
 */
public class ResolutionFileResolver : FileHandleResolver {

	public class Resolution {
		public readonly int portraitWidth;
		public readonly int portraitHeight;

		/** The name of the folder, where the assets which fit this resolution, are located. */
		public readonly String folder;

		/** Constructs a {@code Resolution}.
		 * @param portraitWidth This resolution's width.
		 * @param portraitHeight This resolution's height.
		 * @param folder The name of the folder, where the assets which fit this resolution, are located. */
		public Resolution (int portraitWidth, int portraitHeight, String folder) {
			this.portraitWidth = portraitWidth;
			this.portraitHeight = portraitHeight;
			this.folder = folder;
		}
	}

	protected readonly FileHandleResolver baseResolver;
	protected readonly Resolution[] descriptors;

	/** Creates a {@code ResolutionFileResolver} based on a given {@link FileHandleResolver} and a list of {@link Resolution}s.
	 * @param baseResolver The {@link FileHandleResolver} that will ultimately used to resolve the file.
	 * @param descriptors A list of {@link Resolution}s. At least one has to be supplied. */
	public ResolutionFileResolver (FileHandleResolver baseResolver, Resolution[] descriptors) {
		if (descriptors.Length == 0) throw new IllegalArgumentException("At least one Resolution needs to be supplied.");
		this.baseResolver = baseResolver;
		this.descriptors = descriptors;
	}

	public FileHandle resolve (String fileName) {
		Resolution bestResolution = choose(descriptors);
		FileHandle originalHandle = new FileHandle(fileName);
		FileHandle handle = baseResolver.resolve(resolve(originalHandle, bestResolution.folder));
		if (!handle.exists()) handle = baseResolver.resolve(fileName);
		return handle;
	}

	protected String resolve (FileHandle originalHandle, String suffix) {
		String parentString = "";
		FileHandle parent = originalHandle.parent();
		if (parent != null && !parent.name().Equals("")) {
			parentString = parent + "/";
		}
		return parentString + suffix + "/" + originalHandle.name();
	}

	static public Resolution choose (Resolution[] descriptors) {
		int w = Gdx.graphics.getBackBufferWidth(), h = Gdx.graphics.getBackBufferHeight();

		// Prefer the shortest side.
		Resolution best = descriptors[0];
		if (w < h) {
			for (int i = 0, n = descriptors.Length; i < n; i++) {
				Resolution other = descriptors[i];
				if (w >= other.portraitWidth && other.portraitWidth >= best.portraitWidth && h >= other.portraitHeight
					&& other.portraitHeight >= best.portraitHeight) best = descriptors[i];
			}
		} else {
			for (int i = 0, n = descriptors.Length; i < n; i++) {
				Resolution other = descriptors[i];
				if (w >= other.portraitHeight && other.portraitHeight >= best.portraitHeight && h >= other.portraitWidth
					&& other.portraitWidth >= best.portraitWidth) best = descriptors[i];
			}
		}
		return best;
	}
}
