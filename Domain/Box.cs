using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using WpfApp.DataProvider.Repository;

namespace WpfApp.Domain
{
	/// <summary>
	/// Коробка с договорами
	/// </summary>
	public class Box : Entity
	{
		public new static BoxRepository Repository => new BoxRepository();

		/// <summary>
		/// Идентификатор склада
		/// </summary>
		public string StorageId { get; set; }

		/// <summary>
		/// Название
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Самая ранняя дата договора
		/// </summary>
		public DateTime MinDate { get; set; }

		/// <summary>
		/// Самая поздняя дата договора
		/// </summary>
		public DateTime MaxDate { get; set; }

		public BsonDocument Serialize()
		{
			return new BsonDocument
			{
				{"_id", Id},
				{"StorageId", StorageId},
				{"MinDate", MinDate},
				{"MaxDate", MaxDate},
				{"Description", Description}
			};
		}

		public static Box Reconstitute(BsonDocument bsonDocument)
		{
			return new Box
			{
				Id = bsonDocument["_id"].ToString(),
				StorageId = bsonDocument["StorageId"].AsString,
				MinDate = bsonDocument["MinDate"].ToUniversalTime(),
				MaxDate = bsonDocument["MaxDate"].ToUniversalTime(),
				Description = bsonDocument["Description"].AsString
			};
		}
	}
}