using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX.Shims
{
	public static class AL11
	{
		public static readonly int
			AL_SEC_OFFSET = 0x1024,
			AL_SAMPLE_OFFSET = 0x1025,
			AL_BYTE_OFFSET = 0x1026,
			AL_STATIC = 0x1028,
			AL_STREAMING = 0x1029,
			AL_UNDETERMINED = 0x1030,
			AL_ILLEGAL_COMMAND = 0xA004,
			AL_SPEED_OF_SOUND = 0xC003,
			AL_LINEAR_DISTANCE = 0xD003,
			AL_LINEAR_DISTANCE_CLAMPED = 0xD004,
			AL_EXPONENT_DISTANCE = 0xD005,
			AL_EXPONENT_DISTANCE_CLAMPED = 0xD006;
	}
}
