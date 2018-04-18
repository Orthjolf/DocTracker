using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using MongoDB.Driver;
using WpfApp.DbMock;

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
		}
//
//		static async Task MainAsync()
//		{
//			var connectionString = "mongodb://localhost:27017";
//
//			var client = new MongoClient(connectionString);
//		}
//
//		private void InitializeChildPanels()
//		{
//			MainPanel = BuildMainPanel();
//		}
//
//		private void RecognizeQrCode(object sender, RoutedEventArgs e)
//		{
//			Title = "Clicked";
//		}
//
//		public StackPanel BuildMainPanel()
//		{
//			var label = new Label
//			{
//				Content = "Хранилища",
//				Width = UiConstants.SideBarWidth,
//				HorizontalAlignment = HorizontalAlignment.Left,
//				FontSize = 28,
//				Background = new SolidColorBrush(UiConstants.UiPrimaryColor)
//			};
//
//
//			var dataGrid = new DataGrid
//			{
//				Name = "Storages",
//				HorizontalAlignment = HorizontalAlignment.Stretch,
//				BorderBrush = new SolidColorBrush(Colors.LightGray),
//				BorderThickness = new Thickness(1)
//			};
//			dataGrid.Columns.Add(new DataGridTextColumn {Header = "Id", Binding = new Binding("Id")});
//			dataGrid.Columns.Add(new DataGridTextColumn {Header = "Название", Binding = new Binding("Name")});
//			dataGrid.Columns.Add(new DataGridTextColumn {Header = "Адрес", Binding = new Binding("Address")});
//			dataGrid.Columns.Add(new DataGridTextColumn {Header = "Описание", Binding = new Binding("Описание")});
//
//			var rowStyle = new Style(typeof(DataGridRow));
//			rowStyle.Setters.Add(new EventSetter(Control.MouseDoubleClickEvent,
//				new MouseButtonEventHandler(Row_DoubleClick)));
//			dataGrid.RowStyle = rowStyle;
//			foreach (var storage in StoragesRepository.Instance().StoragesList)
//			{
//				dataGrid.Items.Add(new Storage
//				{
//					Id = storage.Id,
//					Name = storage.Name,
//					Address = storage.Address,
//					Description = storage.Description
//				});
//			}
//
//			var addButton = new Button
//			{
//				Content = "Добавить",
//				Width = UiConstants.SideBarWidth / 2
//			};
//
//			var removeButton = new Button
//			{
//				Content = "Удалить",
//				Width = UiConstants.SideBarWidth / 2
//			};
//
//			return new StackPanel
//			{
//				Name = "MainLayout",
//				Children =
//				{
//					label,
//					dataGrid,
//					addButton,
//					removeButton
//				}
//			};
//		}
//
//		private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
//		{
//			var row = sender as DataGridRow;
//			var storagePanel = BuildStoragePanel(50);
//		}
//
//		private StackPanel BuildStoragePanel(int id)
//		{
//			return new StackPanel();
//		}
	}
}