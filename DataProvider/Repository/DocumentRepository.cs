using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using WpfApp.Domain;
using WpfApp.Enum;

namespace WpfApp.DataProvider.Repository
{
	public class DocumentRepository : IDocumentRepository
	{
		protected static IMongoCollection<BsonDocument> GetCollection(DocumentType type)
		{
			return DataContext.Instance.Database.GetCollection<BsonDocument>(type.ToString());
		}

		public BsonDocument Get(string id, DocumentType type)
		{
			var filter = new BsonDocument
			{
				{"_id", new ObjectId(id)},
			};

			return GetFiltered(filter, type).FirstOrDefault();
		}

		public IReadOnlyCollection<BsonDocument> GetAll(DocumentType type)
		{
			var emptyFilter = new BsonDocument();
			return GetCollection(type).Find(emptyFilter).ToList().AsReadOnly();
		}

		public IReadOnlyCollection<BsonDocument> GetFiltered(BsonDocument filter, DocumentType type)
		{
			var collection = DataContext.Instance.Database.GetCollection<BsonDocument>(type.ToString());
			return collection.Find(filter).ToList().AsReadOnly();
		}

		protected void AddAndSave(BsonDocument document, DocumentType type)
		{
			GetCollection(type).InsertOneAsync(document);
		}

		protected void DeleteById(string id, DocumentType type)
		{
			GetCollection(type).DeleteOneAsync(d => d["_id"] == new ObjectId(id));
		}
	}
}