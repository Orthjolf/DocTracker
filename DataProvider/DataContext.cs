using System;
using MongoDB.Driver;

namespace WpfApp.DataProvider
{
	public class DataContext
	{
		private const string ConnectionString = "mongodb://localhost";
		private const string DbName = "DocTracker";

		private MongoClient Client { get; }

		public IMongoDatabase Database { get; }

		private static readonly Lazy<DataContext> Lazy =
			new Lazy<DataContext>(() => new DataContext());

		public static DataContext Instance => Lazy.Value;

		private DataContext()
		{
			Client = new MongoClient(ConnectionString);
			Database = Client.GetDatabase("DocTracker");
		}
	}
}