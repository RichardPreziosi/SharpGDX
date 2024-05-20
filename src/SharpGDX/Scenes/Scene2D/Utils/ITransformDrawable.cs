using System;
using SharpGDX.Graphics;
using SharpGDX.Graphics.GLUtils;
using SharpGDX.Graphics.G2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX.Scenes.Scene2D.Utils
{
	/** A drawable that supports scale and rotation. */
	public interface TransformDrawable : Drawable
	{
	public void draw(Batch batch, float x, float y, float originX, float originY, float width, float height, float scaleX,
		float scaleY, float rotation);
	}
}
