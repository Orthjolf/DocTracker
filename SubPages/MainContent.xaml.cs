using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MongoDB.Bson;
using WpfApp.Domain;
using WpfApp.SubPages.Modals;

namespace WpfApp.SubPages
{
	public partial class MainContent : UserControl
	{
		private List<Storage> Storages { get; set; }

		private Storage _selectedStorage;

		public MainContent()
		{
			InitializeComponent();

			Storages = Storage.Repository.GetAll().ToList();
			StorageMenuItems.ItemsSource = Storages;
			if (!Storages.Any()) return;
			SetContent(Storages.First());
		}

		/// <summary>
		/// Выбор хранилища из главного окна
		/// </summary>
		/// <param name="selectedStorageId">Id хранилища</param>
		public void SelectItem(string selectedStorageId)
		{
			var storage = Storages.First(s => s.Id == selectedStorageId);
			SetContent(storage);
		}

		/// <summary>
		/// Установка панели с содержимым склада
		/// </summary>
		/// <param name="storage">Склад, содержимое которого будет отображаться</param>
		private void SetContent(Storage storage)
		{
			_selectedStorage = storage;
			ContentPresenter.Content = new ContentPartial(storage);
		}

		/// <summary>
		/// Поиск хранилищ в списке
		/// </summary>
		private void SearchInput_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			var filteredItems = Storages.Where(item => item.Name.ToLower().Contains(SearchInput.Text.ToLower())).ToList();
			StorageMenuItems.ItemsSource = filteredItems;
			if (!filteredItems.Any()) return;
			SetContent(filteredItems.First());
		}

		/// <summary>
		/// Выбор хранилища
		/// </summary>
		private void StorageMenuItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var item = (Storage) StorageMenuItems.SelectedItem;
			if (item == null) return;
			if (item.Id == _selectedStorage.Id) return;
			SetContent(item);
		}

		/// <summary>
		/// Добавление нового хранилища
		/// </summary>
		private void AddStorageButton_OnClick(object sender, RoutedEventArgs e)
		{
			var inputDialog = new AddStorageDialog();
			if (inputDialog.ShowDialog() != true) return;

			var storageBson = new BsonDocument
			{
				{"Name", inputDialog.Name.Text},
				{"Address", inputDialog.Address.Text},
				{"Description", inputDialog.Description.Text},
			};
			Storage.Repository.AddAndSave(storageBson);
			Storages = Storage.Repository.GetAll().ToList();
			StorageMenuItems.ItemsSource = Storages;
			SetContent(Storages.First(s => s.Id == storageBson["_id"].ToString()));
		}

		/// <summary>
		/// Удаление хранилища
		/// </summary>
		private async void DeleteStorageButton_OnClick(object sender, RoutedEventArgs e)
		{
			if (Storages.Count == 1) return;
			await Storage.Repository.DeleteById(_selectedStorage.Id);
			Storages = Storages.Where(s => s.Id != _selectedStorage.Id).ToList();
			SetContent(Storages.First());
			StorageMenuItems.ItemsSource = Storages;
		}
	}
}