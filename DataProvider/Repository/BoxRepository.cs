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

		public Box Get(string boxId)
		{
			var bsonDocument = base.Get(boxId, Type);
			return Box.Reconstitute(bsonDocument);
		}

		public async void Update(Box box)
		{
			await GetCollection(Type).ReplaceOneAsync(
				new BsonDocument("_id", new ObjectId(box.Id)),
				box.Serialize()
			);
		}

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