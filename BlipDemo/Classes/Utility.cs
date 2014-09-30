//
//  Utility.cs
//
//  Author:
//       pnovodon <>
//
//  Copyright (c) 2014 Switch Media
//
//
using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Net.Cache;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.Diagnostics;

namespace BlipLib
{
	public class Utility
	{


		static long requestNumber = 0;

		public static Task<Stream> HTTPLoadAsync(string url, int retryCount = 3)
		{
			Task<Stream> t = new Task<Stream>(() => {
				return HTTPLoad(url, retryCount);
			});
			t.Start();
			return t;
		}

		public static Stream HTTPLoad(string url, int retryCount = 3)
		{


			Stream outputStream = null;
		

			try
			{
				//Create the web request
				HttpWebRequest request = (HttpWebRequest) HttpWebRequest.Create(url);

				//HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
				//request.CachePolicy = policy;

				request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
				request.Method = "GET";
				request.Timeout = 10000;
				request.Proxy = null;
				request.ServicePoint.Expect100Continue = false;


				Stopwatch stopWatch = Stopwatch.StartNew();


				//Get the stream from the web
				WebResponse response = request.GetResponse();
				Stream returnStream = response.GetResponseStream();


				stopWatch.Stop();

				Console.WriteLine("Data loaded for request {0} in {1} ms",
				                  requestNumber++,
				                  stopWatch.ElapsedMilliseconds);


				//Make the returned stream into a memory stream
				outputStream = ConvertStreamToMemoryStream(returnStream);
				returnStream.Close();
				returnStream.Dispose();
				response.Close();
				response.Dispose();

			}
			catch(System.Net.WebException e)
			{
				Console.WriteLine("\tHTTP failed to load with error {0}. Retry remaining: {1}",
				                  e.Message,
				                  retryCount);
				if(retryCount > 0)
				{
					return HTTPLoad(url, --retryCount);
				}

				return null;
			}
			catch(Exception)
			{
				return null;
			}

			return outputStream;
		}

		private static MemoryStream ConvertStreamToMemoryStream(Stream stream)
		{
			MemoryStream memStream = new MemoryStream();
			byte[] buffer = new byte[2048];
			int read;
			if(stream != null)
			{
				while((read = stream.Read(buffer, 0, buffer.Length)) > 0)
				{
					memStream.Write(buffer, 0, read);
				}
				memStream.Flush();
			}
			memStream.Position = 0;
			return memStream;
		}



	}
}

