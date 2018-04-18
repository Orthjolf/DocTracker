using WpfApp.Service;
using WpfApp.Utils;
using System;
using System.ComponentModel;

namespace WpfApp.SeparateDemo
{
	class SeparateDemoViewModel : INotifyPropertyChanged, IDisposable
	{
		public event PropertyChangedEventHandler PropertyChanged = delegate { };

		public SeparateDemoViewModel()
		{
			HeadText = "Это вьюшка SeparateDemo";
		}

		//Properties
		private string _HeadText;

		public string HeadText
		{
			get { return _HeadText; }
			set
			{
				_HeadText = value;
				PropertyChanged(this, new PropertyChangedEventArgs(nameof(HeadText)));
			}
		}

		//Methods
		public void Dispose()
		{
		}

		//Commands
		private RelayCommand _ChangeHeadTextCommand;

		public RelayCommand ChangeHeadTextCommand
		{
			get
			{
				return _ChangeHeadTextCommand = _ChangeHeadTextCommand ??
				                                new RelayCommand(OnChangeHeadText);
			}
		}

		private void OnChangeHeadText()
		{
			MessageService.ShowMessage(HeadText);
		}
	}
}