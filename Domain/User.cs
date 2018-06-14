using WpfApp.DataProvider.Repository;
using WpfApp.Enum;

namespace WpfApp.Domain
{
	public class User : Entity
	{
		public static Repository<User> Repository => Repository<User>.Instance;

		/// <summary>
		/// Логин
		/// </summary>
		public string Login { get; set; }

		/// <summary>
		/// Зашифрованный пароль
		/// </summary>
		public string PasswordHash { get; set; }

		/// <summary>
		/// Имя пользователя
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Роль
		/// </summary>
		public UserRole Role { get; set; }
	}
}