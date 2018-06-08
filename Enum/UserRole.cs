using System.ComponentModel;

namespace WpfApp.Enum
{
	public enum UserRole
	{
		[Description("Пользователь")] User,

		[Description("Администратор")] Admin
	}
}