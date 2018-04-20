using System;
using System.Windows.Controls;

namespace WpfApp.Utils
{
	abstract class ModuleBase : IModule
	{
		private UserControl view;

		protected abstract UserControl CreateViewAndViewModel();

		public abstract string Id { get; }
		public abstract string Name { get; }

		public UserControl UserInterface => view ?? (view = CreateViewAndViewModel());

		public void Deactivate()
		{
			if (view == null) return;
			var d = view.DataContext as IDisposable;
			d?.Dispose();
			view = null;
		}
	}
}