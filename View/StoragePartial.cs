using System.Windows.Controls;
using WpfApp.Utils;
using WpfApp.ViewModel;

namespace WpfApp.View
{
	class FirstDemo : ModuleBase
	{
		public override string Name => "Вьюшка storageView";

		protected override UserControl CreateViewAndViewModel()
		{
			return new StorageView() { DataContext = new StorageViewModel() };
		}
	}
}