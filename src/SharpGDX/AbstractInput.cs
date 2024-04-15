using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGDX.Utils;

namespace SharpGDX
{
	public abstract class AbstractInput : Input
	{
	protected readonly bool[] pressedKeys;
	protected readonly bool[] justPressedKeys;
	private readonly IntSet keysToCatch = new IntSet();
	protected int pressedKeyCount;
	protected bool keyJustPressed;

	public AbstractInput()
	{
		pressedKeys = new bool[Input.Keys.MAX_KEYCODE + 1];
		justPressedKeys = new bool[Input.Keys.MAX_KEYCODE + 1];
	}

	public abstract float getAccelerometerX();
	public abstract float getAccelerometerY();
	public abstract float getAccelerometerZ();
	public abstract float getGyroscopeX();
	public abstract float getGyroscopeY();
	public abstract float getGyroscopeZ();
	public abstract int getMaxPointers();
	public abstract int getX();
	public abstract int getX(int pointer);
	public abstract int getDeltaX();
	public abstract int getDeltaX(int pointer);
	public abstract int getY();
	public abstract int getY(int pointer);
	public abstract int getDeltaY();
	public abstract int getDeltaY(int pointer);
	public abstract bool isTouched();
	public abstract bool justTouched();
	public abstract bool isTouched(int pointer);
	public abstract float getPressure();
	public abstract float getPressure(int pointer);
	public abstract bool isButtonPressed(int button);
	public abstract bool isButtonJustPressed(int button);

	public bool isKeyPressed(int key)
	{
		if (key == Input.Keys.ANY_KEY)
		{
			return pressedKeyCount > 0;
		}
		if (key < 0 || key > Input.Keys.MAX_KEYCODE)
		{
			return false;
		}
		return pressedKeys[key];
	}

	public bool isKeyJustPressed(int key)
	{
		if (key == Input.Keys.ANY_KEY)
		{
			return keyJustPressed;
		}
		if (key < 0 || key > Input.Keys.MAX_KEYCODE)
		{
			return false;
		}
		return justPressedKeys[key];
	}

	public abstract void getTextInput(Input.TextInputListener listener, string title, string text, string hint);
	public abstract void getTextInput(Input.TextInputListener listener, string title, string text, string hint, Input.OnscreenKeyboardType type);
	public abstract void setOnscreenKeyboardVisible(bool visible);
	public abstract void setOnscreenKeyboardVisible(bool visible, Input.OnscreenKeyboardType type);
	public abstract void vibrate(int milliseconds);
	public abstract void vibrate(int milliseconds, bool fallback);
	public abstract void vibrate(int milliseconds, int amplitude, bool fallback);
	public abstract void vibrate(Input.VibrationType vibrationType);
	public abstract float getAzimuth();
	public abstract float getPitch();
	public abstract float getRoll();
	public abstract void getRotationMatrix(float[] matrix);
	public abstract long getCurrentEventTime();

	public bool isCatchBackKey()
	{
		return keysToCatch.contains(Input.Keys.BACK);
	}

	public void setCatchBackKey(bool catchBack)
	{
		setCatchKey(Input.Keys.BACK, catchBack);
	}

	public bool isCatchMenuKey()
	{
		return keysToCatch.contains(Input.Keys.MENU);
	}

	public void setCatchMenuKey(bool catchMenu)
	{
		setCatchKey(Input.Keys.MENU, catchMenu);
	}

	public void setCatchKey(int keycode, bool catchKey)
	{
		if (!catchKey)
		{
			keysToCatch.remove(keycode);
		}
		else
		{
			keysToCatch.add(keycode);
		}
	}

	public bool isCatchKey(int keycode)
	{
		return keysToCatch.contains(keycode);
	}

	public abstract void setInputProcessor(InputProcessor processor);
	public abstract InputProcessor getInputProcessor();
	public abstract bool isPeripheralAvailable(Input.Peripheral peripheral);
	public abstract int getRotation();
	public abstract Input.Orientation getNativeOrientation();
	public abstract void setCursorCatched(bool catched);
	public abstract bool isCursorCatched();
	public abstract void setCursorPosition(int x, int y);
	}
}
