using ServicesContracts;
using System;
using System.Collections.Generic;

namespace SupplierSvc1.DataAccess
{
    public class VacanciesEmptyRepository : IVacanciesRepository
    {
        public Vacancy[] GetVacanciesByFilter(Dictionary<string, string> filterParams)
        {
            return new Vacancy[0];
        }
    }
}
