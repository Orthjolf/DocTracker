using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Domain;

namespace WpfApp.SubPages.Modals
{
	public partial class AddBoxDialog : Window
	{
		private readonly List<string> _boxesNames;

		public AddBoxDialog(IEnumerable<Box> boxes)
		{
			InitializeComponent();
			_boxesNames = boxes.Select(b => b.Name).ToList();
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
			SubmitButton.IsEnabled = Name.Text.Length > 1 && !_boxesNames.Contains(Name.Text);;
		}
	}
}