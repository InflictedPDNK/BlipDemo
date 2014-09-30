//
//  BaseProvider.cs
//
//  Author:
//       pnovodon <>
//
//  Copyright (c) 2014 Switch Media
//
//
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BlipLib.Core;
using System.Threading.Tasks;

namespace BlipLib.Provider
{
	/**
	 * Base provider implementation
	 * 
	 * Governs the Web connection and request caching*/
	public abstract class BaseProvider : IProvider
	{
		LinkedList<IBlip> cache;
		object lockObj = new object();
		string requestURL;
		bool primingInProcess = false;

		public BaseProvider(string URL)
		{
			requestURL = URL;
		}

#region IProvider implementation

		public async Task<IBlip> AcquireBlip()
		{
			IBlip blip = null;


			//try to retrieve a cached blip
			if(cache != null && cache.Count > 0)
			{
				lock(lockObj)
				{
					blip = cache.First();
					cache.RemoveFirst();
				}

			}
			else
			{
				System.Diagnostics.Debug.WriteLine("\tLoading data directly from Provider..");
				using(Stream webResponse = await Utility.HTTPLoadAsync(requestURL))
				{
					//parse the response
					blip = ParseData(webResponse);
				}
			}

			return blip;
		}

		public async Task UpdateBlip(IBlip blip)
		{
			IBlip newBlip = await AcquireBlip();

			//update only "mutable" properties
			if(newBlip != null)
			{
				blip.Colour = newBlip.Colour;
				blip.Title = newBlip.Title;
			}

		}

		public int RequestCacheCapacity
		{
			set
			{
				cacheCapacity = value;

				lock(lockObj)
				{
					if(cacheCapacity > 0)
						cache = new LinkedList<IBlip>();
					else
						cache = null;
				}

			}
		}

		public async Task PrimeCache()
		{
			if(cache != null && cacheCapacity > 0 && !primingInProcess)
			{
				primingInProcess = true;
				System.Diagnostics.Debug.WriteLine("\tCache is priming...");
				while(cache.Count < cacheCapacity)
				{
					using(Stream webResponse = await Utility.HTTPLoadAsync(requestURL))
					{
						IBlip newBlip = ParseData(webResponse);

						if(newBlip == null)
							break;

						lock(lockObj)
						{
							cache.AddLast(newBlip);
						}
					}
				}
				System.Diagnostics.Debug.WriteLine("\tCache priming has finished");
				primingInProcess = false;
			}
		}

		/**
		 * Subclasses must implement this parsing function.
		 * Params:
		 * incomingStream - binary stream in the native format of the provider
		 * Returns:
		 * new instance of IBlip or null if parsing has failed*/
		protected abstract IBlip ParseData(Stream incomingStream);

#endregion

		private int cacheCapacity;
	}
}

