namespace SharpGDX.graphics;

public class Pixmap
{
	public Pixmap(int width, int height, Format format)
	{
	}

	public byte[] getPixels()
	{
		return [];
	}

	public enum Blending
	{
		None,
		SourceOver
	}

	public enum Format
	{
		Alpha,
		Intensity,
		LuminanceAlpha,
		RGB565,
		RGBA4444,
		RGB888,
		RGBA8888
	}

	public void dispose()
	{
	}

	public void drawPixmap(Pixmap pixmap, int width, int height)
	{
	}

	public Format getFormat()
	{
		return Format.Alpha;
	}

	public int getHeight()
	{
		return 0;
	}

	public int getWidth()
	{
		return 0;
	}

	public void setBlending(Blending blending)
	{
	}
}