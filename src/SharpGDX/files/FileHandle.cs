﻿using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using SharpGDX.files;
using System.Text;
using SharpGDX.shims;
using SharpGDX.utils;
using static SharpGDX.Files;
using SharpGDX;

namespace SharpGDX.files
{
	/** Represents a file or directory on the filesystem, classpath, Android app storage, or Android assets directory. FileHandles are
 * created via a {@link Files} instance.
 * 
 * Because some of the file types are backed by composite files and may be compressed (for example, if they are in an Android .apk
 * or are found via the classpath), the methods for extracting a {@link #path()} or {@link #GetFile()} may not be appropriate for all
 * types. Use the Reader or Stream methods here to hide these dependencies from your platform independent code.
 * 
 * @author mzechner
 * @author Nathan Sweet */
	public class FileHandle
	{
		protected FileSystemInfo file;
		protected FileType type;

		protected FileHandle()
		{
		}

		/** Creates a new absolute FileHandle for the file name. Use this for tools on the desktop that don't need any of the backends.
		 * Do not use this constructor in case you write something cross-platform. Use the {@link Files} interface instead.
		 * @param fileName the filename. */
		public FileHandle(String fileName)
			: this(fileName, FileType.Absolute)
		{
		}

		/** Creates a new absolute FileHandle for the {@link File}. Use this for tools on the desktop that don't need any of the
		 * backends. Do not use this constructor in case you write something cross-platform. Use the {@link Files} interface instead.
		 * @param file the file. */
		public FileHandle(FileInfo file)
			: this(file, FileType.Absolute)
		{
		}

		protected FileHandle(String fileName, FileType type)
		{
			this.type = type;
			file = new FileInfo(fileName);
		}

		protected FileHandle(FileInfo file, FileType type)
		{
			this.file = file;
			this.type = type;
		}

		/** @return the path of the file as specified on construction, e.g. Gdx.files.internal("dir/file.png") -> dir/file.png.
		 *         backward slashes will be replaced by forward slashes. */
		public String path()
		{
			return Path.GetDirectoryName(file.ToString()).Replace('\\', '/');
		}

		/** @return the name of the file, without any parent paths. */
		public String name()
		{
			// TODO: 
			throw new NotImplementedException();
			//return file.getName();
		}

		/** Returns the file extension (without the dot) or an empty string if the file name doesn't contain a dot. */
		public String extension()
		{
			String name = file.Name;
			int dotIndex = name.LastIndexOf('.');
			if (dotIndex == -1) return "";
			return name.Substring(dotIndex + 1);
		}

		/** @return the name of the file, without parent paths or the extension. */
		public String nameWithoutExtension()
		{
			// TODO: 
			throw new NotImplementedException();
			//String name = file.getName();
			//int dotIndex = name.LastIndexOf('.');
			//if (dotIndex == -1) return name;
			//return name.Substring(0, dotIndex);
		}

		/** @return the path and filename without the extension, e.g. dir/dir2/file.png -> dir/dir2/file. backward slashes will be
		 *         returned as forward slashes. */
		public String pathWithoutExtension()
		{
			// TODO: 
			throw new NotImplementedException();
			//String path = file.getPath().replace('\\', '/');
			//int dotIndex = path.LastIndexOf('.');
			//if (dotIndex == -1) return path;
			//return path.Substring(0, dotIndex);
		}

		public FileType GetType()
		{
			return type;
		}

		/** Returns a java.io.File that represents this file handle. Note the returned file will only be usable for
		 * {@link FileType#Absolute} and {@link FileType#External} file handles. */
		public FileSystemInfo GetFile()
		{
			if (type == FileType.External)
			{
				if ((file.Attributes & FileAttributes.Directory) != 0)
				{
					return new DirectoryInfo(Path.Combine(Gdx.files.getExternalStoragePath(), file.ToString()));
				}

				return new FileInfo(Path.Combine(Gdx.files.getExternalStoragePath(), file.ToString()));
			}
			return file;
		}

		/** Returns a stream for reading this file as bytes.
		 * @throws GdxRuntimeException if the file handle represents a directory, doesn't exist, or could not be read. */
		public Stream read()
		{
			if (type == FileType.Classpath || (type == FileType.Internal && !GetFile().Exists)
			                               || (type == FileType.Local && !GetFile().Exists))
			{
				// TODO: No clue what this is doing.
				throw new NotImplementedException();
				//InputStream input = FileHandle.class.getResourceAsStream("/" + file.getPath().replace('\\', '/'));
				//if (input == null) throw new GdxRuntimeException("File not found: " + file + " (" + type + ")");
				//return input;
			}

			try
			{
				if (GetFile() is FileInfo fileInfo)
					return fileInfo.OpenRead();
				
				throw new GdxRuntimeException("Cannot open a stream to a directory: " + file + " (" + type + ")");
			}
			catch (Exception ex)
			{

				throw new GdxRuntimeException("Error reading file: " + file + " (" + type + ")", ex);
			}
		}

		/** Returns a buffered stream for reading this file as bytes.
		 * @throws GdxRuntimeException if the file handle represents a directory, doesn't exist, or could not be read. */
	public Stream read(int bufferSize)
{
	// TODO: 
	throw new NotImplementedException();
	//return new BufferedInputStream(read(), bufferSize);
}

/** Returns a reader for reading this file as characters the platform's default charset.
 * @throws GdxRuntimeException if the file handle represents a directory, doesn't exist, or could not be read. */
public StreamReader reader()
{
	// TODO: 
	throw new NotImplementedException();
	//return new InputStreamReader(read());
}

/** Returns a reader for reading this file as characters.
 * @throws GdxRuntimeException if the file handle represents a directory, doesn't exist, or could not be read. */
public StreamReader reader(String charset)
{
	// TODO: 
	throw new NotImplementedException();
	//InputStream stream = read();
	//try
	//{
	//	return new InputStreamReader(stream, charset);
	//}
	//catch (UnsupportedEncodingException ex)
	//{
	//	StreamUtils.closeQuietly(stream);
	//	throw new GdxRuntimeException("Error reading file: " + this, ex);
	//}
}

/** Returns a buffered reader for reading this file as characters using the platform's default charset.
 * @throws GdxRuntimeException if the file handle represents a directory, doesn't exist, or could not be read. */
public StreamReader reader(int bufferSize)
{
	// TODO: 
	throw new NotImplementedException();
	//return new BufferedReader(new InputStreamReader(read()), bufferSize);
}

/** Returns a buffered reader for reading this file as characters.
 * @throws GdxRuntimeException if the file handle represents a directory, doesn't exist, or could not be read. */
public StreamReader reader(int bufferSize, String charset)
{
	// TODO: 
	throw new NotImplementedException();
	//try
	//{
	//	return new BufferedReader(new InputStreamReader(read(), charset), bufferSize);
	//}
	//catch (UnsupportedEncodingException ex)
	//{
	//	throw new GdxRuntimeException("Error reading file: " + this, ex);
	//}
}

/** Reads the entire file into a string using the platform's default charset.
 * @throws GdxRuntimeException if the file handle represents a directory, doesn't exist, or could not be read. */
public String readString()
{
	return readString(null);
}

/** Reads the entire file into a string using the specified charset.
 * @param charset If null the default charset is used.
 * @throws GdxRuntimeException if the file handle represents a directory, doesn't exist, or could not be read. */
public String readString(String charset)
{
	// TODO: 
	throw new NotImplementedException();
	//StringBuilder output = new StringBuilder(estimateLength());
	//InputStreamReader reader = null;
	//try
	//{
	//	if (charset == null)
	//		reader = new InputStreamReader(read());
	//	else
	//		reader = new InputStreamReader(read(), charset);
	//	char[] buffer = new char[256];
	//	while (true)
	//	{
	//		int length = reader.read(buffer);
	//		if (length == -1) break;
	//		output.Append(buffer, 0, length);
	//	}
	//}
	//catch (IOException ex)
	//{
	//	throw new GdxRuntimeException("Error reading layout file: " + this, ex);
	//}
	//finally
	//{
	//	StreamUtils.closeQuietly(reader);
	//}
	//return output.ToString();
}

/** Reads the entire file into a byte array.
 * @throws GdxRuntimeException if the file handle represents a directory, doesn't exist, or could not be read. */
public byte[] readBytes()
{
	// TODO: 
	throw new NotImplementedException();
	//Stream input = read();
	//try
	//{
	//	return StreamUtils.copyStreamToByteArray(input, estimateLength());
	//}
	//catch (IOException ex)
	//{
	//	throw new GdxRuntimeException("Error reading file: " + this, ex);
	//}
	//finally
	//{
	//	StreamUtils.closeQuietly(input);
	//}
}

private int estimateLength()
{
	// TODO: 
	throw new NotImplementedException();
	//int length = (int)length();
	//return length != 0 ? length : 512;
}

/** Reads the entire file into the byte array. The byte array must be big enough to hold the file's data.
 * @param bytes the array to load the file into
 * @param offset the offset to start writing bytes
 * @param size the number of bytes to read, see {@link #length()}
 * @return the number of read bytes */
public int readBytes(byte[] bytes, int offset, int size)
{
	// TODO: 
	throw new NotImplementedException();
	//InputStream input = read();
	//int position = 0;
	//try
	//{
	//	while (true)
	//	{
	//		int count = input.read(bytes, offset + position, size - position);
	//		if (count <= 0) break;
	//		position += count;
	//	}
	//}
	//catch (IOException ex)
	//{
	//	throw new GdxRuntimeException("Error reading file: " + this, ex);
	//}
	//finally
	//{
	//	StreamUtils.closeQuietly(input);
	//}
	//return position - offset;
}

/** Attempts to memory map this file in READ_ONLY mode. Android files must not be compressed.
 * @throws GdxRuntimeException if this file handle represents a directory, doesn't exist, or could not be read, or memory
 *            mapping fails, or is a {@link FileType#Classpath} file. */
public byte[] map()
{
	return map(FileAccess.Read);
}

/** Attempts to memory map this file. Android files must not be compressed.
 * @throws GdxRuntimeException if this file handle represents a directory, doesn't exist, or could not be read, or memory
 *            mapping fails, or is a {@link FileType#Classpath} file. */
public byte[] map(FileAccess mode)
{
	// TODO: 
	throw new NotImplementedException();
	//if (type == FileType.Classpath) throw new GdxRuntimeException("Cannot map a classpath file: " + this);
	//RandomAccessFile raf = null;
	//try
	//{
	//	File f = GetFile();
	//	raf = new RandomAccessFile(f, mode == FileAccess.Read ? "r" : "rw");
	//	FileChannel fileChannel = raf.getChannel();
	//	ByteBuffer map = fileChannel.map(mode, 0, f.length());
	//	map.order(ByteOrder.nativeOrder());
	//	return map;
	//}
	//catch (Exception ex)
	//{
	//	throw new GdxRuntimeException("Error memory mapping file: " + this + " (" + type + ")", ex);
	//}
	//finally
	//{
	//	StreamUtils.closeQuietly(raf);
	//}
}

/** Returns a stream for writing to this file. Parent directories will be created if necessary.
 * @param append If false, this file will be overwritten if it exists, otherwise it will be appended.
 * @throws GdxRuntimeException if this file handle represents a directory, if it is a {@link FileType#Classpath} or
 *            {@link FileType#Internal} file, or if it could not be written. */
public OutputStream write(bool append)
{
	// TODO: 
	throw new NotImplementedException();
	//if (type == FileType.Classpath) throw new GdxRuntimeException("Cannot write to a classpath file: " + file);
	//if (type == FileType.Internal) throw new GdxRuntimeException("Cannot write to an internal file: " + file);
	//parent().mkdirs();
	//try
	//{
	//	return new FileOutputStream(GetFile(), append);
	//}
	//catch (Exception ex)
	//{
	//	if (GetFile().isDirectory())
	//		throw new GdxRuntimeException("Cannot open a stream to a directory: " + file + " (" + type + ")", ex);
	//	throw new GdxRuntimeException("Error writing file: " + file + " (" + type + ")", ex);
	//}
}

/** Returns a buffered stream for writing to this file. Parent directories will be created if necessary.
 * @param append If false, this file will be overwritten if it exists, otherwise it will be appended.
 * @param bufferSize The size of the buffer.
 * @throws GdxRuntimeException if this file handle represents a directory, if it is a {@link FileType#Classpath} or
 *            {@link FileType#Internal} file, or if it could not be written. */
public OutputStream write(bool append, int bufferSize)
{
	// TODO: 
	throw new NotImplementedException();
	//return new BufferedOutputStream(write(append), bufferSize);
}

/** Reads the remaining bytes from the specified stream and writes them to this file. The stream is closed. Parent directories
 * will be created if necessary.
 * @param append If false, this file will be overwritten if it exists, otherwise it will be appended.
 * @throws GdxRuntimeException if this file handle represents a directory, if it is a {@link FileType#Classpath} or
 *            {@link FileType#Internal} file, or if it could not be written. */
public void write(InputStream input, bool append)
{
	// TODO: 
	throw new NotImplementedException();
	//OutputStream output = null;
	//try
	//{
	//	output = write(append);
	//	StreamUtils.copyStream(input, output);
	//}
	//catch (Exception ex)
	//{
	//	throw new GdxRuntimeException("Error stream writing to file: " + file + " (" + type + ")", ex);
	//}
	//finally
	//{
	//	StreamUtils.closeQuietly(input);
	//	StreamUtils.closeQuietly(output);
	//}
}

/** Returns a writer for writing to this file using the default charset. Parent directories will be created if necessary.
 * @param append If false, this file will be overwritten if it exists, otherwise it will be appended.
 * @throws GdxRuntimeException if this file handle represents a directory, if it is a {@link FileType#Classpath} or
 *            {@link FileType#Internal} file, or if it could not be written. */
public StreamWriter writer(bool append)
{
	return writer(append, null);
}

/** Returns a writer for writing to this file. Parent directories will be created if necessary.
 * @param append If false, this file will be overwritten if it exists, otherwise it will be appended.
 * @param charset May be null to use the default charset.
 * @throws GdxRuntimeException if this file handle represents a directory, if it is a {@link FileType#Classpath} or
 *            {@link FileType#Internal} file, or if it could not be written. */
public StreamWriter writer(bool append, String charset)
{
	// TODO: 
	throw new NotImplementedException();
	//if (type == FileType.Classpath) throw new GdxRuntimeException("Cannot write to a classpath file: " + file);
	//if (type == FileType.Internal) throw new GdxRuntimeException("Cannot write to an internal file: " + file);
	//parent().mkdirs();
	//try
	//{
	//	FileOutputStream output = new FileOutputStream(GetFile(), append);
	//	if (charset == null)
	//		return new OutputStreamWriter(output);
	//	else
	//		return new OutputStreamWriter(output, charset);
	//}
	//catch (IOException ex)
	//{
	//	if (GetFile().isDirectory())
	//		throw new GdxRuntimeException("Cannot open a stream to a directory: " + file + " (" + type + ")", ex);
	//	throw new GdxRuntimeException("Error writing file: " + file + " (" + type + ")", ex);
	//}
}

/** Writes the specified string to the file using the default charset. Parent directories will be created if necessary.
 * @param append If false, this file will be overwritten if it exists, otherwise it will be appended.
 * @throws GdxRuntimeException if this file handle represents a directory, if it is a {@link FileType#Classpath} or
 *            {@link FileType#Internal} file, or if it could not be written. */
public void writeString(String @string, bool append)
{
	writeString(@string, append, null);
}

/** Writes the specified string to the file using the specified charset. Parent directories will be created if necessary.
 * @param append If false, this file will be overwritten if it exists, otherwise it will be appended.
 * @param charset May be null to use the default charset.
 * @throws GdxRuntimeException if this file handle represents a directory, if it is a {@link FileType#Classpath} or
 *            {@link FileType#Internal} file, or if it could not be written. */
public void writeString(String @string, bool append, String charset)
{
	// TODO: 
	throw new NotImplementedException();
	//Writer writer = null;
	//try
	//{
	//	writer = writer(append, charset);
	//	writer.write(@string);
	//}
	//catch (Exception ex)
	//{
	//	throw new GdxRuntimeException("Error writing file: " + file + " (" + type + ")", ex);
	//}
	//finally
	//{
	//	StreamUtils.closeQuietly(writer);
	//}
}

/** Writes the specified bytes to the file. Parent directories will be created if necessary.
 * @param append If false, this file will be overwritten if it exists, otherwise it will be appended.
 * @throws GdxRuntimeException if this file handle represents a directory, if it is a {@link FileType#Classpath} or
 *            {@link FileType#Internal} file, or if it could not be written. */
public void writeBytes(byte[] bytes, bool append)
{
	// TODO: 
	throw new NotImplementedException();
	//OutputStream output = write(append);
	//try
	//{
	//	output.write(bytes);
	//}
	//catch (IOException ex)
	//{
	//	throw new GdxRuntimeException("Error writing file: " + file + " (" + type + ")", ex);
	//}
	//finally
	//{
	//	StreamUtils.closeQuietly(output);
	//}
}

/** Writes the specified bytes to the file. Parent directories will be created if necessary.
 * @param append If false, this file will be overwritten if it exists, otherwise it will be appended.
 * @throws GdxRuntimeException if this file handle represents a directory, if it is a {@link FileType#Classpath} or
 *            {@link FileType#Internal} file, or if it could not be written. */
public void writeBytes(byte[] bytes, int offset, int length, bool append)
{
	// TODO: 
	throw new NotImplementedException();
	//Stream output = write(append);
	//try
	//{
	//	output.write(bytes, offset, length);
	//}
	//catch (IOException ex)
	//{
	//	throw new GdxRuntimeException("Error writing file: " + file + " (" + type + ")", ex);
	//}
	//finally
	//{
	//	StreamUtils.closeQuietly(output);
	//}
}

/** Returns the paths to the children of this directory. Returns an empty list if this file handle represents a file and not a
 * directory. On the desktop, an {@link FileType#Internal} handle to a directory on the classpath will return a zero length
 * array.
 * @throws GdxRuntimeException if this file is an {@link FileType#Classpath} file. */
public FileHandle[] list()
{
	// TODO: 
	throw new NotImplementedException();
	//if (type == FileType.Classpath) throw new GdxRuntimeException("Cannot list a classpath directory: " + file);
	//String[] relativePaths = GetFile().list();
	//if (relativePaths == null) return new FileHandle[0];
	//FileHandle[] handles = new FileHandle[relativePaths.Length];
	//for (int i = 0, n = relativePaths.Length; i < n; i++)
	//	handles[i] = child(relativePaths[i]);
	//return handles;
}

/** Returns the paths to the children of this directory that satisfy the specified filter. Returns an empty list if this file
 * handle represents a file and not a directory. On the desktop, an {@link FileType#Internal} handle to a directory on the
 * classpath will return a zero length array.
 * @param filter the {@link FileFilter} to filter files
 * @throws GdxRuntimeException if this file is an {@link FileType#Classpath} file. */
// TODO: 
//public FileHandle[] list(FileFilter filter)
//{
//	if (type == FileType.Classpath) throw new GdxRuntimeException("Cannot list a classpath directory: " + file);
//	File file = GetFile();
//	String[] relativePaths = file.list();
//	if (relativePaths == null) return new FileHandle[0];
//	FileHandle[] handles = new FileHandle[relativePaths.Length];
//	int count = 0;
//	for (int i = 0, n = relativePaths.Length; i < n; i++)
//	{
//		String path = relativePaths[i];
//		FileHandle child = child(path);
//		if (!filter.accept(child.GetFile())) continue;
//		handles[count] = child;
//		count++;
//	}
//	if (count < relativePaths.Length)
//	{
//		FileHandle[] newHandles = new FileHandle[count];
//		System.arraycopy(handles, 0, newHandles, 0, count);
//		handles = newHandles;
//	}
//	return handles;
//}

/** Returns the paths to the children of this directory that satisfy the specified filter. Returns an empty list if this file
 * handle represents a file and not a directory. On the desktop, an {@link FileType#Internal} handle to a directory on the
 * classpath will return a zero length array.
 * @param filter the {@link FilenameFilter} to filter files
 * @throws GdxRuntimeException if this file is an {@link FileType#Classpath} file. */
// TODO: 
//public FileHandle[] list(FilenameFilter filter)
//{
//	if (type == FileType.Classpath) throw new GdxRuntimeException("Cannot list a classpath directory: " + file);
//	File file = GetFile();
//	String[] relativePaths = file.list();
//	if (relativePaths == null) return new FileHandle[0];
//	FileHandle[] handles = new FileHandle[relativePaths.Length];
//	int count = 0;
//	for (int i = 0, n = relativePaths.Length; i < n; i++)
//	{
//		String path = relativePaths[i];
//		if (!filter.accept(file, path)) continue;
//		handles[count] = child(path);
//		count++;
//	}
//	if (count < relativePaths.Length)
//	{
//		FileHandle[] newHandles = new FileHandle[count];
//		System.arraycopy(handles, 0, newHandles, 0, count);
//		handles = newHandles;
//	}
//	return handles;
//}

/** Returns the paths to the children of this directory with the specified suffix. Returns an empty list if this file handle
 * represents a file and not a directory. On the desktop, an {@link FileType#Internal} handle to a directory on the classpath
 * will return a zero length array.
 * @throws GdxRuntimeException if this file is an {@link FileType#Classpath} file. */
public FileHandle[] list(String suffix)
{
	// TODO: 
	throw new NotImplementedException();
	//if (type == FileType.Classpath) throw new GdxRuntimeException("Cannot list a classpath directory: " + file);
	//String[] relativePaths = GetFile().list();
	//if (relativePaths == null) return new FileHandle[0];
	//FileHandle[] handles = new FileHandle[relativePaths.Length];
	//int count = 0;
	//for (int i = 0, n = relativePaths.Length; i < n; i++)
	//{
	//	String path = relativePaths[i];
	//	if (!path.endsWith(suffix)) continue;
	//	handles[count] = child(path);
	//	count++;
	//}
	//if (count < relativePaths.Length)
	//{
	//	FileHandle[] newHandles = new FileHandle[count];
	//	System.arraycopy(handles, 0, newHandles, 0, count);
	//	handles = newHandles;
	//}
	//return handles;
}

/** Returns true if this file is a directory. Always returns false for classpath files. On Android, an
 * {@link FileType#Internal} handle to an empty directory will return false. On the desktop, an {@link FileType#Internal}
 * handle to a directory on the classpath will return false. */
public bool isDirectory()
{
	// TODO: 
	throw new NotImplementedException();
	//if (type == FileType.Classpath) return false;
	//return GetFile().isDirectory();
}

/** Returns a handle to the child with the specified name. */
public FileHandle child(String name)
{
	// TODO: 
	throw new NotImplementedException();
	//if (file.getPath().length() == 0) return new FileHandle(new File(name), type);
	//return new FileHandle(new File(file, name), type);
}

/** Returns a handle to the sibling with the specified name.
 * @throws GdxRuntimeException if this file is the root. */
public FileHandle sibling(String name)
{
	// TODO: 
	throw new NotImplementedException();
	//if (file.getPath().length() == 0) throw new GdxRuntimeException("Cannot get the sibling of the root.");
	//return new FileHandle(new File(file.getParent(), name), type);
}

public FileHandle parent()
{
	// TODO: 
	throw new NotImplementedException();
	//File parent = file.getParentFile();
	//if (parent == null)
	//{
	//	if (type == FileType.Absolute)
	//		parent = new File("/");
	//	else
	//		parent = new File("");
	//}
	//return new FileHandle(parent, type);
}

/** @throws GdxRuntimeException if this file handle is a {@link FileType#Classpath} or {@link FileType#Internal} file. */
public void mkdirs()
{
	// TODO: 
	throw new NotImplementedException();
	//if (type == FileType.Classpath) throw new GdxRuntimeException("Cannot mkdirs with a classpath file: " + file);
	//if (type == FileType.Internal) throw new GdxRuntimeException("Cannot mkdirs with an internal file: " + file);
	//GetFile().mkdirs();
}

/** Returns true if the file exists. On Android, a {@link FileType#Classpath} or {@link FileType#Internal} handle to a
 * directory will always return false. Note that this can be very slow for internal files on Android! */
public bool exists()
{
	// TODO: 
	throw new NotImplementedException();
	//switch (type)
	//{
	//	case Files.FileType.Internal:
	//		if (GetFile().exists()) return true;
	//	// Fall through.
	//	case Files.FileType.Classpath:
	//		return FileHandle.class.getResource("/" + file.getPath().replace('\\', '/')) != null;
	//}

	//return GetFile().exists();
}

/** Deletes this file or empty directory and returns success. Will not delete a directory that has children.
 * @throws GdxRuntimeException if this file handle is a {@link FileType#Classpath} or {@link FileType#Internal} file. */
	public bool delete()
{
	// TODO: 
	throw new NotImplementedException();
	//if (type == FileType.Classpath) throw new GdxRuntimeException("Cannot delete a classpath file: " + file);
	//if (type == FileType.Internal) throw new GdxRuntimeException("Cannot delete an internal file: " + file);
	//return GetFile().delete();
}

/** Deletes this file or directory and all children, recursively.
 * @throws GdxRuntimeException if this file handle is a {@link FileType#Classpath} or {@link FileType#Internal} file. */
public bool deleteDirectory()
{
	if (type == FileType.Classpath) throw new GdxRuntimeException("Cannot delete a classpath file: " + file);
	if (type == FileType.Internal) throw new GdxRuntimeException("Cannot delete an internal file: " + file);
	return deleteDirectory((DirectoryInfo)GetFile());
}

/** Deletes all children of this directory, recursively.
 * @throws GdxRuntimeException if this file handle is a {@link FileType#Classpath} or {@link FileType#Internal} file. */
public void emptyDirectory()
{
	emptyDirectory(false);
}

/** Deletes all children of this directory, recursively. Optionally preserving the folder structure.
 * @throws GdxRuntimeException if this file handle is a {@link FileType#Classpath} or {@link FileType#Internal} file. */
public void emptyDirectory(bool preserveTree)
{
	if (type == FileType.Classpath) throw new GdxRuntimeException("Cannot delete a classpath file: " + file);
	if (type == FileType.Internal) throw new GdxRuntimeException("Cannot delete an internal file: " + file);
	emptyDirectory((DirectoryInfo)GetFile(), preserveTree);
}

/** Copies this file or directory to the specified file or directory. If this handle is a file, then 1) if the destination is a
 * file, it is overwritten, or 2) if the destination is a directory, this file is copied into it, or 3) if the destination
 * doesn't exist, {@link #mkdirs()} is called on the destination's parent and this file is copied into it with a new name. If
 * this handle is a directory, then 1) if the destination is a file, GdxRuntimeException is thrown, or 2) if the destination is
 * a directory, this directory is copied into it recursively, overwriting existing files, or 3) if the destination doesn't
 * exist, {@link #mkdirs()} is called on the destination and this directory is copied into it recursively.
 * @throws GdxRuntimeException if the destination file handle is a {@link FileType#Classpath} or {@link FileType#Internal}
 *            file, or copying failed. */
public void copyTo(FileHandle dest)
{
	if (!isDirectory())
	{
		if (dest.isDirectory()) dest = dest.child(name());
		copyFile(this, dest);
		return;
	}
	if (dest.exists())
	{
		if (!dest.isDirectory()) throw new GdxRuntimeException("Destination exists but is not a directory: " + dest);
	}
	else
	{
		dest.mkdirs();
		if (!dest.isDirectory()) throw new GdxRuntimeException("Destination directory cannot be created: " + dest);
	}
	copyDirectory(this, dest.child(name()));
}

/** Moves this file to the specified file, overwriting the file if it already exists.
 * @throws GdxRuntimeException if the source or destination file handle is a {@link FileType#Classpath} or
 *            {@link FileType#Internal} file. */
public void moveTo(FileHandle dest)
{
	// TODO: 
	throw new NotImplementedException();
	//switch (type)
	//{
	//	case Files.FileType.Classpath:
	//		throw new GdxRuntimeException("Cannot move a classpath file: " + file);
	//	case Files.FileType.Internal:
	//		throw new GdxRuntimeException("Cannot move an internal file: " + file);
	//	case Files.FileType.Absolute:
	//	case Files.FileType.External:
	//		// Try rename for efficiency and to change case on case-insensitive file systems.
	//		if (GetFile().renameTo(dest.GetFile())) return;
	//}
	//copyTo(dest);
	//delete();
	//if (exists() && isDirectory()) deleteDirectory();
}

/** Returns the length in bytes of this file, or 0 if this file is a directory, does not exist, or the size cannot otherwise be
 * determined. */
public long length()
{
	// TODO: 
	throw new NotImplementedException();
	//if (type == FileType.Classpath || (type == FileType.Internal && !file.exists()))
	//{
	//	InputStream input = read();
	//	try
	//	{
	//		return input.available();
	//	}
	//	catch (Exception ignored)
	//	{
	//	}
	//	finally
	//	{
	//		StreamUtils.closeQuietly(input);
	//	}
	//	return 0;
	//}
	//return GetFile().length();
}

/** Returns the last modified time in milliseconds for this file. Zero is returned if the file doesn't exist. Zero is returned
 * for {@link FileType#Classpath} files. On Android, zero is returned for {@link FileType#Internal} files. On the desktop, zero
 * is returned for {@link FileType#Internal} files on the classpath. */
public long lastModified()
{
	// TODO: 
	throw new NotImplementedException();
	//return GetFile().lastModified();
}

public override bool Equals(Object? obj)
{
	if (!(obj is FileHandle)) return false;
	FileHandle other = (FileHandle)obj;
	return type == other.type && path().Equals(other.path());
}

public override int GetHashCode()
{
	int hash = 1;
	hash = hash * 37 + type.GetHashCode();
	hash = hash * 67 + path().GetHashCode();
	return hash;
}

public override String ToString()
{
	return file.ToString().Replace('\\', '/');
}

static public FileHandle tempFile(String prefix)
{
	// TODO: 
	throw new NotImplementedException();
	//try
	//{
	//	return new FileHandle(File.createTempFile(prefix, null));
	//}
	//catch (IOException ex)
	//{
	//	throw new GdxRuntimeException("Unable to create temp file.", ex);
	//}
}

static public FileHandle tempDirectory(String prefix)
{
	// TODO: 
	throw new NotImplementedException();
	//try
	//{
	//	File file = File.createTempFile(prefix, null);
	//	if (!file.delete()) throw new IOException("Unable to delete temp file: " + file);
	//	if (!file.mkdir()) throw new IOException("Unable to create temp directory: " + file);
	//	return new FileHandle(file);
	//}
	//catch (IOException ex)
	//{
	//	throw new GdxRuntimeException("Unable to create temp file.", ex);
	//}
}

static private void emptyDirectory(DirectoryInfo file, bool preserveTree)
{
	// TODO: 
	throw new NotImplementedException();
	//if (file.exists())
	//{
	//	File[] files = file.listFiles();
	//	if (files != null)
	//	{
	//		for (int i = 0, n = files.Length; i < n; i++)
	//		{
	//			if (!files[i].isDirectory())
	//				files[i].delete();
	//			else if (preserveTree)
	//				emptyDirectory(files[i], true);
	//			else
	//				deleteDirectory(files[i]);
	//		}
	//	}
	//}
}

static private bool deleteDirectory(DirectoryInfo file)
{
	// TODO: 
	throw new NotImplementedException();
	//emptyDirectory(file, false);
	//return file.delete();
}

static private void copyFile(FileHandle source, FileHandle dest)
{
	// TODO: 
	throw new NotImplementedException();
	//try
	//{
	//	dest.write(source.read(), false);
	//}
	//catch (Exception ex)
	//{
	//	throw new GdxRuntimeException("Error copying source file: " + source.file + " (" + source.type + ")\n" //
	//		+ "To destination: " + dest.file + " (" + dest.type + ")", ex);
	//}
}

static private void copyDirectory(FileHandle sourceDir, FileHandle destDir)
{
	destDir.mkdirs();
	FileHandle[] files = sourceDir.list();
	for (int i = 0, n = files.Length; i < n; i++)
	{
		FileHandle srcFile = files[i];
		FileHandle destFile = destDir.child(srcFile.name());
		if (srcFile.isDirectory())
			copyDirectory(srcFile, destFile);
		else
			copyFile(srcFile, destFile);
	}
}
}
}
