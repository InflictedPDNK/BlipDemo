//
//  BlipView.cs
//
//  Author:
//       pnovodon <>
//
//  Copyright (c) 2014 Switch Media
//
//
using System;
using Android.Widget;
using Android.Views;
using Android.Content;
using Android.Runtime;
using Android.Util;
using BlipLib.Core;
using BlipLib.DataType;
using Android.Graphics.Drawables;
using Android.Graphics;

namespace BlipDemoApp
{
	/**
	 * Custom drawn view representing a Blip
	 * Adds custom Single and Double click events,
	 * Handles view movement on touch
	 * Adds custom round shaping and colouring*/
	public class BlipView : RelativeLayout
	{
		public delegate void VoidEvent();

		GestureDetector gestureDetector;

		public event VoidEvent DoubleClick;

		//state used to prevent raising a Single click after the move action
		bool movementDetected = false;
		float dx = 0;
		float dy = 0;


		public BlipView(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
		{
		}

		public BlipView(Context context) : this(context, null)
		{
		}

		public BlipView(Context context, IAttributeSet attrs) : this(context, attrs, 0)
		{
		}

		public BlipView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
		{
			gestureDetector = new GestureDetector(context, new GestureListener(this));
		}

		public void SetRoundDrawable(CommonTypes.Color drawableColour)
		{
			ShapeDrawable shapeDrawable = new ShapeDrawable(new Android.Graphics.Drawables.Shapes.OvalShape());
			shapeDrawable.Paint.Color = Color.Argb(255, drawableColour.R,
			                                       drawableColour.G,
			                                       drawableColour.B);
			SetBackgroundDrawable(shapeDrawable);
		}


		public override bool OnTouchEvent(MotionEvent e)
		{

			if(e.Action == MotionEventActions.Down)
			{
				//store original delta
				dx = e.RawX - GetX();
				dy = e.RawY - GetY();
			}
			else
			if(e.Action == MotionEventActions.Move)
			{
				//move the view immediately
				SetX(e.RawX - dx);
				SetY(e.RawY - dy);
				movementDetected = true;
				return true;
			}
			else
			if(e.Action == MotionEventActions.Up && movementDetected)
			{
				movementDetected = false;
				return true;
			}

			return gestureDetector.OnTouchEvent(e);
		}

		protected void RaiseDoubleClick()
		{
			if(DoubleClick != null)
				DoubleClick();
		}

		private class GestureListener : GestureDetector.SimpleOnGestureListener
		{
			readonly BlipView parentView;

			public GestureListener(BlipView parentView)
			{
				this.parentView = parentView;
			}

			public override bool OnDown(MotionEvent e)
			{
				parentView.BringToFront();
				return true;
			}

			public override bool OnDoubleTap(MotionEvent e)
			{
				parentView.RaiseDoubleClick();
				return true;
			}

			public override bool OnSingleTapConfirmed(MotionEvent e)
			{
				parentView.CallOnClick();
				return true;
			}


		}
	
	}

}

