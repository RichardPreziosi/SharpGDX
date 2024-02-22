using System.Runtime.CompilerServices;
using File = SharpGDX.shims.File;

namespace SharpGDX.files
{
	public class FileHandle
	{
		protected Files.FileType type;

		protected File File;

		protected FileHandle(File file, Files.FileType type)
		{
			this.File = file;
			this.type = type;
		}

		protected FileHandle(string filename, Files.FileType type)
			:this(new File(filename), type)
		{

		}
	}
}
