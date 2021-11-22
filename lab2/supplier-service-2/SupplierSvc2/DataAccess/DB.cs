using ServicesContracts;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace SupplierSvc2.DataAccess
{
    public static class DB
    {
        public static Vacancy[] GetVacancies()
        {
            return JsonSerializer.Deserialize<Vacancy[]>(Json.Value);
        }

        public static Vacancy GetVacancyById(string id)
        {
            var vacancies = GetVacancies();
            return vacancies.FirstOrDefault(x => x.Id == id);
        }

        private static readonly Lazy<string> Json = new(() => File.ReadAllText("dataaccess/db.json"));
    }
}
