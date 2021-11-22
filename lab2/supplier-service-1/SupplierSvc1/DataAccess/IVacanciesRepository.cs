using ServicesContracts;
using System.Collections.Generic;

namespace SupplierSvc1.DataAccess
{
    public interface IVacanciesRepository
    {
        Vacancy[] GetVacanciesByFilter(Dictionary<string, string> filterParams);
    }
}
