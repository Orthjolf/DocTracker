using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WpfApp.DataProvider.Repository;
using WpfApp.Domain;

namespace WpfApp.DataProvider.SqlServer
{
	public class SqlServerDataAccessLayer<T> : IDocumentRepository<T> where T : Entity
	{
		private readonly string _type;

		private DbSet<T> _dbSet;

		public SqlServerDataAccessLayer()
		{
			_type = typeof(T).ToString().Split('.').Last();
		}

		public T Get(string id)
		{
			using (var db = new SqlServerDataContext())
			{
				var query = from b in _dbSet select b;
				return query.First(e => e.Id == id);
			}
		}

		public IReadOnlyCollection<T> GetAll()
		{
			var query = from b in _dbSet select b;
			return query.ToList();
		}

		public IReadOnlyCollection<T> GetFiltered(string filter)
		{
			
			var query = from b in _dbSet select b;
			return query.ToList();
		}

		public void Add(T entity)
		{
			throw new System.NotImplementedException();
		}

		public void Update(T entity)
		{
			throw new System.NotImplementedException();
		}

		public void Delete(T entity)
		{
			throw new System.NotImplementedException();
		}

		public void DeleteById(string id)
		{
			throw new System.NotImplementedException();
		}

		public override string ToString()
		{
			return "Sql";
		}
	}
}