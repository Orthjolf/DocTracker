using System;
using System.Linq;
using System.Windows.Controls;
using MahApps.Metro.Controls.Dialogs;
using WpfApp.Domain;
using WpfApp.SubPages;
using WpfApp.Temp;

namespace WpfApp
{
	public partial class MainWindow
	{
		public static MainWindow Instance;

		private static MainContent _mainContent;

		public MainWindow()
		{
			InitializeComponent();
			Instance = this;

			var user = LoginInformation.GetLastUser();
			if (user == null)
			{
				RootContent.Content = new Login();
			}
			else
			{
				_mainContent = new MainContent();
				RootContent.Content = _mainContent;
			}

//			RootContent.Content = new TestDb();
		}

		/// <summary>
		/// Устанавливает содержимое главного окна
		/// </summary>
		/// <param name="content">Содержимое, которе будет отображаться</param>
		public static void SetContent(UserControl content)
		{
			Instance.Content = content;
		}

		/// <summary>
		/// Возвращает содержимое окна к главному экрану с выбранным хранилищем
		/// </summary>
		/// <param name="selectedStorageId">Id ранее выбранного хранилища</param>
		public static void ToMainScreen(string selectedStorageId = "")
		{
			Instance.Content = _mainContent;
			_mainContent.SelectItem(selectedStorageId);
		}
	}
}