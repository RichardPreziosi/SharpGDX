using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SharpGDX.Shims;
using File = SharpGDX.Shims.File;

namespace SharpGDX
{
	public class FileHandle
	{
		public InputStream read()
		{
			throw new NotImplementedException();
		}
		public byte[] readBytes()
		{
			throw new NotImplementedException();
		}
		public FileHandle(string filename, Files.FileType type)
			: this(new File(filename), type)
		{

		}

		public FileHandle(File file, Files.FileType type)
		{
			this.type = type;
			_file = file;
		}

		public Files.FileType type;

		public File _file;
	}
}
