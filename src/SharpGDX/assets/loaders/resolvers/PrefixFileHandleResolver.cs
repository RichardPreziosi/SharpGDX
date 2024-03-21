using SharpGDX.files;

namespace SharpGDX.assets.loaders.resolvers;

/**
 * {@link FileHandleResolver} that adds a prefix to the filename before passing it to the base resolver. Can be used e.g. to use
 * a given subfolder from the base resolver. The prefix is added as is, you have to include any trailing '/' character if needed.
 * @author Xoppa
 */
public class PrefixFileHandleResolver : FileHandleResolver
{
	private FileHandleResolver baseResolver;
	private string prefix;

	public PrefixFileHandleResolver(FileHandleResolver baseResolver, string prefix)
	{
		this.baseResolver = baseResolver;
		this.prefix = prefix;
	}

	public FileHandleResolver getBaseResolver()
	{
		return baseResolver;
	}

	public string getPrefix()
	{
		return prefix;
	}

	public FileHandle resolve(string fileName)
	{
		return baseResolver.resolve(prefix + fileName);
	}

	public void setBaseResolver(FileHandleResolver baseResolver)
	{
		this.baseResolver = baseResolver;
	}

	public void setPrefix(string prefix)
	{
		this.prefix = prefix;
	}
}