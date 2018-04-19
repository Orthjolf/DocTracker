using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace WpfApp.DataProvider
{
	public class DataContext
	{
		public const string ConnectionString = "mongodb://localhost";

		public MongoClient Client { get; }

		public IMongoDatabase Database { get; }

		private static readonly Lazy<DataContext> Lazy =
			new Lazy<DataContext>(() => new DataContext());

		public static DataContext Instance => Lazy.Value;

		private DataContext()
		{
			Client = new MongoClient(ConnectionString);
			Database = Client.GetDatabase("DocTracker");
		}

		public async Task<string> GetCollectionsNames(MongoClient client)
		{
			var dbNames = new StringBuilder();
			using (var cursor = await client.ListDatabasesAsync())
			{
				var dbs = await cursor.ToListAsync();
				foreach (var db in dbs)
				{
					dbNames.Append("В базе данных {0} имеются следующие коллекции:" + db["name"]);
					var database = client.GetDatabase(db["name"].ToString());

					using (var collCursor = await database.ListCollectionsAsync())
					{
						var colls = await collCursor.ToListAsync();
						foreach (var col in colls)
						{
							dbNames.Append(col["name"] + "\n");
						}
					}
				}
			}

			return dbNames.ToString();
		}
	}
}