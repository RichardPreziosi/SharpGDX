using SharpGDX.files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX.assets.loaders
{
	/** Interface for classes the can map a file name to a {@link FileHandle}. Used to allow the {@link AssetManager} to load
 * resources from anywhere or implement caching strategies.
 * @author mzechner */
	public interface FileHandleResolver
	{
		public FileHandle resolve(String fileName);
	}
}
