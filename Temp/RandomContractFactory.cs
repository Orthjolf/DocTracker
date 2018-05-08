using System;
using System.Linq;

namespace WpfApp.Service
{
	public class RandomContractFactory
	{
		private static Random r = new Random();

		static string GetRandomString()
		{
			const string chars = "0123456789";
			return new string(Enumerable.Repeat(chars, 14)
				.Select(s => s[r.Next(s.Length)]).ToArray());
		}

		static string GetRandomContractNumber()
		{
			var chars = "01234567890абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ.-/".ToCharArray();
			return new string(Enumerable.Repeat(chars, 30)
				.Select(s => s[r.Next(s.Length)]).ToArray());
		}
	}
}