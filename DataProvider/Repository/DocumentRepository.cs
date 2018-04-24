using System;
using System.Collections.Generic;
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
		private static IMongoCollection<BsonDocument> GetCollection(DocumentType type)
		{
			return DataContext.Instance.Database.GetCollection<BsonDocument>(type.ToString());
		}

		public BsonDocument Get(string id, DocumentType type)
		{
			var document = GetCollection(type).Find(id).ToBsonDocument();

			if (document == null)
			{
				throw new Exception($"Не найдено документа с Id = {id}");
			}

			return document;
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

		protected static async Task AddAndSave(BsonDocument document, DocumentType type)
		{
			await GetCollection(type).InsertOneAsync(document);
		}

		protected static async Task DeleteById(string id, DocumentType type)
		{
			await GetCollection(type).DeleteOneAsync(d => d["_id"] == new ObjectId(id));
		}
	}
}