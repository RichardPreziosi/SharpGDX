namespace SharpGDX.math;

public interface Shape2D
{
	/**
	 * Returns whether the given point is contained within the shape.
	 */
	bool contains(Vector2 point);

	/**
	 * Returns whether a point with the given coordinates is contained within the shape.
	 */
	bool contains(float x, float y);
}