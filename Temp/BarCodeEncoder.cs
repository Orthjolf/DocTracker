using System;
using System.Linq;
using System.Text;

namespace WpfApp.Temp
{
	public static class BarCodeEncoder
	{
		private static char[] SourceCharacters =>
			"01234567890абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ.-/".ToCharArray();

		private const int LengthOfId = 8;

		/// <summary>
		/// Формирует числовой код на основе строки с цифрами, буквами и спец. символами
		/// </summary>
		/// <param name="str">Строка</param>
		/// <returns>Числовой код</returns>
		private static string EncodeStringWithCharacters(string str)
		{
			if (str.Contains(' '))
			{
				str = new string(str.ToCharArray().Where(c => c != ' ').ToArray());
			}

			var sb = new StringBuilder();

			foreach (var c in str)
			{
				sb.Append(EncryptCharacter(c));
			}

			return sb.ToString();
		}

		/// <summary>
		/// Формирует числовой код на основе строки, содержащий только цифры
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		private static string EncodeNumericString(string str)
		{
			if (str.Length > 8)
			{
				throw new Exception("Длина ID не может быть больше 0");
			}

			var strWithNulls = str;
			for (var i = 0; i < LengthOfId - str.Length; i++)
			{
				strWithNulls = "0" + strWithNulls;
			}

			return strWithNulls;
		}

		/// <summary>
		/// Формирует числовой код символа
		/// </summary>
		/// <param name="c">Символ</param>
		/// <returns>Числовой код</returns>
		private static string EncryptCharacter(char c)
		{
			var index = Array.IndexOf(SourceCharacters, c);
			return (index < 10 ? "0" : "") + index;
		}

		/// <summary>
		/// Кодирует ID и номер договора в числовой код
		/// </summary>
		/// <param name="id">Id договора меньше 8 символов</param>
		/// <param name="contractNumber">Номер договора</param>
		/// <returns>Числовой код</returns>
		public static string BuildCode(string id, string contractNumber)
		{
			var encodedId = EncodeNumericString(id);
			var encodedContractNumber = EncodeStringWithCharacters(contractNumber);
			return encodedId + encodedContractNumber;
		}
	}
}