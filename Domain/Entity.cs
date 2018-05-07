using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using WpfApp.DataProvider.Repository;

namespace WpfApp.Domain
{
	public abstract class Entity
	{
		/// <summary>
		/// Идентификатор
		/// </summary>
		[BsonId]
		public string Id { get; set; }

		/// <summary>
		/// Описание
		/// </summary>
		public string Description { get; set; }
	}
}