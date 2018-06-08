using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using MongoDB.Bson;
using WpfApp.Domain;
using WpfApp.Enum;

namespace WpfApp.DataProvider.Repository
{
	public interface IDocumentRepository<T>
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

		/// <summary>
		/// Добавить документ в базу данных
		/// </summary>
		/// <param name="entity">Документ</param>
		void Add(T entity);

		/// <summary>
		/// Добавить коллекцию документов в базу данных
		/// </summary>
		/// <param name="entities">Коллекция документов</param>
		void AddAll(List<T> entities);

		/// <summary>
		/// Обновить документ в базе данных
		/// </summary>
		/// <param name="entity">Обновляемый документ</param>
		void Update(T entity);

		/// <summary>
		/// Удалить документ из базы данных
		/// </summary>
		/// <param name="entity">Удаляемый документ</param>
		void Delete(T entity);

		/// <summary>
		/// Удалить документ из базы данных по идентификатору
		/// </summary>
		/// <param name="id">Удаляемый документ</param>
		void DeleteById(string id);

		/// <summary>
		/// Удалить все элементы из таблицы
		/// </summary>
		void DeleteAll();
	}
}