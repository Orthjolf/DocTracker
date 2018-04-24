namespace WpfApp.Domain
{
	/// <summary>
	/// Договор
	/// </summary>
	public class Contract
	{
		/// <summary>
		/// Номер контракта
		/// </summary>
		public string Number { get; set; }

		/// <summary>
		/// Идентификатор коробки
		/// </summary>
		public int BoxId { get; set; }

		/// <summary>
		/// Имя клиента
		/// </summary>
		public string ClientFirstName { get; set; }

		/// <summary>
		/// Фамилия клиента
		/// </summary>
		public string ClientLastName { get; set; }

		/// <summary>
		/// Отчество клиента
		/// </summary>
		public string ClientPatronymic { get; set; }

		/// <summary>
		/// Номер телефона клиента
		/// </summary>
		public string PhoneNumber { get; set; }

		/// <summary>
		/// Идентификатор займа
		/// </summary>
		public string LoanId { get; set; }

		/// <summary>
		/// Префикс точки
		/// </summary>
		/// <returns></returns>
		public string PrefixOfPlace { get; set; }
	}
}