using SharpGDX.files;

namespace SharpGDX.assets.loaders.resolvers;

public class ExternalFileHandleResolver : FileHandleResolver
{
	public FileHandle resolve(string fileName)
	{
		return Gdx.files.external(fileName);
	}
}