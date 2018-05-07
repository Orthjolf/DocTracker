using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp.SubPages.Modals
{
	public partial class AddContractModal : Window
	{
		public AddContractModal()
		{
			InitializeComponent();
		}

		private void btnDialogOk_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}

		private void Window_ContentRendered(object sender, EventArgs e)
		{
//			Name.SelectAll();
//			Name.Focus();
		}


	}
}
