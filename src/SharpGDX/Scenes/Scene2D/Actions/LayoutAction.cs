using SharpGDX;
using SharpGDX.Mathematics;
using SharpGDX.Utils;
using SharpGDX.Shims;
using SharpGDX.Scenes.Scene2D;
using SharpGDX.Scenes.Scene2D.Utils;

namespace SharpGDX.Scenes.Scene2D.Actions;

/** Sets an actor's {@link Layout#setLayoutEnabled(boolean) layout} to enabled or disabled. The actor must implements
 * {@link Layout}.
 * @author Nathan Sweet */
public class LayoutAction : Action {
	private bool enabled;

	public void setTarget (Actor actor) {
		if (actor != null && !(actor is Layout)) throw new GdxRuntimeException("Actor must implement layout: " + actor);
		base.setTarget(actor);
	}

	public override bool act (float delta) {
		((Layout)target).setLayoutEnabled(enabled);
		return true;
	}

	public bool isEnabled () {
		return enabled;
	}

	public void setLayoutEnabled (bool enabled) {
		this.enabled = enabled;
	}
}