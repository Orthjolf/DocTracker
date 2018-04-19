using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using WpfApp.DataProvider.Repository;
using WpfApp.Domain;
using WpfApp.Enum;

namespace WpfApp.DataProvider.Repository
{
	public class DocumentRepository : IDocumentRepository
	{
		public Storage Get(string id, DocumentType type)
		{
			var document = DataContext.Instance.Database.GetCollection<BsonDocument>(type.ToString())
				.Find(id).ToBsonDocument();

			if (document == null)
			{
				throw new Exception($"Не найдено документа с Id = {id}");
			}

			return BsonSerializer.Deserialize<Storage>(document);
		}

		public IReadOnlyCollection<BsonDocument> GetAll(DocumentType type)
		{
			var collection = DataContext.Instance.Database.GetCollection<BsonDocument>(type.ToString());
			var emptyFilter = new BsonDocument();
			return collection.Find(emptyFilter).ToList().AsReadOnly();
		}

		public IReadOnlyCollection<BsonDocument> GetFiltered(BsonDocument filter, DocumentType type)
		{
			var collection = DataContext.Instance.Database.GetCollection<BsonDocument>(type.ToString());
			return collection.Find(filter).ToList().AsReadOnly();
		}
	}
}