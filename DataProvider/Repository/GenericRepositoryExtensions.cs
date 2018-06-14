using System.Collections.Generic;
using System.Linq;
using WpfApp.Domain;

namespace WpfApp.DataProvider.Repository
{
	/// <summary>
	/// Набор методов, расширяющих дженерик репозитории
	/// </summary>
	public static class GenericRepositoryExtensions
	{
		/// <summary>
		/// Получить контракты по идентификатору коробки
		/// </summary>
		/// <param name="repository"></param>
		/// <param name="boxId">Id коробки</param>
		/// <returns>Коллекция контрактов</returns>
		public static IReadOnlyCollection<Contract> GetByBoxId(this Repository<Contract> repository, string boxId)
		{
			return repository.GetAll().Where(c => c.BoxId == boxId).ToList();
		}

		/// <summary>
		/// Получить пользователя по имени
		/// </summary>
		/// <param name="repository"></param>
		/// <param name="name">Имя пользоваля</param>
		/// <returns>Пользователь</returns>
		public static User GetByName(this Repository<User> repository, string name)
		{
			return repository.GetAll().FirstOrDefault(u => u.Name == name);
		}

		/// <summary>
		/// Получить коробки по идентификатору склада
		/// </summary>
		/// <param name="repository"></param>
		/// <param name="storageId">Идентификатор склада</param>
		/// <returns>Коллекция коробок</returns>
		public static IReadOnlyCollection<Box> GetByStrorageId(this Repository<Box> repository, string storageId)
		{
			return repository.GetAll().Where(b => b.StorageId == storageId).ToList();
		}
	}
}