using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfApp.Debug;
using WpfApp.Enum;
using WpfApp.SubPages;
using Console = WpfApp.Service.Console;

namespace WpfApp.Scanning
{
	public static class BarCodeListener
	{
		/// <summary>
		/// Идентификатор открытой в данный момент коробки
		/// </summary>
		public static string CurrentBoxId { get; set; }

		public static ActionPerformed Action { get; set; }

		private const int AsciiCodeOf0 = 48;
		private const int AsciiCodeOf9 = 57;
		private const int AsciiCodeOfReturn = 13;

		/// <summary>
		/// Время, которое прошло между двумя вводами с клавиатуры
		/// </summary>
		private static long _deltaTime;

		/// <summary>
		/// Время, в которое была нажата предыдущая клавиша
		/// </summary>
		private static long _lastCharacterTime = 0;

		private static char _firstCharacter;

		private static StringBuilder _barCodeBuilder = new StringBuilder();

		/// <summary>
		/// Обработчик ввода символа с клавиатуры
		/// </summary>
		/// <param name="virtualKeyCode">Код введенного символа</param>
		public static void KeyCodeEntered(int virtualKeyCode)
		{
			if (IsCharacterScanned())
			{
				if (KeyIsNumber(virtualKeyCode))
				{
					var scannedCharacter = (char) virtualKeyCode;
					_barCodeBuilder.Append(scannedCharacter);
				}
				else if (virtualKeyCode == AsciiCodeOfReturn)
				{
					_lastCharacterTime = 0;
					var barCodeCharacters = new List<char>();
					barCodeCharacters.AddRange(_barCodeBuilder.ToString());
					var finalBarCode = _firstCharacter + new string(barCodeCharacters.Where((c, i) => i % 2 != 0).ToArray());

					Console.Write("Код просканирован: " + finalBarCode);
					
					//TODO: При реальном сканировании убрать рандомайзер
					finalBarCode = RandomBarCodeGenerator.GetRandomBarCode();

					BoxContent.Instance.PerformCommand();
					ScanningCommand.DoWork(CurrentBoxId, finalBarCode, Action);
				}
			}
			else
			{
				_firstCharacter = (char) virtualKeyCode;
				_barCodeBuilder = new StringBuilder();
			}
		}

		/// <summary>
		/// Просканирован ли символ, или введен вручную.
		/// Определяется путем вычисления интервала между введенными символами
		/// </summary>
		/// <returns>Просканирован ли символ</returns>
		private static bool IsCharacterScanned()
		{
			_deltaTime = DateTime.Now.Ticks - _lastCharacterTime;
			_lastCharacterTime = DateTime.Now.Ticks;
			return _deltaTime < 60000;
		}

		/// <summary>
		/// Является ли символ числом
		/// </summary>
		/// <param name="virtualKeyCode">Код символа в ASCII</param>
		/// <returns>Является ли символ числом</returns>
		private static bool KeyIsNumber(int virtualKeyCode)
		{
			return virtualKeyCode >= AsciiCodeOf0 && virtualKeyCode <= AsciiCodeOf9;
		}
	}
}