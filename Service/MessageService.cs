using System.Windows;

namespace WpfApp.Service
{
	public static class MessageService
	{
		/// <summary>
		/// Сообщение пользователю
		/// </summary>
		/// <param name="message">текст сообщения</param>
		public static void ShowMessage(string message)
		{
			MessageBox.Show(message, "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
		}
	}
}