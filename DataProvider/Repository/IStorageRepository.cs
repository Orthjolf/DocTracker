using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using WpfApp.Domain;

namespace WpfApp.DataProvider.Repository
{
	public interface IStorageRepository
	{
		/// <summary>
		/// Получить хранилище по идентификатору
		/// </summary>
		/// <param name="id">Идентификатор</param>
		/// <returns>Хранилище</returns>
		Storage Get(int id);

		/// <summary>
		/// Получить все хранилища
		/// </summary>
		/// <returns></returns>
		IReadOnlyCollection<BsonDocument> GetAll();
	}
}