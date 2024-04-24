namespace SharpGDX.Shims
{
	public class RandomAccessFile : Closeable
	{
		public RandomAccessFile(File f, string mode) { 
		}

		public FileChannel getChannel()
		{
			throw new NotImplementedException();
		}

		public void close()
		{
			throw new NotImplementedException();
		}
	}
}
