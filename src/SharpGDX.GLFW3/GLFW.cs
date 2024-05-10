using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace SharpGDX.GLFW3;

public static class GLFW
{
	/**
	 * Function keys.
	 */
	public const int
		GLFW_KEY_ESCAPE = 256,
		GLFW_KEY_ENTER = 257,
		GLFW_KEY_TAB = 258,
		GLFW_KEY_BACKSPACE = 259,
		GLFW_KEY_INSERT = 260,
		GLFW_KEY_DELETE = 261,
		GLFW_KEY_RIGHT = 262,
		GLFW_KEY_LEFT = 263,
		GLFW_KEY_DOWN = 264,
		GLFW_KEY_UP = 265,
		GLFW_KEY_PAGE_UP = 266,
		GLFW_KEY_PAGE_DOWN = 267,
		GLFW_KEY_HOME = 268,
		GLFW_KEY_END = 269,
		GLFW_KEY_CAPS_LOCK = 280,
		GLFW_KEY_SCROLL_LOCK = 281,
		GLFW_KEY_NUM_LOCK = 282,
		GLFW_KEY_PRINT_SCREEN = 283,
		GLFW_KEY_PAUSE = 284,
		GLFW_KEY_F1 = 290,
		GLFW_KEY_F2 = 291,
		GLFW_KEY_F3 = 292,
		GLFW_KEY_F4 = 293,
		GLFW_KEY_F5 = 294,
		GLFW_KEY_F6 = 295,
		GLFW_KEY_F7 = 296,
		GLFW_KEY_F8 = 297,
		GLFW_KEY_F9 = 298,
		GLFW_KEY_F10 = 299,
		GLFW_KEY_F11 = 300,
		GLFW_KEY_F12 = 301,
		GLFW_KEY_F13 = 302,
		GLFW_KEY_F14 = 303,
		GLFW_KEY_F15 = 304,
		GLFW_KEY_F16 = 305,
		GLFW_KEY_F17 = 306,
		GLFW_KEY_F18 = 307,
		GLFW_KEY_F19 = 308,
		GLFW_KEY_F20 = 309,
		GLFW_KEY_F21 = 310,
		GLFW_KEY_F22 = 311,
		GLFW_KEY_F23 = 312,
		GLFW_KEY_F24 = 313,
		GLFW_KEY_F25 = 314,
		GLFW_KEY_KP_0 = 320,
		GLFW_KEY_KP_1 = 321,
		GLFW_KEY_KP_2 = 322,
		GLFW_KEY_KP_3 = 323,
		GLFW_KEY_KP_4 = 324,
		GLFW_KEY_KP_5 = 325,
		GLFW_KEY_KP_6 = 326,
		GLFW_KEY_KP_7 = 327,
		GLFW_KEY_KP_8 = 328,
		GLFW_KEY_KP_9 = 329,
		GLFW_KEY_KP_DECIMAL = 330,
		GLFW_KEY_KP_DIVIDE = 331,
		GLFW_KEY_KP_MULTIPLY = 332,
		GLFW_KEY_KP_SUBTRACT = 333,
		GLFW_KEY_KP_ADD = 334,
		GLFW_KEY_KP_ENTER = 335,
		GLFW_KEY_KP_EQUAL = 336,
		GLFW_KEY_LEFT_SHIFT = 340,
		GLFW_KEY_LEFT_CONTROL = 341,
		GLFW_KEY_LEFT_ALT = 342,
		GLFW_KEY_LEFT_SUPER = 343,
		GLFW_KEY_RIGHT_SHIFT = 344,
		GLFW_KEY_RIGHT_CONTROL = 345,
		GLFW_KEY_RIGHT_ALT = 346,
		GLFW_KEY_RIGHT_SUPER = 347,
		GLFW_KEY_MENU = 348,
		GLFW_KEY_LAST = GLFW_KEY_MENU;

	/**
	 * Printable keys.
	 */
	public const int
		GLFW_KEY_SPACE = 32,
		GLFW_KEY_APOSTROPHE = 39,
		GLFW_KEY_COMMA = 44,
		GLFW_KEY_MINUS = 45,
		GLFW_KEY_PERIOD = 46,
		GLFW_KEY_SLASH = 47,
		GLFW_KEY_0 = 48,
		GLFW_KEY_1 = 49,
		GLFW_KEY_2 = 50,
		GLFW_KEY_3 = 51,
		GLFW_KEY_4 = 52,
		GLFW_KEY_5 = 53,
		GLFW_KEY_6 = 54,
		GLFW_KEY_7 = 55,
		GLFW_KEY_8 = 56,
		GLFW_KEY_9 = 57,
		GLFW_KEY_SEMICOLON = 59,
		GLFW_KEY_EQUAL = 61,
		GLFW_KEY_A = 65,
		GLFW_KEY_B = 66,
		GLFW_KEY_C = 67,
		GLFW_KEY_D = 68,
		GLFW_KEY_E = 69,
		GLFW_KEY_F = 70,
		GLFW_KEY_G = 71,
		GLFW_KEY_H = 72,
		GLFW_KEY_I = 73,
		GLFW_KEY_J = 74,
		GLFW_KEY_K = 75,
		GLFW_KEY_L = 76,
		GLFW_KEY_M = 77,
		GLFW_KEY_N = 78,
		GLFW_KEY_O = 79,
		GLFW_KEY_P = 80,
		GLFW_KEY_Q = 81,
		GLFW_KEY_R = 82,
		GLFW_KEY_S = 83,
		GLFW_KEY_T = 84,
		GLFW_KEY_U = 85,
		GLFW_KEY_V = 86,
		GLFW_KEY_W = 87,
		GLFW_KEY_X = 88,
		GLFW_KEY_Y = 89,
		GLFW_KEY_Z = 90,
		GLFW_KEY_LEFT_BRACKET = 91,
		GLFW_KEY_BACKSLASH = 92,
		GLFW_KEY_RIGHT_BRACKET = 93,
		GLFW_KEY_GRAVE_ACCENT = 96,
		GLFW_KEY_WORLD_1 = 161,
		GLFW_KEY_WORLD_2 = 162;

	/**
	 * The unknown key.
	 */
	public const int GLFW_KEY_UNKNOWN = -1;

	public const int
		GLFW_MOUSE_BUTTON_1 = 0,
		GLFW_MOUSE_BUTTON_2 = 1,
		GLFW_MOUSE_BUTTON_3 = 2,
		GLFW_MOUSE_BUTTON_4 = 3,
		GLFW_MOUSE_BUTTON_5 = 4,
		GLFW_MOUSE_BUTTON_6 = 5,
		GLFW_MOUSE_BUTTON_7 = 6,
		GLFW_MOUSE_BUTTON_8 = 7,
		GLFW_MOUSE_BUTTON_LAST = GLFW_MOUSE_BUTTON_8,
		GLFW_MOUSE_BUTTON_LEFT = GLFW_MOUSE_BUTTON_1,
		GLFW_MOUSE_BUTTON_RIGHT = GLFW_MOUSE_BUTTON_2,
		GLFW_MOUSE_BUTTON_MIDDLE = GLFW_MOUSE_BUTTON_3;

	/**
	 * The key or button was pressed.
	 */
	public const int GLFW_PRESS = 1;

	/**
	 * The key or button was released.
	 */
	public const int GLFW_RELEASE = 0;

	/**
	 * The key was held down until it repeated.
	 */
	public const int GLFW_REPEAT = 2;

	private const string Library = "glfw3";

	public static readonly int GLFW_ANGLE_PLATFORM_TYPE = 0x50002;

	public static readonly int
		GLFW_ANGLE_PLATFORM_TYPE_NONE = 0x37001,
		GLFW_ANGLE_PLATFORM_TYPE_OPENGL = 0x37002,
		GLFW_ANGLE_PLATFORM_TYPE_OPENGLES = 0x37003,
		GLFW_ANGLE_PLATFORM_TYPE_D3D9 = 0x37004,
		GLFW_ANGLE_PLATFORM_TYPE_D3D11 = 0x37005,
		GLFW_ANGLE_PLATFORM_TYPE_VULKAN = 0x37007,
		GLFW_ANGLE_PLATFORM_TYPE_METAL = 0x37008;

	public static readonly int GLFW_ARROW_CURSOR = 0x36001;

	public static readonly int
		GLFW_CLIENT_API = 0x22001,
		GLFW_CONTEXT_VERSION_MAJOR = 0x22002,
		GLFW_CONTEXT_VERSION_MINOR = 0x22003,
		GLFW_CONTEXT_REVISION = 0x22004,
		GLFW_CONTEXT_ROBUSTNESS = 0x22005,
		GLFW_OPENGL_FORWARD_COMPAT = 0x22006,
		GLFW_CONTEXT_DEBUG = 0x22007,
		GLFW_OPENGL_DEBUG_CONTEXT = GLFW_CONTEXT_DEBUG,
		GLFW_OPENGL_PROFILE = 0x22008,
		GLFW_CONTEXT_RELEASE_BEHAVIOR = 0x22009,
		GLFW_CONTEXT_NO_ERROR = 0x2200A,
		GLFW_CONTEXT_CREATION_API = 0x2200B,
		GLFW_SCALE_TO_MONITOR = 0x2200C;

	public static readonly int GLFW_CROSSHAIR_CURSOR = 0x36003;

	public static readonly int
		GLFW_CURSOR = 0x33001,
		GLFW_STICKY_KEYS = 0x33002,
		GLFW_STICKY_MOUSE_BUTTONS = 0x33003,
		GLFW_LOCK_KEY_MODS = 0x33004,
		GLFW_RAW_MOUSE_MOTION = 0x33005,
		GLFW_IME = 0x33006;

	public static readonly int
		GLFW_CURSOR_NORMAL = 0x34001,
		GLFW_CURSOR_HIDDEN = 0x34002,
		GLFW_CURSOR_DISABLED = 0x34003,
		GLFW_CURSOR_CAPTURED = 0x34004;

	public static readonly int GLFW_DONT_CARE = -1;

	public static readonly int
		GLFW_FOCUSED = 0x20001,
		GLFW_ICONIFIED = 0x20002,
		GLFW_RESIZABLE = 0x20003,
		GLFW_VISIBLE = 0x20004,
		GLFW_DECORATED = 0x20005,
		GLFW_AUTO_ICONIFY = 0x20006,
		GLFW_FLOATING = 0x20007,
		GLFW_MAXIMIZED = 0x20008,
		GLFW_CENTER_CURSOR = 0x20009,
		GLFW_TRANSPARENT_FRAMEBUFFER = 0x2000A,
		GLFW_HOVERED = 0x2000B,
		GLFW_FOCUS_ON_SHOW = 0x2000C,
		GLFW_MOUSE_PASSTHROUGH = 0x2000D,
		GLFW_POSITION_X = 0x2000E,
		GLFW_POSITION_Y = 0x2000F,
		GLFW_SOFT_FULLSCREEN = 0x20010;

	public static readonly int GLFW_HAND_CURSOR = GLFW_POINTING_HAND_CURSOR;
	public static readonly int GLFW_HRESIZE_CURSOR = GLFW_RESIZE_EW_CURSOR;
	public static readonly int GLFW_IBEAM_CURSOR = 0x36002;

	public static readonly int GLFW_JOYSTICK_HAT_BUTTONS = 0x50001;

	public static readonly int
		GLFW_NATIVE_CONTEXT_API = 0x36001,
		GLFW_EGL_CONTEXT_API = 0x36002,
		GLFW_OSMESA_CONTEXT_API = 0x36003;

	public static readonly int
		GLFW_NO_API = 0,
		GLFW_OPENGL_API = 0x30001,
		GLFW_OPENGL_ES_API = 0x30002;

	public static readonly int GLFW_NOT_ALLOWED_CURSOR = 0x3600A;

	public static readonly int
		GLFW_OPENGL_ANY_PROFILE = 0,
		GLFW_OPENGL_CORE_PROFILE = 0x32001,
		GLFW_OPENGL_COMPAT_PROFILE = 0x32002;

	public static readonly int GLFW_POINTING_HAND_CURSOR = 0x36004;

	public static readonly int
		GLFW_RED_BITS = 0x21001,
		GLFW_GREEN_BITS = 0x21002,
		GLFW_BLUE_BITS = 0x21003,
		GLFW_ALPHA_BITS = 0x21004,
		GLFW_DEPTH_BITS = 0x21005,
		GLFW_STENCIL_BITS = 0x21006,
		GLFW_ACCUM_RED_BITS = 0x21007,
		GLFW_ACCUM_GREEN_BITS = 0x21008,
		GLFW_ACCUM_BLUE_BITS = 0x21009,
		GLFW_ACCUM_ALPHA_BITS = 0x2100A,
		GLFW_AUX_BUFFERS = 0x2100B,
		GLFW_STEREO = 0x2100C,
		GLFW_SAMPLES = 0x2100D,
		GLFW_SRGB_CAPABLE = 0x2100E,
		GLFW_REFRESH_RATE = 0x2100F,
		GLFW_DOUBLEBUFFER = 0x21010;

	public static readonly int GLFW_RESIZE_ALL_CURSOR = 0x36009;
	public static readonly int GLFW_RESIZE_EW_CURSOR = 0x36005;
	public static readonly int GLFW_RESIZE_NESW_CURSOR = 0x36008;
	public static readonly int GLFW_VRESIZE_CURSOR = GLFW_RESIZE_NS_CURSOR;
	public static readonly int GLFW_RESIZE_NS_CURSOR = 0x36006;
	public static readonly int GLFW_RESIZE_NWSE_CURSOR = 0x36007;

	public static readonly int
		GLFW_TRUE = 1,
		GLFW_FALSE = 0;

	[DllImport(Library)]
	public static extern long glfwCreateCursor(GLFWImage image, int xhot, int yhot);

	[DllImport(Library)]
	public static extern long glfwCreateStandardCursor(int shape);

	public static long glfwCreateWindow(int width, int height, string title, long monitor, long window)
	{
		// TODO: Does this need to be freed?
		var pointer = Marshal.StringToCoTaskMemUTF8(title);

		return glfwCreateWindow(width, height, pointer, monitor, window);

		[DllImport(Library)]
		static extern long glfwCreateWindow(int width, int height, long title, long monitor, long window);
	}

	[DllImport(Library)]
	public static extern void glfwDefaultWindowHints();

	[DllImport(Library)]
	public static extern void glfwDestroyCursor(long cursor);

	[DllImport(Library)]
	public static extern void glfwDestroyWindow(long window);

	public static bool glfwExtensionSupported(string extension)
	{
		// TODO: 
		return false;
	}

	[DllImport(Library)]
	public static extern void glfwFocusWindow(long window);

		[DllImport(Library)]
		public static extern void glfwGetFramebufferSize(long window, out int width, out int height);

	[DllImport(Library)]
	public static extern int glfwGetInputMode(long window, int mode);

	public static string? glfwGetMonitorName(long monitor)
	{
		return Marshal.PtrToStringUTF8(new IntPtr(glfwGetMonitorName(monitor)));

		[DllImport(Library)]
		static extern long glfwGetMonitorName(long monitor);
	}

		[DllImport(Library)]
		public static extern void glfwGetMonitorPhysicalSize(long window, out int widthMM, out int heightMM);

		[DllImport(Library)]
		public static extern void glfwGetMonitorPos(long window, out int xpos, out int ypos);
	
	[DllImport(Library)]
	public static extern void glfwGetMonitorWorkarea(long window, out int xpos, out int ypos, out int width, out int height);

	[DllImport(Library)]
	public static extern int glfwGetMouseButton(long window, int button);

	[DllImport(Library)]
	public static extern long glfwGetPrimaryMonitor();

	[DllImport(Library)]
	public static extern double glfwGetTime();


	public static GLFWVidMode glfwGetVideoMode(long monitor)
	{
		var s = glfwGetVideoMode(monitor);

		return Marshal.PtrToStructure<GLFWVidMode>(s);

		[DllImport(Library)]
		static extern IntPtr glfwGetVideoMode(long monitor);
	}

	public static GLFWVidMode[] glfwGetVideoModes(long monitor, out int count)
	{
		var videoModes = glfwGetVideoModes(monitor, out count);

		Marshal.ReadIntPtr(videoModes, 1 * IntPtr.Size);
		
		throw new NotImplementedException();

		[DllImport(Library)]
		static extern long glfwGetVideoModes(long monitor, out int count);
	}

	[DllImport(Library)]
	public static extern long glfwGetWindowMonitor(long window);
	
	[DllImport(Library)]
	public static extern void glfwGetWindowPos(long window, out int xpos, out int ypos);

		[DllImport(Library)]
		public static extern void glfwGetWindowSize(long window, out int width, out int height);

	[DllImport(Library)]
	public static extern void glfwHideWindow(long window);

	[DllImport(Library)]
	public static extern void glfwIconifyWindow(long window);

	[DllImport(Library)]
	public static extern int glfwInit();

	[DllImport(Library)]
	public static extern void glfwInitHint(int hint, int value);

	[DllImport(Library)]
	public static extern void glfwMakeContextCurrent(long window);

	[DllImport(Library)]
	public static extern void glfwMaximizeWindow(long window);

	[DllImport(Library)]
	public static extern void glfwPollEvents();

	[DllImport(Library)]
	public static extern void glfwRequestWindowAttention(long window);

	[DllImport(Library)]
	public static extern void glfwRestoreWindow(long window);

	[DllImport(Library)]
	public static extern void glfwSetCharCallback(long window, GLFWCharCallback callback);

	[DllImport(Library)]
	public static extern void glfwSetCursor(long window, long cursor);

	[DllImport(Library)]
	public static extern void glfwSetCursorPos(long window, int xpos, int ypos);

	[DllImport(Library)]
	public static extern void glfwSetCursorPosCallback(long window, GLFWCursorPosCallback? callback);

	[DllImport(Library)]
	public static extern void glfwSetDropCallback(long window, GLFWDropCallback? callback);

	[DllImport(Library)]
	public static extern void glfwSetErrorCallback(GLFWErrorCallback? callback);

	[DllImport(Library)]
	public static extern void glfwSetFramebufferSizeCallback(long window, GLFWFramebufferSizeCallback? callback);

	[DllImport(Library)]
	public static extern void glfwSetInputMode(long window, int mode, int value);

	[DllImport(Library)]
	public static extern void glfwSetKeyCallback(long window, GLFWKeyCallback? callback);

	[DllImport(Library)]
	public static extern void glfwSetMouseButtonCallback(long window, GLFWMouseButtonCallback? callback);

	[DllImport(Library)]
	public static extern void glfwSetScrollCallback(long window, GLFWScrollCallback? callback);

	[DllImport(Library)]
	public static extern void glfwSetWindowAttrib(long window, int attrib, int value);

	[DllImport(Library)]
	public static extern IntPtr glfwSetWindowCloseCallback(long window, GLFWWindowCloseCallback? callback);

	[DllImport(Library)]
	public static extern void glfwSetWindowFocusCallback(long window, GLFWWindowFocusCallback? callback);

	public static void glfwSetWindowIcon(long window, GLFWImage buffer)
	{
		// TODO: 
		throw new NotImplementedException();
	}

	[DllImport(Library)]
	public static extern void glfwSetWindowIconifyCallback(long window, GLFWWindowIconifyCallback? callback);

	[DllImport(Library)]
	public static extern void glfwSetWindowMaximizeCallback(long window, GLFWWindowMaximizeCallback? callback);

	[DllImport(Library)]
	public static extern void glfwSetWindowMonitor(long window, long monitor, int xpos, int ypos, int width, int height,
		int refreshRate);

	[DllImport(Library)]
	public static extern void glfwSetWindowPos(long window, int xpos, int ypos);

	[DllImport(Library)]
	public static extern void glfwSetWindowRefreshCallback(long window, GLFWWindowRefreshCallback? callback);

	[DllImport(Library)]
	public static extern void glfwSetWindowShouldClose(long window, bool shouldClose);

	[DllImport(Library)]
	public static extern void glfwSetWindowSize(long window, int width, int height);

	[DllImport(Library)]
	public static extern double glfwSetWindowSizeLimits(long window, int minWidth, int minHeight, int maxWidth,
		int maxHeight);

	public static void glfwSetWindowTitle(long window, string title)
	{
		glfwSetWindowTitle(window, Marshal.StringToCoTaskMemUTF8(title));

		[DllImport(Library)]
		static extern IntPtr glfwSetWindowTitle(long window, long title);
	}

	[DllImport(Library)]
	public static extern void glfwShowWindow(long window);

	[DllImport(Library)]
	public static extern void glfwSwapBuffers(long window);

	[DllImport(Library)]
	public static extern void glfwSwapInterval(int interval);

	[DllImport(Library)]
	public static extern void glfwTerminate();

	[DllImport(Library)]
	public static extern void glfwWindowHint(long window, int hint);

	[DllImport(Library)]
	public static extern int glfwWindowShouldClose(long window);

	

	
}