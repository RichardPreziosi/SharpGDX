﻿using SharpGDX.Shims;
using SharpGDX.Graphics;
using SharpGDX.Graphics.GLUtils;
using SharpGDX.Graphics.G2D;
using SharpGDX.Utils.Reflect;

namespace SharpGDX.Scenes.Scene2D.Utils
{
	/** Drawable that stores the size information but doesn't draw anything.
 * @author Nathan Sweet */
public class BaseDrawable : IDrawable {
	private  String? name;
	private float leftWidth, rightWidth, topHeight, bottomHeight, minWidth, minHeight;

	public BaseDrawable () {
	}

	/** Creates a new empty drawable with the same sizing information as the specified drawable. */
	public BaseDrawable (IDrawable drawable) {
		if (drawable is BaseDrawable) name = ((BaseDrawable)drawable).getName();
		leftWidth = drawable.getLeftWidth();
		rightWidth = drawable.getRightWidth();
		topHeight = drawable.getTopHeight();
		bottomHeight = drawable.getBottomHeight();
		minWidth = drawable.getMinWidth();
		minHeight = drawable.getMinHeight();
	}

	public virtual void draw (IBatch batch, float x, float y, float width, float height) {
	}

	public float getLeftWidth () {
		return leftWidth;
	}

	public void setLeftWidth (float leftWidth) {
		this.leftWidth = leftWidth;
	}

	public float getRightWidth () {
		return rightWidth;
	}

	public void setRightWidth (float rightWidth) {
		this.rightWidth = rightWidth;
	}

	public float getTopHeight () {
		return topHeight;
	}

	public void setTopHeight (float topHeight) {
		this.topHeight = topHeight;
	}

	public float getBottomHeight () {
		return bottomHeight;
	}

	public void setBottomHeight (float bottomHeight) {
		this.bottomHeight = bottomHeight;
	}

	public void setPadding (float topHeight, float leftWidth, float bottomHeight, float rightWidth) {
		setTopHeight(topHeight);
		setLeftWidth(leftWidth);
		setBottomHeight(bottomHeight);
		setRightWidth(rightWidth);
	}

	public float getMinWidth () {
		return minWidth;
	}

	public void setMinWidth (float minWidth) {
		this.minWidth = minWidth;
	}

	public float getMinHeight () {
		return minHeight;
	}

	public void setMinHeight (float minHeight) {
		this.minHeight = minHeight;
	}

	public void setMinSize (float minWidth, float minHeight) {
		setMinWidth(minWidth);
		setMinHeight(minHeight);
	}

	public  String? getName () {
		return name;
	}

	public void setName ( String? name) {
		this.name = name;
	}

	public  String? toString () {
		if (name == null) return ClassReflection.getSimpleName(GetType());
		return name;
	}
}
}
