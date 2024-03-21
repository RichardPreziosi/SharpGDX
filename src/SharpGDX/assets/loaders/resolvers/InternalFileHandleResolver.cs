using SharpGDX.files;

namespace SharpGDX.assets.loaders.resolvers;

public class InternalFileHandleResolver : FileHandleResolver
{
	public FileHandle resolve(string fileName)
	{
		return Gdx.files.Internal(fileName);
	}
}