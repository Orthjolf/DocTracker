using System.Collections.Generic;
using System.ComponentModel;
using MongoDB.Bson;
using WpfApp.Domain;
using WpfApp.Enum;

namespace WpfApp.DataProvider.Repository
{
	public interface IDocumentRepository
	{
		/// <summary>
		/// Получить документ по идентификатору
		/// </summary>
		/// <param name="id">Идентификатор</param>
		/// <param name="type">Тип</param>
		/// <returns>Документ из базы</returns>
		Storage Get(string id, DocumentType type);

		/// <summary>
		/// Получить все документы данного типа
		/// </summary>
		/// <returns></returns>
		IReadOnlyCollection<BsonDocument> GetAll(DocumentType type);

		/// <summary>
		/// Получить все документы
		/// </summary>
		/// <returns></returns>
		IReadOnlyCollection<BsonDocument> GetFiltered(BsonDocument filter, DocumentType type);
	}
}