using System;
using System.Collections.Generic;
using System.Linq;
using WpfApp.DataProvider.Repository;
using WpfApp.Domain;
using WpfApp.Enum;

namespace WpfApp.DataProvider.Synchronizer
{
	public static class DbSynchronizer
	{
		/// <summary>
		/// Информация о датах последних изменений таблиц в локальной базе
		/// </summary>
		private static List<LastTimeModifiedTableInfo> LocalDbActualityInfo { get; set; }

		/// <summary>
		/// Информация о датах последних изменений таблиц в удаленной базе
		/// </summary>
		private static List<LastTimeModifiedTableInfo> RemoteDbActualityInfo { get; set; }

		/// <summary>
		/// Актуальна ли на данный момент локальная база данных.
		/// Определяется путем сравнения дат последнего изменения таблицы
		/// </summary>
		public static bool LocalDbIsActual()
		{
			DataBaseSwitcher.SetActiveDataBase(ConnectionType.Local);
			LocalDbActualityInfo = LastTimeModifiedTableInfo.Repository.GetAll().ToList();

			DataBaseSwitcher.SetActiveDataBase(ConnectionType.Remote);
			RemoteDbActualityInfo = LastTimeModifiedTableInfo.Repository.GetAll().ToList();

			return RemoteDbActualityInfo.Max(i => i.LastTimeModified) <= LocalDbActualityInfo.Max(i => i.LastTimeModified);
		}

		/// <summary>
		/// Обновление локальной базы данных.
		/// Обновляет устаревшие таблицы в локальной базе.
		/// </summary>
		public static void UpdateLocalDb()
		{
			var tablesToUpdate = RemoteDbActualityInfo
				.Where((t, i) => t.LastTimeModified > LocalDbActualityInfo[i].LastTimeModified)
				.Select(t => t.Id).ToList();

			tablesToUpdate.Add("LastTimeModifiedTableInfo");

			var typesToUpdate = tablesToUpdate.Select(s => Type.GetType("WpfApp.Domain." + s)).ToList();

			typesToUpdate.ForEach(t =>
			{
				var updateLocalTableGenericMethod = typeof(DbSynchronizer).GetMethod("UpdateLocalTable")?.MakeGenericMethod(t);
				updateLocalTableGenericMethod?.Invoke(null, null);
			});

			DataBaseSwitcher.SetActiveDataBase(ConnectionType.Remote);
		}

		/// <summary>
		/// Обновить таблицу документов типа Т в локальной базе данных
		/// </summary>
		public static void UpdateLocalTable<T>() where T : Entity
		{
			var repository = (Repository<T>) typeof(T).GetProperty("Repository")?.GetValue(null, null);

			DataBaseSwitcher.SetActiveDataBase(ConnectionType.Remote);
			var documents = repository?.GetAll().ToList();

			DataBaseSwitcher.SetActiveDataBase(ConnectionType.Local);
			repository?.DeleteAll();
			repository?.AddAll(documents);
		}
	}
}