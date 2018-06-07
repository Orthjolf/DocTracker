using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Controls;
using WpfApp.DataProvider;
using WpfApp.Domain;
using WpfApp.Enum;
using WpfApp.Service;
using WpfApp.SubPages;
using WpfApp.Temp;

namespace WpfApp
{
	public partial class MainWindow
	{
		public static MainWindow Instance;

		public static MainContent MainContent;

		public MainWindow()
		{
			InitializeComponent();

			localDbSeed.InitRepositories();
			localDbSeed.Generate();

			NetworkChange.NetworkAvailabilityChanged += ConnectionChanged;

			Instance = this;
			RootContent.Content = new Login();
//			RootContent.Content = new TestDb();
		}

		private void ConnectionChanged(object sender, NetworkAvailabilityEventArgs e)
		{
			DataBaseSwitcher.SetActiveDataBase(e.IsAvailable ? ConnectionType.Remote : ConnectionType.Local);
			Dispatcher.Invoke(() => SetContent(new Login()));
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
			Instance.Content = MainContent;
			MainContent.SelectItem(selectedStorageId);
		}

		/// <summary>
		/// Перейти на экран логина
		/// </summary>
		public static void ToLoginScreen()
		{
			SetContent(new Login());
		}
	}
}