using WpfApp.Utils;
using System.Windows.Controls;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using WpfApp.Domain;

namespace WpfApp.SeparateDemo
{
	class SeparateDemo : IModule
	{
		private SeparateDemoView view;
		private SeparateDemoViewModel viewModel;

		private readonly Storage _storage;

		public SeparateDemo(BsonDocument storage)
		{
			_storage = new Storage
			{
				Id = storage["_id"].ToString(),
				Name = storage["Name"].AsString,
				Address = storage["Address"].AsString,
				Description = storage["Description"].AsString
			};
		}

		public string Name => "Вьюшка SeparateDemo";

		public UserControl UserInterface
		{
			get
			{
				if (view == null) CreateView();
				return view;
			}
		}

		private void CreateView()
		{
			view = new SeparateDemoView();
			viewModel = new SeparateDemoViewModel(_storage);
			view.DataContext = viewModel;
		}

		public void Deactivate()
		{
			viewModel.Dispose();
			view = null;
		}
	}
}