using System;
using System.Collections.Generic;
using Console = WpfApp.Service.Console;

namespace WpfApp.Tests
{
	public static class Test
	{
		/// <summary>
		/// Информация о выбранных тестах. Ключ - название теста. Значение - результат прохождения теста
		/// </summary>
		private static readonly Dictionary<string, bool> TestsInfo = new Dictionary<string, bool>();

		public static void RunAllTestSets()
		{
			RepositoryTestSet.Run();
			PrintTestsInfo();
		}

		/// <summary>
		/// Запускает тест
		/// </summary>
		/// <param name="test">Тест</param>
		public static void Run(Func<bool> test)
		{
			var testInfo = test.Method.DeclaringType + " " + test.Method.Name;
			if (TestsInfo.ContainsKey(testInfo))
			{
				testInfo += "1";
			}

			try
			{
				var result = test();
				TestsInfo.Add(testInfo, result);
			}
			catch (Exception e)
			{
				TestsInfo.Add(testInfo, false);
			}
		}

		/// <summary>
		/// Выводит в консоль информацию о прохождении тестов
		/// </summary>
		private static void PrintTestsInfo()
		{
			foreach (var testInfo in TestsInfo)
			{
				Console.Write("Тест " + testInfo.Key + " " + (testInfo.Value ? "Пройден" : "Провален"));
			}
		}
	}
}