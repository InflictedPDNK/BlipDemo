//
//  ColourLoversProvider.cs
//
//  Author:
//       pnovodon <>
//
//  Copyright (c) 2014 Switch Media
//
//
using System;
using System.Xml;
using System.IO;
using BlipLib.DataType;
using BlipLib.Core;
using System.Globalization;

namespace BlipLib.Provider
{
	/**
	 * Colour Lovers service provider
	 * Uses XML as a data transport*/
	public class ColourLoversProvider : BaseProvider
	{
		public ColourLoversProvider() : base("http://www.colourlovers.com/api/colors/random")
		{
		}

#region implemented abstract members of BaseProvider

		protected override IBlip ParseData(System.IO.Stream incomingStream)
		{
			XmlDocument xmlDoc = new XmlDocument();

			try
			{
				xmlDoc.Load(incomingStream as MemoryStream);
			}
			catch(Exception)
			{
				return null;
			}

			XmlElement rootElement = xmlDoc["colors"];
			if(rootElement == null)
				return null;

			XmlElement colorElement = rootElement["color"];

			if(colorElement == null)
				return null;

			int colour = 0;
			string title = null;

			foreach(var it in colorElement)
			{
				//skip comments and other not relevant elements
				if(!(it is XmlElement))
					continue;

				XmlElement element = it as XmlElement;

				if(element.Name == "title")
				{
					title = element.InnerText;
				}
				else if(element.Name == "hex")
				{
					Int32.TryParse(element.InnerText,
					               System.Globalization.NumberStyles.HexNumber,
					               CultureInfo.CurrentCulture.NumberFormat,
					               out colour);

					//speed up a bit - break the loop if title was read
					if(!String.IsNullOrEmpty(title))
						break;
				}
			}

			return new Blip(colour, title);

		}

#endregion
	}
}

