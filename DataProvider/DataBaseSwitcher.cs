using System;
using System.Linq;
using WpfApp.DataProvider.Repository;
using WpfApp.Enum;
using WpfApp.Service;

namespace WpfApp.DataProvider
{
	public static class DataBaseSwitcher
	{
		/// <summary>
		/// Переключает активную базу данных в дженерик репозиториях
		/// </summary>
		/// <param name="type">Удаленная или локальная база</param>
		public static void SetActiveDataBase(ConnectionType type)
		{
			var values = System.Enum.GetValues(typeof(DocumentType)).Cast<DocumentType>();
			var types = values.Select(value => Type.GetType("WpfApp.Domain." + value)).ToList();

			types.ForEach(t =>
			{
				var repository = typeof(Repository<>).MakeGenericType(t);
				var method = repository.GetMethod("SetConnectionType");
				method?.Invoke(null, new object[] {type});
			});

			ConsoleWriter.Write(
				"Активная база данных переключена на" +
				(type == ConnectionType.Local ? "MongoDb" : "SqlServer")
			);
		}
	}
}