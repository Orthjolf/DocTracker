using System;
using System.Configuration;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace WpfApp.DbMock
{
	public class DataAccessLayer
	{
		public DataAccessLayer()
		{
			string connectionString = "mongodb://localhost:27017";
			MongoClient client = new MongoClient(connectionString);
			IMongoDatabase database = client.GetDatabase("test");

			GetDatabaseNames(client).GetAwaiter();
			Console.ReadLine();
		}
		
		private static async Task GetDatabaseNames(MongoClient client)
		{
			using (var cursor = await client.ListDatabasesAsync())
			{
				var databaseDocuments = await cursor.ToListAsync();
				foreach (var databaseDocument in databaseDocuments)
				{
					Console.WriteLine(databaseDocument["name"]);
				}
			}
		}
	}
}