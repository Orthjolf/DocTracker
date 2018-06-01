using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Domain;
using WpfApp.Enum;
using WpfApp.Service;

namespace WpfApp.SubPages
{
	public partial class Login : UserControl
	{
		public Login()
		{
			InitializeComponent();
		}

		private void Validate(object sender, TextChangedEventArgs e)
		{
		}

		private void ValidatePassword(object sender, TextChangedEventArgs e)
		{
		}

		private void PasswordChangedHandler(object sender, RoutedEventArgs e)
		{
		}

		private void LoginButtonClick(object sender, RoutedEventArgs e)
		{
			if (!ConnectionChecker.ConnectionIsAvailable())
			{
				Info.Content = "Отсутствует соединение с интернетом";
				return;
			}

			var user = User.Repository.GetAll().FirstOrDefault(u => u.Name == UserName.Text);
			if (user == null)
			{
				Info.Content = "Не найден пользователь с именем " + UserName.Text;
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
				MainWindow.SetContent(new MainContent());
			}
			else
			{
				Info.Content = "Неверный пароль";
			}
		}
	}
}