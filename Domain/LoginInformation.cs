using System.IO;
using WpfApp.SubPages;

namespace WpfApp.Domain
{
	public static class LoginInformation
	{
		public static User CurrentUser { get; set; }

		/// <summary>
		/// Название файла, в котором хранится Id ранее вошедшего пользователя
		/// </summary>
		private const string FileName = @"UserInfo.txt";

		/// <summary>
		/// Запоминает пользователя. Сохраняет Id пользователя в текстовый файл
		/// </summary>
		/// <param name="user">Пользователь</param>
		public static void Remember(User user)
		{
			string[] lines = {user.Id};
			File.WriteAllLines(FileName, lines);
		}

		/// <summary>
		/// "Забыть" пользователя. Удалить из системы информацию о ранее вошедшем пользователе
		/// </summary>
		public static void Forget()
		{
			if (File.Exists(FileName))
				File.Delete(FileName);
		}

		/// <summary>
		/// Установка информации о текущем пользователе
		/// </summary>
		/// <param name="user">Пользователь</param>
		public static void SetCurrentUser(User user)
		{
			CurrentUser = user;
		}

		/// <summary>
		/// Возвращает последнего пользователя, если таковой был
		/// </summary>
		/// <returns>Последний вошедший пользователь</returns>
		public static User GetLastUser()
		{
			if (!File.Exists(FileName)) return null;
			var id = File.ReadAllLines(FileName)[0];
			return User.Repository.Get(id);
		}
	}
}