using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MongoDB.Bson;
using WpfApp.Domain;
using WpfApp.Service;
using WpfApp.SubPages.Modals;

namespace WpfApp.SubPages
{
	public partial class StorageContent
	{
		private List<Box> _boxes;

		private readonly string _storageId;

		private readonly ManualResetEvent _resetEvent = new ManualResetEvent(false);

		public StorageContent(Storage storage)
		{
			InitializeComponent();
			_storageId = storage.Id;

			StorageName.Text = storage.Name;
			StorageAddress.Text = storage.Address;
			StorageDescription.Text = storage.Description;

			Task.Factory.StartNew(GetBoxes)
				.ContinueWith(result => UpdateUi());
		}

		private void GetBoxes()
		{
			ConsoleWriter.Write("Загрузка коробок");
			_boxes = Box.Repository.GetByStorageId(_storageId).ToList();
			Thread.Sleep(10000);
			_resetEvent.Set();
		}

		private void UpdateUi()
		{
			_resetEvent.WaitOne();
			if (!_boxes.Any()) return;

			_boxes.ForEach(s => ConsoleWriter.Write(s.Name));

			Dispatcher.Invoke(() => { BoxGridItems.ItemsSource = _boxes; });
		}

		/// <summary>
		/// Добавление коробки
		/// </summary>
		private void AddBox(object sender, RoutedEventArgs e)
		{
			var inputDialog = new AddBoxDialog(_boxes);
			if (inputDialog.ShowDialog() != true) return;

			var boxBson = new BsonDocument
			{
				{"StorageId", _storageId},
				{"Name", inputDialog.Name.Text},
				{"Description", ""},
				{"MinDate", DateTime.MinValue},
				{"MaxDate", DateTime.MinValue},
				{"ContractsCount", 0}
			};
			Box.Repository.AddAndSave(boxBson);
			_boxes.Add(Box.Reconstitute(boxBson));
			BoxGridItems.ItemsSource = _boxes.ToList();
		}

		/// <summary>
		/// Удаление коробки
		/// </summary>
		private async void DeleteBox(object sender, RoutedEventArgs e)
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
		private void SelectBox(object sender, SelectionChangedEventArgs e)
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