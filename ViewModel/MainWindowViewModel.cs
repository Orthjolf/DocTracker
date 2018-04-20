using System;
using WpfApp.Utils;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using WpfApp.Domain;
using WpfApp.Enum;
using WpfApp.Service;

namespace WpfApp.ViewModel
{
	class MainWindowViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged = delegate { };

		public UserControl UserInterface => SelectedModule?.UserInterface;

		private List<IModule> AllModules { get; set; }

		public MainWindowViewModel(IEnumerable<IModule> storages)
		{
			AllModules = storages.OrderBy(m => m.Name).ToList();
			Modules = new List<IModule>(AllModules);
			if (Modules.Count > 0)
			{
				SelectedModule = Modules[0];
			}
		}

		private IModule _selectedModule;

		public IModule SelectedModule
		{
			get { return _selectedModule; }
			set
			{
				if (value == _selectedModule) return;
				_selectedModule?.Deactivate();
				_selectedModule = value;
				PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedModule)));
				PropertyChanged(this, new PropertyChangedEventArgs("UserInterface"));
			}
		}

		private List<IModule> _modules;

		public List<IModule> Modules
		{
			get { return _modules; }
			set
			{
				_modules = value;
				PropertyChanged(this, new PropertyChangedEventArgs(nameof(Modules)));
			}
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

		/// <summary>
		/// Удаление хранилища
		/// </summary>
		private RelayCommand _deleteStorageCommand;

		public RelayCommand DeleteStorageCommand
		{
			get { return _deleteStorageCommand = _deleteStorageCommand ?? new RelayCommand(DeleteStorage); }
		}

		private void DeleteStorage()
		{
			AllModules = AllModules.Where(m => m.Id != SelectedModule.Id).ToList();
			Modules = new List<IModule>(AllModules);
			SelectedModule = Modules.First();
			//TODO добавить удаление из базы
		}

		/// <summary>
		/// Поиск хранилищ
		/// </summary>
		private RelayCommand _searchStoragesCommand;

		public RelayCommand SearchStoragesCommand
		{
			get { return _searchStoragesCommand = _searchStoragesCommand ?? new RelayCommand(SearchStorages); }
		}

		private void SearchStorages()
		{
			if (string.IsNullOrEmpty(InputText))
			{
				Modules = new List<IModule>(AllModules);
			}

			Modules = AllModules.Where(m => m.Name.Contains(InputText)).ToList();
			SelectedModule = Modules.First();
		}
	}
}