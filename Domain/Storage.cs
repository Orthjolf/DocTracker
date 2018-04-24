using System.Windows.Controls;
using MongoDB.Bson;
using WpfApp.DataProvider.Repository;

namespace WpfApp.Domain
{
	/// <summary>
	/// Хранилище коробок
	/// </summary>
	public class Storage : Entity
	{
		public new static StorageRepository Repository => new StorageRepository();

		/// <summary>
		/// Название
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Адрес
		/// </summary>ам
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