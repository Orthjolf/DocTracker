using System.Windows;
using System.Windows.Controls;
using WpfApp.Domain;
using WpfApp.Enum;
using WpfApp.Extensions;

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
			AdministrationButton.Visibility = user.Role == UserRole.Admin ? Visibility.Visible : Visibility.Hidden;
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
	}
}