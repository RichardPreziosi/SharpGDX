using SharpGDX.files;

namespace SharpGDX.assets.loaders.resolvers;

public class AbsoluteFileHandleResolver : FileHandleResolver
{
	public FileHandle resolve(string fileName)
	{
		return Gdx.files.absolute(fileName);
	}
}