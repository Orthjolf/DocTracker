using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using WpfApp.DataProvider;
using WpfApp.Domain;
using WpfApp.Utils;

namespace WpfApp.ViewModel
{
	class StorageViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged = delegate { };

		private IReadOnlyCollection<Storage> Storages { get; set; }

		public StorageViewModel()
		{
			HeadText = "FirstDemo";
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

		private RelayCommand _changeHeadTextCommand;

		public RelayCommand ChangeHeadTextCommand
		{
			get
			{
				return _changeHeadTextCommand = _changeHeadTextCommand ?? new RelayCommand(OnChangeHeadText);
			}
		}

		private void OnChangeHeadText()
		{
			HeadText = "Привет!";
		}
	}
}