using System.Collections.Generic;
using System.ComponentModel;
using MongoDB.Bson;
using WpfApp.Domain;
using WpfApp.Enum;

namespace WpfApp.DataProvider.Repository
{
	public interface IDocumentRepository1<T>
	{
		/// <summary>
		/// Получить документ по идентификатору
		/// </summary>
		/// <param name="id">Идентификатор</param>
		/// <returns>Документ из базы</returns>
		T Get(string id);

		/// <summary>
		/// Получить все документы данного типа
		/// </summary>
		/// <returns>Все документы данного типа</returns>
		IReadOnlyCollection<T> GetAll();

		/// <summary>
		/// Получить документы, соответствующие фильтру
		/// </summary>
		/// <param name="filter">Строка-фильтр в формате Json</param>
		/// <returns>Документы, соответствующие фильтру</returns>
		IReadOnlyCollection<T> GetFiltered(string filter);

		void Add(T entity);
	}
}