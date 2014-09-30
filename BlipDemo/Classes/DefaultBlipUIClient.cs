//
//  DefaultBlipUIClient.cs
//
//  Author:
//       pnovodon <>
//
//  Copyright (c) 2014 Switch Media
//
//
using System;
using BlipLib.Core;

namespace BlipLib
{
	/**
	 * Base IBlipUIClient implementation
	 * 
	 * Provides convenient wrappers for events*/
	public abstract class DefaultBlipUIClient : IBlipUIClient
	{

#region IBlipUIClient implementation

		public event BlipLib.DataType.CommonTypes.ClickEvent BlipClick;

		public event BlipLib.DataType.CommonTypes.ClickEvent BlipDoubleClick;

		public event BlipLib.DataType.CommonTypes.MoveEvent BlipMove;

#endregion

		protected void RaiseBlipClickEvent(long blipID)
		{
			if(BlipClick != null)
				BlipClick(blipID);
		}

		protected void RaiseBlipDoubleClickEvent(long blipID)
		{
			if(BlipDoubleClick != null)
				BlipDoubleClick(blipID);
		}

		protected void RaiseBlipClickEvent(long blipID, int posX, int posY)
		{
			if(BlipMove != null)
				BlipMove(blipID, posX, posY);
		}

		public abstract void DrawBlip(IBlip blipData);

		public abstract void UpdateBlip(IBlip blipData);

	}
}

