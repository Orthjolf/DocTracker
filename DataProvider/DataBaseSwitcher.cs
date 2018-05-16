using System;
using System.Collections.Generic;
using System.Linq;
using WpfApp.DataProvider.Repository;
using WpfApp.Enum;

namespace WpfApp.DataProvider
{
	public static class DataBaseSwitcher
	{
		public static void SetActiveDataBase(ConnectionType type)
		{
			var values = System.Enum.GetValues(typeof(DocumentType));

			var types = new List<Type>();
			foreach (var value in values)
			{
				types.Add(Type.GetType("WpfApp.Domain." + value));
			}


			foreach (var t in types)
			{
				var repository = typeof(Repository<>).MakeGenericType(t);

				var method = repository.GetMethod("SetConnectionType");

				method?.Invoke(null, new object[] {type});

//				var dataAccessLayer = repository.GetProperty("DataAccessLayer");
//				var sql = typeof(SqlServerDataAccessLayer<>).MakeGenericType(t);
//				var mongo = typeof(MongoDbDataAccessLayer<>).MakeGenericType(t);
//				dataAccessLayer.SetValue(null, type == ConnectionType.Local ? mongo : sql);
			}
		}
	}
}