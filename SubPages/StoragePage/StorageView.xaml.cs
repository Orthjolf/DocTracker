using System.Windows.Controls;
using MongoDB.Bson;
using WpfApp.Domain;

namespace WpfApp.SubPages.StoragePage
{
	public partial class StorageView : UserControl
	{
		public StorageView(BsonDocument bsonDocument)
		{
			InitializeComponent();
			var storage = Storage.Reconstitute(bsonDocument);
			StorageName.Text = storage.Name;
			StorageAddress.Text = storage.Address;
			StorageDescription.Text = storage.Description;
		}
	}
}
