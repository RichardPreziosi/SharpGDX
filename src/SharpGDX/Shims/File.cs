using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX.Shims
{
	public class File
	{
		public File(File file){}

		public File(File file, string name) { }

		public File(string file, string name) { }

		public File(string name) { }

		public string getPath()
		{
			throw new NotImplementedException();
		}

		public string getParent()
		{
			throw new NotImplementedException();
		}

		public File getParentFile()
		{
			throw new NotImplementedException();
		}
	}
}
