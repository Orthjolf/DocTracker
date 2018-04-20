using System;
using MongoDB.Bson;

namespace WpfApp.Domain
{
	/// <summary>
	/// Коробка с договорами
	/// </summary>
	public class Box : Entity
	{
		/// <summary>
		/// Идентификатор склада
		/// </summary>
		public string StorageId { get; set; }

		/// <summary>
		/// Самая ранняя дата договора
		/// </summary>
		public DateTime MinDate { get; set; }

		/// <summary>
		/// Самая поздняя дата договора
		/// </summary>
		public DateTime MaxDate { get; set; }

		public static Box Reconstitute(BsonDocument document)
		{
			return new Box
			{
				Id = document["_id"].ToString(),
				StorageId = document["StorageId"].AsString,
				MinDate = document["MinDate"].ToUniversalTime(),
				MaxDate = document["MaxDate"].ToUniversalTime(),
				Description = document["Description"].AsString
			};
		}
	}
}