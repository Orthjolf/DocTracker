using System;
using WpfApp.DataProvider.Repository;
using WpfApp.Enum;

namespace WpfApp.Domain
{
	public class User : Entity
	{
		public new static Repository<User> Repository => Repository<User>.Instance;

		public string Login { get; set; }

		public string PasswordHash { get; set; }

		public string Name { get; set; }

		public UserRole Role { get; set; }
	}
}