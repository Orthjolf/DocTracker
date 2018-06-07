using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls.Dialogs;
using WpfApp.DataProvider.Synchronizer;
using WpfApp.Domain;
using WpfApp.Enum;
using WpfApp.Extensions;
using WpfApp.Service;

namespace WpfApp.SubPages
{
	public partial class NavBar : UserControl
	{
		public static NavBar Instance;

		public NavBar(User user)
		{
			InitializeComponent();
			Instance = this;
			if (user == null) return;
			if (user.Role == UserRole.Admin)
			{
				AdministrationButton.Visibility = Visibility.Visible;
			}

			//todo сделать через наблюдателя
			AdministrationButton.IsEnabled = ConnectionChecker.ConnectionIsAvailable;
			UpdateButton.IsEnabled = true;
			UserName.Content = user.Role.GetDescription() + " " + user.Name;
		}

		/// <summary>
		/// Разлогиниться
		/// </summary>
		private void Logout(object sender, RoutedEventArgs e)
		{
			LoginInformation.Forget();
			MainWindow.SetContent(new Login());
		}

		private void Administrate(object sender, RoutedEventArgs e)
		{
			MainWindow.SetContent(new Administraion());
		}

		private void UpdateLocalDb(object sender, RoutedEventArgs e)
		{
			if (DbSynchronizer.LocalDbIsActual())
			{
				MainWindow.Instance.ShowMessageAsync("", "Локальная база актуальна,обновление не требуется");
				return;
			}

			Console.Write("Обновление");
			DbSynchronizer.UpdateLocalDb();
		}
	}
}