using OpenTK.Windowing.GraphicsLibraryFramework;
using SharpGDX.Desktop;
using SharpGDX.graphics.glutils;
using SharpGDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SharpGDX.utils;
using static OpenTK.Windowing.GraphicsLibraryFramework.GLFWCallbacks;
using static SharpGDX.Input;
using GLFWWindow = OpenTK.Windowing.GraphicsLibraryFramework.Window;
using Keys = OpenTK.Windowing.GraphicsLibraryFramework.Keys;

namespace SharpGDX.Desktop
{
	public unsafe class DefaultDesktopInput : AbstractInput , DesktopInput
	{
		readonly DesktopWindow window;
	private InputProcessor inputProcessor;
	readonly InputEventQueue eventQueue = new InputEventQueue();

	int mouseX, mouseY;
	int mousePressed;
	int deltaX, deltaY;
	bool _justTouched;
	readonly bool[] justPressedButtons = new bool[5];
	char lastCharacter;

	private void keyCallback(GLFWWindow* window, Keys key, int scancode, InputAction action, KeyModifiers mods)
	{
		switch (action)
		{
			case InputAction.Press:
				var k1 = getGdxKeyCode(key);
				eventQueue.keyDown(k1, TimeUtils.nanoTime());
				pressedKeyCount++;
				keyJustPressed = true;
				pressedKeys[k1] = true;
				justPressedKeys[k1] = true;
				this.window.getGraphics().requestRendering();
				lastCharacter = (char)0;
				char character = characterForKeyCode(k1);
				if (character != 0) charCallback(window, character);
				break;
			case InputAction.Release:
				var k2 = getGdxKeyCode(key);
				pressedKeyCount--;
				pressedKeys[k2] = false;
				this.window.getGraphics().requestRendering();
				eventQueue.keyUp(k2, TimeUtils.nanoTime());
				break;
			case InputAction.Repeat:
				if (lastCharacter != 0)
				{
					this.window.getGraphics().requestRendering();
					eventQueue.keyTyped(lastCharacter, TimeUtils.nanoTime());
				}
				break;
		}
		}


private void charCallback(GLFWWindow* window, uint codepoint)
{
	if ((codepoint & 0xff00) == 0xf700) return;
	lastCharacter = (char)codepoint;
	this.window.getGraphics().requestRendering();
	eventQueue.keyTyped((char)codepoint, TimeUtils.nanoTime());
}

private void scrollCallback(GLFWWindow* window, double scrollX, double scrollY)
{
	this.window.getGraphics().requestRendering();
	eventQueue.scrolled(-(float)scrollX, -(float)scrollY, TimeUtils.nanoTime());
}

private int logicalMouseY;
private int logicalMouseX;

		private void cursorPosCallback(GLFWWindow* windowHandle, double x, double y)
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

private void mouseButtonCallback (GLFWWindow* window, MouseButton button, InputAction action, KeyModifiers mods)
{
	int gdxButton = toGdxButton(button);
	if (gdxButton == -1) return;

			long time = TimeUtils.nanoTime();
	if (action == InputAction.Press)
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

private int toGdxButton(MouseButton button)
{
	if (button == MouseButton.Button1) return Buttons.LEFT;
	if (button == MouseButton.Button2) return Buttons.RIGHT;
	if (button == MouseButton.Button3) return Buttons.MIDDLE;
	if (button == MouseButton.Button4) return Buttons.BACK;
	if (button == MouseButton.Button5) return Buttons.FORWARD;
	return -1;
}

public DefaultDesktopInput (DesktopWindow window)
{
	this.window = window;
	windowHandleChanged(window.getWindowHandle());
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
public void windowHandleChanged(GLFWWindow* windowHandle)
{
	resetPollingStates();
	GLFW.SetKeyCallback(window.getWindowHandle(), keyCallback);
	GLFW.SetCharCallback(window.getWindowHandle(), charCallback);
	GLFW.SetScrollCallback(window.getWindowHandle(), scrollCallback);
	GLFW.SetCursorPosCallback(window.getWindowHandle(), cursorPosCallback);
	GLFW.SetMouseButtonCallback(window.getWindowHandle(), mouseButtonCallback);
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
	return GLFW.GetMouseButton(window.getWindowHandle(), MouseButton.Button1) == InputAction.Press
		|| GLFW.GetMouseButton(window.getWindowHandle(), MouseButton.Button2) == InputAction.Press
		|| GLFW.GetMouseButton(window.getWindowHandle(), MouseButton.Button3) == InputAction.Press
		|| GLFW.GetMouseButton(window.getWindowHandle(), MouseButton.Button4) == InputAction.Press
		|| GLFW.GetMouseButton(window.getWindowHandle(), MouseButton.Button5) == InputAction.Press;
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
	return GLFW.GetMouseButton(window.getWindowHandle(), (MouseButton)button) == InputAction.Press;
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
	GLFW.SetInputMode(window.getWindowHandle(), CursorStateAttribute.Cursor,
		catched ? CursorModeValue.CursorDisabled : CursorModeValue.CursorNormal);
}
public override bool isCursorCatched()
{
	return GLFW.GetInputMode(window.getWindowHandle(), CursorStateAttribute.Cursor) == CursorModeValue.CursorDisabled;
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
	GLFW.SetCursorPos(window.getWindowHandle(), x, y);
	cursorPosCallback(window.getWindowHandle(), x, y);
}

protected char characterForKeyCode(int key)
{
	// Map certain key codes to character codes.
	switch (key)
	{
		case Input.Keys.BACKSPACE:
			return (char)8;
		case Input.Keys.TAB:
			return '\t';
		case Input.Keys.FORWARD_DEL:
			return (char)127;
		case Input.Keys.NUMPAD_ENTER:
		case Input.Keys.ENTER:
			return '\n';
	}
	return (char)0;
}

public int getGdxKeyCode(Keys lwjglKeyCode)
{
	switch (lwjglKeyCode)
	{
		case Keys.Space:
			return Input.Keys.SPACE;
		case Keys.Apostrophe:
			return Input.Keys.APOSTROPHE;
		case Keys.Comma:
			return Input.Keys.COMMA;
		case Keys.Minus:
			return Input.Keys.MINUS;
		case Keys.Period:
			return Input.Keys.PERIOD;
		case Keys.Slash:
			return Input.Keys.SLASH;
		case Keys.D0:
			return Input.Keys.NUM_0;
		case Keys.D1:
			return Input.Keys.NUM_1;
		case Keys.D2:
			return Input.Keys.NUM_2;
		case Keys.D3:
			return Input.Keys.NUM_3;
		case Keys.D4:
			return Input.Keys.NUM_4;
		case Keys.D5:
			return Input.Keys.NUM_5;
		case Keys.D6:
			return Input.Keys.NUM_6;
		case Keys.D7:
			return Input.Keys.NUM_7;
		case Keys.D8:
			return Input.Keys.NUM_8;
		case Keys.D9:
			return Input.Keys.NUM_9;
		case Keys.Semicolon:
			return Input.Keys.SEMICOLON;
		case Keys.Equal:
			return Input.Keys.EQUALS;
		case Keys.A:
			return Input.Keys.A;
		case Keys.B:
			return Input.Keys.B;
		case Keys.C:
			return Input.Keys.C;
		case Keys.D:
			return Input.Keys.D;
		case Keys.E:
			return Input.Keys.E;
		case Keys.F:
			return Input.Keys.F;
		case Keys.G:
			return Input.Keys.G;
		case Keys.H:
			return Input.Keys.H;
		case Keys.I:
			return Input.Keys.I;
		case Keys.J:
			return Input.Keys.J;
		case Keys.K:
			return Input.Keys.K;
		case Keys.L:
			return Input.Keys.L;
		case Keys.M:
			return Input.Keys.M;
		case Keys.N:
			return Input.Keys.N;
		case Keys.O:
			return Input.Keys.O;
		case Keys.P:
			return Input.Keys.P;
		case Keys.Q:
			return Input.Keys.Q;
		case Keys.R:
			return Input.Keys.R;
		case Keys.S:
			return Input.Keys.S;
		case Keys.T:
			return Input.Keys.T;
		case Keys.U:
			return Input.Keys.U;
		case Keys.V:
			return Input.Keys.V;
		case Keys.W:
			return Input.Keys.W;
		case Keys.X:
			return Input.Keys.X;
		case Keys.Y:
			return Input.Keys.Y;
		case Keys.Z:
			return Input.Keys.Z;
		case Keys.LeftBracket:
			return Input.Keys.LEFT_BRACKET;
		case Keys.Backslash:
			return Input.Keys.BACKSLASH;
		case Keys.RightBracket:
			return Input.Keys.RIGHT_BRACKET;
		case Keys.GraveAccent:
			return Input.Keys.GRAVE;
		// TODO: case Keys.GLFW_KEY_WORLD_1:
		// TODO: case Keys.GLFW_KEY_WORLD_2:
		// TODO: return Input.Keys.UNKNOWN;
		case Keys.Escape:
			return Input.Keys.ESCAPE;
		case Keys.Enter:
			return Input.Keys.ENTER;
		case Keys.Tab:
			return Input.Keys.TAB;
		case Keys.Backspace:
			return Input.Keys.BACKSPACE;
		case Keys.Insert:
			return Input.Keys.INSERT;
		case Keys.Delete:
			return Input.Keys.FORWARD_DEL;
		case Keys.Right:
			return Input.Keys.RIGHT;
		case Keys.Left:
			return Input.Keys.LEFT;
		case Keys.Down:
			return Input.Keys.DOWN;
		case Keys.Up:
			return Input.Keys.UP;
		case Keys.PageUp:
			return Input.Keys.PAGE_UP;
		case Keys.PageDown:
			return Input.Keys.PAGE_DOWN;
		case Keys.Home:
			return Input.Keys.HOME;
		case Keys.End:
			return Input.Keys.END;
		case Keys.CapsLock:
			return Input.Keys.CAPS_LOCK;
		case Keys.ScrollLock:
			return Input.Keys.SCROLL_LOCK;
		case Keys.PrintScreen:
			return Input.Keys.PRINT_SCREEN;
		case Keys.Pause:
			return Input.Keys.PAUSE;
		case Keys.F1:
			return Input.Keys.F1;
		case Keys.F2:
			return Input.Keys.F2;
		case Keys.F3:
			return Input.Keys.F3;
		case Keys.F4:
			return Input.Keys.F4;
		case Keys.F5:
			return Input.Keys.F5;
		case Keys.F6:
			return Input.Keys.F6;
		case Keys.F7:
			return Input.Keys.F7;
		case Keys.F8:
			return Input.Keys.F8;
		case Keys.F9:
			return Input.Keys.F9;
		case Keys.F10:
			return Input.Keys.F10;
		case Keys.F11:
			return Input.Keys.F11;
		case Keys.F12:
			return Input.Keys.F12;
		case Keys.F13:
			return Input.Keys.F13;
		case Keys.F14:
			return Input.Keys.F14;
		case Keys.F15:
			return Input.Keys.F15;
		case Keys.F16:
			return Input.Keys.F16;
		case Keys.F17:
			return Input.Keys.F17;
		case Keys.F18:
			return Input.Keys.F18;
		case Keys.F19:
			return Input.Keys.F19;
		case Keys.F20:
			return Input.Keys.F20;
		case Keys.F21:
			return Input.Keys.F21;
		case Keys.F22:
			return Input.Keys.F22;
		case Keys.F23:
			return Input.Keys.F23;
		case Keys.F24:
			return Input.Keys.F24;
		case Keys.F25:
			return Input.Keys.UNKNOWN;
		case Keys.NumLock:
			return Input.Keys.NUM_LOCK;
		case Keys.KeyPad0:
			return Input.Keys.NUMPAD_0;
		case Keys.KeyPad1:
			return Input.Keys.NUMPAD_1;
		case Keys.KeyPad2:
			return Input.Keys.NUMPAD_2;
		case Keys.KeyPad3:
			return Input.Keys.NUMPAD_3;
		case Keys.KeyPad4:
			return Input.Keys.NUMPAD_4;
		case Keys.KeyPad5:
			return Input.Keys.NUMPAD_5;
		case Keys.KeyPad6:
			return Input.Keys.NUMPAD_6;
		case Keys.KeyPad7:
			return Input.Keys.NUMPAD_7;
		case Keys.KeyPad8:
			return Input.Keys.NUMPAD_8;
		case Keys.KeyPad9:
			return Input.Keys.NUMPAD_9;
		case Keys.KeyPadDecimal:
			return Input.Keys.NUMPAD_DOT;
		case Keys.KeyPadDivide:
			return Input.Keys.NUMPAD_DIVIDE;
		case Keys.KeyPadMultiply:
			return Input.Keys.NUMPAD_MULTIPLY;
		case Keys.KeyPadSubtract:
			return Input.Keys.NUMPAD_SUBTRACT;
		case Keys.KeyPadAdd:
			return Input.Keys.NUMPAD_ADD;
		case Keys.KeyPadEnter:
			return Input.Keys.NUMPAD_ENTER;
		case Keys.KeyPadEqual:
			return Input.Keys.NUMPAD_EQUALS;
		case Keys.LeftShift:
			return Input.Keys.SHIFT_LEFT;
		case Keys.LeftControl:
			return Input.Keys.CONTROL_LEFT;
		case Keys.LeftAlt:
			return Input.Keys.ALT_LEFT;
		case Keys.LeftSuper:
			return Input.Keys.SYM;
		case Keys.RightShift:
			return Input.Keys.SHIFT_RIGHT;
		case Keys.RightControl:
			return Input.Keys.CONTROL_RIGHT;
		case Keys.RightAlt:
			return Input.Keys.ALT_RIGHT;
		case Keys.RightSuper:
			return Input.Keys.SYM;
		case Keys.Menu:
			return Input.Keys.MENU;
		default:
			return Input.Keys.UNKNOWN;
	}
}

public void dispose()
{
	GLFW.SetKeyCallback(window.getWindowHandle(), null);
	GLFW.SetCharCallback(window.getWindowHandle(), null);
	GLFW.SetScrollCallback(window.getWindowHandle(), null);
	GLFW.SetCursorPosCallback(window.getWindowHandle(), null);
	GLFW.SetMouseButtonCallback(window.getWindowHandle(), null);
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
