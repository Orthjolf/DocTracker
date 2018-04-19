using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using WpfApp.DataProvider.Repository;
using WpfApp.Domain;

namespace WpfApp.DataProvider
{
	public class StoragesRepository : IStorageRepository
	{
		public Storage Get(int id)
		{
			var document = DataContext.Instance.Database.GetCollection<BsonDocument>("Storage")
				.Find(id.ToString()).ToBsonDocument();

			if (document == null)
			{
				throw new Exception($"Не найдено хранилища с Id = {id}");
			}

			return BsonSerializer.Deserialize<Storage>(document);
		}

		public IReadOnlyCollection<BsonDocument> GetAll()
		{
			var collection = DataContext.Instance.Database.GetCollection<BsonDocument>("Storage");
			var emptyFilter = new BsonDocument();
			return collection.Find(emptyFilter).ToList().AsReadOnly();
		}
	}
}