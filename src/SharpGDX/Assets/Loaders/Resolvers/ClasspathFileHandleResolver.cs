using SharpGDX.Shims;
using SharpGDX.Utils;
using SharpGDX.Mathematics;

namespace SharpGDX.Assets.Loaders.Resolvers;

public class ClasspathFileHandleResolver : FileHandleResolver {
	public FileHandle resolve (String fileName) {
		return Gdx.files.classpath(fileName);
	}
}
