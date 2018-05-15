using MongoDB.Bson;
using WpfApp.Domain;

namespace WpfApp.DataProvider.MongoDb
{
	public static class BsonDocumentReconstituteExtension
	{
		public static T Reconstitute<T>(this BsonDocument document) where T : Entity
		{
			var s = new Storage()
			{
				Id = document["_id"].ToString(),
				Name = document["Name"].ToString(),
				Address = document["Address"].ToString(),
				Description = document["Description"].ToString()
			};
			return (T) (Entity) s;
		}
	}
}