//
//  BlipCore.cs
//
//  Author:
//       pnovodon <>
//
//  Copyright (c) 2014 Switch Media
//
//
using System;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlipLib.Core
{
	/**
	 * Blip Core implementation
	 * Accepts ProviderClass as a generic to nominate a service provider type
	 * */
	public class BlipCore<ProviderClass> : IBlipCore where ProviderClass: IProvider, new()
	{
		ProviderClass provider;
		IBlipUIClient client;

		Dictionary<long, IBlip> blips;

		public BlipCore()
		{
			blips = new Dictionary<long, IBlip>();
		}

#region IBlipCore implementation


		public void Start(IBlipUIClient blipClient, int requestCacheCapacity = 5)
		{
			if(blipClient == null)
				throw new ArgumentNullException("Client reference is null");

			requestCacheCapacity = requestCacheCapacity > 0 ? requestCacheCapacity : 0;

			provider = new ProviderClass();
			provider.RequestCacheCapacity = requestCacheCapacity;
			provider.PrimeCache();

			client = blipClient;

			client.BlipClick += HandleBlipClick;

			client.BlipDoubleClick += HandleBlipDoubleClick;

			client.BlipMove += HandleBlipMove;
		}

		async void HandleBlipMove(long blipID, int posX, int posY)
		{
			await Task.Run(() => {

				IBlip blip;
				if(blips.TryGetValue(blipID, out blip))
				{
					blip.PosX = posX;
					blip.PosY = posY;
				}
			});

			return;
		}

		async void HandleBlipDoubleClick(long blipID)
		{

			await Task.Run(() => {
				IBlip blip;
				if(blips.TryGetValue(blipID, out blip))
				{
					blip.TitleVisible = !blip.TitleVisible;
					client.UpdateBlip(blip);
				}
			});

			return;
		}

		async void HandleBlipClick(long blipID)
		{


			IBlip blip;
			if(blips.TryGetValue(blipID, out blip))
			{
				await provider.UpdateBlip(blip);

				client.UpdateBlip(blip);

				await provider.PrimeCache();
			}


			return;
		}

		public async Task SpawnBlip(int posX, int posY)
		{
			if(client == null)
				throw new ArgumentNullException("Client reference is null");

			IBlip newBlip = await provider.AcquireBlip();
			newBlip.PosX = posX;
			newBlip.PosY = posY;

			blips[newBlip.ID] = newBlip;

			client.DrawBlip(newBlip);

			await provider.PrimeCache();

		}

#endregion
	}
}

