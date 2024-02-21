namespace SharpGDX.math;

/**
 * Takes a linear value in the range of 0-1 and outputs a (usually) non-linear, interpolated value.
 * @author Nathan Sweet
 */
public abstract class Interpolation
{
	public static readonly Bounce bounce = new(4);
	public static readonly BounceIn bounceIn = new(4);
	public static readonly BounceOut bounceOut = new(4);

	public static readonly Interpolation circle = new Circle();

	public static readonly Interpolation circleIn = new CircleIn();

	public static readonly Interpolation circleOut = new CircleOut();

	public static readonly Elastic elastic = new(2, 10, 7, 1);
	public static readonly ElasticIn elasticIn = new(2, 10, 6, 1);
	public static readonly ElasticOut elasticOut = new(2, 10, 7, 1);

	public static readonly Exp exp10 = new(2, 10);
	public static readonly ExpIn exp10In = new(2, 10);
	public static readonly ExpOut exp10Out = new(2, 10);

	public static readonly Exp exp5 = new(2, 5);
	public static readonly ExpIn exp5In = new(2, 5);
	public static readonly ExpOut exp5Out = new(2, 5);

	/**
	 * By Ken Perlin.
	 */
	public static readonly Interpolation smoother = new Smoother();

	public static readonly Interpolation fade = smoother;

	/**
	 * Fast, then slow.
	 */
	public static readonly PowOut pow2Out = new(2);

	public static readonly PowOut fastSlow = pow2Out;

	//

	public static readonly Interpolation linear = new Linear();

	//

	public static readonly Pow pow2 = new(2);

	/**
	 * Slow, then fast.
	 */
	public static readonly PowIn pow2In = new(2);

	public static readonly Interpolation pow2InInverse = new Pow2InInverse();


	public static readonly Interpolation pow2OutInverse = new Pow2OutInverse();

	public static readonly Pow pow3 = new(3);
	public static readonly PowIn pow3In = new(3);

	public static readonly Interpolation pow3InInverse = new Pow3InInverse();

	public static readonly PowOut pow3Out = new(3);

	public static readonly Interpolation pow3OutInverse = new Pow3OutInverse();

	public static readonly Pow pow4 = new(4);
	public static readonly PowIn pow4In = new(4);
	public static readonly PowOut pow4Out = new(4);

	public static readonly Pow pow5 = new(5);
	public static readonly PowIn pow5In = new(5);
	public static readonly PowOut pow5Out = new(5);

	public static readonly Interpolation sine = new Sine();

	public static readonly Interpolation sineIn = new SineIn();

	public static readonly Interpolation sineOut = new SineOut();

	public static readonly PowIn slowFast = pow2In;

	//

	/**
	 * Aka "smoothstep".
	 */
	public static readonly Interpolation smooth = new Smooth();

	public static readonly Interpolation smooth2 = new Smooth2();


	public static readonly Swing swing = new(1.5f);
	public static readonly SwingIn swingIn = new(2f);
	public static readonly SwingOut swingOut = new(2f);

	/**
	 * @param a Alpha value between 0 and 1.
	 */
	public abstract float apply(float a);

	/**
	 * @param a Alpha value between 0 and 1.
	 */
	public float apply(float start, float end, float a)
	{
		return start + (end - start) * apply(a);
	}

	//

	public class Bounce : BounceOut
	{
		public Bounce(float[] widths, float[] heights)
			: base(widths, heights)
		{
		}

		public Bounce(int bounces)
			: base(bounces)
		{
		}

		public override float apply(float a)
		{
			if (a <= 0.5f)
			{
				return (1 - @out(1 - a * 2)) / 2;
			}

			return @out(a * 2 - 1) / 2 + 0.5f;
		}

		private float @out(float a)
		{
			var test = a + widths[0] / 2;
			if (test < widths[0])
			{
				return test / (widths[0] / 2) - 1;
			}

			return base.apply(a);
		}
	}

	public class BounceIn : BounceOut
	{
		public BounceIn(float[] widths, float[] heights)
			: base(widths, heights)
		{
		}

		public BounceIn(int bounces)
			: base(bounces)
		{
		}

		public override float apply(float a)
		{
			return 1 - base.apply(1 - a);
		}
	}

	public class BounceOut : Interpolation
	{
		protected readonly float[] widths, heights;

		public BounceOut(float[] widths, float[] heights)
		{
			if (widths.Length != heights.Length)
			{
				throw new ArgumentException("Must be the same number of widths and heights.");
			}

			this.widths = widths;
			this.heights = heights;
		}

		public BounceOut(int bounces)
		{
			if (bounces < 2 || bounces > 5)
			{
				throw new ArgumentException("bounces cannot be < 2 or > 5: " + bounces);
			}

			widths = new float[bounces];
			heights = new float[bounces];
			heights[0] = 1;
			switch (bounces)
			{
				case 2:
					widths[0] = 0.6f;
					widths[1] = 0.4f;
					heights[1] = 0.33f;
					break;
				case 3:
					widths[0] = 0.4f;
					widths[1] = 0.4f;
					widths[2] = 0.2f;
					heights[1] = 0.33f;
					heights[2] = 0.1f;
					break;
				case 4:
					widths[0] = 0.34f;
					widths[1] = 0.34f;
					widths[2] = 0.2f;
					widths[3] = 0.15f;
					heights[1] = 0.26f;
					heights[2] = 0.11f;
					heights[3] = 0.03f;
					break;
				case 5:
					widths[0] = 0.3f;
					widths[1] = 0.3f;
					widths[2] = 0.2f;
					widths[3] = 0.1f;
					widths[4] = 0.1f;
					heights[1] = 0.45f;
					heights[2] = 0.3f;
					heights[3] = 0.15f;
					heights[4] = 0.06f;
					break;
			}

			widths[0] *= 2;
		}

		public override float apply(float a)
		{
			if (a == 1)
			{
				return 1;
			}

			a += widths[0] / 2;
			float width = 0, height = 0;
			for (int i = 0, n = widths.Length; i < n; i++)
			{
				width = widths[i];
				if (a <= width)
				{
					height = heights[i];
					break;
				}

				a -= width;
			}

			a /= width;
			var z = 4 / width * height * a;
			return 1 - (z - z * a) * width;
		}
	}

	public class Circle : Interpolation
	{
		public override float apply(float a)
		{
			if (a <= 0.5f)
			{
				a *= 2;
				return (1 - (float)Math.Sqrt(1 - a * a)) / 2;
			}

			a--;
			a *= 2;
			return ((float)Math.Sqrt(1 - a * a) + 1) / 2;
		}
	}

	public class CircleIn : Interpolation
	{
		public override float apply(float a)
		{
			return 1 - (float)Math.Sqrt(1 - a * a);
		}
	}

	public class CircleOut : Interpolation
	{
		public override float apply(float a)
		{
			a--;
			return (float)Math.Sqrt(1 - a * a);
		}
	}

	//

	public class Elastic : Interpolation
	{
		protected readonly float value, power, scale, bounces;

		public Elastic(float value, float power, int bounces, float scale)
		{
			this.value = value;
			this.power = power;
			this.scale = scale;
			this.bounces = bounces * MathUtils.PI * (bounces % 2 == 0 ? 1 : -1);
		}

		public override float apply(float a)
		{
			if (a <= 0.5f)
			{
				a *= 2;
				return (float)Math.Pow(value, power * (a - 1)) * MathUtils.sin(a * bounces) * scale / 2;
			}

			a = 1 - a;
			a *= 2;
			return 1 - (float)Math.Pow(value, power * (a - 1)) * MathUtils.sin(a * bounces) * scale / 2;
		}
	}

	public class ElasticIn : Elastic
	{
		public ElasticIn(float value, float power, int bounces, float scale)
			: base(value, power, bounces, scale)
		{
		}

		public override float apply(float a)
		{
			if (a >= 0.99)
			{
				return 1;
			}

			return (float)Math.Pow(value, power * (a - 1)) * MathUtils.sin(a * bounces) * scale;
		}
	}

	public class ElasticOut : Elastic
	{
		public ElasticOut(float value, float power, int bounces, float scale)
			: base(value, power, bounces, scale)
		{
		}

		public override float apply(float a)
		{
			if (a == 0)
			{
				return 0;
			}

			a = 1 - a;
			return 1 - (float)Math.Pow(value, power * (a - 1)) * MathUtils.sin(a * bounces) * scale;
		}
	}

	//

	public class Exp : Interpolation
	{
		protected readonly float value, power, min, scale;

		public Exp(float value, float power)
		{
			this.value = value;
			this.power = power;
			min = (float)Math.Pow(value, -power);
			scale = 1 / (1 - min);
		}

		public override float apply(float a)
		{
			if (a <= 0.5f)
			{
				return ((float)Math.Pow(value, power * (a * 2 - 1)) - min) * scale / 2;
			}

			return (2 - ((float)Math.Pow(value, -power * (a * 2 - 1)) - min) * scale) / 2;
		}
	}

	public class ExpIn : Exp
	{
		public ExpIn(float value, float power)
			: base(value, power)
		{
		}

		public override float apply(float a)
		{
			return ((float)Math.Pow(value, power * (a - 1)) - min) * scale;
		}
	}

	public class ExpOut : Exp
	{
		public ExpOut(float value, float power)
			: base(value, power)
		{
		}

		public override float apply(float a)
		{
			return 1 - ((float)Math.Pow(value, -power * a) - min) * scale;
		}
	}

	//

	public class Pow : Interpolation
	{
		protected readonly int power;

		public Pow(int power)
		{
			this.power = power;
		}

		public override float apply(float a)
		{
			if (a <= 0.5f)
			{
				return (float)Math.Pow(a * 2, power) / 2;
			}

			return (float)Math.Pow((a - 1) * 2, power) / (power % 2 == 0 ? -2 : 2) + 1;
		}
	}

	public class Pow2InInverse : Interpolation
	{
		public override float apply(float a)
		{
			if (a < MathUtils.FLOAT_ROUNDING_ERROR)
			{
				return 0;
			}

			return (float)Math.Sqrt(a);
		}
	}

	public class Pow2OutInverse : Interpolation
	{
		public override float apply(float a)
		{
			if (a < MathUtils.FLOAT_ROUNDING_ERROR)
			{
				return 0;
			}

			if (a > 1)
			{
				return 1;
			}

			return 1 - (float)Math.Sqrt(-(a - 1));
		}
	}

	public class Pow3InInverse : Interpolation
	{
		public override float apply(float a)
		{
			return (float)Math.Cbrt(a);
		}
	}

	public class Pow3OutInverse : Interpolation
	{
		public override float apply(float a)
		{
			return 1 - (float)Math.Cbrt(-(a - 1));
		}
	}

	public class PowIn : Pow
	{
		public PowIn(int power)
			: base(power)
		{
		}

		public override float apply(float a)
		{
			return (float)Math.Pow(a, power);
		}
	}

	public class PowOut : Pow
	{
		public PowOut(int power)
			: base(power)
		{
		}

		public override float apply(float a)
		{
			return (float)Math.Pow(a - 1, power) * (power % 2 == 0 ? -1 : 1) + 1;
		}
	}

	public class Sine : Interpolation
	{
		public override float apply(float a)
		{
			return (1 - MathUtils.cos(a * MathUtils.PI)) / 2;
		}
	}

	public class SineIn : Interpolation
	{
		public override float apply(float a)
		{
			return 1 - MathUtils.cos(a * MathUtils.HALF_PI);
		}
	}

	public class SineOut : Interpolation
	{
		public override float apply(float a)
		{
			return MathUtils.sin(a * MathUtils.HALF_PI);
		}
	}

	public class Smooth : Interpolation
	{
		public override float apply(float a)
		{
			return a * a * (3 - 2 * a);
		}
	}

	public class Smooth2 : Interpolation
	{
		public override float apply(float a)
		{
			a = a * a * (3 - 2 * a);
			return a * a * (3 - 2 * a);
		}
	}

	public class Smoother : Interpolation
	{
		public override float apply(float a)
		{
			return a * a * a * (a * (a * 6 - 15) + 10);
		}
	}

	//

	public class Swing : Interpolation
	{
		private readonly float scale;

		public Swing(float scale)
		{
			this.scale = scale * 2;
		}

		public override float apply(float a)
		{
			if (a <= 0.5f)
			{
				a *= 2;
				return a * a * ((scale + 1) * a - scale) / 2;
			}

			a--;
			a *= 2;
			return a * a * ((scale + 1) * a + scale) / 2 + 1;
		}
	}

	public class SwingIn : Interpolation
	{
		private readonly float scale;

		public SwingIn(float scale)
		{
			this.scale = scale;
		}

		public override float apply(float a)
		{
			return a * a * ((scale + 1) * a - scale);
		}
	}

	public class SwingOut : Interpolation
	{
		private readonly float scale;

		public SwingOut(float scale)
		{
			this.scale = scale;
		}

		public override float apply(float a)
		{
			a--;
			return a * a * ((scale + 1) * a + scale) + 1;
		}
	}

	private class Linear : Interpolation
	{
		public override float apply(float a)
		{
			return a;
		}
	}
}