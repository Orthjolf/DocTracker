using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MongoDB.Bson;
using WpfApp.DataProvider;
using WpfApp.DataProvider.Repository;
using WpfApp.DataProvider.SqlServer;
using WpfApp.Domain;
using WpfApp.Enum;
using WpfApp.Service;

namespace WpfApp.Temp
{
	public partial class TestDb : UserControl
	{
		public TestDb()
		{
			InitializeComponent();
		}

		private void WindowLoaded(object sender, RoutedEventArgs e)
		{
			//testSqlDb();

			TestNewThing();
		}

		private void TestNewThing()
		{
			var all = Storage.Repository.GetAll();
//			var singleById = Storage.Repository1.Get("13ffad3a-7e33-4bb3-920a-4736fcfdf746");

			DataBaseSwitcher.SetActiveDataBase(ConnectionType.Remote);
		}

		private void testSqlDb()
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