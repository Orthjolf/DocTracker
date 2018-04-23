using MongoDB.Bson;
using WpfApp.Enum;

namespace WpfApp.DataProvider.Repository
{
	public class StorageRepository : DocumentRepository
	{
		public async void AddAndSave(BsonDocument document)
		{
			await base.AddAndSave(document, DocumentType.Storage);
		}
	}
}