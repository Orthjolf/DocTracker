using System;
using System.Linq;
using WpfApp.DataProvider;
using WpfApp.Domain;
using WpfApp.Enum;
using WpfApp.Service;
using Console = WpfApp.Service.Console;

namespace WpfApp.Temp
{
	public static class localDbSeed
	{
		public static void Generate()
		{
			var values = System.Enum.GetValues(typeof(DocumentType)).Cast<DocumentType>().Select(v => v.ToString()).ToList();

			if (LastTimeModifiedTableInfo.Repository.GetAll().Count > 0) return;

			foreach (var value in values)
			{
				LastTimeModifiedTableInfo.Repository.Add(new LastTimeModifiedTableInfo
				{
					Id = value,
					LastTimeModified = DateTime.MinValue.ToBinary()
				});
			}

//			User.Repository.Add(new User
//			{
//				Id = "1",
//				Login = "1",
//				Name = "1",
//				PasswordHash = "1",
//				Role = UserRole.Admin
//			});

			DataBaseSwitcher.SetActiveDataBase(ConnectionType.Local);

			foreach (var value in values)
			{
				LastTimeModifiedTableInfo.Repository.Add(new LastTimeModifiedTableInfo
				{
					Id = value,
					LastTimeModified = DateTime.MinValue.ToBinary()
				});
			}

			DataBaseSwitcher.SetActiveDataBase(ConnectionType.Remote);
		}

		public static void InitRepositories()
		{
			Console.Write(Box.Repository.ToString());
			Console.Write(Storage.Repository.ToString());
			Console.Write(User.Repository.ToString());
			Console.Write(LastTimeModifiedTableInfo.Repository.ToString());
			Console.Write(Contract.Repository.ToString());

			DataBaseSwitcher.SetActiveDataBase(ConnectionChecker.ConnectionIsAvailable
				? ConnectionType.Remote
				: ConnectionType.Local);
		}
	}
}