using System;
using MongoDB.Bson;
using WpfApp.Domain;

namespace WpfApp.DataProvider.MongoDb
{
	public static class BsonDocumentReconstituteExtension
	{
		public static T Reconstitute<T>(this BsonDocument document) where T : Entity
		{
			if (typeof(T) == typeof(Storage))
				return (T) (Entity) ReconstituteToStorage(document);
			if (typeof(T) == typeof(Box))
				return (T) (Entity) ReconstituteToBox(document);
			if (typeof(T) == typeof(Contract))
				return (T) (Entity) ReconstituteToContract(document);
			return null;
		}

		private static Box ReconstituteToBox(BsonValue document)
		{
			DateTime? minDate = document["MinDate"].ToUniversalTime();
			DateTime? maxDate = document["MaxDate"].ToUniversalTime();
			if (minDate == DateTime.MinValue) minDate = null;
			if (maxDate == DateTime.MinValue) maxDate = null;
			return new Box
			{
				Id = document["_id"].ToString(),
				Name = document["Name"].AsString,
				StorageId = document["StorageId"].AsString,
				MinDate = minDate,
				MaxDate = maxDate,
				Description = document["Description"].AsString,
				ContractsCount = document["ContractsCount"].AsInt32
			};
		}

		private static Storage ReconstituteToStorage(BsonValue document)
		{
			return new Storage()
			{
				Id = document["_id"].ToString(),
				Name = document["Name"].ToString(),
				Address = document["Address"].ToString(),
				Description = document["Description"].ToString()
			};
		}

		private static Contract ReconstituteToContract(BsonValue document)
		{
			return new Contract
			{
				Id = document["_id"].ToString(),
				Number = document["Number"].AsString,
				BoxId = document["BoxId"].AsString,
				ClientFirstName = document["ClientFirstName"].AsString,
				ClientLastName = document["ClientLastName"].AsString,
				ClientPatronymic = document["ClientPatronymic"].AsString,
				PhoneNumber = document["PhoneNumber"].AsString,
				LoanId = document["LoanId"].AsString,
				PrefixOfPlace = document["PrefixOfPlace"].AsString,
				ContractDate = document["ContractDate"].ToUniversalTime()
			};
		}
	}
}