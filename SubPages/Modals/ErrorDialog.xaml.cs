using System.Windows;
using System.Windows.Controls;

namespace WpfApp.SubPages.Modals
{
	public partial class ErrorDialog : Window
	{
		public ErrorDialog()
		{
			InitializeComponent();
		}

		public static void Show(string message)
		{
			var dialog = new ErrorDialog {ErrorText = {Text = message}};
			dialog.Show();
		}
	}
}