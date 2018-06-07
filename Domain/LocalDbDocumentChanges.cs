using System;
using WpfApp.DataProvider.Repository;
using WpfApp.Enum;

namespace WpfApp.Domain
{
	public class LocalDbDocumentChanges : Entity
	{
		public static Repository<LocalDbDocumentChanges> Repository = Repository<LocalDbDocumentChanges>.Instance;

		public DbOperation Operation { get; set; }

		public string UserId { get; set; }

		public DateTime DateTime { get; set; }
	}
}