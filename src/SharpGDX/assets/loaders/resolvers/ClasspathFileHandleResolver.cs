using SharpGDX.files;

namespace SharpGDX.assets.loaders.resolvers;

public class ClasspathFileHandleResolver : FileHandleResolver
{
	public FileHandle resolve(string fileName)
	{
		return Gdx.files.classpath(fileName);
	}
}