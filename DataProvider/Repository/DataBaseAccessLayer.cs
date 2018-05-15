using System.Collections.Generic;
using WpfApp.DataProvider.MongoDb;
using WpfApp.Domain;
using WpfApp.Enum;

namespace WpfApp.DataProvider.Repository
{
	/// <summary>
	/// Класс предоставляет набор методов для доступа к данным из локальной и удаленной базы данных,
	/// а так же переключение используемой активной базы данных с локальной на удаленную и наоборот
	/// </summary>
	/// <typeparam name="T">Тип репозитория документов</typeparam>
	public class Repository<T> : IDocumentRepository1<T> where T : Entity
	{
		public Repository()
		{
			SetConnectionType(ConnectionType.Local);
		}

		private static IDocumentRepository1<T> _dataAccessLayer;

		/// <summary>
		/// Переключение активной базы данных с локальной на удаленную и наоборот
		/// </summary>
		/// <param name="type">Тип соединения с базой</param>
		public void SetConnectionType(ConnectionType type)
		{
			_dataAccessLayer = type == ConnectionType.Local
				? new MongoDbDataAccessLayer<T>()
				: null;
		}

		public T Get(string id)
		{
			return _dataAccessLayer.Get(id);
		}

		public IReadOnlyCollection<T> GetAll()
		{
			return _dataAccessLayer.GetAll();
		}

		public IReadOnlyCollection<T> GetFiltered(string filter)
		{
			return _dataAccessLayer.GetFiltered(filter);
		}

		public void Add(T entity)
		{
			_dataAccessLayer.Add(entity);
		}
	}
}