using System.Windows.Controls;
using MongoDB.Bson;
using WpfApp.Domain;
using WpfApp.Utils;

namespace WpfApp.SubPages.StoragePage
{
	internal class StoragePage //: IModule
	{
//		private StorageView _view;
//		private StorageViewModel _viewModel;
//
//		private readonly Storage _storage;
//
//		public StoragePage(BsonValue storage)
//		{
//			_storage = new Storage
//			{
//				Id = storage["_id"].ToString(),
//				Name = storage["Name"].AsString,
//				Address = storage["Address"].AsString,
//				Description = storage["Description"].AsString
//			};
//		}
//
//		public string Id => _storage.Id;
//		public string Name => _storage.Name;
//
//		public UserControl UserInterface
//		{
//			get
//			{
//				if (_view == null) CreateView();
//				return _view;
//			}
//		}
//
//		private void CreateView()
//		{
//			_view = new StorageView();
//			_viewModel = new StorageViewModel(_storage);
//			_view.DataContext = _viewModel;
//		}
//
//		public void Deactivate()
//		{
//			_viewModel.Dispose();
//			_view = null;
//		}
	}
}