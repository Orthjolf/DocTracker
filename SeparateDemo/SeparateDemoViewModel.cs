using WpfApp.Service;
using WpfApp.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using WpfApp.Domain;
using WpfApp.Enum;

namespace WpfApp.SeparateDemo
{
	class SeparateDemoViewModel : INotifyPropertyChanged, IDisposable
	{
		public event PropertyChangedEventHandler PropertyChanged = delegate { };
		private IReadOnlyCollection<BsonDocument> Storages { get; set; }

		/// <summary>
		/// Название
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Адрес
		/// </summary>
		public string Address { get; set; }

		/// <summary>
		/// Описание
		/// </summary>
		public string Description { get; set; }

		public SeparateDemoViewModel(Storage storage)
		{
			Name = storage.Name;
			Address = storage.Address;
			Description = storage.Description;
			HeadText = Name;

//			var storages = Entity.Repository.GetAll(DocumentType.Box);
//			var modules = storages.Select(storage => new SeparateDemo.SeparateDemo(storage)).ToList();
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