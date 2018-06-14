using System.Runtime.InteropServices;

namespace WpfApp.Service
{
	/// <summary>
	/// По какой-то причине, в приложении WPF у меня не работал вывод в консоль.
	/// Проблема решилась написанием вот такого костыля
	/// </summary>
	public static class Console
	{
		public static void Writeln(object obj)
		{
			AttachConsole(-1);
			System.Console.WriteLine(obj.ToString());
		}

		public static void Write(string message)
		{
			AttachConsole(-1);
			System.Console.WriteLine(message);
		}

		[DllImport("Kernel32.dll")]
		private static extern bool AttachConsole(int processId);
	}
}