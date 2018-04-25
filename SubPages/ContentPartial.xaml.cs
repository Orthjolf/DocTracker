using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MongoDB.Bson;
using WpfApp.Domain;
using WpfApp.SubPages.Modals;

namespace WpfApp.SubPages
{
	public partial class ContentPartial
	{
		private List<Box> _boxes;

		private readonly string _storageId;

		public ContentPartial(Storage storage)
		{
			InitializeComponent();
			_storageId = storage.Id;

			StorageName.Text = storage.Name;
			StorageAddress.Text = storage.Address;
			StorageDescription.Text = storage.Description;

			_boxes = Box.Repository.GetByStorageId(_storageId).ToList();
			BoxGridItems.ItemsSource = _boxes;
		}

		/// <summary>
		/// Добавление коробки
		/// </summary>
		private void AddBox_OnClick(object sender, RoutedEventArgs e)
		{
			var inputDialog = new AddBoxDialog(_boxes);
			if (inputDialog.ShowDialog() != true) return;

			var boxBson = new BsonDocument
			{
				{"StorageId", _storageId},
				{"Name", inputDialog.Name.Text},
				{"Description", ""},
				{"MinDate", DateTime.MinValue},
				{"MaxDate", DateTime.MinValue}
			};
			Box.Repository.AddAndSave(boxBson);
			_boxes.Add(Box.Reconstitute(boxBson));
			BoxGridItems.ItemsSource = _boxes.ToList();
		}

		/// <summary>
		/// Удаление коробки
		/// </summary>
		private async void DeleteBoxButton_OnClick(object sender, RoutedEventArgs e)
		{
			if (_boxes.Count == 1) return;
			var selected = (Box) BoxGridItems.SelectedItem;
			await Box.Repository.DeleteById(selected.Id);
			_boxes = _boxes.Where(b => b.Id != selected.Id).ToList();
			BoxGridItems.ItemsSource = _boxes;
		}

		/// <summary>
		/// Выбор коробки
		/// </summary>
		private void BoxGridItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var box = (Box) BoxGridItems.SelectedItem;
			if (box == null) return;
			MainWindow.SetContent(new BoxContent(box));
		}

		private void PrintButton_OnClick(object sender, RoutedEventArgs e)
		{
			var selected = (Box) BoxGridItems.SelectedItem;
			var inputDialog = new BoxPrintForm(selected);
			if (inputDialog.ShowDialog() != true) return;
		}
	}
}