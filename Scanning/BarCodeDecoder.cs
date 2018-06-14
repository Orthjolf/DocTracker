using System.Collections.Generic;
using System.Linq;

namespace WpfApp.Scanning
{
	public static class BarCodeDecoder
	{
		private static char[] SourceCharacters =>
			"0123456789абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ.-/".ToCharArray();

		private const int LengthOfId = 8;

		/// <summary>
		/// Восстанавливает ID и номер договора из зашифрованного кода
		/// </summary>
		/// <param name="code">Код</param>
		/// <returns>Пара ID/Номер договора</returns>
		public static KeyValuePair<string, string> Reconstitute(string code)
		{
			var id = GetId(code);
			var number = GetContractNumber(code);
			return new KeyValuePair<string, string>(id, DecodeContractNumber(number));
		}

		/// <summary>
		/// Расшифровывает номер договора
		/// </summary>
		/// <param name="str">Зашифрованный номер договора</param>
		/// <returns>Расшифрованный номер договора</returns>
		private static string DecodeContractNumber(string str)
		{
			var indexes = Enumerable.Range(0, str.Length / 2).Select(i =>
				int.Parse(str.Substring(i * 2, 2)));

			var characters = indexes.Select(i => (SourceCharacters[i]));
			return string.Join("", characters);
		}

		/// <summary>
		/// Вытаскивает ID договора из зашифрованного кода
		/// </summary>
		/// <param name="code">Код</param>
		/// <returns>ID договора</returns>
		private static string GetId(string code)
		{
			var id = code.Substring(0, LengthOfId);
			var numericCode = long.Parse(id);
			return numericCode.ToString();
		}

		/// <summary>
		/// Вытаскивает номер договора из зашифрованного кода
		/// </summary>
		/// <param name="code">Код</param>
		/// <returns>Номер договора</returns>
		private static string GetContractNumber(string code)
		{
			return code.Substring(LengthOfId, code.Length - LengthOfId);
		}
	}
}