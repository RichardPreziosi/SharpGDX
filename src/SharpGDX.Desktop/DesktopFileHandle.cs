using SharpGDX.Files;
using File = SharpGDX.Shims.File;
using SharpGDX.Utils;

namespace SharpGDX.Desktop
{
	/** @author mzechner
 * @author Nathan Sweet */
	public sealed class DesktopFileHandle : FileHandle
	{
	public DesktopFileHandle(String fileName, IFiles.FileType type)
	: base(fileName, type)
		{
	}

	public DesktopFileHandle(File file, IFiles.FileType type)
	:base(file, type)
		{
		
	}

	public FileHandle child(String name)
	{
		if (_file.getPath().Length == 0) return new DesktopFileHandle(new File(name), _type);
		return new DesktopFileHandle(new File(_file, name), _type);
	}

	public FileHandle sibling(String name)
	{
		if (_file.getPath().Length == 0) throw new GdxRuntimeException("Cannot get the sibling of the root.");
		return new DesktopFileHandle(new File(_file.getParent(), name), _type);
	}

	public FileHandle parent()
	{
		File parent = _file.getParentFile();
		if (parent == null)
		{
			if (_type == IFiles.FileType.Absolute)
				parent = new File("/");
			else
				parent = new File("");
		}
		return new DesktopFileHandle(parent, _type);
	}

	public File file()
	{
		if (_type == IFiles.FileType.External) return new File(DesktopFiles.externalPath, _file.getPath());
		if (_type == IFiles.FileType.Local) return new File(DesktopFiles.localPath, _file.getPath());
		return _file;
	}
	}
}
