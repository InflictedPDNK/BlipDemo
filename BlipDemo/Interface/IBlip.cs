//
//  IBlip.cs
//
//  Author:
//       pnovodon <>
//
//  Copyright (c) 2014 Switch Media
//
//
using System;

using BlipLib.DataType;

namespace BlipLib.Core
{
	/**
	 * Type of animation for Blip
	 * */
	public enum BlipAnimation
	{
		/** No Animation used*/
		NO_ANIM,

		/** Fade in Animation*/
		FADEIN
	}


	/**
	 * Blip interface
	 * Contains descriptive data for Blip representation
	 * */
	public interface IBlip
	{
		/** Blip Title*/
		string Title { get; set; }

		/** Blip unique ID*/
		long ID { get; }

		/** Blip background colour*/
		CommonTypes.Color Colour { get; set; }

		/** Blip Title visibility*/
		bool TitleVisible { get; set; }

		/** Position relative to parent*/
		int PosX { get; set; }

		/** Position relative to parent*/
		int PosY { get; set; }

		/** Blip creation animation type*/
		BlipAnimation AnimationType { get; }

		/** Blip radius */
		int Radius { get; set; }
	}
}

