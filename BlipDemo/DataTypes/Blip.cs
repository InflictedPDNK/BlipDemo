//
//  BlipData.cs
//
//  Author:
//       pnovodon <>
//
//  Copyright (c) 2014 Switch Media
//
//
using System;

using BlipLib.Core;

namespace BlipLib.DataType
{


	public class Blip : IBlip
	{
		private static Random rnd = new Random();

		public Blip(int paramColour, string title)
		{
			Colour = new CommonTypes.Color(paramColour);
			Title = title;
			TitleVisible = false;
			AnimationType = BlipAnimation.FADEIN;
			ID = ++_id;

			Radius = rnd.Next(80, 200);

			Console.WriteLine("Created a blip with radius {0}", Radius);
		}

#region IBlip implementation

		public string Title
		{
			get;
			set;
		}

		public long ID
		{
			get;
			protected set;
		}

		public CommonTypes.Color Colour
		{
			get;
			set;

		}

		public bool TitleVisible
		{
			get;
			set;
		}

		public int PosX
		{
			get;
			set;
		}

		public int PosY
		{
			get;
			set;
		}

		public int Radius
		{
			get;
			set;
		}

		public BlipAnimation AnimationType
		{
			get;
			protected set;
		}

#endregion

		private static long _id = 0;


	}
}

