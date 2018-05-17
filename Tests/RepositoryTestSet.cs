using System;
using System.Linq;
using WpfApp.DataProvider;
using WpfApp.Domain;
using WpfApp.Enum;

namespace WpfApp.Tests
{
	public static class RepositoryTestSet
	{
		public static void Run()
		{
			DataBaseSwitcher.SetActiveDataBase(ConnectionType.Local);
			Test.Run(AddEntity);
			Test.Run(DeleteEntity);
			Test.Run(DeleteEntityById);

			DataBaseSwitcher.SetActiveDataBase(ConnectionType.Remote);
			Test.Run(AddEntity);
			Test.Run(DeleteEntity);
			Test.Run(DeleteEntityById);
		}

		private static bool AddEntity()
		{
			var oldLostOfcontracts = Storage.Repository.GetAll().ToList();
			var oldCountOfcontracts = oldLostOfcontracts.Count;

			var storage = new Storage
			{
				Id = "TestId",
				Address = "TestAddress",
				Description = "TestDescription",
				Name = "TestName"
			};

			Storage.Repository.Add(storage);

			var newListOfContracts = Storage.Repository.GetAll().ToList();
			var newCountOfcontracts = newListOfContracts.Count;

			Storage.Repository.Delete(storage);
			return (newCountOfcontracts == oldCountOfcontracts + 1) && (newListOfContracts.Any(c => c.Id == "TestId"));
		}

		private static bool DeleteEntity()
		{
			var storage = new Storage
			{
				Id = "TestId",
				Address = "TestAddress",
				Description = "TestDescription",
				Name = "TestName"
			};

			Storage.Repository.Add(storage);

			var oldLostOfcontracts = Storage.Repository.GetAll();
			var oldCountOfcontracts = oldLostOfcontracts.Count;

			Storage.Repository.Delete(storage);

			var newListOfContracts = Storage.Repository.GetAll();
			var newCountOfcontracts = newListOfContracts.Count;

			return (newCountOfcontracts == oldCountOfcontracts - 1) && (newListOfContracts.All(c => c.Id != "TestId"));
		}

		private static bool DeleteEntityById()
		{
			var storage = new Storage
			{
				Id = "TestId",
				Address = "TestAddress",
				Description = "TestDescription",
				Name = "TestName"
			};

			Storage.Repository.Add(storage);

			var oldLostOfcontracts = Storage.Repository.GetAll();
			var oldCountOfcontracts = oldLostOfcontracts.Count;

			Storage.Repository.DeleteById("TestId");

			var newListOfContracts = Storage.Repository.GetAll();
			var newCountOfcontracts = newListOfContracts.Count;

			return (newCountOfcontracts == oldCountOfcontracts - 1) && (newListOfContracts.All(c => c.Id != "TestId"));
		}
	}
}