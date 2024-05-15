using SharpGDX.Shims;
using Timer = SharpGDX.Utils.Timer;
using Task = SharpGDX.Utils.Timer.Task;
using SharpGDX.Scenes.Scene2D;
using SharpGDX;
using SharpGDX.Scenes.Scene2D.UI;
using System.Text;
using SharpGDX.Scenes.Scene2D.Utils;
using SharpGDX.Mathematics;
using SharpGDX.Utils;


namespace SharpGDX.Scenes.Scene2D.UI;

/** A single-line text input field.
 * <p>
 * The preferred height of a text field is the height of the {@link TextFieldStyle#font} and {@link TextFieldStyle#background}.
 * The preferred width of a text field is 150, a relatively arbitrary size.
 * <p>
 * The text field will copy the currently selected text when ctrl+c is pressed, and paste any text in the clipboard when ctrl+v is
 * pressed. Clipboard functionality is provided via the {@link Clipboard} interface. Currently there are two standard
 * implementations, one for the desktop and one for Android. The Android clipboard is a stub, as copy & pasting on Android is not
 * supported yet.
 * <p>
 * The text field allows you to specify an {@link OnscreenKeyboard} for displaying a softkeyboard and piping all key events
 * generated by the keyboard to the text field. There are two standard implementations, one for the desktop and one for Android.
 * The desktop keyboard is a stub, as a softkeyboard is not needed on the desktop. The Android {@link OnscreenKeyboard}
 * implementation will bring up the default IME.
 * @author mzechner
 * @author Nathan Sweet */
public class TextField : Widget , Disableable {
	protected const char BACKSPACE = (char)8;
	protected const char CARRIAGE_RETURN = '\r';
	protected const char NEWLINE = '\n';
	protected const char TAB = '\t';
	protected const char DELETE = (char)127;
	protected const char BULLET = (char)149;

	static private readonly Vector2 tmp1 = new Vector2();
	static private readonly Vector2 tmp2 = new Vector2();
	static private readonly Vector2 tmp3 = new Vector2();

	static public float keyRepeatInitialTime = 0.4f;
	static public float keyRepeatTime = 0.1f;

	protected String text;
	protected int cursor, selectionStart;
	protected bool hasSelection;
	protected bool writeEnters;
	protected readonly GlyphLayout layout = new GlyphLayout();
	protected readonly FloatArray glyphPositions = new FloatArray();

	protected TextFieldStyle style;
	private String messageText;
	protected string displayText;
	Clipboard clipboard;
	InputListener inputListener;
	TextFieldListener? listener;
	TextFieldFilter? filter;
	OnscreenKeyboard keyboard = new DefaultOnscreenKeyboard();
	protected bool focusTraversal = true, onlyFontChars = true, disabled;
	private int textHAlign = Align.left;
	private float selectionX, selectionWidth;

	String undoText = "";
	long lastChangeTime;

	bool passwordMode;
	private StringBuilder passwordBuffer;
	private char passwordCharacter = BULLET;

	protected float fontOffset, textHeight, textOffset;
	float renderOffset;
	protected int visibleTextStart, visibleTextEnd;
	private int maxLength;

	bool focused;
	bool cursorOn;
	float blinkTime = 0.32f;
	readonly Task blinkTask ;

	private class BlinkTask : Task
	{
		private readonly TextField _textField;

		public BlinkTask(TextField textField)
		{
			_textField = textField;
		}
		public override void run()
		{
			if (_textField.getStage() == null)
			{
				cancel();
				return;
			}
			_textField.cursorOn = !_textField.cursorOn;
			Gdx.graphics.requestRendering();
		}
}

	readonly KeyRepeatTask keyRepeatTask;
	bool programmaticChangeEvents;

	public TextField (String? text, Skin skin) 
	: this(text, skin.get<TextFieldStyle>(typeof(TextFieldStyle)))
	{
		
	}

	public TextField (String? text, Skin skin, String styleName) 
	: this(text, skin.get<TextFieldStyle>(styleName, typeof(TextFieldStyle)))
	{
		
	}

	public TextField (String? text, TextFieldStyle style)
	{
		keyRepeatTask = new KeyRepeatTask(this);
		blinkTask = new BlinkTask(this);
		setStyle(style);
		clipboard = Gdx.app.getClipboard();
		initialize();
		setText(text);
		setSize(getPrefWidth(), getPrefHeight());
	}

	protected void initialize () {
		addListener(inputListener = createInputListener());
	}

	protected InputListener createInputListener () {
		return new TextFieldClickListener(this);
	}

	protected int letterUnderCursor (float x) {
		x -= textOffset + fontOffset - style.font.getData().cursorX - this.glyphPositions.get(visibleTextStart);
		Drawable background = getBackgroundDrawable();
		if (background != null) x -= style.background.getLeftWidth();
		int n = this.glyphPositions.size;
		float[] glyphPositions = this.glyphPositions.items;
		for (int i = 1; i < n; i++) {
			if (glyphPositions[i] > x) {
				if (glyphPositions[i] - x <= x - glyphPositions[i - 1]) return i;
				return i - 1;
			}
		}
		return n - 1;
	}

	protected bool isWordCharacter (char c) {
		// TODO: Not sure if this is 100% equivalent to Java. -RP
		return Char.IsLetterOrDigit(c);
	}

	protected int[] wordUnderCursor (int at) {
		String text = this.text;
		int start = at, right = text.Length, left = 0, index = start;
		if (at >= text.Length) {
			left = text.Length;
			right = 0;
		} else {
			for (; index < right; index++) {
				if (!isWordCharacter(text[index])) {
					right = index;
					break;
				}
			}
			for (index = start - 1; index > -1; index--) {
				if (!isWordCharacter(text[index])) {
					left = index + 1;
					break;
				}
			}
		}
		return new int[] {left, right};
	}

	int[] wordUnderCursor (float x) {
		return wordUnderCursor(letterUnderCursor(x));
	}

	bool withinMaxLength (int size) {
		return maxLength <= 0 || size < maxLength;
	}

	public void setMaxLength (int maxLength) {
		this.maxLength = maxLength;
	}

	public int getMaxLength () {
		return this.maxLength;
	}

	/** When false, text set by {@link #setText(String)} may contain characters not in the font, a space will be displayed instead.
	 * When true (the default), characters not in the font are stripped by setText. Characters not in the font are always stripped
	 * when typed or pasted. */
	public void setOnlyFontChars (bool onlyFontChars) {
		this.onlyFontChars = onlyFontChars;
	}

	public void setStyle (TextFieldStyle style) {
		if (style == null) throw new IllegalArgumentException("style cannot be null.");
		this.style = style;

		textHeight = style.font.getCapHeight() - style.font.getDescent() * 2;
		if (text != null) updateDisplayText();
		invalidateHierarchy();
	}

	/** Returns the text field's style. Modifying the returned style may not have an effect until {@link #setStyle(TextFieldStyle)}
	 * is called. */
	public TextFieldStyle getStyle () {
		return style;
	}

	protected void calculateOffsets () {
		float visibleWidth = getWidth();
		Drawable background = getBackgroundDrawable();
		if (background != null) visibleWidth -= background.getLeftWidth() + background.getRightWidth();

		int glyphCount = this.glyphPositions.size;
		float[] glyphPositions = this.glyphPositions.items;

		// Check if the cursor has gone out the left or right side of the visible area and adjust renderOffset.
		cursor = MathUtils.clamp(cursor, 0, glyphCount - 1);
		float distance = glyphPositions[Math.Max(0, cursor - 1)] + renderOffset;
		if (distance <= 0)
			renderOffset -= distance;
		else {
			int index = Math.Min(glyphCount - 1, cursor + 1);
			float minX = glyphPositions[index] - visibleWidth;
			if (-renderOffset < minX) renderOffset = -minX;
		}

		// Prevent renderOffset from starting too close to the end, eg after text was deleted.
		float maxOffset = 0;
		float width = glyphPositions[glyphCount - 1];
		for (int i = glyphCount - 2; i >= 0; i--) {
			float x = glyphPositions[i];
			if (width - x > visibleWidth) break;
			maxOffset = x;
		}
		if (-renderOffset > maxOffset) renderOffset = -maxOffset;

		// calculate first visible char based on render offset
		visibleTextStart = 0;
		float startX = 0;
		for (int i = 0; i < glyphCount; i++) {
			if (glyphPositions[i] >= -renderOffset) {
				visibleTextStart = i;
				startX = glyphPositions[i];
				break;
			}
		}

		// calculate last visible char based on visible width and render offset
		int end = visibleTextStart + 1;
		float endX = visibleWidth - renderOffset;
		for (int n = Math.Min(displayText.Length, glyphCount); end <= n; end++)
			if (glyphPositions[end] > endX) break;
		visibleTextEnd = Math.Max(0, end - 1);

		if ((textHAlign & Align.left) == 0) {
			textOffset = visibleWidth - glyphPositions[visibleTextEnd] - fontOffset + startX;
			if ((textHAlign & Align.center) != 0) textOffset = (float)Math.Round(textOffset * 0.5f);
		} else
			textOffset = startX + renderOffset;

		// calculate selection x position and width
		if (hasSelection) {
			int minIndex = Math.Min(cursor, selectionStart);
			int maxIndex = Math.Max(cursor, selectionStart);
			float minX = Math.Max(glyphPositions[minIndex] - glyphPositions[visibleTextStart], -textOffset);
			float maxX = Math.Min(glyphPositions[maxIndex] - glyphPositions[visibleTextStart], visibleWidth - textOffset);
			selectionX = minX;
			selectionWidth = maxX - minX - style.font.getData().cursorX;
		}
	}

	protected Drawable? getBackgroundDrawable () {
		if (disabled && style.disabledBackground != null) return style.disabledBackground;
		if (style.focusedBackground != null && hasKeyboardFocus()) return style.focusedBackground;
		return style.background;
	}

	public void draw (Batch batch, float parentAlpha) {
		bool focused = hasKeyboardFocus();
		if (focused != this.focused || (focused && !blinkTask.isScheduled())) {
			this.focused = focused;
			blinkTask.cancel();
			cursorOn = focused;
			if (focused)
				Timer.schedule(blinkTask, blinkTime, blinkTime);
			else
				keyRepeatTask.cancel();
		} else if (!focused) //
			cursorOn = false;

		 BitmapFont font = style.font;
		 Color fontColor = (disabled && style.disabledFontColor != null) ? style.disabledFontColor
			: ((focused && style.focusedFontColor != null) ? style.focusedFontColor : style.fontColor);
		 Drawable selection = style.selection;
		 Drawable cursorPatch = style.cursor;
		 Drawable background = getBackgroundDrawable();

		Color color = getColor();
		float x = getX();
		float y = getY();
		float width = getWidth();
		float height = getHeight();

		batch.setColor(color.r, color.g, color.b, color.a * parentAlpha);
		float bgLeftWidth = 0, bgRightWidth = 0;
		if (background != null) {
			background.draw(batch, x, y, width, height);
			bgLeftWidth = background.getLeftWidth();
			bgRightWidth = background.getRightWidth();
		}

		float textY = getTextY(font, background);
		calculateOffsets();

		if (focused && hasSelection && selection != null) {
			drawSelection(selection, batch, font, x + bgLeftWidth, y + textY);
		}

		float yOffset = font.isFlipped() ? -textHeight : 0;
		if (displayText.Length == 0) {
			if ((!focused || disabled) && messageText != null) {
				BitmapFont messageFont = style.messageFont != null ? style.messageFont : font;
				if (style.messageFontColor != null) {
					messageFont.setColor(style.messageFontColor.r, style.messageFontColor.g, style.messageFontColor.b,
						style.messageFontColor.a * color.a * parentAlpha);
				} else
					messageFont.setColor(0.7f, 0.7f, 0.7f, color.a * parentAlpha);
				drawMessageText(batch, messageFont, x + bgLeftWidth, y + textY + yOffset, width - bgLeftWidth - bgRightWidth);
			}
		} else {
			font.setColor(fontColor.r, fontColor.g, fontColor.b, fontColor.a * color.a * parentAlpha);
			drawText(batch, font, x + bgLeftWidth, y + textY + yOffset);
		}
		if (!disabled && cursorOn && cursorPatch != null) {
			drawCursor(cursorPatch, batch, font, x + bgLeftWidth, y + textY);
		}
	}

	protected float getTextY (BitmapFont font, Drawable? background) {
		float height = getHeight();
		float textY = textHeight / 2 + font.getDescent();
		if (background != null) {
			float bottom = background.getBottomHeight();
			textY = textY + (height - background.getTopHeight() - bottom) / 2 + bottom;
		} else {
			textY = textY + height / 2;
		}
		if (font.usesIntegerPositions()) textY = (int)textY;
		return textY;
	}

	/** Draws selection rectangle **/
	protected void drawSelection (Drawable selection, Batch batch, BitmapFont font, float x, float y) {
		selection.draw(batch, x + textOffset + selectionX + fontOffset, y - textHeight - font.getDescent(), selectionWidth,
			textHeight);
	}

	protected void drawText (Batch batch, BitmapFont font, float x, float y) {
		font.draw(batch, displayText, x + textOffset, y, visibleTextStart, visibleTextEnd, 0, Align.left, false);
	}

	protected void drawMessageText (Batch batch, BitmapFont font, float x, float y, float maxWidth) {
		font.draw(batch, messageText, x, y, 0, messageText.Length, maxWidth, textHAlign, false, "...");
	}

	protected void drawCursor (Drawable cursorPatch, Batch batch, BitmapFont font, float x, float y) {
		cursorPatch.draw(batch,
			x + textOffset + glyphPositions.get(cursor) - glyphPositions.get(visibleTextStart) + fontOffset + font.getData().cursorX,
			y - textHeight - font.getDescent(), cursorPatch.getMinWidth(), textHeight);
	}

	protected void updateDisplayText () {
		BitmapFont font = style.font;
		BitmapFont.BitmapFontData data = font.getData();
		String text = this.text;
		int textLength = text.Length;

		StringBuilder buffer = new StringBuilder();
		for (int i = 0; i < textLength; i++) {
			char c = text[i];
			buffer.Append(data.hasGlyph(c) ? c : ' ');
		}
		String newDisplayText = buffer.ToString();

		if (passwordMode && data.hasGlyph(passwordCharacter)) {
			if (passwordBuffer == null) passwordBuffer = new StringBuilder(newDisplayText.Length);
			if (passwordBuffer.Length > textLength)
				passwordBuffer.Length =(textLength);
			else {
				for (int i = passwordBuffer.Length; i < textLength; i++)
					passwordBuffer.Append(passwordCharacter);
			}
			displayText = passwordBuffer.ToString();
		} else
			displayText = newDisplayText;

		layout.setText(font, displayText.ToString().Replace('\r', ' ').Replace('\n', ' '));
		glyphPositions.clear();
		float x = 0;
		if (layout.runs.size > 0) {
			GlyphLayout.GlyphRun run = layout.runs.first();
			FloatArray xAdvances = run.xAdvances;
			fontOffset = xAdvances.first();
			for (int i = 1, n = xAdvances.size; i < n; i++) {
				glyphPositions.add(x);
				x += xAdvances.get(i);
			}
		} else
			fontOffset = 0;
		glyphPositions.add(x);

		visibleTextStart = Math.Min(visibleTextStart, glyphPositions.size - 1);
		visibleTextEnd = MathUtils.clamp(visibleTextEnd, visibleTextStart, glyphPositions.size - 1);

		if (selectionStart > newDisplayText.Length) selectionStart = textLength;
	}

	/** Copies the contents of this TextField to the {@link Clipboard} implementation set on this TextField. */
	public void copy () {
		if (hasSelection && !passwordMode) {
			clipboard.setContents(text.Substring(Math.Min(cursor, selectionStart), Math.Max(cursor, selectionStart)));
		}
	}

	/** Copies the selected contents of this TextField to the {@link Clipboard} implementation set on this TextField, then removes
	 * it. */
	public void cut () {
		cut(programmaticChangeEvents);
	}

	void cut (bool fireChangeEvent) {
		if (hasSelection && !passwordMode) {
			copy();
			cursor = delete(fireChangeEvent);
			updateDisplayText();
		}
	}

	void paste (String? content, bool fireChangeEvent) {
		if (content == null) return;
		StringBuilder buffer = new StringBuilder();
		int textLength = text.Length;
		if (hasSelection) textLength -= Math.Abs(cursor - selectionStart);
		BitmapFont.BitmapFontData data = style.font.getData();
		for (int i = 0, n = content.Length; i < n; i++) {
			if (!withinMaxLength(textLength + buffer.Length)) break;
			char c = content[i];
			if (!(writeEnters && (c == NEWLINE || c == CARRIAGE_RETURN))) {
				if (c == '\r' || c == '\n') continue;
				if (onlyFontChars && !data.hasGlyph(c)) continue;
				if (filter != null && !filter.acceptChar(this, c)) continue;
			}
			buffer.Append(c);
		}
		content = buffer.ToString();

		if (hasSelection) cursor = delete(fireChangeEvent);
		if (fireChangeEvent)
			changeText(text, insert(cursor, content, text));
		else
			text = insert(cursor, content, text);
		updateDisplayText();
		cursor += content.Length;
	}

	String insert (int position, string text, String to) {
		if (to.Length == 0) return text.ToString();
		return to.Substring(0, position) + text + to.Substring(position, to.Length);
	}

	int delete (bool fireChangeEvent) {
		int from = selectionStart;
		int to = cursor;
		int minIndex = Math.Min(from, to);
		int maxIndex = Math.Max(from, to);
		String newText = (minIndex > 0 ? text.Substring(0, minIndex) : "")
			+ (maxIndex < text.Length ? text.Substring(maxIndex, text.Length) : "");
		if (fireChangeEvent)
			changeText(text, newText);
		else
			text = newText;
		clearSelection();
		return minIndex;
	}

	/** Sets the {@link Stage#setKeyboardFocus(Actor) keyboard focus} to the next TextField. If no next text field is found, the
	 * onscreen keyboard is hidden. Does nothing if the text field is not in a stage.
	 * @param up If true, the text field with the same or next smallest y coordinate is found, else the next highest. */
	public void next (bool up) {
		Stage stage = getStage();
		if (stage == null) return;
		TextField current = this;
		Vector2 currentCoords = current.getParent().localToStageCoordinates(tmp2.set(current.getX(), current.getY()));
		Vector2 bestCoords = tmp1;
		while (true) {
			TextField textField = current.findNextTextField(stage.getActors(), null, bestCoords, currentCoords, up);
			if (textField == null) { // Try to wrap around.
				if (up)
					currentCoords.set(-float.MaxValue, -float.MaxValue);
				else
					currentCoords.set(float.MaxValue, float.MaxValue);
				textField = current.findNextTextField(stage.getActors(), null, bestCoords, currentCoords, up);
			}
			if (textField == null) {
				Gdx.input.setOnscreenKeyboardVisible(false);
				break;
			}
			if (stage.setKeyboardFocus(textField)) {
				textField.selectAll();
				break;
			}
			current = textField;
			currentCoords.set(bestCoords);
		}
	}

	/** @return May be null. */
	private TextField? findNextTextField (Array<Actor> actors, TextField? best, Vector2 bestCoords,
		Vector2 currentCoords, bool up) {
		for (int i = 0, n = actors.size; i < n; i++) {
			Actor actor = actors.get(i);
			if (actor is TextField) {
				if (actor == this) continue;
				TextField textField = (TextField)actor;
				if (textField.isDisabled() || !textField.focusTraversal || !textField.ascendantsVisible()) continue;
				Vector2 actorCoords = actor.getParent().localToStageCoordinates(tmp3.set(actor.getX(), actor.getY()));
				bool below = actorCoords.y != currentCoords.y && (actorCoords.y < currentCoords.y ^ up);
				bool right = actorCoords.y == currentCoords.y && (actorCoords.x > currentCoords.x ^ up);
				if (!below && !right) continue;
				bool better = best == null || (actorCoords.y != bestCoords.y && (actorCoords.y > bestCoords.y ^ up));
				if (!better) better = actorCoords.y == bestCoords.y && (actorCoords.x < bestCoords.x ^ up);
				if (better) {
					best = (TextField)actor;
					bestCoords.set(actorCoords);
				}
			} else if (actor is Group)
				best = findNextTextField(((Group)actor).getChildren(), best, bestCoords, currentCoords, up);
		}
		return best;
	}

	public InputListener getDefaultInputListener () {
		return inputListener;
	}

	/** @param listener May be null. */
	public void setTextFieldListener ( TextFieldListener? listener) {
		this.listener = listener;
	}

	/** @param filter May be null. */
	public void setTextFieldFilter (TextFieldFilter? filter) {
		this.filter = filter;
	}

	public  TextFieldFilter? getTextFieldFilter () {
		return filter;
	}

	/** If true (the default), tab/shift+tab will move to the next text field. */
	public void setFocusTraversal (bool focusTraversal) {
		this.focusTraversal = focusTraversal;
	}

	public bool getFocusTraversal () {
		return focusTraversal;
	}

	/** @return May be null. */
	public String? getMessageText () {
		return messageText;
	}

	/** Sets the text that will be drawn in the text field if no text has been entered.
	 * @param messageText may be null. */
	public void setMessageText (String? messageText) {
		this.messageText = messageText;
	}

	/** @param str If null, "" is used. */
	public void appendText (String? str) {
		if (str == null) str = "";

		clearSelection();
		cursor = text.Length;
		paste(str, programmaticChangeEvents);
	}

	/** @param str If null, "" is used. */
	public void setText (String? str) {
		if (str == null) str = "";
		if (str.Equals(text)) return;

		clearSelection();
		String oldText = text;
		text = "";
		paste(str, false);
		if (programmaticChangeEvents) changeText(oldText, text);
		cursor = 0;
	}

	/** @return Never null, might be an empty string. */
	public String getText () {
		return text;
	}

	/** @return True if the text was changed. */
	bool changeText (String oldText, String newText) {
		if (newText.Equals(oldText)) return false;
		text = newText;
		ChangeListener.ChangeEvent changeEvent = Pools.obtain< ChangeListener.ChangeEvent>(typeof(ChangeListener.ChangeEvent));
		bool cancelled = fire(changeEvent);
		if (cancelled) text = oldText;
		Pools.free(changeEvent);
		return !cancelled;
	}

	/** If false, methods that change the text will not fire {@link ChangeEvent}, the event will be fired only when the user
	 * changes the text. */
	public void setProgrammaticChangeEvents (bool programmaticChangeEvents) {
		this.programmaticChangeEvents = programmaticChangeEvents;
	}

	public bool getProgrammaticChangeEvents () {
		return programmaticChangeEvents;
	}

	public int getSelectionStart () {
		return selectionStart;
	}

	public String getSelection () {
		return hasSelection ? text.Substring(Math.Min(selectionStart, cursor), Math.Max(selectionStart, cursor)) : "";
	}

	/** Sets the selected text. */
	public void setSelection (int selectionStart, int selectionEnd) {
		if (selectionStart < 0) throw new IllegalArgumentException("selectionStart must be >= 0");
		if (selectionEnd < 0) throw new IllegalArgumentException("selectionEnd must be >= 0");
		selectionStart = Math.Min(text.Length, selectionStart);
		selectionEnd = Math.Min(text.Length, selectionEnd);
		if (selectionEnd == selectionStart) {
			clearSelection();
			return;
		}
		if (selectionEnd < selectionStart) {
			int temp = selectionEnd;
			selectionEnd = selectionStart;
			selectionStart = temp;
		}

		hasSelection = true;
		this.selectionStart = selectionStart;
		cursor = selectionEnd;
	}

	public void selectAll () {
		setSelection(0, text.Length);
	}

	public void clearSelection () {
		hasSelection = false;
	}

	/** Sets the cursor position and clears any selection. */
	public void setCursorPosition (int cursorPosition) {
		if (cursorPosition < 0) throw new IllegalArgumentException("cursorPosition must be >= 0");
		clearSelection();
		cursor = Math.Min(cursorPosition, text.Length);
	}

	public int getCursorPosition () {
		return cursor;
	}

	/** Default is an instance of {@link DefaultOnscreenKeyboard}. */
	public OnscreenKeyboard getOnscreenKeyboard () {
		return keyboard;
	}

	public void setOnscreenKeyboard (OnscreenKeyboard keyboard) {
		this.keyboard = keyboard;
	}

	public void setClipboard (Clipboard clipboard) {
		this.clipboard = clipboard;
	}

	public float getPrefWidth () {
		return 150;
	}

	public float getPrefHeight () {
		float topAndBottom = 0, minHeight = 0;
		if (style.background != null) {
			topAndBottom = Math.Max(topAndBottom, style.background.getBottomHeight() + style.background.getTopHeight());
			minHeight = Math.Max(minHeight, style.background.getMinHeight());
		}
		if (style.focusedBackground != null) {
			topAndBottom = Math.Max(topAndBottom,
				style.focusedBackground.getBottomHeight() + style.focusedBackground.getTopHeight());
			minHeight = Math.Max(minHeight, style.focusedBackground.getMinHeight());
		}
		if (style.disabledBackground != null) {
			topAndBottom = Math.Max(topAndBottom,
				style.disabledBackground.getBottomHeight() + style.disabledBackground.getTopHeight());
			minHeight = Math.Max(minHeight, style.disabledBackground.getMinHeight());
		}
		return Math.Max(topAndBottom + textHeight, minHeight);
	}

	/** Sets text horizontal alignment (left, center or right).
	 * @see Align */
	public void setAlignment (int alignment) {
		this.textHAlign = alignment;
	}

	public int getAlignment () {
		return textHAlign;
	}

	/** If true, the text in this text field will be shown as bullet characters.
	 * @see #setPasswordCharacter(char) */
	public void setPasswordMode (bool passwordMode) {
		this.passwordMode = passwordMode;
		updateDisplayText();
	}

	public bool isPasswordMode () {
		return passwordMode;
	}

	/** Sets the password character for the text field. The character must be present in the {@link BitmapFont}. Default is 149
	 * (bullet). */
	public void setPasswordCharacter (char passwordCharacter) {
		this.passwordCharacter = passwordCharacter;
		if (passwordMode) updateDisplayText();
	}

	public void setBlinkTime (float blinkTime) {
		this.blinkTime = blinkTime;
	}

	public void setDisabled (bool disabled) {
		this.disabled = disabled;
	}

	public bool isDisabled () {
		return disabled;
	}

	protected void moveCursor (bool forward, bool jump) {
		int limit = forward ? text.Length : 0;
		int charOffset = forward ? 0 : -1;
		while ((forward ? ++cursor < limit : --cursor > limit) && jump) {
			if (!continueCursor(cursor, charOffset)) break;
		}
	}

	protected bool continueCursor (int index, int offset) {
		char c = text[index + offset];
		return isWordCharacter(c);
	}

	class KeyRepeatTask : Task {
		private readonly TextField _textField;
		internal int keycode;

		public KeyRepeatTask(TextField textField)
		{
			_textField = textField;
		}

		public override void run () {
			if (_textField.getStage() == null) {
				cancel();
				return;
			}
			_textField.inputListener.keyDown(null, keycode);
		}
	}

	/** Interface for listening to typed characters.
	 * @author mzechner */
	public interface TextFieldListener {
		public void keyTyped (TextField textField, char c);
	}

	/** Interface for filtering characters entered into the text field.
	 * @author mzechner */
	public interface TextFieldFilter {
		public bool acceptChar (TextField textField, char c);

		public class DigitsOnlyFilter : TextFieldFilter {
			public bool acceptChar (TextField textField, char c) {
				// TODO: Not sure if this is 100% equivalent to Java. -RP
				return Char.IsDigit(c);
			}
		}
	}

	/** An interface for onscreen keyboards. Can invoke the default keyboard or render your own keyboard!
	 * @author mzechner */
	public interface OnscreenKeyboard {
		public void show (bool visible);
	}

	/** The default {@link OnscreenKeyboard} used by all {@link TextField} instances. Just uses
	 * {@link Input#setOnscreenKeyboardVisible(boolean)} as appropriate. Might overlap your actual rendering, so use with care!
	 * @author mzechner */
	public class DefaultOnscreenKeyboard : OnscreenKeyboard {
		public void show (bool visible) {
			Gdx.input.setOnscreenKeyboardVisible(visible);
		}
	}

	/** Basic input listener for the text field */
	public class TextFieldClickListener : ClickListener {
		private readonly TextField _textField;

		public TextFieldClickListener(TextField textField)
		{
			_textField = textField;
		}

		public void clicked (InputEvent @event, float x, float y) {
			int count = getTapCount() % 4;
			if (count == 0) _textField.clearSelection();
			if (count == 2) {
				int[] array = _textField.wordUnderCursor(x);
				_textField.setSelection(array[0], array[1]);
			}
			if (count == 3) _textField.selectAll();
		}

		public bool touchDown (InputEvent @event, float x, float y, int pointer, int button) {
			if (!base.touchDown(@event, x, y, pointer, button)) return false;
			if (pointer == 0 && button != 0) return false;
			if (_textField.disabled) return true;
			setCursorPosition(x, y);
			_textField.selectionStart = _textField.cursor;
			Stage stage = _textField.getStage();
			if (stage != null) stage.setKeyboardFocus(_textField);
			_textField.keyboard.show(true);
			_textField.hasSelection = true;
			return true;
		}

		public void touchDragged (InputEvent @event, float x, float y, int pointer) {
			base.touchDragged(@event, x, y, pointer);
			setCursorPosition(x, y);
		}

		public void touchUp (InputEvent @event, float x, float y, int pointer, int button) {
			if (_textField.selectionStart == _textField.cursor) _textField.hasSelection = false;
			base.touchUp(@event, x, y, pointer, button);
		}

		protected void setCursorPosition (float x, float y) {
			_textField.cursor = _textField.letterUnderCursor(x);

			_textField.cursorOn = _textField.focused;
			_textField.blinkTask.cancel();
			if (_textField.focused) Timer.schedule(_textField.blinkTask, _textField.blinkTime, _textField.blinkTime);
		}

		protected void goHome (bool jump) {
			_textField.cursor = 0;
		}

		protected void goEnd (bool jump) {
			_textField.cursor = _textField.text.Length;
		}

		public bool keyDown (InputEvent @event, int keycode) {
			if (_textField.disabled) return false;

			_textField.cursorOn = _textField.focused;
			_textField.blinkTask.cancel();
			if (_textField.focused) Timer.schedule(_textField.blinkTask, _textField.blinkTime, _textField.blinkTime);

			if (!_textField.hasKeyboardFocus()) return false;

			bool repeat = false;
			bool ctrl = UIUtils.ctrl();
			bool jump = ctrl && !_textField.passwordMode;
			bool handled = true;

			if (ctrl) {
				switch (keycode) {
				case Input.Keys.V:
					_textField.paste(_textField.clipboard.getContents(), true);
					repeat = true;
					break;
				case Input.Keys.C:
				case Input.Keys.INSERT:
					_textField.copy();
					return true;
				case Input.Keys.X:
					_textField.cut(true);
					return true;
				case Input.Keys.A:
					_textField.selectAll();
					return true;
				case Input.Keys.Z:
					String oldText = _textField.text;
					_textField.setText(_textField.undoText);
					_textField.undoText = oldText;
					_textField.updateDisplayText();
					return true;
				default:
					handled = false;
					break;
				}
			}

			if (UIUtils.shift()) {
				switch (keycode) {
				case Input.Keys.INSERT:
					_textField.paste(_textField.clipboard.getContents(), true);
					break;
				case Input.Keys.FORWARD_DEL:
					_textField.cut(true);
					break;
				}

				selection:
				{
					int temp = _textField.cursor;
					keys:
					{
						switch (keycode)
						{
							case Input.Keys.LEFT:
								_textField.moveCursor(false, jump);
								repeat = true;
								handled = true;
								goto endOfKeys;
							case Input.Keys.RIGHT:
								_textField.moveCursor(true, jump);
								repeat = true;
								handled = true;
								goto endOfKeys;
							case Input.Keys.HOME:
								goHome(jump);
								handled = true;
								goto endOfKeys;
							case Input.Keys.END:
								goEnd(jump);
								handled = true;
								goto endOfKeys;
						}

						break selection;
					}
					endOfKeys:
					if (!_textField.hasSelection) {
						_textField.selectionStart = temp;
						_textField.hasSelection = true;
					}
				}
			} else {
				// Cursor movement or other keys (kills selection).
				switch (keycode) {
				case Input.Keys.LEFT:
					_textField.moveCursor(false, jump);
					_textField.clearSelection();
					repeat = true;
					handled = true;
					break;
				case Input.Keys.RIGHT:
					_textField.moveCursor(true, jump);
					_textField.clearSelection();
					repeat = true;
					handled = true;
					break;
				case Input.Keys.HOME:
					goHome(jump);
					_textField.clearSelection();
					handled = true;
					break;
				case Input.Keys.END:
					goEnd(jump);
					_textField.clearSelection();
					handled = true;
					break;
				}
			}

			_textField.cursor = MathUtils.clamp(_textField.cursor, 0, _textField.text.Length);

			if (repeat) scheduleKeyRepeatTask(keycode);
			return handled;
		}

		protected void scheduleKeyRepeatTask (int keycode) {
			if (!_textField.keyRepeatTask.isScheduled() || _textField.keyRepeatTask.keycode != keycode) {
				_textField.keyRepeatTask.keycode = keycode;
				_textField.keyRepeatTask.cancel();
				Timer.schedule(_textField.keyRepeatTask, keyRepeatInitialTime, keyRepeatTime);
			}
		}

		public bool keyUp (InputEvent @event, int keycode) {
			if (_textField.disabled) return false;
			_textField.keyRepeatTask.cancel();
			return true;
		}

		/** Checks if focus traversal should be triggered. The default implementation uses {@link TextField#focusTraversal} and the
		 * typed character, depending on the OS.
		 * @param character The character that triggered a possible focus traversal.
		 * @return true if the focus should change to the {@link TextField#next(boolean) next} input field. */
		protected bool checkFocusTraversal (char character) {
			return _textField.focusTraversal && (character == TAB
			                                     || ((character == CARRIAGE_RETURN || character == NEWLINE) && (UIUtils.isAndroid || UIUtils.isIos)));
		}

		public bool keyTyped (InputEvent @event, char character) {
			if (_textField.disabled) return false;

			// Disallow "typing" most ASCII control characters, which would show up as a space when onlyFontChars is true.
			switch (character) {
			case BACKSPACE:
			case TAB:
			case NEWLINE:
			case CARRIAGE_RETURN:
				break;
			default:
				if (character < 32) return false;
			}

			if (!_textField.hasKeyboardFocus()) return false;

			if (UIUtils.isMac && Gdx.input.isKeyPressed(Input.Keys.SYM)) return true;

			if (checkFocusTraversal(character))
				_textField.next(UIUtils.shift());
			else {
				bool enter = character == CARRIAGE_RETURN || character == NEWLINE;
				bool delete = character == DELETE;
				bool backspace = character == BACKSPACE;
				bool add = enter ? _textField.writeEnters : (!_textField.onlyFontChars || _textField.style.font.getData().hasGlyph(character));
				bool remove = backspace || delete;
				if (add || remove) {
					String oldText = _textField.text;
					int oldCursor = _textField.cursor;
					if (remove) {
						if (_textField.hasSelection)
							_textField.cursor = _textField.delete(false);
						else {
							if (backspace && _textField.cursor > 0) {
								_textField.text = _textField.text.Substring(0, _textField.cursor - 1) + _textField.text.Substring(_textField.cursor--);
								_textField.renderOffset = 0;
							}
							if (delete && _textField.cursor < _textField.text.Length) {
								_textField.text = _textField.text.Substring(0, _textField.cursor) + _textField.text.Substring(_textField.cursor + 1);
							}
						}
					}
					if (add && !remove) {
						// Character may be added to the text.
						if (!enter && _textField.filter != null && !_textField.filter.acceptChar(_textField, character)) return true;
						if (!_textField.withinMaxLength(_textField.text.Length - (_textField.hasSelection ? Math.Abs(_textField.cursor - _textField.selectionStart) : 0))) return true;
						if (_textField.hasSelection) _textField.cursor = _textField.delete(false);
						String insertion = enter ? "\n" : String.valueOf(character);
						_textField.text = _textField.insert(_textField.cursor++, insertion, _textField.text);
					}
					String tempUndoText = _textField.undoText;
					if (_textField.changeText(oldText, _textField.text)) {
						long time = TimeUtils.currentTimeMillis();
						if (time - 750 > _textField.lastChangeTime) _textField.undoText = oldText;
						_textField.lastChangeTime = time;
						_textField.updateDisplayText();
					} else if (!_textField.text.Equals(oldText)) // Keep cursor movement if the text is the same.
						_textField.cursor = oldCursor;
				}
			}
			if (_textField.listener != null) _textField.listener.keyTyped(_textField, character);
			return true;
		}
	}

	/** The style for a text field, see {@link TextField}.
	 * @author mzechner
	 * @author Nathan Sweet */
	 public class TextFieldStyle {
		public BitmapFont font;
		public Color fontColor;
		public  Color? focusedFontColor, disabledFontColor;
		public Drawable? background, focusedBackground, disabledBackground, cursor, selection;
		public  BitmapFont? messageFont;
		public  Color? messageFontColor;

		public TextFieldStyle () {
		}

		public TextFieldStyle (BitmapFont font, Color fontColor, Drawable? cursor, Drawable? selection,
			Drawable? background) {
			this.font = font;
			this.fontColor = fontColor;
			this.cursor = cursor;
			this.selection = selection;
			this.background = background;
		}

		public TextFieldStyle (TextFieldStyle style) {
			font = style.font;
			if (style.fontColor != null) fontColor = new Color(style.fontColor);
			if (style.focusedFontColor != null) focusedFontColor = new Color(style.focusedFontColor);
			if (style.disabledFontColor != null) disabledFontColor = new Color(style.disabledFontColor);

			background = style.background;
			focusedBackground = style.focusedBackground;
			disabledBackground = style.disabledBackground;
			cursor = style.cursor;
			selection = style.selection;

			messageFont = style.messageFont;
			if (style.messageFontColor != null) messageFontColor = new Color(style.messageFontColor);
		}
	}
}
