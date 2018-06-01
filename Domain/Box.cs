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
		public static Repository<Box> Repository =>
			new Lazy<Repository<Box>>(() => new Repository<Box>()).Value;

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
	}
}