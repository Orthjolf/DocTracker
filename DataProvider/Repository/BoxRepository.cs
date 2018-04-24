using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using WpfApp.Domain;
using WpfApp.Enum;

namespace WpfApp.DataProvider.Repository
{
	public class BoxRepository : DocumentRepository
	{
		private static DocumentType Type => DocumentType.Box;

		public IEnumerable<Box> GetByStorageId(string storageId)
		{
			var filter = new BsonDocument
			{
				{"StorageId", storageId},
			};

			var bsonDocuments = GetFiltered(filter, Type);
			return bsonDocuments.Select(Box.Reconstitute).ToList().AsReadOnly();
		}

		public IEnumerable<Box> GetAll()
		{
			var bsonDocuments = base.GetAll(Type);
			return bsonDocuments.Select(Box.Reconstitute).ToList().AsReadOnly();
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