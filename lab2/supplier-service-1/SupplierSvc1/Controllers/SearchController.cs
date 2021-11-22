using Microsoft.AspNetCore.Mvc;
using SupplierSvc1.DataAccess;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace SupplierSvc1.Controllers
{
    [ApiController]
    [Route("search")]
    public class SearchController : ControllerBase
    {
        public SearchController(IVacanciesRepository vacanciesRepo)
        {
            _vacanciesRepo = vacanciesRepo;
        }

        [HttpGet]
        public async Task<JsonResult> GetVacanciesByFilter([FromQuery]Dictionary<string, string> filters)
        {
            var vacancies = _vacanciesRepo.GetVacanciesByFilter(filters);
            return new JsonResult(vacancies);
        }

        private readonly IVacanciesRepository _vacanciesRepo;
    }
}
