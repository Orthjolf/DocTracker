using System.Windows;
using System.Windows.Controls;
using WpfApp.Domain;

namespace WpfApp.SubPages
{
	public partial class BoxContent : UserControl
	{
		private Box _box;

		public BoxContent(Box box)
		{
			InitializeComponent();
			_box = box;
		}

		private void BackButton_OnClick(object sender, RoutedEventArgs e)
		{
			MainWindow.SetDefault(_box.StorageId);
		}
	}
}