using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MongoDB.Bson;
using WpfApp.DataProvider.Repository;
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
	}
}