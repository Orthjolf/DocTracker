using WpfApp.Utils;
using System.Windows.Controls;

namespace WpfApp.SeparateDemo
{
	class SeparateDemo : IModule
	{
		private SeparateDemoView view;
		private SeparateDemoViewModel viewModel;

		public string Name => "Вьюшка SeparateDemo";
        

		public UserControl UserInterface
		{
			get { if (view == null) CreateView(); return view; }
		}

		private void CreateView()
		{
			view = new SeparateDemoView();
			viewModel = new SeparateDemoViewModel();
			view.DataContext = viewModel;
		}

		public void Deactivate()
		{
			viewModel.Dispose();
			view = null;
		}
	}
}
