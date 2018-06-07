using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using WpfApp.DataProvider.Repository;
using WpfApp.Domain;

namespace WpfApp.DataProvider.MongoDb
{
	/// <summary>
	/// Слой доступа к данным в локальной базе MongoDb.
	/// Предоставляет методы получения элементов из базы типа T
	/// </summary>
	/// <typeparam name="T">Тип возвращаемых значений</typeparam>
	public class MongoDbDataAccessLayer<T> : IDocumentRepository<T> where T : Entity
	{
		private readonly string _type;

		private readonly IMongoDatabase _db;

		public MongoDbDataAccessLayer()
		{
			_type = typeof(T).ToString().Split('.').Last();
			_db = MongoDbDataContext.Instance.Database;
		}

		/// <summary>
		/// Получение коллекции Bson документов из базы
		/// </summary>
		/// <returns>Коллекция документов</returns>
		private IMongoCollection<BsonDocument> GetCollection()
		{
			return _db.GetCollection<BsonDocument>(_type);
		}

		public T Get(string id)
		{
			var filter = new BsonDocument("_id", id).ToJson();
			var document = GetCollection().Find(filter).First();
			return BsonSerializer.Deserialize<T>(document);
		}

		public IReadOnlyCollection<T> GetAll()
		{
			var emptyFilter = new BsonDocument();
			var bsonDocuments = GetCollection().Find(emptyFilter).ToList();
			return bsonDocuments.Select(d => BsonSerializer.Deserialize<T>(d)).ToList();
		}

		public IReadOnlyCollection<T> GetFiltered(string filter)
		{
			var bsonDocuments = GetCollection().Find(filter).ToList();
			return bsonDocuments.Select(d => BsonSerializer.Deserialize<T>(d)).ToList();
		}

		public void Add(T entity)
		{
			GetCollection().InsertOneAsync(entity.ToBsonDocument());
		}

		public void AddAll(List<T> entities)
		{
			var documents = entities.Select(e => e.ToBsonDocument());
			GetCollection().InsertManyAsync(documents);
		}

		public void Update(T entity)
		{
			GetCollection().ReplaceOneAsync(
				new BsonDocument("_id", entity.Id),
				entity.ToBsonDocument()
			);
		}

		public void Delete(T entity)
		{
			GetCollection().DeleteOneAsync(d => d["_id"] == entity.Id);
		}

		public void DeleteById(string id)
		{
			GetCollection().DeleteOneAsync(d => d["_id"] == id);
		}

		public void DeleteAll()
		{
			var emptyFilter = new BsonDocument();
			GetCollection().DeleteMany(emptyFilter);
		}

		public override string ToString()
		{
			return "Mongo";
		}
	}
}