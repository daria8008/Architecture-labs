using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using ServicesContracts;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SupplierSvc2.DataAccess
{
    public static class DB
    {
        static DB()
        {
            _mongo = new();
        }
        public static async Task<Vacancy[]> GetVacanciesAsync(int pageNumber, int pageSize)
        {
            return await _mongo.GetAsync<Vacancy>("vacancies_supp_2", FilterDefinition<BsonDocument>.Empty, pageNumber, pageSize);
            //return JsonSerializer.Deserialize<Vacancy[]>(Json.Value);
        }

        //public static Vacancy GetVacancyById(string id)
        //{
        //    var vacancies = GetVacancies();
        //    return vacancies.FirstOrDefault(x => x.Id == id);
        //}

        //private static readonly Lazy<string> Json = new(() => File.ReadAllText("dataaccess/db.json"));
        private static readonly MongodbClient _mongo;
    }
}
