using System;
using System.Collections.Generic;
using System.ComponentModel;
using MongoDB.Bson;
using WpfApp.Domain;
using WpfApp.Service;
using WpfApp.Utils;

namespace WpfApp.SubPages.BoxPage
{
	class BoxViewModel : INotifyPropertyChanged, IDisposable
	{
		public event PropertyChangedEventHandler PropertyChanged = delegate { };
		private IReadOnlyCollection<BsonDocument> Storages { get; set; }

		public string Description { get; set; }
		public DateTime MinDate { get; set; }
		public DateTime MaxDate { get; set; }
		public int ContractsCount { get; set; }

		public BoxViewModel(Box box)
		{
			MinDate = box.MinDate;
			MaxDate = box.MaxDate;
			Description = box.Description;
			ContractsCount = 0;
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