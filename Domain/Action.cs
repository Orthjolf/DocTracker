using System;
using WpfApp.Enum;

namespace WpfApp.Domain
{
	/// <summary>
	/// Действие, произведенное с договором
	/// </summary>
	public class Action : Entity
	{
		/// <summary>
		/// Идентификатор контракта
		/// </summary>
		public int ContractId { get; set; }

		/// <summary>
		/// Дата и время
		/// </summary>
		public DateTime DateTime { get; set; }

		/// <summary>
		/// Действие
		/// </summary>
		public ActionPerformed ActionName { get; set; }

		/// <summary>
		/// Идентификатор пользователя, производившего действие
		/// </summary>
		public int UserId { get; set; }
	}
}