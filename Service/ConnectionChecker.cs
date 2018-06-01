using System;
using System.Net.NetworkInformation;
using WpfApp.Enum;

namespace WpfApp.Service
{
	public static class ConnectionChecker
	{
		/// <summary>
		/// Проверяет наличие соединения с интернетом пингуя гугл.
		/// </summary>
		/// <returns>Тип соединения на данный момент. Локальный или удаленный</returns>
		public static ConnectionType GetConnection()
		{
			const string host = "google.com";
			const int timeout = 1000;
			var buffer = new byte[32];

			try
			{
				var myPing = new Ping();
				var pingOptions = new PingOptions();
				var reply = myPing.Send(host, timeout, buffer, pingOptions);

				if (reply == null) return ConnectionType.Remote;
				return reply.Status == IPStatus.Success ? ConnectionType.Remote : ConnectionType.Local;
			}
			catch (Exception)
			{
				return ConnectionType.Local;
			}
		}
	}
}