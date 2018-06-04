using System;
using System.Net.NetworkInformation;
using WpfApp.Enum;

namespace WpfApp.Service
{
	public static class ConnectionChecker
	{
		/// <summary>
		/// Возвращает тип соединения с базой данных
		/// </summary>
		/// <returns>Тип соединения на данный момент. Локальный или удаленный</returns>
		public static ConnectionType GetConnectionType()
		{
			return ConnectionIsAvailable() ? ConnectionType.Remote : ConnectionType.Local;
		}

		/// <summary>
		/// Проверяет не доступно ли подключение к интернету
		/// </summary>
		/// <returns>Не доступно ли подключение к интернету</returns>
		public static bool ConnectionIsNotAvailable()
		{
			return !ConnectionIsAvailable();
		}

		/// <summary>
		/// Проверяет доступно ли подключение к интернету
		/// </summary>
		/// <returns>Доступно ли подключение к интернету</returns>
		public static bool ConnectionIsAvailable()
		{
			const string host = "google.com";
			const int timeout = 1000;
			var buffer = new byte[32];

			try
			{
				var myPing = new Ping();
				var pingOptions = new PingOptions();
				var reply = myPing.Send(host, timeout, buffer, pingOptions);

				if (reply == null) return false;
				return reply.Status == IPStatus.Success;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}