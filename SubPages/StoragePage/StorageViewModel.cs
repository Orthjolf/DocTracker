using System;
using System.Collections.Generic;
using System.ComponentModel;
using MongoDB.Bson;
using WpfApp.Domain;
using WpfApp.SubPages.Modals;
using WpfApp.Utils;

namespace WpfApp.SubPages.StoragePage
{
	class StorageViewModel : INotifyPropertyChanged, IDisposable
	{
		public event PropertyChangedEventHandler PropertyChanged = delegate { };
		private IReadOnlyCollection<BsonDocument> Storages { get; set; }

		public string Name { get; }

		public string Address { get; }

		public string Description { get; }

		public List<Box> Boxes { get; }

		public StorageViewModel(Storage storage)
		{
//			Name = storage.Name;
//			Address = storage.Address;
//			Description = storage.Description;
//
//			var documents = Box.Repository.GetByStorageId(storage.Id);
//			Boxes = new List<Box>();
//			foreach (var bsonDocument in documents)
//			{
//				Boxes.Add(Box.Reconstitute(bsonDocument));
//			}
		}

		public void Dispose()
		{
		}

		/// <summary>
		/// Команда добавления хранилища
		/// </summary>
		private RelayCommand _openBoxCommand;

		public RelayCommand OpenBoxCommand
		{
			get { return _openBoxCommand = _openBoxCommand ?? new RelayCommand(OpenBox); }
		}

		private void OpenBox()
		{
			var inputDialog = new BoxContentModal();
			if (inputDialog.ShowDialog() != true) return;

			
		}
	}
}