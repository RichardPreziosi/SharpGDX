namespace SharpGDX.utils
{
	/** An element with this annotation claims that the element may have a {@code null} value. Apart from documentation purposes this
 * annotation is intended to be used by static analysis tools to validate against probable runtime errors or contract violations.
 * @author maltaisn */
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Parameter)]
	public class Null: Attribute
	{
	}
}
