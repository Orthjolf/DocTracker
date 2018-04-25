using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Domain;

namespace WpfApp.SubPages
{
	public partial class BoxContent
	{
		private readonly Box _box;

		private readonly List<Contract> _contracts;

		public BoxContent(Box box)
		{
			InitializeComponent();
			_box = box;
			_contracts = Contract.Repository.GetByBoxId(_box.Id).ToList();
			if (!_contracts.Any()) return;

			ContractGridItems.ItemsSource = _contracts;
			ContractGridItems.SelectedItem = _contracts.First();
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

		private void SearchField_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			var filteredItems = _contracts
				.Where(item => item.Number.ToLower().Contains(ContractNumberSearch.Text.ToLower()))
				.Where(item => item.ClientFullName.ToLower().Contains(FullNameSearchField.Text.ToLower()))
				.ToList();

			ContractGridItems.ItemsSource = filteredItems;
			if (!filteredItems.Any()) return;
			ContractPresenter.Content = new ContractDetails(filteredItems.First());
		}
	}
}