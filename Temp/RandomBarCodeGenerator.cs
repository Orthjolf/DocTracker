using System;
using System.Linq;
using WpfApp.Service;

namespace WpfApp.Debug
{
	public static class RandomBarCodeGenerator
	{
		private static readonly Random R = new Random();

		public static string GetRandomBarCode()
		{
			var id = GetRandomId();
			var contractNumber = GetRandomContractNumber();
			return BarCodeEncoder.BuildCode(id, contractNumber);
		}

		private static string GetRandomId()
		{
			const string chars = "0123456789";
			return new string(Enumerable.Repeat(chars, 8)
				.Select(s => s[R.Next(s.Length)]).ToArray());
		}

		private static string GetRandomContractNumber()
		{
			const string chars = "01234567890абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ.-/";
			return new string(Enumerable.Repeat(chars, 30)
				.Select(s => s[R.Next(s.Length)]).ToArray());
		}
	}
}