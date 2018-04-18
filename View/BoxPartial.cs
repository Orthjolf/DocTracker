using WpfApp.Utils;
using WpfApp.ViewModel;
using System.Windows.Controls;

namespace WpfApp.View
{
	class BoxPartial : ModuleBase
	{
		public override string Name => "Вьюшка box";

		protected override UserControl CreateViewAndViewModel()
		{
			return new BoxView() { DataContext = new BoxViewModel() };
		}
	}
}