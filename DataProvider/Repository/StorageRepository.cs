using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using WpfApp.Domain;
using WpfApp.Enum;

namespace WpfApp.DataProvider.Repository
{
	public class StorageRepository : DocumentRepository
	{
		private static DocumentType Type => DocumentType.Storage;

		public IEnumerable<Storage> GetAll()
		{
			var bsonDocuments = base.GetAll(Type);
			return bsonDocuments.Select(Storage.Reconstitute).ToList().AsReadOnly();
		}

		public async void AddAndSave(BsonDocument document)
		{
			await AddAndSave(document, Type);
		}

		public async Task DeleteById(string id)
		{
			await DeleteById(id, Type);
		}
	}
}