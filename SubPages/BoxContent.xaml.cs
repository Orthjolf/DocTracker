﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp.Debug;
using WpfApp.Domain;
using WpfApp.Scanning;
using WpfApp.Service;
using WpfApp.SubPages.Modals;

namespace WpfApp.SubPages
{
	public partial class BoxContent
	{
		private readonly string _storageId;

		private readonly string _boxId;

		private readonly List<Contract> _contracts;

		private string _selectedContractId;

		private IScanningCommand _command;

		private readonly IScanningCommand _deleteContractCommand;

		private readonly IScanningCommand _addContractCommand;

		public BoxContent(Box box)
		{
			InitializeComponent();
			_storageId = box.StorageId;
			_boxId = box.Id;
			_deleteContractCommand = new DeleteContractCommand(_boxId);
			_addContractCommand = new AddContractCommand(_boxId);
			_command = _addContractCommand;
			_contracts = Contract.Repository.GetByBoxId(box.Id).ToList();
			if (!_contracts.Any()) return;

			ContractGridItems.ItemsSource = _contracts;

			SetContent(_contracts.First());
		}

		private void OnPartialLoaded(object sender, RoutedEventArgs e)
		{
			var window = Window.GetWindow(this);
			if (window == null) return;
			window.KeyDown += HandleKeyPress;
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

		private void SwitchScanningMode(object sender, RoutedEventArgs e)
		{
			_command = ScanningModeToggle.IsChecked.GetValueOrDefault()
				? _deleteContractCommand
				: _addContractCommand;
		}

		private void HandleKeyPress(object sender, KeyEventArgs e)
		{
			if (_command.IsWorking)
			{
				ConsoleWriter.Write("Уже работает");
				return;
			}

			Task.Factory.StartNew(PerformCommand)
				.ContinueWith(result => UpdateUi());
		}

		private void PerformCommand()
		{
			Dispatcher.Invoke(() =>
			{
				Success.Visibility = Visibility.Hidden;
				Processing.Visibility = Visibility.Visible;
				LoadingIndicator.Visibility = Visibility.Visible;
				ScanningModeToggle.IsEnabled = false;
			});
			_command.DoWork();
		}

		private void UpdateUi()
		{
			Dispatcher.Invoke(() =>
			{
				Success.Visibility = Visibility.Visible;
				Processing.Visibility = Visibility.Hidden;
				LoadingIndicator.Visibility = Visibility.Hidden;
				ScanningModeToggle.IsEnabled = true;
			});

			ConsoleWriter.Write("Закончил работу");
		}
	}
}