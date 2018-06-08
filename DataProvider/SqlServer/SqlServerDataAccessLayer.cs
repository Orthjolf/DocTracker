using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using WpfApp.DataProvider.Repository;
using WpfApp.DataProvider.Synchronizer;
using WpfApp.Domain;

namespace WpfApp.DataProvider.SqlServer
{
	public class SqlServerDataAccessLayer<T> : IDocumentRepository<T> where T : Entity
	{
		private readonly DbSet<T> _dbSet;

		private readonly SqlServerDataContext _db;

		public SqlServerDataAccessLayer()
		{
			_db = SqlServerDataContext.Instance;
			_dbSet = _db.Set<T>();
		}

		public T Get(string id)
		{
			try
			{
				return _dbSet.First(e => e.Id == id);
			}
			catch (Exception e)
			{
				return null;
			}
		}

		public IReadOnlyCollection<T> GetAll()
		{
			return _dbSet.ToList();
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
			UpdateActualityInfo();
		}

		public void AddAll(List<T> entities)
		{
			entities.ForEach(e => _dbSet.AddOrUpdate(e));
			_db.SaveChanges();
			UpdateActualityInfo();
		}

		public void Update(T entity)
		{
			_dbSet.AddOrUpdate(entity);
			_db.SaveChanges();
			UpdateActualityInfo();
		}

		public void Delete(T entity)
		{
			_dbSet.Remove(_dbSet.First(e => e.Id == entity.Id));
			_db.SaveChanges();
			UpdateActualityInfo();
		}

		public void DeleteById(string id)
		{
			_dbSet.Remove(_dbSet.First(e => e.Id == id));
			_db.SaveChanges();
			UpdateActualityInfo();
		}

		public void DeleteAll()
		{
			throw new NotImplementedException();
		}

		public override string ToString()
		{
			return "Sql";
		}

		/// <summary>
		/// Обновить время обновления таблицы
		/// </summary>
		private static void UpdateActualityInfo()
		{
			if (typeof(T) == typeof(LastTimeModifiedTableInfo)) return;
			var tableName = typeof(T).ToString().Split('.').Last();

			var actualityInfo = LastTimeModifiedTableInfo.Repository.Get(tableName);
			if (actualityInfo == null)
			{
				LastTimeModifiedTableInfo.Repository.Add(new LastTimeModifiedTableInfo(tableName));
			}
			else
			{
				LastTimeModifiedTableInfo.Repository.Update(new LastTimeModifiedTableInfo(tableName));
			}
		}
	}
}