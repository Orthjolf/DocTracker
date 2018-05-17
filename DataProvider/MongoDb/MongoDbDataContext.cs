using System;
using MongoDB.Driver;

namespace WpfApp.DataProvider.MongoDb
{
	/// <summary>
	/// Контекст для доступа к базе MongoDb
	/// </summary>
	public class MongoDbDataContext
	{
		private const string ConnectionString = "mongodb://localhost";
		private const string DbName = "DocTracker";

		private MongoClient Client { get; }

		/// <summary>
		/// База данных
		/// </summary>
		public IMongoDatabase Database { get; }

		/// <summary>
		/// Экземпляр контекста
		/// </summary>
		public static MongoDbDataContext Instance =>
			new Lazy<MongoDbDataContext>(() => new MongoDbDataContext()).Value;

		private MongoDbDataContext()
		{
			Client = new MongoClient(ConnectionString);
			Database = Client.GetDatabase(DbName);
		}
	}
}