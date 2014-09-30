//
//  IBlipCore.cs
//
//  Author:
//       pnovodon <>
//
//  Copyright (c) 2014 Switch Media
//
//
using System;
using BlipLib.DataType;
using System.Threading.Tasks;



namespace BlipLib.Core
{
	/**
	 * Core interface
	 * Responsible for business logic
	 * Acts as an entry point to the Library and corresponds to top level control interface
	 * */
	public interface IBlipCore
	{
		/**
		 * Start the Core
		 * 
		 * Params:
		 * blipClient - reference to an instance of IBlipUIClient created on the App level
		 * requestCacheCapacity - set the number of items to keep in cache for speeding the process. Set 0 to disable caching*/
		void Start(IBlipUIClient blipClient, int requestCacheCapacity);

		/**
		 * Request the Core to create a blip (Async)
		 * 
		 * This is only a request, and it may be not satisfied if data acquisition fails
		 * 
		 * Params:
		 * coordinates of the Blip to create
		 * */
		Task SpawnBlip(int posX, int posY);

	}
}

