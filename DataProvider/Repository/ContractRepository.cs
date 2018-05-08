using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using WpfApp.Domain;
using WpfApp.Enum;

namespace WpfApp.DataProvider.Repository
{
	public class ContractRepository : DocumentRepository
	{
		private static DocumentType Type => DocumentType.Contract;

		public IEnumerable<Contract> GetByBoxId(string boxId)
		{
			var filter = new BsonDocument
			{
				{"BoxId", boxId},
			};

			var bsonDocuments = GetFiltered(filter, Type);
			return bsonDocuments.Select(Contract.Reconstitute).ToList().AsReadOnly();
		}

		public IEnumerable<Contract> GetAll()
		{
			var bsonDocuments = base.GetAll(Type);
			return bsonDocuments.Select(Contract.Reconstitute).ToList().AsReadOnly();
		}

		public async void AddAndSave(BsonDocument document)
		{
			await AddAndSave(document, Type);
		}

		public async void DeleteById(string id)
		{
			await DeleteById(id, Type);
		}
	}
}