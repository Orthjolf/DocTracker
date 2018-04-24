using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Domain;
using WpfApp.SubPages;

namespace WpfApp
{
	public partial class MainWindow
	{
		public List<Storage> Storages { get; set; }

		private Storage _selectedStorage;

		public MainWindow()
		{
			InitializeComponent();
			Storages = Storage.Repository.GetAll().ToList();
			StorageMenuItems.ItemsSource = Storages;
			if (!Storages.Any()) return;
			SetContent(Storages.First());
		}

		private void SearchInput_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			var filteredItems = Storages.Where(item => item.Name.ToLower().Contains(SearchInput.Text.ToLower())).ToList();
			StorageMenuItems.ItemsSource = filteredItems;
			if (!filteredItems.Any()) return;
			SetContent(filteredItems.First());
		}

		private void StorageMenuItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var item = (Storage) StorageMenuItems.SelectedItem;
			if (item.Id == _selectedStorage.Id) return;
			SetContent(item);
		}

		private void SetContent(Storage storage)
		{
			_selectedStorage = storage;
			ContentPresenter.Content = new ContentPartial(storage);
		}
	}
}