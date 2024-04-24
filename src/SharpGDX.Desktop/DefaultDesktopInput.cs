using SharpGDX.Desktop;
using SharpGDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SharpGDX.Utils;
using static SharpGDX.Input;

namespace SharpGDX.Desktop
{
	public class DefaultLwjgl3Input : AbstractInput , Lwjgl3Input
	{
		readonly Lwjgl3Window window;
	private InputProcessor inputProcessor;
	readonly InputEventQueue eventQueue = new InputEventQueue();

	int mouseX, mouseY;
	int mousePressed;
	int deltaX, deltaY;
	bool _justTouched;
	readonly bool[] justPressedButtons = new bool[5];
	char lastCharacter;

	private void keyCallbac(long window, int key, int scancode, int action, int mods)
	{
		keyCallback(window, key, scancode, action, mods);
	}


private void charCallback(long window, uint codepoint)
{
	if ((codepoint & 0xff00) == 0xf700) return;
	lastCharacter = (char)codepoint;
	this.window.getGraphics().requestRendering();
	eventQueue.keyTyped((char)codepoint, TimeUtils.nanoTime());
}
	

private void scrollCallback(long window, double scrollX, double scrollY)
{
	this.window.getGraphics().requestRendering();
	eventQueue.scrolled(-(float)scrollX, -(float)scrollY, TimeUtils.nanoTime());
}
private int logicalMouseY;
private int logicalMouseX;

private GLFW.GLFWCursorPosCallback cursorPosCallback;

private void mouseButtonCallback(long window, int button, int action, int mods)
{
	int gdxButton = toGdxButton(button);
	if (button != -1 && gdxButton == -1) return;

	long time = TimeUtils.nanoTime();
	if (action == GLFW.GLFW_PRESS)
	{
		mousePressed++;
		_justTouched = true;
		justPressedButtons[gdxButton] = true;
		this.window.getGraphics().requestRendering();
		eventQueue.touchDown(mouseX, mouseY, 0, gdxButton, time);
	}
	else
	{
		mousePressed = Math.Max(0, mousePressed - 1);
		this.window.getGraphics().requestRendering();
		eventQueue.touchUp(mouseX, mouseY, 0, gdxButton, time);
	}
}

private int toGdxButton(int button)
{
	if (button == 0) return Buttons.LEFT;
	if (button == 1) return Buttons.RIGHT;
	if (button == 2) return Buttons.MIDDLE;
	if (button == 3) return Buttons.BACK;
	if (button == 4) return Buttons.FORWARD;
	return -1;
}
	

public DefaultLwjgl3Input (Lwjgl3Window window)
{
	this.window = window;
	windowHandleChanged(window.getWindowHandle());
}

void keyCallback(long window, int key, int scancode, int action, int mods)
{
	switch (action)
	{
		case GLFW.GLFW_PRESS:
			key = getGdxKeyCode(key);
			eventQueue.keyDown(key, TimeUtils.nanoTime());
			pressedKeyCount++;
			keyJustPressed = true;
			pressedKeys[key] = true;
			justPressedKeys[key] = true;
			this.window.getGraphics().requestRendering();
			lastCharacter = (char)0;
			char character = characterForKeyCode(key);
			if (character != 0) charCallback(window, character);
			break;
		case GLFW.GLFW_RELEASE:
			key = getGdxKeyCode(key);
			pressedKeyCount--;
			pressedKeys[key] = false;
			this.window.getGraphics().requestRendering();
			eventQueue.keyUp(key, TimeUtils.nanoTime());
			break;
		case GLFW.GLFW_REPEAT:
			if (lastCharacter != 0)
			{
				this.window.getGraphics().requestRendering();
				eventQueue.keyTyped(lastCharacter, TimeUtils.nanoTime());
			}
			break;
	}
}

public void resetPollingStates()
{
	_justTouched = false;
	keyJustPressed = false;
	for (int i = 0; i < justPressedKeys.Length; i++)
	{
		justPressedKeys[i] = false;
	}
	for (int i = 0; i < justPressedButtons.Length; i++)
	{
		justPressedButtons[i] = false;
	}
	eventQueue.drain(null);
}

	public void windowHandleChanged(long windowHandle)
{
	resetPollingStates();
	GLFW.glfwSetKeyCallback(window.getWindowHandle(), keyCallback);
	GLFW.glfwSetCharCallback(window.getWindowHandle(), charCallback);
	GLFW.glfwSetScrollCallback(window.getWindowHandle(), scrollCallback);

	GLFW.glfwSetCursorPosCallback
		(
			window.getWindowHandle(),
			cursorPosCallback = (long windowHandle, double x, double y) =>
	{
		deltaX = (int)x - logicalMouseX;
		deltaY = (int)y - logicalMouseY;
		mouseX = logicalMouseX = (int)x;
		mouseY = logicalMouseY = (int)y;

		if (window.getConfig().hdpiMode == HdpiMode.Pixels)
		{
			float xScale = window.getGraphics().getBackBufferWidth() / (float)window.getGraphics().getLogicalWidth();
			float yScale = window.getGraphics().getBackBufferHeight() / (float)window.getGraphics().getLogicalHeight();
			deltaX = (int)(deltaX * xScale);
			deltaY = (int)(deltaY * yScale);
			mouseX = (int)(mouseX * xScale);
			mouseY = (int)(mouseY * yScale);
		}

		this.window.getGraphics().requestRendering();
		long time = TimeUtils.nanoTime();
		if (mousePressed > 0)
		{
			eventQueue.touchDragged(mouseX, mouseY, 0, time);
		}
		else
		{
			eventQueue.mouseMoved(mouseX, mouseY, time);
		}
	}
			);

	GLFW.glfwSetMouseButtonCallback(window.getWindowHandle(), mouseButtonCallback);
}

	public void update()
{
	eventQueue.drain(inputProcessor);
}

	public void prepareNext()
{
	if (_justTouched)
	{
		_justTouched = false;
		for (int i = 0; i < justPressedButtons.Length; i++)
		{
			justPressedButtons[i] = false;
		}
	}

	if (keyJustPressed)
	{
		keyJustPressed = false;
		for (int i = 0; i < justPressedKeys.Length; i++)
		{
			justPressedKeys[i] = false;
		}
	}
	deltaX = 0;
	deltaY = 0;
}

public override int getMaxPointers()
{
	return 1;
}

public override int getX()
{
	return mouseX;
}

	public override int getX(int pointer)
{
	return pointer == 0 ? mouseX : 0;
}

public override int getDeltaX()
{
	return deltaX;
}

public override int getDeltaX(int pointer)
{
	return pointer == 0 ? deltaX : 0;
}

public override int getY()
{
	return mouseY;
}

public override int getY(int pointer)
{
	return pointer == 0 ? mouseY : 0;
}

public override int getDeltaY()
{
	return deltaY;
}

public override int getDeltaY(int pointer)
{
	return pointer == 0 ? deltaY : 0;
}

public override bool isTouched()
{
	return GLFW.glfwGetMouseButton(window.getWindowHandle(), GLFW.GLFW_MOUSE_BUTTON_1) == GLFW.GLFW_PRESS
		|| GLFW.glfwGetMouseButton(window.getWindowHandle(), GLFW.GLFW_MOUSE_BUTTON_2) == GLFW.GLFW_PRESS
		|| GLFW.glfwGetMouseButton(window.getWindowHandle(), GLFW.GLFW_MOUSE_BUTTON_3) == GLFW.GLFW_PRESS
		|| GLFW.glfwGetMouseButton(window.getWindowHandle(), GLFW.GLFW_MOUSE_BUTTON_4) == GLFW.GLFW_PRESS
		|| GLFW.glfwGetMouseButton(window.getWindowHandle(), GLFW.GLFW_MOUSE_BUTTON_5) == GLFW.GLFW_PRESS;
}

public override bool justTouched()
{
	return _justTouched;
}

public override bool isTouched(int pointer)
{
	return pointer == 0 ? isTouched() : false;
}

public override float getPressure()
{
	return getPressure(0);
}

public override float getPressure(int pointer)
{
	return isTouched(pointer) ? 1 : 0;
}

public override bool isButtonPressed(int button)
{
	return GLFW.glfwGetMouseButton(window.getWindowHandle(), button) == GLFW.GLFW_PRESS;
}

public override bool isButtonJustPressed(int button)
{
	if (button < 0 || button >= justPressedButtons.Length)
	{
		return false;
	}
	return justPressedButtons[button];
}

public override void getTextInput(TextInputListener listener, String title, String text, String hint)
{
	getTextInput(listener, title, text, hint, OnscreenKeyboardType.Default);
}

public override void getTextInput(TextInputListener listener, String title, String text, String hint, OnscreenKeyboardType type)
{
	// FIXME getTextInput does nothing
	listener.canceled();
}

public override long getCurrentEventTime()
{
	// queue sets its event time for each event dequeued/processed
	return eventQueue.getCurrentEventTime();
}

public override void setInputProcessor(InputProcessor processor)
{
	this.inputProcessor = processor;
}

public override InputProcessor getInputProcessor()
{
	return inputProcessor;
}

public override void setCursorCatched(bool catched)
{
	GLFW.glfwSetInputMode(window.getWindowHandle(), GLFW.GLFW_CURSOR,
		catched ? GLFW.GLFW_CURSOR_DISABLED : GLFW.GLFW_CURSOR_NORMAL);
}

public override bool isCursorCatched()
{
	return GLFW.glfwGetInputMode(window.getWindowHandle(), GLFW.GLFW_CURSOR) == GLFW.GLFW_CURSOR_DISABLED;
}

public override void setCursorPosition(int x, int y)
{
	if (window.getConfig().hdpiMode == HdpiMode.Pixels)
	{
		float xScale = window.getGraphics().getLogicalWidth() / (float)window.getGraphics().getBackBufferWidth();
		float yScale = window.getGraphics().getLogicalHeight() / (float)window.getGraphics().getBackBufferHeight();
		x = (int)(x * xScale);
		y = (int)(y * yScale);
	}
	GLFW.glfwSetCursorPos(window.getWindowHandle(), x, y);
	cursorPosCallback(window.getWindowHandle(), x, y);
}

protected char characterForKeyCode(int key)
{
	// Map certain key codes to character codes.
	switch (key)
	{
		case Keys.BACKSPACE:
			return (char)8;
		case Keys.TAB:
			return '\t';
		case Keys.FORWARD_DEL:
			return (char)127;
		case Keys.NUMPAD_ENTER:
		case Keys.ENTER:
			return '\n';
	}
	return (char)0;
}

public int getGdxKeyCode(int lwjglKeyCode)
{
	switch (lwjglKeyCode)
	{
		case GLFW.GLFW_KEY_SPACE:
			return Input.Keys.SPACE;
		case GLFW.GLFW_KEY_APOSTROPHE:
			return Input.Keys.APOSTROPHE;
		case GLFW.GLFW_KEY_COMMA:
			return Input.Keys.COMMA;
		case GLFW.GLFW_KEY_MINUS:
			return Input.Keys.MINUS;
		case GLFW.GLFW_KEY_PERIOD:
			return Input.Keys.PERIOD;
		case GLFW.GLFW_KEY_SLASH:
			return Input.Keys.SLASH;
		case GLFW.GLFW_KEY_0:
			return Input.Keys.NUM_0;
		case GLFW.GLFW_KEY_1:
			return Input.Keys.NUM_1;
		case GLFW.GLFW_KEY_2:
			return Input.Keys.NUM_2;
		case GLFW.GLFW_KEY_3:
			return Input.Keys.NUM_3;
		case GLFW.GLFW_KEY_4:
			return Input.Keys.NUM_4;
		case GLFW.GLFW_KEY_5:
			return Input.Keys.NUM_5;
		case GLFW.GLFW_KEY_6:
			return Input.Keys.NUM_6;
		case GLFW.GLFW_KEY_7:
			return Input.Keys.NUM_7;
		case GLFW.GLFW_KEY_8:
			return Input.Keys.NUM_8;
		case GLFW.GLFW_KEY_9:
			return Input.Keys.NUM_9;
		case GLFW.GLFW_KEY_SEMICOLON:
			return Input.Keys.SEMICOLON;
		case GLFW.GLFW_KEY_EQUAL:
			return Input.Keys.EQUALS;
		case GLFW.GLFW_KEY_A:
			return Input.Keys.A;
		case GLFW.GLFW_KEY_B:
			return Input.Keys.B;
		case GLFW.GLFW_KEY_C:
			return Input.Keys.C;
		case GLFW.GLFW_KEY_D:
			return Input.Keys.D;
		case GLFW.GLFW_KEY_E:
			return Input.Keys.E;
		case GLFW.GLFW_KEY_F:
			return Input.Keys.F;
		case GLFW.GLFW_KEY_G:
			return Input.Keys.G;
		case GLFW.GLFW_KEY_H:
			return Input.Keys.H;
		case GLFW.GLFW_KEY_I:
			return Input.Keys.I;
		case GLFW.GLFW_KEY_J:
			return Input.Keys.J;
		case GLFW.GLFW_KEY_K:
			return Input.Keys.K;
		case GLFW.GLFW_KEY_L:
			return Input.Keys.L;
		case GLFW.GLFW_KEY_M:
			return Input.Keys.M;
		case GLFW.GLFW_KEY_N:
			return Input.Keys.N;
		case GLFW.GLFW_KEY_O:
			return Input.Keys.O;
		case GLFW.GLFW_KEY_P:
			return Input.Keys.P;
		case GLFW.GLFW_KEY_Q:
			return Input.Keys.Q;
		case GLFW.GLFW_KEY_R:
			return Input.Keys.R;
		case GLFW.GLFW_KEY_S:
			return Input.Keys.S;
		case GLFW.GLFW_KEY_T:
			return Input.Keys.T;
		case GLFW.GLFW_KEY_U:
			return Input.Keys.U;
		case GLFW.GLFW_KEY_V:
			return Input.Keys.V;
		case GLFW.GLFW_KEY_W:
			return Input.Keys.W;
		case GLFW.GLFW_KEY_X:
			return Input.Keys.X;
		case GLFW.GLFW_KEY_Y:
			return Input.Keys.Y;
		case GLFW.GLFW_KEY_Z:
			return Input.Keys.Z;
		case GLFW.GLFW_KEY_LEFT_BRACKET:
			return Input.Keys.LEFT_BRACKET;
		case GLFW.GLFW_KEY_BACKSLASH:
			return Input.Keys.BACKSLASH;
		case GLFW.GLFW_KEY_RIGHT_BRACKET:
			return Input.Keys.RIGHT_BRACKET;
		case GLFW.GLFW_KEY_GRAVE_ACCENT:
			return Input.Keys.GRAVE;
		case GLFW.GLFW_KEY_WORLD_1:
		case GLFW.GLFW_KEY_WORLD_2:
			return Input.Keys.UNKNOWN;
		case GLFW.GLFW_KEY_ESCAPE:
			return Input.Keys.ESCAPE;
		case GLFW.GLFW_KEY_ENTER:
			return Input.Keys.ENTER;
		case GLFW.GLFW_KEY_TAB:
			return Input.Keys.TAB;
		case GLFW.GLFW_KEY_BACKSPACE:
			return Input.Keys.BACKSPACE;
		case GLFW.GLFW_KEY_INSERT:
			return Input.Keys.INSERT;
		case GLFW.GLFW_KEY_DELETE:
			return Input.Keys.FORWARD_DEL;
		case GLFW.GLFW_KEY_RIGHT:
			return Input.Keys.RIGHT;
		case GLFW.GLFW_KEY_LEFT:
			return Input.Keys.LEFT;
		case GLFW.GLFW_KEY_DOWN:
			return Input.Keys.DOWN;
		case GLFW.GLFW_KEY_UP:
			return Input.Keys.UP;
		case GLFW.GLFW_KEY_PAGE_UP:
			return Input.Keys.PAGE_UP;
		case GLFW.GLFW_KEY_PAGE_DOWN:
			return Input.Keys.PAGE_DOWN;
		case GLFW.GLFW_KEY_HOME:
			return Input.Keys.HOME;
		case GLFW.GLFW_KEY_END:
			return Input.Keys.END;
		case GLFW.GLFW_KEY_CAPS_LOCK:
			return Keys.CAPS_LOCK;
		case GLFW.GLFW_KEY_SCROLL_LOCK:
			return Keys.SCROLL_LOCK;
		case GLFW.GLFW_KEY_PRINT_SCREEN:
			return Keys.PRINT_SCREEN;
		case GLFW.GLFW_KEY_PAUSE:
			return Keys.PAUSE;
		case GLFW.GLFW_KEY_F1:
			return Input.Keys.F1;
		case GLFW.GLFW_KEY_F2:
			return Input.Keys.F2;
		case GLFW.GLFW_KEY_F3:
			return Input.Keys.F3;
		case GLFW.GLFW_KEY_F4:
			return Input.Keys.F4;
		case GLFW.GLFW_KEY_F5:
			return Input.Keys.F5;
		case GLFW.GLFW_KEY_F6:
			return Input.Keys.F6;
		case GLFW.GLFW_KEY_F7:
			return Input.Keys.F7;
		case GLFW.GLFW_KEY_F8:
			return Input.Keys.F8;
		case GLFW.GLFW_KEY_F9:
			return Input.Keys.F9;
		case GLFW.GLFW_KEY_F10:
			return Input.Keys.F10;
		case GLFW.GLFW_KEY_F11:
			return Input.Keys.F11;
		case GLFW.GLFW_KEY_F12:
			return Input.Keys.F12;
		case GLFW.GLFW_KEY_F13:
			return Input.Keys.F13;
		case GLFW.GLFW_KEY_F14:
			return Input.Keys.F14;
		case GLFW.GLFW_KEY_F15:
			return Input.Keys.F15;
		case GLFW.GLFW_KEY_F16:
			return Input.Keys.F16;
		case GLFW.GLFW_KEY_F17:
			return Input.Keys.F17;
		case GLFW.GLFW_KEY_F18:
			return Input.Keys.F18;
		case GLFW.GLFW_KEY_F19:
			return Input.Keys.F19;
		case GLFW.GLFW_KEY_F20:
			return Input.Keys.F20;
		case GLFW.GLFW_KEY_F21:
			return Input.Keys.F21;
		case GLFW.GLFW_KEY_F22:
			return Input.Keys.F22;
		case GLFW.GLFW_KEY_F23:
			return Input.Keys.F23;
		case GLFW.GLFW_KEY_F24:
			return Input.Keys.F24;
		case GLFW.GLFW_KEY_F25:
			return Input.Keys.UNKNOWN;
		case GLFW.GLFW_KEY_NUM_LOCK:
			return Keys.NUM_LOCK;
		case GLFW.GLFW_KEY_KP_0:
			return Input.Keys.NUMPAD_0;
		case GLFW.GLFW_KEY_KP_1:
			return Input.Keys.NUMPAD_1;
		case GLFW.GLFW_KEY_KP_2:
			return Input.Keys.NUMPAD_2;
		case GLFW.GLFW_KEY_KP_3:
			return Input.Keys.NUMPAD_3;
		case GLFW.GLFW_KEY_KP_4:
			return Input.Keys.NUMPAD_4;
		case GLFW.GLFW_KEY_KP_5:
			return Input.Keys.NUMPAD_5;
		case GLFW.GLFW_KEY_KP_6:
			return Input.Keys.NUMPAD_6;
		case GLFW.GLFW_KEY_KP_7:
			return Input.Keys.NUMPAD_7;
		case GLFW.GLFW_KEY_KP_8:
			return Input.Keys.NUMPAD_8;
		case GLFW.GLFW_KEY_KP_9:
			return Input.Keys.NUMPAD_9;
		case GLFW.GLFW_KEY_KP_DECIMAL:
			return Keys.NUMPAD_DOT;
		case GLFW.GLFW_KEY_KP_DIVIDE:
			return Keys.NUMPAD_DIVIDE;
		case GLFW.GLFW_KEY_KP_MULTIPLY:
			return Keys.NUMPAD_MULTIPLY;
		case GLFW.GLFW_KEY_KP_SUBTRACT:
			return Keys.NUMPAD_SUBTRACT;
		case GLFW.GLFW_KEY_KP_ADD:
			return Keys.NUMPAD_ADD;
		case GLFW.GLFW_KEY_KP_ENTER:
			return Keys.NUMPAD_ENTER;
		case GLFW.GLFW_KEY_KP_EQUAL:
			return Keys.NUMPAD_EQUALS;
		case GLFW.GLFW_KEY_LEFT_SHIFT:
			return Input.Keys.SHIFT_LEFT;
		case GLFW.GLFW_KEY_LEFT_CONTROL:
			return Input.Keys.CONTROL_LEFT;
		case GLFW.GLFW_KEY_LEFT_ALT:
			return Input.Keys.ALT_LEFT;
		case GLFW.GLFW_KEY_LEFT_SUPER:
			return Input.Keys.SYM;
		case GLFW.GLFW_KEY_RIGHT_SHIFT:
			return Input.Keys.SHIFT_RIGHT;
		case GLFW.GLFW_KEY_RIGHT_CONTROL:
			return Input.Keys.CONTROL_RIGHT;
		case GLFW.GLFW_KEY_RIGHT_ALT:
			return Input.Keys.ALT_RIGHT;
		case GLFW.GLFW_KEY_RIGHT_SUPER:
			return Input.Keys.SYM;
		case GLFW.GLFW_KEY_MENU:
			return Input.Keys.MENU;
		default:
			return Input.Keys.UNKNOWN;
	}
}

public void dispose()
{
	// TODO: Set to null
	//keyCallback.free();
	//charCallback.free();
	//scrollCallback.free();
	//cursorPosCallback.free();
	//mouseButtonCallback.free();
}

// --------------------------------------------------------------------------
// -------------------------- Nothing to see below this line except for stubs
// --------------------------------------------------------------------------

public override float getAccelerometerX()
{
	return 0;
}
public override float getAccelerometerY()
{
	return 0;
}

public override float getAccelerometerZ()
{
	return 0;
}

public override bool isPeripheralAvailable(Peripheral peripheral)
{
	return peripheral == Peripheral.HardwareKeyboard;
}

public override int getRotation()
{
	return 0;
}

public override Orientation getNativeOrientation()
{
	return Orientation.Landscape;
}
public override void setOnscreenKeyboardVisible(bool visible)
{
}

public override void setOnscreenKeyboardVisible(bool visible, OnscreenKeyboardType type)
{
}

public override void vibrate(int milliseconds)
{
}

public override void vibrate(int milliseconds, bool fallback)
{
}

public override void vibrate(int milliseconds, int amplitude, bool fallback)
{
}

public override void vibrate(VibrationType vibrationType)
{
}

public override float getAzimuth()
{
	return 0;
}

public override float getPitch()
{
	return 0;
}

public override float getRoll()
{
	return 0;
}

public override void getRotationMatrix(float[] matrix)
{
}

public override float getGyroscopeX()
{
	return 0;
}

public override float getGyroscopeY()
{
	return 0;
}

public override float getGyroscopeZ()
{
	return 0;
}
}
}
