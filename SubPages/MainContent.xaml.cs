using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MongoDB.Bson;
using WpfApp.Domain;
using WpfApp.SubPages.Modals;

namespace WpfApp.SubPages
{
	public partial class MainContent
	{
		private List<Storage> Storages { get; set; }

		private Storage _selectedStorage;

		private readonly ManualResetEvent _resetEvent = new ManualResetEvent(false);

		public MainContent()
		{
			InitializeComponent();

			Task.Factory.StartNew(GetStorages)
				.ContinueWith(result => UpdateUi());
		}

		/// <summary>
		/// Получение списка складов
		/// </summary>
		private void GetStorages()
		{
			Storages = Storage.Repository.GetAll().ToList();
			_resetEvent.Set();
		}

		private void UpdateUi()
		{
			_resetEvent.WaitOne();
			Dispatcher.Invoke(() =>
			{
				if (Storages.Any())
				{
					SetContent(Storages.First());
					StorageMenuItems.ItemsSource = Storages;
				}

				LoadingIndicator.Visibility = Visibility.Hidden;
				LoadingLabel.Visibility = Visibility.Hidden;
				AddStorageButton.IsEnabled = true;
				DeleteStorageButton.IsEnabled = true;
			});
		}

		/// <summary>
		/// Выбор хранилища из главного окна
		/// </summary>
		/// <param name="selectedStorageId">Id хранилища</param>
		public void SelectItem(string selectedStorageId)
		{
			var storage = selectedStorageId == string.Empty
				? Storages.FirstOrDefault()
				: Storages.First(s => s.Id == selectedStorageId);

			if (storage == null) return;
			SetContent(storage);
		}

		/// <summary>
		/// Установка панели с содержимым склада
		/// </summary>
		/// <param name="storage">Склад, содержимое которого будет отображаться</param>
		private void SetContent(Storage storage)
		{
			_selectedStorage = storage;
			BreadCrumbs.Text = _selectedStorage.Name;
			ContentPresenter.Content = new StorageContent(storage);
		}

		/// <summary>
		/// Поиск хранилищ в списке
		/// </summary>
		private void SearchStorages(object sender, TextChangedEventArgs e)
		{
			var filteredItems = Storages.Where(item => item.Name.ToLower().Contains(SearchInput.Text.ToLower())).ToList();
			StorageMenuItems.ItemsSource = filteredItems;
			if (!filteredItems.Any()) return;
			SetContent(filteredItems.First());
		}

		/// <summary>
		/// Выбор хранилища
		/// </summary>
		private void SelectStorage(object sender, SelectionChangedEventArgs e)
		{
			var item = (Storage) StorageMenuItems.SelectedItem;
			if (item == null) return;
			if (item.Id == _selectedStorage.Id) return;
			SetContent(item);
		}

		/// <summary>
		/// Добавление нового хранилища
		/// </summary>
		private void AddStorage(object sender, RoutedEventArgs e)
		{
			var inputDialog = new AddStorageDialog();
			if (inputDialog.ShowDialog() != true) return;

			var newStorage = new Storage
			{
				Id = Guid.NewGuid().ToString(),
				Name = inputDialog.Name.Text,
				Address = inputDialog.Address.Text,
				Description = inputDialog.Description.Text,
			};

			Storage.Repository.Add(newStorage);
			Storages = Storage.Repository.GetAll().ToList();
			StorageMenuItems.ItemsSource = Storages;
			SetContent(Storages.First(s => s.Id == newStorage.Id));
		}

		/// <summary>
		/// Удаление хранилища
		/// </summary>
		private void DeleteStorage(object sender, RoutedEventArgs e)
		{
			if (Storages.Count == 1) return;
			Storage.Repository.DeleteById(_selectedStorage.Id);
			Storages = Storages.Where(s => s.Id != _selectedStorage.Id).ToList();
			SetContent(Storages.First());
			StorageMenuItems.ItemsSource = Storages;
		}
	}
}