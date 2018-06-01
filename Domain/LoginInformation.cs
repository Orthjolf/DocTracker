using System.Linq;

namespace WpfApp.Domain
{
	public static class LoginInformation
	{
		public static User CurrentUser { get; set; }

		public static void Remember(User user)
		{
			string[] lines = {"First line", "Second line", "Third line"};
			// WriteAllLines creates a file, writes a collection of strings to the file,
			// and then closes the file.  You do NOT need to call Flush() or Close().
			System.IO.File.WriteAllLines(@"WriteLines.txt", lines);
		}

		/// <summary>
		/// Установка информации о текущем пользователе
		/// </summary>
		/// <param name="user"></param>
		public static void SetCurrentUser(User user)
		{
			CurrentUser = user;
		}
	}
}