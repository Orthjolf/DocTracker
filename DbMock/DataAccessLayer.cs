using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver;

namespace WpfApp.DbMock
{
	public class DataAccessLayer
	{
		public static void TestDb()
		{
			MainAsync().Wait();

			Console.WriteLine("Press enter to exit");
			Console.ReadLine();
		}

		static async Task<Task<IAsyncCursor<BsonDocument>>> MainAsync()
		{
			var client = new MongoClient();

			IMongoDatabase db = client.GetDatabase("schoool");

			var collection = db.GetCollection<BsonDocument>("students");
			var newStudents = CreateNewStudents();

			await collection.InsertManyAsync(newStudents);

			var a = db.ListCollectionsAsync();
			return a;
		}

		private static IEnumerable<BsonDocument> CreateNewStudents()
		{
			var student1 = new BsonDocument
			{
				{"firstname", "Ugo"},
				{"lastname", "Damian"},
				{"subjects", new BsonArray {"English", "Mathematics", "Physics", "Biology"}},
				{"class", "JSS 3"},
				{"age", 23}
			};

			var student2 = new BsonDocument
			{
				{"firstname", "Julie"},
				{"lastname", "Lerman"},
				{"subjects", new BsonArray {"English", "Mathematics", "Spanish"}},
				{"class", "JSS 3"},
				{"age", 23}
			};

			var student3 = new BsonDocument
			{
				{"firstname", "Julie"},
				{"lastname", "Lerman"},
				{"subjects", new BsonArray {"English", "Mathematics", "Physics", "Chemistry"}},
				{"class", "JSS 1"},
				{"age", 25}
			};

			var newStudents = new List<BsonDocument>();
			newStudents.Add(student1);
			newStudents.Add(student2);
			newStudents.Add(student3);

			return newStudents;
		}
	}
}