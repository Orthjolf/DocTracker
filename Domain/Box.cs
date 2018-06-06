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
		public new static Repository<Box> Repository => Repository<Box>.Instance;

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