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
	public class Repository<T> : IDocumentRepository1<T> where T : Entity
	{
		public Repository()
		{
			ConsoleWriter.Write("Новый экземпляр репозитория " + typeof(T));
			SetConnectionType(ConnectionType.Local);
		}

		public static IDocumentRepository1<T> DataAccessLayer;

		/// <summary>
		/// Переключение активной базы данных с локальной на удаленную и наоборот
		/// </summary>
		/// <param name="type">Тип соединения с базой</param>
		public static void SetConnectionType(ConnectionType type)
		{
			DataAccessLayer = type == ConnectionType.Local
				? (IDocumentRepository1<T>) new MongoDbDataAccessLayer<T>()
				: new SqlServerDataAccessLayer<T>();
			ConsoleWriter.Write(typeof(T) + " теперь берет данные из " + (type == ConnectionType.Local ? "монго" : "sql"));
		}

		public T Get(string id)
		{
			return DataAccessLayer.Get(id);
		}

		public IReadOnlyCollection<T> GetAll()
		{
			return DataAccessLayer.GetAll();
		}

		public IReadOnlyCollection<T> GetFiltered(string filter)
		{
			return DataAccessLayer.GetFiltered(filter);
		}

		public void Add(T entity)
		{
			DataAccessLayer.Add(entity);
		}
	}
}