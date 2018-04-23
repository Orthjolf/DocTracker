using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp.SubPages.Modals
{
	public partial class BoxContentModal : Window
	{
		public BoxContentModal()
		{
			InitializeComponent();
		}

		private void btnDialogOk_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}
	}
}