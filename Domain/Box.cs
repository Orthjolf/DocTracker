using System;
using MongoDB.Bson;
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
		public DateTime? MinDate { get; set; }

		/// <summary>
		/// Самая поздняя дата договора
		/// </summary>
		public DateTime? MaxDate { get; set; }

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
			DateTime? minDate = bsonDocument["MinDate"].ToUniversalTime();
			DateTime? maxDate = bsonDocument["MaxDate"].ToUniversalTime();
			if (minDate == DateTime.MinValue) minDate = null;
			if (maxDate == DateTime.MinValue) maxDate = null;

			return new Box
			{
				Id = bsonDocument["_id"].ToString(),
				Name = bsonDocument["Name"].AsString,
				StorageId = bsonDocument["StorageId"].AsString,
				MinDate = minDate,
				MaxDate = maxDate,
				Description = bsonDocument["Description"].AsString
			};
		}
	}
}