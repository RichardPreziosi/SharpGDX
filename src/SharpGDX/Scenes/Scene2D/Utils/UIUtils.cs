using SharpGDX;
using SharpGDX.Shims;
using SharpGDX.Utils;

namespace SharpGDX.Scenes.Scene2D.Utils
{
	public sealed class UIUtils {
	private UIUtils () {
	}

	static public bool isAndroid = SharedLibraryLoader.isAndroid;
	static public bool isMac = SharedLibraryLoader.isMac;
	static public bool isWindows = SharedLibraryLoader.isWindows;
	static public bool isLinux = SharedLibraryLoader.isLinux;
	static public bool isIos = SharedLibraryLoader.isIos;

	static public bool left () {
		return Gdx.input.isButtonPressed(Input.Buttons.LEFT);
	}

	static public bool left (int button) {
		return button == Input.Buttons.LEFT;
	}

	static public bool right () {
		return Gdx.input.isButtonPressed(Input.Buttons.RIGHT);
	}

	static public bool right (int button) {
		return button == Input.Buttons.RIGHT;
	}

	static public bool middle () {
		return Gdx.input.isButtonPressed(Input.Buttons.MIDDLE);
	}

	static public bool middle (int button) {
		return button == Input.Buttons.MIDDLE;
	}

	static public bool shift () {
		return Gdx.input.isKeyPressed(Input.Keys.SHIFT_LEFT) || Gdx.input.isKeyPressed(Input.Keys.SHIFT_RIGHT);
	}

	static public bool shift (int keycode) {
		return keycode == Input.Keys.SHIFT_LEFT || keycode == Input.Keys.SHIFT_RIGHT;
	}

	static public bool ctrl () {
		if (isMac)
			return Gdx.input.isKeyPressed(Input.Keys.SYM);
		else
			return Gdx.input.isKeyPressed(Input.Keys.CONTROL_LEFT) || Gdx.input.isKeyPressed(Input.Keys.CONTROL_RIGHT);
	}

	static public bool ctrl (int keycode) {
		if (isMac)
			return keycode == Input.Keys.SYM;
		else
			return keycode == Input.Keys.CONTROL_LEFT || keycode == Input.Keys.CONTROL_RIGHT;
	}

	static public bool alt () {
		return Gdx.input.isKeyPressed(Input.Keys.ALT_LEFT) || Gdx.input.isKeyPressed(Input.Keys.ALT_RIGHT);
	}

	static public bool alt (int keycode) {
		return keycode == Input.Keys.ALT_LEFT || keycode == Input.Keys.ALT_RIGHT;
	}
}
}
