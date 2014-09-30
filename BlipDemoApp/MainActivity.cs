using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;


using BlipLib.Core;
using BlipLib.Provider;
using BlipLib;

namespace BlipDemoApp
{
	[Activity(Label = "BlipDemoApp",
	          MainLauncher = true,
	          Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		IBlipCore blipCore;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			SetContentView(Resource.Layout.Main);

			//create a new instance of the BlipCore. Using ColourLovers provider here
			blipCore = new BlipCore<ColourLoversProvider>();

			//supply an UIClient implementation,
			blipCore.Start(new AndroidBlipUIClient(this), 5);



			ViewGroup main = FindViewById<ViewGroup>(Resource.Id.MainLayout);

			//listen to main canvas touch events and request to create blips when touched
			main.Touch += (object sender, View.TouchEventArgs e) => {
				if(e.Event.Action == MotionEventActions.Up)
				{
					blipCore.SpawnBlip((int) e.Event.GetX(), (int) e.Event.GetY());
				}

			};

		}
	}
}


