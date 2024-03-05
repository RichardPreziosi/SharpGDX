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

		public DesktopFileHandle(FileInfo file, Files.FileType type)
			: base(file, type)
		{

		}

		public FileHandle child(String name)
		{
			// TODO: 
			throw new NotImplementedException();
			//if (file.getPath().Length == 0) return new DesktopFileHandle(new FileInfo(name), type);
			//return new DesktopFileHandle(new File(FileInfo, name), type);
		}

		public FileHandle sibling(String name)
		{
			// TODO: 
			throw new NotImplementedException();
			//if (file.getPath().Length == 0) throw new GdxRuntimeException("Cannot get the sibling of the root.");
			//return new DesktopFileHandle(new File(File.getParent(), name), type);
		}

		public FileHandle parent()
		{
			// TODO: 
			throw new NotImplementedException();
			//File parent = File.getParentFile();
			//if (parent == null)
			//{
			//	if (type == Files.FileType.Absolute)
			//		parent = new File("/");
			//	else
			//		parent = new File("");
			//}
			//return new DesktopFileHandle(parent, type);
		}

		public FileInfo file()
		{
			// TODO: 
			throw new NotImplementedException();
			//if (type == Files.FileType.External) return new File(DesktopFiles.externalPath, File.getPath());
			//if (type == Files.FileType.Local) return new File(DesktopFiles.localPath, File.getPath());
			//return File;
		}
	}
}