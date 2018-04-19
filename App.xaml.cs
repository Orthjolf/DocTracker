using System.Linq;
using System.Windows;
using WpfApp.Domain;
using WpfApp.Enum;
using WpfApp.ViewModel;

namespace WpfApp
{
	public partial class App
	{
		private void Application_Startup(object sender, StartupEventArgs e)
		{
			var mainWindow = new MainWindow();
			var storages = Entity.Repository.GetAll(DocumentType.Storage);
			var modules = storages.Select(storage => new SubPages.StoragePage.StoragePage(storage)).ToList();

			var vm = new MainWindowViewModel(modules);

			mainWindow.DataContext = vm;
			mainWindow.Closing += (s, args) => vm.SelectedModule.Deactivate();
			mainWindow.Show();
		}
	}
}