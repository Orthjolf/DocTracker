using System.Windows.Controls;

namespace WpfApp.Utils
{
	public interface IModule
	{
		string Id { get; }
		string Name { get; }
		UserControl UserInterface { get; }
		void Deactivate();
	}
}