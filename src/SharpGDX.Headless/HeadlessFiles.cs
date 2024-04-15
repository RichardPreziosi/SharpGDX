using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX.Headless
{
	/** @author mzechner
		* @author Nathan Sweet */
	public sealed class HeadlessFiles : Files
	{
		public static string externalPath { get; } = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

		public static string localPath => "";


		public FileHandle getFileHandle(String fileName, Files.FileType type)
		{
			return new HeadlessFileHandle(fileName, type);
		}

		public FileHandle classpath(String path)
		{
			return new HeadlessFileHandle(path, Files.FileType.Classpath);
		}

		public FileHandle @internal(String path)
		{
			return new HeadlessFileHandle(path, Files.FileType.Internal);
		}

		public FileHandle external(String path)
		{
			return new HeadlessFileHandle(path, Files.FileType.External);
		}

		public FileHandle absolute(String path)
		{
			return new HeadlessFileHandle(path, Files.FileType.Absolute);
		}

		public FileHandle local(String path)
		{
			return new HeadlessFileHandle(path, Files.FileType.Local);
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