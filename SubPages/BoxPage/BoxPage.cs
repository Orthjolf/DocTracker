using System.Windows.Controls;
using MongoDB.Bson;
using WpfApp.Domain;
using WpfApp.Utils;

namespace WpfApp.SubPages.BoxPage
{
	internal class BoxPage : IModule
	{
		private BoxView _view;
		private BoxViewModel _viewModel;

		private readonly Box _box;

		public BoxPage(Box box)
		{
			_box = box;
		}

		public string Id => _box.Id;
		public string Name => _box.Description;

		public UserControl UserInterface
		{
			get
			{
				if (_view == null) CreateView();
				return _view;
			}
		}

		private void CreateView()
		{
			_view = new BoxView();
			_viewModel = new BoxViewModel(_box);
			_view.DataContext = _viewModel;
		}

		public void Deactivate()
		{
			_viewModel.Dispose();
			_view = null;
		}
	}
}