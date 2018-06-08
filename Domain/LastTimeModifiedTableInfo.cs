using System;
using WpfApp.DataProvider.Repository;

namespace WpfApp.Domain
{
	public class LastTimeModifiedTableInfo : Entity
	{
		public static Repository<LastTimeModifiedTableInfo> Repository => Repository<LastTimeModifiedTableInfo>.Instance;

		public long LastTimeModified { get; set; }

		public LastTimeModifiedTableInfo()
		{
		}

		public LastTimeModifiedTableInfo(string tableName)
		{
			Id = tableName;
			LastTimeModified = DateTime.Now.Ticks;
		}
	}
}