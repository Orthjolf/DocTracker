using WpfApp.Service;
using WpfApp.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using WpfApp.Domain;

namespace WpfApp.SeparateDemo
{
	class SeparateDemoViewModel : INotifyPropertyChanged, IDisposable
	{
		public event PropertyChangedEventHandler PropertyChanged = delegate { };
		private IReadOnlyCollection<BsonDocument> Storages { get; set; }

		public SeparateDemoViewModel()
		{
			var rep = Storage.Repository;
			Storages = rep.GetAll();

			var sb = new StringBuilder();
			foreach (var bsonDocument in Storages)
			{
				sb.Append(bsonDocument);
			}

			HeadText = sb.ToString();
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