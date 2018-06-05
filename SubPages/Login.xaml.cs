using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls.Dialogs;
using WpfApp.DataProvider.Synchronizer;
using WpfApp.Domain;
using WpfApp.Service;

namespace WpfApp.SubPages
{
	public partial class Login : UserControl
	{
		private readonly ManualResetEvent _resetEvent = new ManualResetEvent(false);

		public Login()
		{
			InitializeComponent();

			var user = LoginInformation.GetLastUser();
			if (user == null) return;
			TryToLogin(user);
		}

		/// <summary>
		/// Попытаться войти в программу под учетными данными
		/// </summary>
		private void LoginButtonClick(object sender, RoutedEventArgs e)
		{
			//todo сделать нормальный репозиторий
			var user = User.Repository.GetAll().FirstOrDefault(u => u.Name == UserName.Text);
			if (user == null)
			{
				Info.Content = "Не найден пользователь с именем " + UserName.Text;
				MainWindow.Instance.ShowMessageAsync("Ошибка входа", "Не найден пользователь с именем " + UserName.Text);
				return;
			}

			var accessGranted = PasswordEncoder.VerifyHashedPassword(user.PasswordHash, Password.Password);
			if (accessGranted)
			{
				if (RememberMe.IsChecked != null && RememberMe.IsChecked.Value)
					LoginInformation.Remember(user);
				else
					LoginInformation.Forget();

				TryToLogin(user);
			}
			else
			{
				Info.Content = "Неверный пароль";
				MainWindow.Instance.ShowMessageAsync("Ошибка входа", "Неверный пароль");
			}
		}

		/// <summary>
		/// Попытаться войти в программу
		/// </summary>
		private void TryToLogin(User user)
		{
			if (ConnectionChecker.ConnectionIsNotAvailable())
			{
				var localDbIsActual = DbSynchronizer.IsLocalDbActual();
				if (localDbIsActual)
				{
					MainWindow.Instance.ShowMessageAsync("Автономный режим",
						"Отсутствует соединение с интернетом, доступена только работа в автономном режиме");
				}
				else
				{
					Info.Content = "Работа в автономном режиме не доступна, актуализируйте базу данных";
					MainWindow.Instance.ShowMessageAsync("Автономный режим",
						"Работа в автономном режиме не доступна, актуализируйте базу данных");
					return;
				}
			}

			Task.Factory.StartNew(SetLoadingScreen)
				.ContinueWith(result => InitializeMainContent(user));
		}

		/// <summary>
		/// Установка экрана загрузки
		/// </summary>
		private void SetLoadingScreen()
		{
			Dispatcher.Invoke(() => { MainWindow.SetContent(new Loading()); });
		}

		/// <summary>
		/// Инициализация главной панели с контентом
		/// </summary>
		/// <param name="user"></param>
		private void InitializeMainContent(User user)
		{
			Dispatcher.Invoke(() =>
			{
				MainWindow.MainContent = new MainContent(user);
				MainWindow.ToMainScreen();
			});
		}
	}
}