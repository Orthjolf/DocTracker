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
		/// <summary>
		/// Название
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Адрес
		/// </summary>ам
		public string Address { get; set; }
	}
}