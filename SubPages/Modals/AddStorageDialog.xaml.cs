using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp.SubPages.Modals
{
	public partial class AddStorageDialog : Window
	{
		public AddStorageDialog()
		{
			InitializeComponent();
		}

		private void btnDialogOk_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}

		private void Window_ContentRendered(object sender, EventArgs e)
		{
			Name.SelectAll();
			Name.Focus();
		}

		private void Validate(object sender, TextChangedEventArgs e)
		{
			SubmitButton.IsEnabled = Name.Text.Length > 0 && Address.Text.Length > 0;
		}
	}
}