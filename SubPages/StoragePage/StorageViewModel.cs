using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using MongoDB.Bson;
using WpfApp.Domain;
using WpfApp.Enum;
using WpfApp.Service;
using WpfApp.Utils;

namespace WpfApp.SubPages.StoragePage
{
	class StorageViewModel : INotifyPropertyChanged, IDisposable
	{
		public event PropertyChangedEventHandler PropertyChanged = delegate { };
		private IReadOnlyCollection<BsonDocument> Storages { get; set; }

		/// <summary>
		/// Название
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// Адрес
		/// </summary>
		public string Address { get; }

		/// <summary>
		/// Описание
		/// </summary>
		public string Description { get; }

		public List<Box> Boxes { get; set; }

		public StorageViewModel(Storage storage)
		{
			Name = storage.Name;
			Address = storage.Address;
			Description = storage.Description;
			HeadText = Name;

			var documents = Box.Repository.GetByStorageId(storage.Id);
			Boxes = new List<Box>();
			foreach (var bsonDocument in documents)
			{
				Boxes.Add(Box.Reconstitute(bsonDocument));
			}

//			var modules = Boxes.Select(box => new BoxPage.BoxPage(box)).ToList();
		}

		private string _headText;

		public string HeadText
		{
			get { return _headText; }
			set
			{
				_headText = value;
				PropertyChanged(this, new PropertyChangedEventArgs(nameof(HeadText)));
			}
		}

		public void Dispose()
		{
		}

		private RelayCommand _changeHeadTextCommand;

		public RelayCommand ChangeHeadTextCommand
		{
			get
			{
				return _changeHeadTextCommand = _changeHeadTextCommand ??
				                                new RelayCommand(OnChangeHeadText);
			}
		}

		private void OnChangeHeadText()
		{
			MessageService.ShowMessage(HeadText);
		}
	}
}