//
//  AndroidUtility.cs
//
//  Author:
//       pnovodon <>
//
//  Copyright (c) 2014 Switch Media
//
//
using System;
using Android.Graphics;
using Android.Content;
using Android.Views;
using Android.Animation;


namespace BlipDemoApp
{
	public class AndroidUtility
	{
		public static Color ColorFromRGBInt(long color)
		{
			return Color.Argb(255,
			                  (int) ((color & 0x00FF0000) >> 16),
			                  (int) ((color & 0x0000FF00) >> 8),
			                  (int) (color & 0x000000FF));
		}

		public static int ConvertDpToPx(int dp, Context ctx)
		{
			return (int) (dp * ((float) (ctx.Resources.DisplayMetrics.DensityDpi) / 160f));
		}

		public static int ConvertPxToDp(int px, Context ctx)
		{
			return (int) ((float) px / ((float) (ctx.Resources.DisplayMetrics.DensityDpi) / 160f));
		}

		public static int ConvertDpToPx(float dp, Context ctx)
		{
			return (int) (dp * ((float) (ctx.Resources.DisplayMetrics.DensityDpi) / 160f));
		}

		public static int ConvertPxToDp(float px, Context ctx)
		{
			return (int) (px / ((float) (ctx.Resources.DisplayMetrics.DensityDpi) / 160f));
		}

		public static void AnimateFadeIn(View v, int duration = 200)
		{
			ObjectAnimator a = ObjectAnimator.OfFloat(v, "alpha", 0.0f, 1.0f);
			a.SetDuration(duration);
			//a.Start();

			ObjectAnimator b = ObjectAnimator.OfFloat(v, "scaleX", 0.0f, 1.5f, 1.0f);
			b.SetDuration(duration);


			ObjectAnimator c = ObjectAnimator.OfFloat(v, "scaleY", 0.0f, 1.5f, 1.0f);
			c.SetDuration(duration);


			AnimatorSet animBlock = new AnimatorSet();

			animBlock.Play(a).With(b).With(c);
			animBlock.Start();
		}

		public static void AnimateFadeOut(View v, int duration = 200)
		{
			ObjectAnimator a = ObjectAnimator.OfFloat(v, "alpha", 1.0f, 0.0f);
			a.SetDuration(duration);
			a.Start();
		}
	}
}

