using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls.Dialogs;
using WpfApp.DataProvider.Synchronizer;
using WpfApp.Domain;
using WpfApp.Enum;
using WpfApp.Extensions;
using WpfApp.Service;

namespace WpfApp.SubPages
{
	public partial class NavBar
	{
		private readonly ManualResetEvent _resetEvent = new ManualResetEvent(false);

		public NavBar(User user)
		{
			InitializeComponent();
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
				MainWindow.Instance.ShowMessageAsync("Обновление не требуется",
					"Локальная база актуальна, обновление не требуется");
				return;
			}

			MainWindow.SetContent(new Loading("Обновление локальной базы данных"));
			Console.Write("Обновление");

			Task.Factory.StartNew(() =>
				{
					DbSynchronizer.UpdateLocalDb();
					_resetEvent.Set();
				})
				.ContinueWith(result => { Dispatcher.Invoke(() => { MainWindow.ToMainScreen(); }); });
		}
	}
}