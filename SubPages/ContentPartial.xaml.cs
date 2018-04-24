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
		private List<Box> Boxes { get; set; }

		private Storage Storage { get; set; }

		public ContentPartial(Storage storage)
		{
			InitializeComponent();
			Storage = storage;
			StorageName.Text = Storage.Name;
			StorageAddress.Text = Storage.Address;
			StorageDescription.Text = Storage.Description;

			Boxes = Box.Repository.GetByStorageId(Storage.Id).ToList();
			BoxGridItems.ItemsSource = Boxes;
		}

		private void AddBox_OnClick(object sender, RoutedEventArgs e)
		{
			var inputDialog = new AddBoxDialog(Boxes);
			if (inputDialog.ShowDialog() != true) return;

			var boxBson = new BsonDocument
			{
				{"StorageId", Storage.Id},
				{"Name", inputDialog.Name.Text},
				{"Description", ""},
				{"MinDate", DateTime.MinValue},
				{"MaxDate", DateTime.MinValue}
			};
			Box.Repository.AddAndSave(boxBson);
			Boxes = Box.Repository.GetByStorageId(Storage.Id).ToList();
			BoxGridItems.ItemsSource = Boxes;
		}

		private async void DeleteBoxButton_Onclick(object sender, RoutedEventArgs e)
		{
			if (Boxes.Count == 1) return;
			var selected = (Box) BoxGridItems.SelectedItem;
			await Box.Repository.DeleteById(selected.Id);
			Boxes = Boxes.Where(b => b.Id != selected.Id).ToList();
			BoxGridItems.ItemsSource = Boxes;
		}

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