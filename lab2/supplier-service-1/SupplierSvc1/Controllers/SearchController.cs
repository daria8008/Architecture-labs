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
        [HttpGet]
        public async Task<JsonResult> GetVacanciesByFilter([FromQuery]Dictionary<string, string> filters)
        {
            var vacancies = DB.GetVacanciesByFilter(filters);
            return new JsonResult(vacancies);
        }
    }
}
