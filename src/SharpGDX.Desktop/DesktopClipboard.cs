using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGDX.Utils;

namespace SharpGDX.Desktop
{
	/** Clipboard implementation for desktop that uses the system clipboard via GLFW.
 * @author mzechner */
	public class Lwjgl3Clipboard : Clipboard
	{
	public bool hasContents()
	{
		// TODO: Should this be string.IsNullOrEmpty or string.IsNullOrWhitespace?
			//String contents = getContents();
			//return contents != null && !contents.isEmpty();
			throw new NotImplementedException();
		}

	public String getContents()
	{
		//return GLFW.glfwGetClipboardString(((Lwjgl3Graphics)Gdx.graphics).getWindow().getWindowHandle());
		throw new NotImplementedException();
	}

	public void setContents(String content)
	{
		//GLFW.glfwSetClipboardString(((Lwjgl3Graphics)Gdx.graphics).getWindow().getWindowHandle(), content);
		throw new NotImplementedException();
	}
	}
}
