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