using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using WpfApp.Domain;
using WpfApp.Service;
using WpfApp.SubPages.Modals;
using Action = System.Action;

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

			StorageAddress.Text = storage.Address;
			StorageDescription.Text = storage.Description;

			Task.Factory.StartNew(GetBoxes)
				.ContinueWith(result => UpdateUi());
		}

		private void GetBoxes()
		{
			//@TODO переписать
//			_boxes = Box.Repository.GetByStorageId(_storageId).ToList();
			_boxes = Box.Repository.GetAll().Where(b => b.StorageId == _storageId).ToList();
			_resetEvent.Set();
		}

		private void UpdateUi()
		{
			_resetEvent.WaitOne();
			if (_boxes.Any())
			{
				Dispatcher.Invoke(() => { BoxGridItems.ItemsSource = _boxes; });
			}

			Dispatcher.Invoke(() =>
			{
				LoadingIndicator.Visibility = Visibility.Hidden;
				LoadingLabel.Visibility = Visibility.Hidden;
				if (_boxes.Any())
				{
					BoxGridItems.SelectedItem = _boxes.FirstOrDefault();
				}
			});
		}

		/// <summary>
		/// Добавление коробки
		/// </summary>
		private void AddBox(object sender, RoutedEventArgs e)
		{
			var inputDialog = new AddBoxDialog(_boxes);
			if (inputDialog.ShowDialog() != true) return;

			var newBox = new Box
			{
				Id = Guid.NewGuid().ToString(),
				StorageId = _storageId,
				Name = inputDialog.Name.Text,
				Description = "",
				MinDate = null,
				MaxDate = null,
				ContractsCount = 0,
			};

			Box.Repository.Add(newBox);
			_boxes.Add(newBox);
			BoxGridItems.ItemsSource = _boxes.ToList();
			BoxGridItems.SelectedItem = _boxes.First(b => b.Name == inputDialog.Name.Text);
		}

		/// <summary>
		/// Удаление коробки
		/// </summary>
		private void DeleteBox(object sender, RoutedEventArgs e)
		{
			if (!_boxes.Any()) return;
			var selected = (Box) BoxGridItems.SelectedItem;
			Box.Repository.DeleteById(selected.Id);
			_boxes = _boxes.Where(b => b.Id != selected.Id).ToList();
			BoxGridItems.ItemsSource = _boxes;
			if (!_boxes.Any()) return;
			BoxGridItems.SelectedItem = _boxes.First();
		}

		/// <summary>
		/// Выбор коробки
		/// </summary>
		private void OpenBox(object sender, MouseButtonEventArgs e)
		{
			var box = (Box) BoxGridItems.SelectedItem;
			if (box == null) return;
			MainWindow.SetContent(new BoxContent(box));
		}

		private void PrintButton_OnClick(object sender, RoutedEventArgs e)
		{
			var box = (Box) BoxGridItems.SelectedItem;
			if (box == null) return;
			MainWindow.SetContent(new BoxPrintForm(box));
		}
	}
}