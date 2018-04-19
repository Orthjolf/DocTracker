using WpfApp.Utils;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using WpfApp.SeparateDemo;

namespace WpfApp.ViewModel
{
	class MainWindowViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged = delegate { };

		public List<IModule> Modules { get; private set; }

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

		public UserControl UserInterface
		{
			get { return SelectedModule?.UserInterface; }
		}

		public MainWindowViewModel(IEnumerable<IModule> storages)
		{
			Modules = storages.OrderBy(m => m.Name).ToList();
			if (Modules.Count > 0)
			{
				SelectedModule = Modules[0];
			}
		}
	}
}