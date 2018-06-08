using System.Data.Entity;
using WpfApp.Domain;
using WpfApp.Enum;

namespace WpfApp.DataProvider.SqlServer
{
	/// <summary>
	/// Контекст для доступа к базе SqlServer
	/// </summary>
	public class SqlServerDataContext : DbContext
	{
		/// <summary>
		/// Экземпляр контекста
		/// </summary>
		public static SqlServerDataContext Instance => new SqlServerDataContext();

		public DbSet<Box> Boxes { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Storage> Storages { get; set; }
		public DbSet<Contract> Contracts { get; set; }
		public DbSet<LastTimeModifiedTableInfo> TableActualityInfos { get; set; }

		private const string ConnectionString = "DefaultConnection";

		private SqlServerDataContext() : base(ConnectionString)
		{
		}
	}
}