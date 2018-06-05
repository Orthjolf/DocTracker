using System;
using WpfApp.DataProvider.Repository;

namespace WpfApp.Domain
{
	public class TableActualityInfo : Entity
	{
		public static Repository<TableActualityInfo> Repository => Repository<TableActualityInfo>.Instance;

		public string TableName { get; set; }

		public DateTime LastTimeUpdated { get; set; }
	}
}