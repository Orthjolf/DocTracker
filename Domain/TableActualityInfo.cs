using System;
using WpfApp.DataProvider.Repository;

namespace WpfApp.Domain
{
	public class TableActualityInfo : Entity
	{
		public static Repository<TableActualityInfo> Repository =>
			new Lazy<Repository<TableActualityInfo>>(() => new Repository<TableActualityInfo>()).Value;

		public string TableName { get; set; }

		public DateTime LastTimeUpdated { get; set; }
	}
}