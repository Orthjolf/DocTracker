using System.Windows.Controls;
using WpfApp.Domain;

namespace WpfApp.SubPages
{
	public partial class ContentPartial : UserControl
	{
		private Storage _storage { get; set; }
		
		public ContentPartial(Storage storage)
		{
			InitializeComponent();
			_storage = storage;
			StorageName.Text = _storage.Name;
			StorageAddress.Text = _storage.Address;
			StorageDescription.Text = _storage.Description;
		}
	}
}
