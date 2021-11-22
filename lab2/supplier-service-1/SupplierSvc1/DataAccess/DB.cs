using ServicesContracts;
using SupplierSvc1.Filtering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace SupplierSvc1.DataAccess
{
    public static class DB
    {
        public static Vacancy[] GetVacanciesByFilter(Dictionary<string, string> filterParams)
        {
            var vacancies = JsonSerializer.Deserialize<Vacancy[]>(Json.Value);
            var filteredVacancies = vacancies
                .Where(vacancy =>
                {
                    foreach (var filterParam in filterParams)
                    {
                        var filter = FilterConfigurator.DefaultSettings.GetFor<Vacancy>(filterParam.Key);
                        if (!filter(vacancy, filterParam.Value))
                        {
                            return false;
                        }
                    }
                    return true;
                })
                .ToArray();

            return filteredVacancies;
        }

        private static readonly Lazy<string> Json = new(() => File.ReadAllText("dataaccess/db.json"));
    }
}
