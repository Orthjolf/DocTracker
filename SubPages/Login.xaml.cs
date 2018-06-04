using System.Linq;
using System.Windows;
using MahApps.Metro.Controls.Dialogs;
using WpfApp.Domain;
using WpfApp.Service;

namespace WpfApp.SubPages
{
	public partial class Login
	{
		public Login()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Попытаться войти в программу под учетными данными
		/// </summary>
		private void TryToLogin(object sender, RoutedEventArgs e)
		{
			if (ConnectionChecker.ConnectionIsNotAvailable())
			{
				Info.Content = "Отсутствует соединение с интернетом";
				MainWindow.Instance.ShowMessageAsync("Ошибка входа", "Отсутствует соединение с интернетом");
				return;
			}

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
				{
					LoginInformation.Remember(user);
				}

				LoginInformation.SetCurrentUser(user);
				MainWindow.ToMainScreen();
			}
			else
			{
				Info.Content = "Неверный пароль";
				MainWindow.Instance.ShowMessageAsync("Ошибка входа", "Неверный пароль");
			}
		}
	}
}