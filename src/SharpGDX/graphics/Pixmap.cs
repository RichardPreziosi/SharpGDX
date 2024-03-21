using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotImplementedException = System.NotImplementedException;

namespace SharpGDX.graphics
{
	public class Pixmap
	{
		public Pixmap(int width, int height, Format format)
		{
			throw new NotImplementedException();
		}

		public enum Format
		{
			Alpha, Intensity, LuminanceAlpha, RGB565, RGBA4444, RGB888, RGBA8888
		}

		public enum Blending
		{
			None, SourceOver
		}

		public void dispose()
		{
			throw new NotImplementedException();
		}

		public void drawPixmap(Pixmap pixmap, int x, int y)
		{
			throw new NotImplementedException();
		}

		public void setBlending(Blending blending)
		{
			throw new NotImplementedException();
		}

		public Format getFormat()
		{
			throw new NotImplementedException();
		}

		public int getWidth()
		{
			throw new NotImplementedException();
		}

		public int getHeight()
		{
			throw new NotImplementedException();
		}
	}
}
