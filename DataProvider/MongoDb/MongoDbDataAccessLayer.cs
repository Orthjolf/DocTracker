using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
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

		public MongoDbDataAccessLayer()
		{
			_type = typeof(T).ToString().Split('.').Last();
		}

		/// <summary>
		/// Получение коллекции Bson документов из базы
		/// </summary>
		/// <returns>Коллекция документов</returns>
		private IMongoCollection<BsonDocument> GetCollection()
		{
			return MongoDbDataContext.Instance.Database.GetCollection<BsonDocument>(_type);
		}

		public T Get(string id)
		{
			var filter = new BsonDocument("_id", id).ToJson();
			var bsonDocument = GetCollection().Find(filter).First();
			return bsonDocument.Reconstitute<T>();
		}

		public IReadOnlyCollection<T> GetAll()
		{
			var emptyFilter = new BsonDocument();
			var bsonDocuments = GetCollection().Find(emptyFilter).ToList();
			return bsonDocuments.Select(d => d.Reconstitute<T>()).ToList();
		}

		public IReadOnlyCollection<T> GetFiltered(string filter)
		{
			var bsonDocuments = GetCollection().Find(filter).ToList();
			return bsonDocuments.Select(d => d.Reconstitute<T>()).ToList();
		}

		public void Add(T entity)
		{
			GetCollection().InsertOneAsync(entity.ToBsonDocument());
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

		public override string ToString()
		{
			return "Mongo";
		}
	}
}