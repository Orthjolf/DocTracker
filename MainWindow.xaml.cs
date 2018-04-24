using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MongoDB.Bson;
using WpfApp.DataProvider.Repository;
using WpfApp.Domain;
using WpfApp.SubPages;
using WpfApp.SubPages.Modals;

namespace WpfApp
{
	public partial class MainWindow
	{
		public static MainWindow Instance { get; private set; }

		public readonly MainContent MainContent;

		public MainWindow()
		{
			InitializeComponent();
			Instance = this;
			MainContent = new MainContent();
			RootContent.Content = MainContent;
		}
	}
}