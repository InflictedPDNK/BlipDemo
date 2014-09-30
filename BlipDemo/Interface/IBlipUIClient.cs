//
//  IBlipUI.cs
//
//  Author:
//       pnovodon <>
//
//  Copyright (c) 2014 Switch Media
//
//
using System;
using BlipLib.DataType;
using System.Drawing;

namespace BlipLib.Core
{
	/**
	 * Client UI controls interface
	 * 
	 * The App level must implement this interface (or derive from the provided Base class)
	 * This object is responsible for interaction between Blips and the Core.
	 * */
	public interface IBlipUIClient
	{
		/** Event raised when user clicks the Blip*/
		event CommonTypes.ClickEvent BlipClick;

		/** Event raised when user double-clicks the Blip*/
		event CommonTypes.ClickEvent BlipDoubleClick;

		/** Event raised when user moves the Blip*/
		event CommonTypes.MoveEvent BlipMove;

		/** Draw a Blip
		 * The core requests the App to draw the blip with provided IBlip descriptive data.
		 * It is App (platform) responsibility to implement the correct logic of drawing
		 * 
		 * Params:
		 * blipData - an instance of IBlip which contains the info required to draw the blip
		 * */
		void DrawBlip(IBlip blipData);

		/** Update the Blip
		 * The core requests the App to update the blip with new IBlip data data.
		 * This is a hint to the App to not re-create, but precisely update the related Blip
		 * 
		 * Params:
		 * blipData - an instance of IBlip which contains the updated info
		 * */
		void UpdateBlip(IBlip blipData);
	}
}

