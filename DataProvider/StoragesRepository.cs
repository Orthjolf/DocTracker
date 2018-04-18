//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Runtime.InteropServices;
//using System.Security.Cryptography.X509Certificates;
//using System.Windows.Documents;
//using WpfApp.Backend;
//
//namespace WpfApp.DbMock
//{
//	public class StoragesRepository
//	{
//		private static StoragesRepository _instance;
//
//		public static StoragesRepository Instance()
//		{
//			return _instance ?? (_instance = new StoragesRepository());
//		}
//
//		public readonly List<Storage> StoragesList = new List<Storage>();
//
//		private StoragesRepository()
//		{
//			for (var i = 0; i < 10; i++)
//			{
//				StoragesList.Add(new Storage
//				{
//					Id = i,
//					Address = "Lelin street " + i,
//					Description = "Storage number " + i,
//					Name = "Storage number " + i
//				});
//			}
//		}
//
//		public Storage Get(int id)
//		{
//			if (StoragesList.Select(x => x.Id).Contains(id))
//			{
//				return StoragesList.FirstOrDefault(storage => storage.Id == id);
//			}
//
//			throw new Exception($"Не найдено хранилища с Id = {id}");
//		}
//
//		public List<Storage> GetAll()
//		{
//			return StoragesList;
//		}
//	}
//}