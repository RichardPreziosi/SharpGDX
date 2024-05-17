using SharpGDX;
using SharpGDX.Mathematics;
using SharpGDX.Utils;
using SharpGDX.Shims;
using SharpGDX.Scenes.Scene2D;
using SharpGDX.Scenes.Scene2D.Utils;

namespace SharpGDX.Scenes.Scene2D.Actions;

/** Removes a listener from an actor.
 * @author Nathan Sweet */
public class RemoveListenerAction : Action {
	private EventListener listener;
	private bool capture;

	public override bool act (float delta) {
		if (capture)
			target.removeCaptureListener(listener);
		else
			target.removeListener(listener);
		return true;
	}

	public EventListener getListener () {
		return listener;
	}

	public void setListener (EventListener listener) {
		this.listener = listener;
	}

	public bool getCapture () {
		return capture;
	}

	public void setCapture (bool capture) {
		this.capture = capture;
	}

	public void reset () {
		base.reset();
		listener = null;
	}
}
