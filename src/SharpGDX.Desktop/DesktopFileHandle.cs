using File = SharpGDX.shims.File;
using SharpGDX.files;
using SharpGDX.utils;

namespace SharpGDX.Desktop
{
	/** @author mzechner
 * @author Nathan Sweet */
	public sealed class DesktopFileHandle : FileHandle
	{
	public DesktopFileHandle(String fileName, Files.FileType type)
	: base(fileName, type)
		{
		
	}

	public DesktopFileHandle(File file, Files.FileType type)
	: base(file, type)
		{
		
	}

	public FileHandle child(String name)
	{
		if (File.getPath().Length == 0) return new DesktopFileHandle(new File(name), type);
		return new DesktopFileHandle(new File(File, name), type);
	}

	public FileHandle sibling(String name)
	{
		if (File.getPath().Length == 0) throw new GdxRuntimeException("Cannot get the sibling of the root.");
		return new DesktopFileHandle(new File(File.getParent(), name), type);
	}

	public FileHandle parent()
	{
		File parent = File.getParentFile();
		if (parent == null)
		{
			if (type == Files.FileType.Absolute)
				parent = new File("/");
			else
				parent = new File("");
		}
		return new DesktopFileHandle(parent, type);
	}

	public File file()
	{
		if (type == Files.FileType.External) return new File(DesktopFiles.externalPath, File.getPath());
		if (type == Files.FileType.Local) return new File(DesktopFiles.localPath, File.getPath());
		return File;
	}
	}
}
