using System.Data.Entity.Migrations;
using WpfApp.DataProvider.SqlServer;

namespace WpfApp.Migrations
{
	internal sealed class Configuration : DbMigrationsConfiguration<SqlServerDataContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
		}

		protected override void Seed(SqlServerDataContext context)
		{
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data.
		}
	}
}