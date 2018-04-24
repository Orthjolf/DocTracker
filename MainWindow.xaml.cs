using System.Reflection.Emit;
using System.Windows.Controls;
using WpfApp.SubPages;

namespace WpfApp
{
	public partial class MainWindow
	{
		private static MainWindow _instance;

		private static MainContent _mainContent;

		public MainWindow()
		{
			InitializeComponent();
			_instance = this;
			_mainContent = new MainContent();
			RootContent.Content = _mainContent;
		}

		public static void SetContent(UserControl content)
		{
			_instance.Content = content;
		}

		public static void SetDefault(string selectedStorageId)
		{
			_instance.Content = _mainContent;
			_mainContent.SelectItem(selectedStorageId);
		}
	}
}