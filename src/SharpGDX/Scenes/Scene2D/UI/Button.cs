﻿using SharpGDX.Shims;
using SharpGDX.Graphics;
using SharpGDX.Graphics.GLUtils;
using SharpGDX.Graphics.G2D;
using SharpGDX.Scenes.Scene2D.Utils;
using SharpGDX.Mathematics;
using SharpGDX.Utils;
using SharpGDX.Scenes.Scene2D.UI;
using System.Security.Cryptography.X509Certificates;

namespace SharpGDX.Scenes.Scene2D.UI
{
	/** A button is a {@link Table} with a checked state and additional {@link ButtonStyle style} fields for pressed, unpressed, and
 * checked. Each time a button is clicked, the checked state is toggled. Being a table, a button can contain any other actors.<br>
 * <br>
 * The button's padding is set to the background drawable's padding when the background changes, overwriting any padding set
 * manually. Padding can still be set on the button's table cells.
 * <p>
 * {@link ChangeEvent} is fired when the button is clicked. Cancelling the event will restore the checked button state to what is
 * was previously.
 * <p>
 * The preferred size of the button is determined by the background and the button contents.
 * @author Nathan Sweet */
public class Button : Table , IDisableable {
	private ButtonStyle style;
	internal bool _isChecked;
		bool _isDisabled;
	internal ButtonGroup<Button> buttonGroup;
	private ClickListener clickListener;
	private bool programmaticChangeEvents = true;

	public Button (Skin skin) 
	: base(skin)
	{
		
		initialize();
		setStyle(skin.get<ButtonStyle>(typeof(ButtonStyle)));
		setSize(getPrefWidth(), getPrefHeight());
	}

	public Button (Skin skin, String styleName) 
	: base(skin)
	{
		
		initialize();
		setStyle(skin.get< ButtonStyle>(styleName, typeof(ButtonStyle)));
		setSize(getPrefWidth(), getPrefHeight());
	}

	public Button (Actor child, Skin skin, String styleName)
	:this(child, skin.get< ButtonStyle>(styleName, typeof(ButtonStyle))){
		
		setSkin(skin);
	}

	public Button (Actor child, ButtonStyle style) {
		initialize();
		add(child);
		setStyle(style);
		setSize(getPrefWidth(), getPrefHeight());
	}

	public Button (ButtonStyle style) {
		initialize();
		setStyle(style);
		setSize(getPrefWidth(), getPrefHeight());
	}

	/** Creates a button without setting the style or size. At least a style must be set before using this button. */
	public Button () {
		initialize();
	}

	private void initialize () {
		setTouchable(Touchable.enabled);
		addListener(clickListener = new ClickListener() {
			
		});
	}

	private class ButtonClickListener : ClickListener
	{
		private readonly Button _button;

		public ButtonClickListener(Button button)
		{
			_button = button;
		}

		public override void clicked(InputEvent @event, float x, float y) {
			if (_button.isDisabled()) return;
			_button.setChecked(!_button._isChecked, true);
		}
	}

	public Button (IDrawable? up)
	:this(new ButtonStyle(up, null, null)){
		
	}

	public Button (IDrawable? up, IDrawable? down)
	:this(new ButtonStyle(up, down, null)){
		
	}

	public Button (IDrawable? up, IDrawable? down, IDrawable? @checked)
	:this(new ButtonStyle(up, down, @checked)){
		
	}

	public Button (Actor child, Skin skin)
	:this(child, skin.get<ButtonStyle>(typeof(ButtonStyle))){
		
	}

	public void setChecked (bool isChecked) {
		setChecked(isChecked, programmaticChangeEvents);
	}

	void setChecked (bool isChecked, bool fireEvent) {
		if (this._isChecked == isChecked) return;
		if (buttonGroup != null && !buttonGroup.canCheck(this, isChecked)) return;
		this._isChecked = isChecked;

		if (fireEvent) {
			ChangeListener.ChangeEvent changeEvent = Pools.obtain< ChangeListener.ChangeEvent>(typeof(ChangeListener.ChangeEvent));
			if (fire(changeEvent)) this._isChecked = !isChecked;
			Pools.free(changeEvent);
		}
	}

	/** Toggles the checked state. This method changes the checked state, which fires a {@link ChangeEvent} (if programmatic change
	 * events are enabled), so can be used to simulate a button click. */
	public void toggle () {
		setChecked(!_isChecked);
	}

	public bool isChecked () {
		return _isChecked;
	}

	public bool isPressed () {
		return clickListener.isVisualPressed();
	}

	public bool isOver () {
		return clickListener.isOver();
	}

	public ClickListener getClickListener () {
		return clickListener;
	}

	public bool isDisabled () {
		return _isDisabled;
	}

	/** When true, the button will not toggle {@link #isChecked()} when clicked and will not fire a {@link ChangeEvent}. */
	public void setDisabled (bool isDisabled) {
		this._isDisabled = isDisabled;
	}

	/** If false, {@link #setChecked(boolean)} and {@link #toggle()} will not fire {@link ChangeEvent}. The event will only be
	 * fired only when the user clicks the button */
	public void setProgrammaticChangeEvents (bool programmaticChangeEvents) {
		this.programmaticChangeEvents = programmaticChangeEvents;
	}

	public virtual void setStyle (ButtonStyle style) {
		if (style == null) throw new IllegalArgumentException("style cannot be null.");
		this.style = style;

		setBackground(getBackgroundDrawable());
	}

	/** Returns the button's style. Modifying the returned style may not have an effect until {@link #setStyle(ButtonStyle)} is
	 * called. */
	public virtual ButtonStyle getStyle () {
		return style;
	}

	/** @return May be null. */
	public  ButtonGroup<Button>? getButtonGroup () {
		return buttonGroup;
	}

	/** Returns appropriate background drawable from the style based on the current button state. */
	protected IDrawable? getBackgroundDrawable () {
		if (isDisabled() && style.disabled != null) return style.disabled;
		if (isPressed()) {
			if (isChecked() && style.checkedDown != null) return style.checkedDown;
			if (style.down != null) return style.down;
		}
		if (isOver()) {
			if (isChecked()) {
				if (style.checkedOver != null) return style.checkedOver;
			} else {
				if (style.over != null) return style.over;
			}
		}
		bool focused = hasKeyboardFocus();
		if (isChecked()) {
			if (focused && style.checkedFocused != null) return style.checkedFocused;
			if (style.@checked != null) return style.@checked;
			if (isOver() && style.over != null) return style.over;
		}
		if (focused && style.focused != null) return style.focused;
		return style.up;
	}

	public virtual void draw (IBatch batch, float parentAlpha) {
		validate();

		setBackground(getBackgroundDrawable());

		float offsetX = 0, offsetY = 0;
		if (isPressed() && !isDisabled()) {
			offsetX = style.pressedOffsetX;
			offsetY = style.pressedOffsetY;
		} else if (isChecked() && !isDisabled()) {
			offsetX = style.checkedOffsetX;
			offsetY = style.checkedOffsetY;
		} else {
			offsetX = style.unpressedOffsetX;
			offsetY = style.unpressedOffsetY;
		}
		bool offset = offsetX != 0 || offsetY != 0;

		Array<Actor> children = getChildren();
		if (offset) {
			for (int i = 0; i < children.size; i++)
				children.get(i).moveBy(offsetX, offsetY);
		}
		base.draw(batch, parentAlpha);
		if (offset) {
			for (int i = 0; i < children.size; i++)
				children.get(i).moveBy(-offsetX, -offsetY);
		}

		Stage stage = getStage();
		if (stage != null && stage.getActionsRequestRendering() && isPressed() != clickListener.isPressed())
			Gdx.graphics.requestRendering();
	}

		public override float getPrefWidth () {
		float width = base.getPrefWidth();
		if (style.up != null) width = Math.Max(width, style.up.getMinWidth());
		if (style.down != null) width = Math.Max(width, style.down.getMinWidth());
		if (style.@checked != null) width = Math.Max(width, style.@checked.getMinWidth());
		return width;
	}

		public override float getPrefHeight () {
		float height = base.getPrefHeight();
		if (style.up != null) height = Math.Max(height, style.up.getMinHeight());
		if (style.down != null) height = Math.Max(height, style.down.getMinHeight());
		if (style.@checked != null) height = Math.Max(height, style.@checked.getMinHeight());
		return height;
	}

		public override float getMinWidth () {
		return getPrefWidth();
	}

		public override float getMinHeight () {
		return getPrefHeight();
	}

	/** The style for a button, see {@link Button}.
	 * @author mzechner */
	public class ButtonStyle {
		public IDrawable? up, down, over, focused, disabled;
		public IDrawable? @checked, checkedOver, checkedDown, checkedFocused;
		public float pressedOffsetX, pressedOffsetY, unpressedOffsetX, unpressedOffsetY, checkedOffsetX, checkedOffsetY;

		public ButtonStyle () {
		}

		public ButtonStyle (IDrawable? up, IDrawable? down, IDrawable? @checked) {
			this.up = up;
			this.down = down;
			this.@checked = @checked;
		}

		public ButtonStyle (ButtonStyle style) {
			up = style.up;
			down = style.down;
			over = style.over;
			focused = style.focused;
			disabled = style.disabled;

			@checked = style.@checked;
			checkedOver = style.checkedOver;
			checkedDown = style.checkedDown;
			checkedFocused = style.checkedFocused;

			pressedOffsetX = style.pressedOffsetX;
			pressedOffsetY = style.pressedOffsetY;
			unpressedOffsetX = style.unpressedOffsetX;
			unpressedOffsetY = style.unpressedOffsetY;
			checkedOffsetX = style.checkedOffsetX;
			checkedOffsetY = style.checkedOffsetY;
		}
	}
}
}
