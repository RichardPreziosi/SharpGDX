using SharpGDX.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX.Headless
{
	/** @author mzechner
		* @author Nathan Sweet */
	public sealed class HeadlessFiles : IFiles
	{
		public static string externalPath { get; } = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

		public static string localPath => "";


		public FileHandle getFileHandle(String fileName, IFiles.FileType type)
		{
			return new HeadlessFileHandle(fileName, type);
		}

		public FileHandle classpath(String path)
		{
			return new HeadlessFileHandle(path, IFiles.FileType.Classpath);
		}

		public FileHandle @internal(String path)
		{
			return new HeadlessFileHandle(path, IFiles.FileType.Internal);
		}

		public FileHandle external(String path)
		{
			return new HeadlessFileHandle(path, IFiles.FileType.External);
		}

		public FileHandle absolute(String path)
		{
			return new HeadlessFileHandle(path, IFiles.FileType.Absolute);
		}

		public FileHandle local(String path)
		{
			return new HeadlessFileHandle(path, IFiles.FileType.Local);
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