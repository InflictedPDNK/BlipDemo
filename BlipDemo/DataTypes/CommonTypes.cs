//
//  CommonTypes.cs
//
//  Author:
//       pnovodon <>
//
//  Copyright (c) 2014 Switch Media
//
//
using System;

namespace BlipLib.DataType
{
	public class CommonTypes
	{
		public delegate void IntEvent(int param);

		public delegate void IntIntEvent(int param);

		public delegate void ClickEvent(long blipID);

		public delegate void MoveEvent(long blipID, int posX, int posY);

		public delegate void StringEvent(string param);

		public delegate void StringHandledEvent(string param, out bool handled);

		public delegate void VoidEvent();

		public delegate void BoolEvent(bool result);

	
		public class Color
		{
			public Color(int colorRGB)
			{
				colorValue = colorRGB;
			}

			public int ToARGB()
			{
				return colorValue;
			}

			public int R
			{
				get
				{
					return (colorValue >> 16) & 0xFF;
				}
			}

			public int G
			{
				get
				{
					return (colorValue >> 8) & 0xFF;
				}
			}

			public int B
			{
				get
				{
					return colorValue & 0xFF;
				}
			}

			private int colorValue;
		}

	}


}

