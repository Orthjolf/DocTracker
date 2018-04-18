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
		private int BoxId { get; set; }
	}
}