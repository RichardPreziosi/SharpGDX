using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGDX.GLFW3
{
	public delegate void GLFWCharCallback(long window, uint codepoint);

	public delegate void GLFWCursorPosCallback(long window, int xpos, int ypos);

	public delegate void GLFWDropCallback(long window, int count, long names);

	public delegate void GLFWErrorCallback(int error, string description);

	public delegate void GLFWFramebufferSizeCallback(long window, int width, int height);

	public delegate void GLFWKeyCallback(long window, int key, int scancode, int action, int mods);

	public delegate void GLFWMouseButtonCallback(long window, int button, int action, int mods);

	public delegate void GLFWScrollCallback(long window, int xoffset, int yoffset);

	public delegate void GLFWWindowCloseCallback(long window);

	public delegate void GLFWWindowFocusCallback(long window, bool focused);

	public delegate void GLFWWindowIconifyCallback(long window, bool iconified);

	public delegate void GLFWWindowMaximizeCallback(long window, bool maximized);

	public delegate void GLFWWindowRefreshCallback(long window);
}
