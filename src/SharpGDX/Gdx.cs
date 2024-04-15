﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX
{
	/** Environment class holding references to the {@link Application}, {@link Graphics}, {@link Audio}, {@link Files} and
 * {@link Input} instances. The references are held in public static fields which allows static access to all sub systems. Do not
 * use Graphics in a thread that is not the rendering thread.
 * <p>
 * This is normally a design faux pas but in this case is better than the alternatives.
 * @author mzechner */
	public class Gdx
	{
		public static Application app;
		public static Graphics graphics;
		public static Audio audio;
		public static Input input;
		public static Files files;
		public static Net net;

		public static GL20 gl;
		public static GL20 gl20;
		public static GL30 gl30;
	 public static GL31 gl31;
		 public static GL32 gl32;
	}
}
