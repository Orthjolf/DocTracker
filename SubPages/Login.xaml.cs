﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls.Dialogs;
using WpfApp.DataProvider.Repository;
using WpfApp.DataProvider.Synchronizer;
using WpfApp.Domain;
using WpfApp.Extensions;
using WpfApp.Service;

namespace WpfApp.SubPages
{
	public partial class Login
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
			var user = User.Repository.GetByName(UserName.Text);
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
			if (ConnectionChecker.ConnectionIsNotAvailable)
			{
				if (DbSynchronizer.LocalDbIsActual())
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
			_resetEvent.Set();
		}

		/// <summary>
		/// Инициализация главной панели с контентом
		/// </summary>
		/// <param name="user">Текущий пользователь</param>
		private void InitializeMainContent(User user)
		{
			_resetEvent.WaitOne();
			Dispatcher.Invoke(() =>
			{
				MainWindow.MainContent = new MainContent(user);
				MainWindow.ToMainScreen();
			});
		}
	}
}