﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX.Shims
{
	internal static class Integer
	{
		public static int ValueOf(string s)
		{
			return int.Parse(s);
		}
	}
}
