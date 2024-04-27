using File = SharpGDX.Shims.File;
using SharpGDX.Utils;

namespace SharpGDX.Headless
{
	/** @author mzechner
 * @author Nathan Sweet */
	public sealed class HeadlessFileHandle : FileHandle
	{
	public HeadlessFileHandle(String fileName, Files.FileType type)
	: base(fileName, type)
		{
	}

	public HeadlessFileHandle(File file, Files.FileType type)
	: base(file, type)
		{
	}

	public FileHandle child(String name)
	{
		if (_file.getPath().Length == 0) return new HeadlessFileHandle(new File(name), _type);
		return new HeadlessFileHandle(new File(_file, name), _type);
	}

	public FileHandle sibling(String name)
	{
		if (_file.getPath().Length == 0) throw new GdxRuntimeException("Cannot get the sibling of the root.");
		return new HeadlessFileHandle(new File(_file.getParent(), name), _type);
	}

	public FileHandle parent()
	{
		File parent = _file.getParentFile();
		if (parent == null)
		{
			if (_type == Files.FileType.Absolute)
				parent = new File("/");
			else
				parent = new File("");
		}
		return new HeadlessFileHandle(parent,_type);
	}

	public File file()
	{
		if (_type == Files.FileType.External) return new File(HeadlessFiles.externalPath, _file.getPath());
		if (_type == Files.FileType.Local) return new File(HeadlessFiles.localPath, _file.getPath());
		return _file;
	}
	}
}
