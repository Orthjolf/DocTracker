using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Domain;

namespace WpfApp.DataProvider
{
	public class SqlServerDataContext : DbContext
	{
		public DbSet<Storage> Storages { get; set; }

		public SqlServerDataContext() : base("DefaultConnection")
		{
//			Database.SetInitializer(new DropCreateDatabaseAlways<SqlServerDataContext>());
		}
	}
}