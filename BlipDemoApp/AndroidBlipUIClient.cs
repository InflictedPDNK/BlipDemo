//
//  AndroidBlipUIClient.cs
//
//  Author:
//       pnovodon <>
//
//  Copyright (c) 2014 Switch Media
//
//
using System;
using BlipLib;
using Android.App;
using Android.Views;
using Android.Widget;
using Android.Content;
using Android.Graphics.Drawables;
using BlipLib.Core;
using System.Linq;
using Android.Graphics;

namespace BlipDemoApp
{
	/**
	 * Android client subclass of IBlipUIClient base class
	 * */
	public class AndroidBlipUIClient : DefaultBlipUIClient
	{
		Activity parentActivity;
		ViewGroup mainLayout;
		TextView welcomeText;

		public AndroidBlipUIClient(Activity parentActivity)
		{
			this.parentActivity = parentActivity;
			mainLayout = parentActivity.FindViewById<ViewGroup>(Resource.Id.MainLayout);

			welcomeText = mainLayout.FindViewById<TextView>(Resource.Id.WelcomeText);

			if(mainLayout == null)
				throw new ArgumentException("Couldn't find main layout");
		}

#region implemented abstract members of DefaultBlipUIClient

		public override void DrawBlip(BlipLib.Core.IBlip blipData)
		{
			//use post to offload the Core requests and gracefully schedule it on the main thread
			mainLayout.Post(() => {

				if(welcomeText != null)
					welcomeText.Visibility = ViewStates.Gone;

				BlipView v = new BlipView(parentActivity);
				v.Id = (int) blipData.ID;
				v.SetRoundDrawable(blipData.Colour);

				v.SetX(blipData.PosX - blipData.Radius);
				v.SetY(blipData.PosY - blipData.Radius);

				v.Click += (object sender, EventArgs e) => RaiseBlipClickEvent(v.Id);
				v.DoubleClick += () => RaiseBlipDoubleClickEvent(v.Id);

				mainLayout.AddView(v, blipData.Radius << 1, blipData.Radius << 1);

				//animate only if the Core requires
				if(blipData.AnimationType == BlipAnimation.FADEIN)
					AndroidUtility.AnimateFadeIn(v);
			});

		}

		public override void UpdateBlip(BlipLib.Core.IBlip blipData)
		{
			BlipView v = mainLayout.FindViewById<BlipView>((int) blipData.ID);
			if(v != null)
			{
				parentActivity.RunOnUiThread(() => {

					v.SetRoundDrawable(blipData.Colour);

					TextView tv = (TextView) v.GetChildAt(0);

					if(blipData.TitleVisible)
					{
						if(tv != null)
						{
							tv.Text = blipData.Title;

							//use inversed color to draw text
							tv.SetTextColor(AndroidUtility.ColorFromRGBInt(blipData.Colour.ToARGB() ^ 0xFFFFFF));
						}
						else
							CreateTitle(blipData.Title, AndroidUtility.ColorFromRGBInt(blipData.Colour.ToARGB() ^ 0xFFFFFF), v);
					}
					else
					{
						v.RemoveAllViews();
					}
				});
			}

		}

		private TextView CreateTitle(string title, Color textColor, ViewGroup parentView)
		{
			TextView tv = new TextView(parentActivity);
			tv.Text = title;
			tv.TextSize = 15;
			tv.SetTextColor(textColor);
			tv.Gravity = GravityFlags.Center;
			if(parentView != null)
			{
				if(parentView is RelativeLayout)
				{
					RelativeLayout.LayoutParams lp = new RelativeLayout.LayoutParams(Android.Widget.RelativeLayout.LayoutParams.WrapContent, 
					                                                                 Android.Widget.RelativeLayout.LayoutParams.WrapContent);

					lp.AddRule(LayoutRules.CenterInParent);
					parentView.AddView(tv, lp);
				}
				else
				{
					parentView.AddView(tv);
				}
			}

			return tv;
		}

#endregion
	}
}

