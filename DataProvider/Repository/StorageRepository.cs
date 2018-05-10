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

		public void AddAndSave(BsonDocument document)
		{
			base.AddAndSave(document, Type);
		}

		public void DeleteById(string id)
		{
			base.DeleteById(id, Type);
		}
	}
}