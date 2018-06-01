using System;
using MongoDB.Bson;
using WpfApp.DataProvider.Repository;

namespace WpfApp.Domain
{
	/// <summary>
	/// Договор
	/// </summary>
	public class Contract : Entity
	{
		public static Repository<Contract> Repository =>
			new Lazy<Repository<Contract>>(() => new Repository<Contract>()).Value;

		/// <summary>
		/// Номер контракта
		/// </summary>
		public string Number { get; set; }

		/// <summary>
		/// Идентификатор коробки
		/// </summary>
		public string BoxId { get; set; }

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
		/// Фамилия имя отчество
		/// </summary>
		public string ClientFullName => $"{ClientFirstName} {ClientPatronymic} {ClientLastName}";

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

		/// <summary>
		/// Дата выдачи займа
		/// </summary>
		public DateTime ContractDate { get; set; }

		public override string ToString()
		{
			return Number + " " + LoanId;
		}
	}
}