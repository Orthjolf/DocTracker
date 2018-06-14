using System.Collections.Generic;
using WpfApp.DataProvider.Repository;
using WpfApp.Domain;

namespace WpfApp.DataProvider.WebApi
{
	public class WebApiDataAccessLayer<T> : IDocumentRepository<T> where T:Entity
	{
		public T Get(string id)
		{
			throw new System.NotImplementedException();
		}

		public IReadOnlyCollection<T> GetAll()
		{
			throw new System.NotImplementedException();
		}

		public IReadOnlyCollection<T> GetFiltered(string filter)
		{
			throw new System.NotImplementedException();
		}

		public void Add(T entity)
		{
			throw new System.NotImplementedException();
		}

		public void AddAll(List<T> entities)
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

		public void DeleteAll()
		{
			throw new System.NotImplementedException();
		}
	}
}