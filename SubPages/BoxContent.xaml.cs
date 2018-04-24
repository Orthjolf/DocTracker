using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Domain;

namespace WpfApp.SubPages
{
	public partial class BoxContent : UserControl
	{
		private Box _box;

		private List<Contract> _contracts;

		public BoxContent(Box box)
		{
			InitializeComponent();
			_box = box;

			_contracts = Contract.Repository.GetByBoxId(_box.Id).ToList();
			ContractGridItems.ItemsSource = _contracts;
		}


		private void ContractGridItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var contract = (Contract) ContractGridItems.SelectedItem;
			if (contract == null) return;
			ContractPresenter.Content = new ContractDetails(contract);
		}

		private void BackButton_OnClick(object sender, RoutedEventArgs e)
		{
			MainWindow.SetDefault(_box.StorageId);
		}
	}
}