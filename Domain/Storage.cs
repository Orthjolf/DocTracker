using MongoDB.Bson.Serialization.Attributes;
using WpfApp.DataProvider;
using WpfApp.DataProvider.Repository;


namespace WpfApp.Domain
{
	/// <summary>
	/// Хранилище коробок
	/// </summary>
	public class Storage : Entity
	{
		[BsonIgnore]
//		public static StoragesRepository Repository => ObjectFactory.Instance.GetObject<StoragesRepository>();
		public static StoragesRepository Repository => new StoragesRepository();

		/// <summary>
		/// Название
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Адрес
		/// </summary>
		public string Address { get; set; }
	}
}