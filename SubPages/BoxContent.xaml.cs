﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp.DataProvider.Repository;
using WpfApp.Debug;
using WpfApp.Domain;
using WpfApp.Enum;
using WpfApp.Extensions;
using WpfApp.Scanning;
using WpfApp.Service;

namespace WpfApp.SubPages
{
	public partial class BoxContent
	{
		public static BoxContent Instance;

		private readonly Storage _storage;

		private readonly Box _box;

		private List<Contract> _contracts;

		private string _selectedContractId;

		private readonly KeyboardHookHelper _keyboardHookHelper;

		private readonly ManualResetEvent _resetEvent = new ManualResetEvent(false);

		public BoxContent(Box box)
		{
			InitializeComponent();
			_box = box;
			_storage = Storage.Repository.Get(box.StorageId);

			Instance = this;
			_keyboardHookHelper = new KeyboardHookHelper();
			_keyboardHookHelper.SetHook();
			BarCodeListener.CurrentBoxId = box.Id;
			BarCodeListener.Action = ActionPerformed.PutInBox;

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
			_keyboardHookHelper.Unhook();
			;
			MainWindow.ToMainScreen(_storage.Id);
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
			BreadCrumbs.Text = _storage.Name + " / " + _box.Name;
			ContractPresenter.Content = new ContractDetails(contract);
		}

		private void SwitchScanningMode(object sender, RoutedEventArgs e)
		{
			BarCodeListener.Action = ScanningModeToggle.IsChecked.GetValueOrDefault()
				? ActionPerformed.RemovedFromBox
				: ActionPerformed.PutInBox;
		}

		/// <summary>
		/// Производит действие команды при сканировании
		/// </summary>
		public void PerformCommand()
		{
			Task.Factory.StartNew(() =>
				{
					Dispatcher.Invoke(() =>
					{
						Success.Visibility = Visibility.Hidden;
						Processing.Visibility = Visibility.Visible;
						LoadingIndicator.Visibility = Visibility.Visible;
						ScanningModeToggle.IsEnabled = false;
					});
					_contracts = Contract.Repository.GetByBoxId(_box.Id).ToList();
					_resetEvent.Set();
				})
				.ContinueWith(result => UpdateUi());
		}

		/// <summary>
		/// Обновляет интерфейс после сканирования
		/// </summary>
		private void UpdateUi()
		{
			_resetEvent.WaitOne();
			Dispatcher.Invoke(() =>
			{
				Success.Visibility = Visibility.Visible;
				Processing.Visibility = Visibility.Hidden;
				LoadingIndicator.Visibility = Visibility.Hidden;
				ScanningModeToggle.IsEnabled = true;
				ContractGridItems.ItemsSource = _contracts;
				SetContent(_contracts.First());
			});
		}
	}
}