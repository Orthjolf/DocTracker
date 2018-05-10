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
		public static BoxRepository Repository =>
			new Lazy<BoxRepository>(() => new BoxRepository()).Value;

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

		/// <summary>
		/// Количество договоров в коробке
		/// </summary>
		public int ContractsCount { get; set; }

		public BsonDocument Serialize()
		{
			var minDate = MinDate ?? DateTime.MinValue;
			var maxDate = MaxDate ?? DateTime.MinValue;
			return new BsonDocument
			{
				{"StorageId", StorageId},
				{"Name", Name},
				{"MinDate", minDate},
				{"MaxDate", maxDate},
				{"Description", Description},
				{"ContractsCount", ContractsCount}
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
				Description = bsonDocument["Description"].AsString,
				ContractsCount = bsonDocument["ContractsCount"].AsInt32
			};
		}
	}
}