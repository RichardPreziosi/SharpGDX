using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGDX.Shims;
using SharpGDX.Utils;
using SharpGDX.Mathematics;

namespace SharpGDX.Graphics
{
	/** A Camera with perspective projection.
 * 
 * @author mzechner */
public class PerspectiveCamera : Camera {
	/** the field of view of the height, in degrees **/
	public float fieldOfView = 67;

	public PerspectiveCamera () {
	}

	/** Constructs a new {@link PerspectiveCamera} with the given field of view and viewport size. The aspect ratio is derived from
	 * the viewport size.
	 * 
	 * @param fieldOfViewY the field of view of the height, in degrees, the field of view for the width will be calculated
	 *           according to the aspect ratio.
	 * @param viewportWidth the viewport width
	 * @param viewportHeight the viewport height */
	public PerspectiveCamera (float fieldOfViewY, float viewportWidth, float viewportHeight) {
		this.fieldOfView = fieldOfViewY;
		this.viewportWidth = viewportWidth;
		this.viewportHeight = viewportHeight;
		update();
	}

	readonly Vector3 tmp = new Vector3();

	public override void update () {
		update(true);
	}

		public override void update (bool updateFrustum) {
		float aspect = viewportWidth / viewportHeight;
		projection.setToProjection(Math.Abs(near), Math.Abs(far), fieldOfView, aspect);
		view.setToLookAt(position, tmp.set(position).add(direction), up);
		combined.set(projection);
		Matrix4.mul(combined.val, view.val);

		if (updateFrustum) {
			invProjectionView.set(combined);
			Matrix4.inv(invProjectionView.val);
			frustum.update(invProjectionView);
		}
	}
}
}
