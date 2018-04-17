namespace WpfApp.Backend
{
	/// <summary>
	/// Хранилище коробок с договорами займа
	/// </summary>
	public class Storage : Entity
	{
		/// <summary>
		/// Название
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Адрес
		/// </summary>
		public string Address { get; set; }

		/// <summary>
		/// Описание
		/// </summary>
		public string Description { get; set; }
	}
}