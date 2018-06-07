using System.Collections.Generic;
using WpfApp.DataProvider.MongoDb;
using WpfApp.DataProvider.SqlServer;
using WpfApp.Domain;
using WpfApp.Enum;
using WpfApp.Service;

namespace WpfApp.DataProvider.Repository
{
	/// <summary>
	/// Класс предоставляет набор методов для доступа к данным из локальной и удаленной базы данных,
	/// а так же переключение используемой активной базы данных с локальной на удаленную и наоборот
	/// </summary>
	/// <typeparam name="T">Тип репозитория документов</typeparam>
	public class Repository<T> : IDocumentRepository<T> where T : Entity
	{
		private static Repository<T> _instance;

		public static Repository<T> Instance => _instance ?? (_instance = new Repository<T>());

		private Repository()
		{
		}

		private static IDocumentRepository<T> _dataAccessLayer;

		/// <summary>
		/// Переключение активной базы данных с локальной на удаленную и наоборот
		/// </summary>
		/// <param name="type">Тип соединения с базой</param>
		public static void SetConnectionType(ConnectionType type)
		{
			_dataAccessLayer = type == ConnectionType.Local
				? (IDocumentRepository<T>) new MongoDbDataAccessLayer<T>()
				: new SqlServerDataAccessLayer<T>();
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

		public void AddAll(List<T> entities)
		{
			_dataAccessLayer.AddAll(entities);
		}

		public void Update(T entity)
		{
			_dataAccessLayer.Update(entity);
		}

		public void Delete(T entity)
		{
			_dataAccessLayer.Delete(entity);
		}

		public void DeleteById(string id)
		{
			_dataAccessLayer.DeleteById(id);
		}

		public void DeleteAll()
		{
			_dataAccessLayer.DeleteAll();
		}
	}
}