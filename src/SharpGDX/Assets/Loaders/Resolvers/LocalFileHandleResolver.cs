using SharpGDX.Files;
using SharpGDX.Shims;
using SharpGDX.Utils;
using SharpGDX.Mathematics;

namespace SharpGDX.Assets.Loaders.Resolvers;

public class LocalFileHandleResolver : IFileHandleResolver {
	public FileHandle resolve (String fileName) {
		return Gdx.files.local(fileName);
	}
}
