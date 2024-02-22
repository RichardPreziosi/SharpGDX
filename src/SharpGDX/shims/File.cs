using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX.shims
{
	public class File
	{
		public File(string filename)
		{

		}

		public File(File file, string filename)
		{

		}

		public File(string path, string filename)
		{

		}

		public File getParent()
		{
			return new File(string.Empty);
		}

		public File getParentFile()
		{
			return new File(string.Empty);
		}

		public string getPath()
		{
			return string.Empty;
		}
	}
}
