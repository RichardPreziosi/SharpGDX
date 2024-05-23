using SharpGDX.Utils;

namespace SharpGDX;

public abstract class AbstractInput : Input
{
	protected readonly bool[] justPressedKeys = new bool[Input.Keys.MAX_KEYCODE + 1];
	protected readonly bool[] pressedKeys = new bool[Input.Keys.MAX_KEYCODE + 1];
	protected bool keyJustPressed;
	protected int pressedKeyCount;
	private readonly IntSet keysToCatch = new();

	public abstract float getAccelerometerX();

	public abstract float getAccelerometerY();

	public abstract float getAccelerometerZ();

	public abstract float getAzimuth();

	public abstract long getCurrentEventTime();

	public abstract int getDeltaX();

	public abstract int getDeltaX(int pointer);

	public abstract int getDeltaY();

	public abstract int getDeltaY(int pointer);

	public abstract float getGyroscopeX();

	public abstract float getGyroscopeY();

	public abstract float getGyroscopeZ();

	public abstract IInputProcessor getInputProcessor();

	public abstract int getMaxPointers();

	public abstract Input.Orientation getNativeOrientation();

	public abstract float getPitch();

	public abstract float getPressure();

	public abstract float getPressure(int pointer);

	public abstract float getRoll();

	public abstract int getRotation();

	public abstract void getRotationMatrix(float[] matrix);

	public abstract void getTextInput(Input.TextInputListener listener, string title, string text, string hint);

	public abstract void getTextInput
	(
		Input.TextInputListener listener,
		string title,
		string text,
		string hint,
		Input.OnscreenKeyboardType type
	);

	public abstract int getX();

	public abstract int getX(int pointer);

	public abstract int getY();

	public abstract int getY(int pointer);

	public abstract bool isButtonJustPressed(int button);

	public abstract bool isButtonPressed(int button);

	public bool isCatchBackKey()
	{
		return keysToCatch.contains(Input.Keys.BACK);
	}

	public bool isCatchKey(int keycode)
	{
		return keysToCatch.contains(keycode);
	}

	public bool isCatchMenuKey()
	{
		return keysToCatch.contains(Input.Keys.MENU);
	}

	public abstract bool isCursorCatched();

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

	public bool isKeyPressed(int key)
	{
		if (key == Input.Keys.ANY_KEY)
		{
			return pressedKeyCount > 0;
		}

		if (key is < 0 or > Input.Keys.MAX_KEYCODE)
		{
			return false;
		}

		return pressedKeys[key];
	}

	public abstract bool isPeripheralAvailable(Input.Peripheral peripheral);
	public abstract bool isTouched();
	public abstract bool isTouched(int pointer);
	public abstract bool justTouched();

	public void setCatchBackKey(bool catchBack)
	{
		setCatchKey(Input.Keys.BACK, catchBack);
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

	public void setCatchMenuKey(bool catchMenu)
	{
		setCatchKey(Input.Keys.MENU, catchMenu);
	}

	public abstract void setCursorCatched(bool catched);

	public abstract void setCursorPosition(int x, int y);

	public abstract void setInputProcessor(IInputProcessor processor);

	public abstract void setOnscreenKeyboardVisible(bool visible);

	public abstract void setOnscreenKeyboardVisible(bool visible, Input.OnscreenKeyboardType type);

	public abstract void vibrate(int milliseconds);

	public abstract void vibrate(int milliseconds, bool fallback);

	public abstract void vibrate(int milliseconds, int amplitude, bool fallback);

	public abstract void vibrate(Input.VibrationType vibrationType);
}