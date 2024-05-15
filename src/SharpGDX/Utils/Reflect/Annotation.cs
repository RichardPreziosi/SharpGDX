using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX.Utils.Reflect
{
	/** Provides information about, and access to, an annotation of a field, class or interface.
 * @author dludwig */
	public sealed class Annotation
	{

		private Attribute annotation;

		Annotation(Attribute annotation)
		{
			this.annotation = annotation;
		}

		// TODO: @SuppressWarnings("unchecked")
		public T getAnnotation<T>(Type annotationType)
			where T : Attribute
		{
			if (annotation.annotationType().equals(annotationType))
			{
				return (T)annotation;
			}
			return null;
		}

		public Type getAnnotationType<T>()
			where T : Attribute
		{
			return annotation.annotationType();
		}
	}
}
