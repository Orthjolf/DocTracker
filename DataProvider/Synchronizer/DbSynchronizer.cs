using System;
using System.Linq;
using System.Reflection;
using WpfApp.DataProvider.Repository;
using WpfApp.Domain;
using WpfApp.Enum;

namespace WpfApp.DataProvider.Synchronizer
{
	public static class DbSynchronizer
	{
		/// <summary>
		/// Проверка локальной базы данных на актуальность
		/// </summary>
		/// <returns>Актуальна ли локальная база данных</returns>
		public static bool IsLocalDbActual()
		{
			DataBaseSwitcher.SetActiveDataBase(ConnectionType.Local);

			var localTableActualityInfos = TableActualityInfo.Repository.GetAll();
			if (localTableActualityInfos == null) return false;

			DataBaseSwitcher.SetActiveDataBase(ConnectionType.Remote);
			var remoteTalbeActualityInfos = TableActualityInfo.Repository.GetAll();

			return localTableActualityInfos.Max(i => i.LastTimeUpdated) < remoteTalbeActualityInfos.Max(i => i.LastTimeUpdated);
		}

		/// <summary>
		/// Обновление локальной базы данных.
		/// Проходит по всем типам документов, описанных в Enum DocumentType и поочередно обновляет таблицы
		/// </summary>
		public static void UpdateLocalDb()
		{
			var enumValues = System.Enum.GetValues(typeof(DocumentType)).Cast<DocumentType>();
			var typesOfDocuments = enumValues.Select(value => Type.GetType("WpfApp.Domain." + value)).ToList();

			typesOfDocuments.ForEach(t =>
			{
				var updateLocalTable = typeof(DbSynchronizer).GetMethod("UpdateLocalTable")?.MakeGenericMethod(t);
				updateLocalTable?.Invoke(null, null);
			});

			DataBaseSwitcher.SetActiveDataBase(ConnectionType.Remote);
		}

		/// <summary>
		/// Обновить таблицу документов типа Т в локальной базе данных
		/// </summary>
		public static void UpdateLocalTable<T>() where T : Entity
		{
			DataBaseSwitcher.SetActiveDataBase(ConnectionType.Remote);
			var repository = (Repository<T>) typeof(T).GetProperty("Repository")?.GetValue(null, null);
			var documents = repository?.GetAll().ToList();

			DataBaseSwitcher.SetActiveDataBase(ConnectionType.Local);
			repository?.AddAll(documents);
		}
	}
}