using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MongoDB
{
    public class MongodbClient
    {
        public async Task<T[]> GetAsync<T>(string collectionName, FilterDefinition<BsonDocument> filter, int pageNumber = 0, int pageSize = 200)
        {
            var collection = Instance.GetCollection<BsonDocument>(collectionName);
            var bsonData = await collection.Find(filter).SortBy(bson => bson["Id"]).Skip(pageNumber * pageSize).Limit(pageSize).ToListAsync();
            var data = bsonData
                .Select(x =>
                {
                    var json = x.ToJson();
                    var startIndex = json.IndexOf("\"");
                    var endIndex = json.IndexOf(",");
                    json = json.Remove(startIndex, endIndex - startIndex + 1);
                    return json;
                })
                .Select(x => JsonSerializer.Deserialize<T>(x))
                .ToArray();
            return data;
        }

        //public async Task<List<T>> GetAsync<T>(string collectionName, int pageNumber, int pageSize)
        //{
        //    var collection = Instance.GetCollection<BsonDocument>(collectionName);
        //    var bsonData = await collection.Find().ToListAsync();
        //    var data = bsonData
        //        .Select(x =>
        //        {
        //            var json = x.ToJson();
        //            var startIndex = json.IndexOf("\"");
        //            var endIndex = json.IndexOf(",");
        //            json = json.Remove(startIndex, endIndex - startIndex + 1);
        //            return json;
        //        })
        //        .Select(x => JsonSerializer.Deserialize<T>(x))
        //        .ToList();
        //    return data;
        //}

        private static IMongoDatabase Instance { get; } = InitClient();

        private static IMongoDatabase InitClient()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("local");

            return db;
        }
    }
}
