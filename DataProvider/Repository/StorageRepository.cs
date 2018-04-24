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
		public IEnumerable<Storage> GetAll()
		{
			var bsonDocuments = base.GetAll(DocumentType.Storage);
			return bsonDocuments.Select(Storage.Reconstitute).ToList().AsReadOnly();
		}

		public static async void AddAndSave(BsonDocument document)
		{
			await AddAndSave(document, DocumentType.Storage);
		}

		public static async Task DeleteById(string id)
		{
			await DeleteById(id, DocumentType.Storage);
		}
	}
}