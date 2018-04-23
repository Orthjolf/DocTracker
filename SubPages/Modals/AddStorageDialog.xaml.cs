using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp.SubPages.Modals
{
	public partial class AddStorageDialog : Window
	{
		private bool _isValid;

		public AddStorageDialog()
		{
			InitializeComponent();
			Name.Text = "";
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
			_isValid = Name.Text.Length > 0 && Address.Text.Length > 0;
			if (_isValid)
			{
				SubmitButton.IsEnabled = true;
			}
		}
	}
}