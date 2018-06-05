using System.Windows.Controls;
using WpfApp.SubPages;

namespace WpfApp
{
	public partial class MainWindow
	{
		public static MainWindow Instance;

		public static MainContent MainContent;

		public MainWindow()
		{
			InitializeComponent();
			Instance = this;
			RootContent.Content = new Login();
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
			Instance.Content = MainContent;
			MainContent.SelectItem(selectedStorageId);
		}
	}
}