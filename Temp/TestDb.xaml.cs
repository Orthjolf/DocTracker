using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MongoDB.Bson;
using WpfApp.DataProvider;
using WpfApp.Domain;
using WpfApp.Service;

namespace WpfApp.Temp
{
	public partial class TestDb : UserControl
	{
		public TestDb()
		{
			InitializeComponent();
			ConsoleWriter.Write("Test");
		}

		private void WindowLoaded(object sender, RoutedEventArgs e)
		{
			using (var db = new SqlServerDataContext())
			{
				db.Storages.Add(new Storage
				{
					Id = Guid.NewGuid().ToString(),
					Name = "ttttt",
					Description = "qweqweqeqweq",
					Address = "qeqwew"
				});
				db.SaveChanges();

				var query = from b in db.Storages
					orderby b.Name
					select b;

				query.Select(x => x.Name).ToList().ForEach(ConsoleWriter.Write);
			}
		}
	}
}