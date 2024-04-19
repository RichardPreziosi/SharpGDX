using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX.Desktop
{
	/** @author mzechner
 * @author Nathan Sweet */
	public sealed class Lwjgl3Files : Files
	{
		public static string externalPath { get; } = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

		public static string localPath => "";


		public FileHandle getFileHandle(String fileName, Files.FileType type)
		{
			return new Lwjgl3FileHandle(fileName, type);
		}

		public FileHandle classpath(String path)
		{
			return new Lwjgl3FileHandle(path, Files.FileType.Classpath);
		}

		public FileHandle @internal(String path)
		{
			return new Lwjgl3FileHandle(path, Files.FileType.Internal);
		}

		public FileHandle external(String path)
		{
			return new Lwjgl3FileHandle(path, Files.FileType.External);
		}

		public FileHandle absolute(String path)
		{
			return new Lwjgl3FileHandle(path, Files.FileType.Absolute);
		}

		public FileHandle local(String path)
		{
			return new Lwjgl3FileHandle(path, Files.FileType.Local);
		}

		public String getExternalStoragePath()
		{
			return externalPath;
		}

		public bool isExternalStorageAvailable()
		{
			return true;
		}

		public String getLocalStoragePath()
		{
			return localPath;
		}

		public bool isLocalStorageAvailable()
		{
			return true;
		}
	}
}
