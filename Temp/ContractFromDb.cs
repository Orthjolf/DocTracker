using System;
using System.Linq;
using WpfApp.Domain;

namespace WpfApp.Temp
{
	public static class ContractFromDb
	{
		private static readonly Random R = new Random();

		public static Contract Get(string id, string contractNumber)
		{
			return new Contract
			{
				Id = id,
				Number = contractNumber,
				ClientFirstName = GetRandomString(),
				ClientLastName = GetRandomString(),
				ClientPatronymic = GetRandomString(),
				PhoneNumber = GetRandomString(),
				LoanId = id,
				PrefixOfPlace = GetRandomString(),
				ContractDate = DateTime.Now
			};
		}

		private static string GetRandomString()
		{
			const string chars = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
			return new string(Enumerable.Repeat(chars, 30)
				.Select(s => s[R.Next(s.Length)]).ToArray());
		}
	}
}