using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using WpfApp.Domain;
using Console = WpfApp.Service.Console;

namespace WpfApp.DataProvider.WebApi
{
	static class WebApiHttpClient
	{
		private static readonly HttpClient Client = new HttpClient();

		static async Task<Storage> GetStoragesAsync()
		{
			try
			{
				var response = await Client.GetAsync("api/values");
				response.EnsureSuccessStatusCode();

				var json = await response.Content.ReadAsStringAsync();
				var storage = BsonSerializer.Deserialize<Storage>(json);
				return storage;
			}
			catch (Exception e)
			{
				System.Console.WriteLine(e);
				throw;
			}
		}

		public static void RunQuery()
		{
			RunAsync().GetAwaiter().GetResult();
		}

		static async Task RunAsync()
		{
			Client.BaseAddress = new Uri("http://localhost:52316/");
			Client.DefaultRequestHeaders.Accept.Clear();
			Client.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json"));
			try
			{
				var storage = await GetStoragesAsync();
				Console.Write(storage.ToString());
			}
			catch (Exception e)
			{
				Console.Write(e.Message);
			}
		}
	}
}