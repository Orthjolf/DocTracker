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
		public new static ContractRepository Repository => new ContractRepository();

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

		public static Contract Reconstitute(BsonDocument bsonDocument)
		{
			return new Contract
			{
				Id = bsonDocument["_id"].ToString(),
				Number = bsonDocument["Number"].AsString,
				BoxId = bsonDocument["BoxId"].AsString,
				ClientFirstName = bsonDocument["ClientFirstName"].AsString,
				ClientLastName = bsonDocument["ClientLastName"].AsString,
				ClientPatronymic = bsonDocument["ClientPatronymic"].AsString,
				PhoneNumber = bsonDocument["PhoneNumber"].AsString,
				LoanId = bsonDocument["LoanId"].AsString,
				PrefixOfPlace = bsonDocument["PrefixOfPlace"].AsString,
				ContractDate = bsonDocument["ContractDate"].ToUniversalTime()
			};
		}
	}
}