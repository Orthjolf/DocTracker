using System.Linq;
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
	}
}