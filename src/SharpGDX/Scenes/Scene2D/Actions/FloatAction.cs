using SharpGDX;
using SharpGDX.Mathematics;
using SharpGDX.Utils;
using SharpGDX.Shims;
using SharpGDX.Scenes.Scene2D;
using SharpGDX.Scenes.Scene2D.Utils;

namespace SharpGDX.Scenes.Scene2D.Actions;

/** An action that has a float, whose value is transitioned over time.
 * @author Nathan Sweet */
public class FloatAction : TemporalAction {
	private float start, end;
	private float value;

	/** Creates a FloatAction that transitions from 0 to 1. */
	public FloatAction () {
		start = 0;
		end = 1;
	}

	/** Creates a FloatAction that transitions from start to end. */
	public FloatAction (float start, float end) {
		this.start = start;
		this.end = end;
	}

	/** Creates a FloatAction that transitions from start to end. */
	public FloatAction (float start, float end, float duration) 
	: base(duration)
	{
		
		this.start = start;
		this.end = end;
	}

	/** Creates a FloatAction that transitions from start to end. */
	public FloatAction (float start, float end, float duration,  Interpolation? interpolation) 
	: base(duration, interpolation)
	{
		
		this.start = start;
		this.end = end;
	}

	protected void begin () {
		value = start;
	}

	protected override void update (float percent) {
		if (percent == 0)
			value = start;
		else if (percent == 1)
			value = end;
		else
			value = start + (end - start) * percent;
	}

	/** Gets the current float value. */
	public float getValue () {
		return value;
	}

	/** Sets the current float value. */
	public void setValue (float value) {
		this.value = value;
	}

	public float getStart () {
		return start;
	}

	/** Sets the value to transition from. */
	public void setStart (float start) {
		this.start = start;
	}

	public float getEnd () {
		return end;
	}

	/** Sets the value to transition to. */
	public void setEnd (float end) {
		this.end = end;
	}
}
