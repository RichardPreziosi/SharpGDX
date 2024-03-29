﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGDX.files;
using SharpGDX.utils;

namespace SharpGDX.Headless
{
	/** @author mzechner
 * @author Nathan Sweet */
	public sealed class HeadlessFileHandle : FileHandle
	{
	public HeadlessFileHandle(String fileName, Files.FileType type)
	: base(fileName, type)
		{
	}

	public HeadlessFileHandle(FileInfo file, Files.FileType type)
	: base(file, type)
		{
		
	}

	public FileHandle child(String name)
	{
		// TODO: 
		throw new NotImplementedException();
			//if (File.getPath().Length == 0) return new HeadlessFileHandle(new File(name), type);
			//return new HeadlessFileHandle(new File(File, name), type);
		}

	public FileHandle sibling(String name)
	{
		// TODO: 
		throw new NotImplementedException();
			//if (File.getPath().Length == 0) throw new GdxRuntimeException("Cannot get the sibling of the root.");
			//return new HeadlessFileHandle(new File(File.getParent(), name), type);
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
			//return new HeadlessFileHandle(parent, type);
		}

	public FileInfo file()
	{
		// TODO: 
		throw new NotImplementedException();
			//if (type == Files.FileType.External) return new File(HeadlessFiles.externalPath, File.getPath());
			//if (type == Files.FileType.Local) return new File(HeadlessFiles.localPath, File.getPath());
			//return File;
		}
	}
}
