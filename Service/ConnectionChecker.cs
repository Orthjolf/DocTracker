using System;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Threading;
using MahApps.Metro.Controls;
using WpfApp.DataProvider;
using WpfApp.Enum;
using WpfApp.SubPages;

namespace WpfApp.Service
{
	public class ConnectionChecker
	{
		/// <summary>
		/// Соединение с интернетом доступно
		/// </summary>
		public static bool ConnectionIsAvailable = CheckConnection();

		/// <summary>
		/// Соединение с интернетом не доступно
		/// </summary>
		public static bool ConnectionIsNotAvailable = !ConnectionIsAvailable;

		private ConnectionChecker()
		{
			ConnectionIsAvailable = CheckConnection();
			ConnectionIsNotAvailable = !ConnectionIsAvailable;
		}

		private static bool CheckConnection()
		{
			const string host = "google.com";
			const int timeout = 1000;
			var buffer = new byte[32];
			try
			{
				var reply = new Ping().Send(host, timeout, buffer, new PingOptions());
				if (reply == null) return false;
				return reply.Status == IPStatus.Success;
			}
			catch (Exception)
			{
				return false;
			}
		}

		/// <summary>
		/// Обработчик события подключения/отключения интернета
		/// </summary>
		public static void ConnectionChanged(object sender, NetworkAvailabilityEventArgs e)
		{
			DataBaseSwitcher.SetActiveDataBase(e.IsAvailable ? ConnectionType.Remote : ConnectionType.Local);
			Console.Write(e.IsAvailable ? "Network connected!" : "Network dis connected!");
			ConnectionIsAvailable = e.IsAvailable;
			ConnectionIsNotAvailable = e.IsAvailable;

			MainWindow.ToLoginScreen();

//			Dispatcher.CurrentDispatcher.Invoke(MainWindow.ToLoginScreen);
//			Application.Current.MainWindow?.Dispatcher.Invoke(MainWindow.ToLoginScreen);
		}


		/// <summary>
		/// Возвращает тип соединения с базой данных
		/// </summary>
		/// <returns>Тип соединения на данный момент. Локальный или удаленный</returns>
		public static ConnectionType GetConnectionType()
		{
			return ConnectionIsAvailable ? ConnectionType.Remote : ConnectionType.Local;
		}
	}
}