//
//  IProvider.cs
//
//  Author:
//       pnovodon <>
//
//  Copyright (c) 2014 Switch Media
//
//
using System;
using System.Threading.Tasks;

namespace BlipLib.Core
{
	/**
	 * Data service provider interface
	 * 
	 * This interface is used as a translation layer between external service provider and internal data representation.
	 * Subclasses implement their business logic of parsing and converting data.
	 * */
	public interface IProvider
	{
		/**
		 * Acquire the data from service provider (Async)
		 * 
		 * Returns:
		 * a new instance of IBlip or null if failed
		 * */
		Task<IBlip> AcquireBlip();

		/**
		 * Update existing data (Async)
		 * 
		 * This technically loads data, but instead of creating new instance it updates the existing one
		 * */
		Task UpdateBlip(IBlip blip);

		/**
		 * Set the number of data acquistion requests to cache
		 * This should be called before starting of the acquisition.
		 * Set 0 to disable caching completely*/
		int RequestCacheCapacity { set; }

		/**
		 * Prime cache
		 * The implementations should not use any automatic (hidden) logic of caching, so this is exposed to the Core level*/
		Task PrimeCache();
	}
}

