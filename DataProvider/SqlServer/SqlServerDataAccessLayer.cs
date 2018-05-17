using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using WpfApp.DataProvider.Repository;
using WpfApp.Domain;

namespace WpfApp.DataProvider.SqlServer
{
	public class SqlServerDataAccessLayer<T> : IDocumentRepository<T> where T : Entity
	{
		private readonly string _type;

		private readonly DbSet<T> _dbSet;

		private readonly SqlServerDataContext _db;

		public SqlServerDataAccessLayer()
		{
			_type = typeof(T).ToString().Split('.').Last();
			_db = SqlServerDataContext.Instance;

			_dbSet = _db.Set<T>();
		}

		public T Get(string id)
		{
			var query = from b in _dbSet select b;
			return query.First(e => e.Id == id);
		}

		public IReadOnlyCollection<T> GetAll()
		{
			var set = _dbSet;
			var query = from b in set select b;
			return query.ToList();
		}

		public IReadOnlyCollection<T> GetFiltered(string filter)
		{
			var query = from b in _dbSet select b;
			return query.ToList();
			
		}

		public void Add(T entity)
		{
			_dbSet.Add(entity);
			_db.SaveChanges();
		}

		public void Update(T entity)
		{
			_dbSet.AddOrUpdate(entity);
			_db.SaveChanges();
		}

		public void Delete(T entity)
		{
			_dbSet.Remove(entity);
			_db.SaveChanges();
		}

		public void DeleteById(string id)
		{
			_dbSet.Remove(_dbSet.First(e => e.Id == id));
			_db.SaveChanges();
		}

		public override string ToString()
		{
			return "Sql";
		}
	}
}