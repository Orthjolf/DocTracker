using System;
using System.Runtime.InteropServices;

namespace WpfApp.Service
{
	public static class Console
	{
		public static void Write(string message)
		{
			AttachConsole(-1);
			System.Console.WriteLine(message);
		}

		[DllImport("Kernel32.dll")]
		private static extern bool AttachConsole(int processId);
	}
}