using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using WpfApp.Backend;
using WpfApp.DbMock;
using static WpfApp.Backend.UiConstants;

namespace WpfApp
{
	public partial class MainWindow
	{
		/// <summary>
		/// Главная панель
		/// </summary>
		private StackPanel MainPanel { get; set; }

		private StackPanel StoragePanel { get; set; }

		private StackPanel BoxPanel { get; set; }

		/// <summary>
		/// Текущая панель
		/// </summary>
		private StackPanel CurrentPanel { get; set; }

		public MainWindow()
		{
			InitializeComponent();
			InitializeRootPanel();
		}

		private void InitializeRootPanel()
		{
			SetRootPanelProperties();
			InitializeChildPanels();
			CurrentPanel = MainPanel;
			RootLayout.Children.Add(CurrentPanel);

			var a = new DataAccessLayer();
		}

		private void SetRootPanelProperties()
		{
			Application.Current.MainWindow = this;
			Application.Current.MainWindow.Height = MainWindowSize.Height;
			Application.Current.MainWindow.Width = MainWindowSize.Width;
		}

		private void InitializeChildPanels()
		{
			var uiBuilder = new UiBuilder();
			MainPanel = uiBuilder.BuildMainPanel();
		}

		private void RecognizeQrCode(object sender, RoutedEventArgs e)
		{
			Title = "Clicked";
		}
	}
}