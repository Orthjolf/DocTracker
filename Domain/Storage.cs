using System;
using MongoDB.Bson;
using WpfApp.DataProvider.Repository;

namespace WpfApp.Domain
{
	/// <summary>
	/// Хранилище коробок
	/// </summary>
	public class Storage : Entity
	{
		public static Repository<Storage> Repository =>
			new Lazy<Repository<Storage>>(() => new Repository<Storage>()).Value;

		/// <summary>
		/// Название
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Адрес
		/// </summary>
		public string Address { get; set; }

		public static Storage Reconstitute(BsonDocument storage)
		{
			return new Storage
			{
				Id = storage["_id"].ToString(),
				Name = storage["Name"].ToString(),
				Address = storage["Address"].ToString(),
				Description = storage["Description"].ToString()
			};
		}
	}
}