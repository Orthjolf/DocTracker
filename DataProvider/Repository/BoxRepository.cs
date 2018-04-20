using System.Collections.Generic;
using MongoDB.Bson;
using WpfApp.Enum;

namespace WpfApp.DataProvider.Repository
{
	public class BoxRepository : DocumentRepository
	{
		public IReadOnlyCollection<BsonDocument> GetByStorageId(string storageId)
		{
			var filter = new BsonDocument
			{
				{"StorageId", storageId},
			};
			return base.GetFiltered(filter, DocumentType.Box);
		}
	}
}