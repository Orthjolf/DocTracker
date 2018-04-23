using System.Threading.Tasks;
using MongoDB.Bson;
using WpfApp.Enum;

namespace WpfApp.DataProvider.Repository
{
	public class StorageRepository : DocumentRepository
	{
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