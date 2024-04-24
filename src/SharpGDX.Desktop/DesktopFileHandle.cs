using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = SharpGDX.Shims.File;
using SharpGDX.Utils;

namespace SharpGDX.Desktop
{
	/** @author mzechner
 * @author Nathan Sweet */
	public sealed class Lwjgl3FileHandle : FileHandle
	{
	public Lwjgl3FileHandle(String fileName, Files.FileType type)
	: base(fileName, type)
		{
	}

	public Lwjgl3FileHandle(File file, Files.FileType type)
	:base(file, type)
		{
		
	}

	public FileHandle child(String name)
	{
		if (_file.getPath().Length == 0) return new Lwjgl3FileHandle(new File(name), _type);
		return new Lwjgl3FileHandle(new File(_file, name), _type);
	}

	public FileHandle sibling(String name)
	{
		if (_file.getPath().Length == 0) throw new GdxRuntimeException("Cannot get the sibling of the root.");
		return new Lwjgl3FileHandle(new File(_file.getParent(), name), _type);
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
		return new Lwjgl3FileHandle(parent, _type);
	}

	public File file()
	{
		if (_type == Files.FileType.External) return new File(Lwjgl3Files.externalPath, _file.getPath());
		if (_type == Files.FileType.Local) return new File(Lwjgl3Files.localPath, _file.getPath());
		return _file;
	}
	}
}
