using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp.DataProvider;
using WpfApp.Domain;
using WpfApp.Enum;
using WpfApp.Service;
using WpfApp.Tests;

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
			testSqlDb();

//			TestNewThing();
		}

		private void TestNewThing()
		{
			var all = Storage.Repository.GetAll();
//			var singleById = Storage.Repository1.Get("13ffad3a-7e33-4bb3-920a-4736fcfdf746");

			DataBaseSwitcher.SetActiveDataBase(ConnectionType.Remote);
		}

		private void testSqlDb()
		{
//			Box.Repository.Add(new Box
//			{
//				Id = Guid.NewGuid().ToString(),
//				ContractsCount = 0,
//				Description = "weq",
//				MaxDate = DateTime.Now,
//				MinDate = DateTime.Now,
//				Name = "qwewq",
//				StorageId = "cd415b99-46a4-4bb0-82b9-68c665035765"
//			});
//
//			Storage.Repository.Add(new Storage
//			{
//				Id = Guid.NewGuid().ToString(),
//				Name = "erwre",
//				Description = "qweqweqeqweq",
//				Address = "qeqwew"
//			});

//			Contract.Repository.Add(ContractFromDb.Get("12312312", "wqeqweqwewqwe"));

//			var storages = Storage.Repository.GetAll().ToList();
//			storages.ForEach(b => Console.Write(b.Id));
//			Storage.Repository.DeleteById("20e48107-c01f-4e08-b4ae-cd179bf96dc6");
//
//			Console.Write("" + storages.Count);
//			Console.Write("удаление");
//
//			storages = Storage.Repository.GetAll().ToList();
//			storages.ForEach(b => Console.Write(b.Id));
//			Console.Write("" + storages.Count);

			Test.RunAllTestSets();
		}
	}
}