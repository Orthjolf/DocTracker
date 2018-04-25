using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Domain;

namespace WpfApp.SubPages
{
	public partial class BoxContent
	{
		private readonly string _storageId;

		private readonly List<Contract> _contracts;

		private string _selectedContractId;

		public BoxContent(Box box)
		{
			InitializeComponent();
			_storageId = box.StorageId;
			_contracts = Contract.Repository.GetByBoxId(box.Id).ToList();
			if (!_contracts.Any()) return;

			ContractGridItems.ItemsSource = _contracts;
			SetContent(_contracts.First());
		}

		/// <summary>
		/// Выбор договора
		/// </summary>
		private void SelectContract(object sender, SelectionChangedEventArgs e)
		{
			var contract = (Contract) ContractGridItems.SelectedItem;
			if (contract == null) return;
			if (contract.Id == _selectedContractId) return;
			SetContent(contract);
		}

		/// <summary>
		/// Возвращение на страницу с ранее открытым складом
		/// </summary>
		private void BackOnMainScreen(object sender, RoutedEventArgs e)
		{
			MainWindow.SetContentAsStoragesPage(_storageId);
		}

		/// <summary>
		/// Поиск договора
		/// </summary>
		private void SearchContracts(object sender, TextChangedEventArgs e)
		{
			var filteredItems = _contracts
				.Where(item => item.Number.ToLower().Contains(ContractNumberSearch.Text.ToLower())).ToList();

			if (filteredItems.Any())
			{
				filteredItems = filteredItems
					.Where(item => item.ClientFullName.ToLower().Contains(FullNameSearchField.Text.ToLower())).ToList();
			}

			ContractGridItems.ItemsSource = filteredItems;
			if (!filteredItems.Any()) return;
			ContractGridItems.SelectedItem = filteredItems.First();
			SetContent(filteredItems.First());
		}

		/// <summary>
		/// Установка содержимого панели с договором
		/// </summary>
		/// <param name="contract">Договор, который будет отображаться</param>
		private void SetContent(Contract contract)
		{
			_selectedContractId = contract.Id;
			ContractPresenter.Content = new ContractDetails(contract);
		}
	}
}