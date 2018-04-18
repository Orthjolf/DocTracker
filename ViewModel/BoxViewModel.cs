using WpfApp.Service;
using WpfApp.Utils;
using System;
using System.ComponentModel;

namespace WpfApp.ViewModel
{
	class BoxViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged = delegate { };

		public BoxViewModel()
		{
			HeadText = "Это вьюшка box";
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


		private string _inputText;

		public string InputText
		{
			get { return _inputText; }
			set
			{
				_inputText = value;
				PropertyChanged(this, new PropertyChangedEventArgs(nameof(InputText)));
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
			if (String.IsNullOrEmpty(InputText))
			{
				MessageService.ShowMessage("Введите текст, пожалуйста.");
				return;
			}

			HeadText = InputText;
		}
	}
}