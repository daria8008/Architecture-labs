using Microsoft.AspNetCore.Mvc;
using SupplierSvc2.DataAccess;
using System.Threading.Tasks;

namespace SupplierSvc2.Controllers
{
    [ApiController]
    [Route("vacancies")]
    public class VacanciesController : ControllerBase
    {
        [HttpGet("list")]
        public async Task<IActionResult> GetVacancies()
        {
            var vacancies = DB.GetVacancies();
            return new JsonResult(vacancies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVacancyById(string id)
        {
            var vacancy = DB.GetVacancyById(id);

            return vacancy != null
                ? new JsonResult(vacancy)
                : new NotFoundResult();
        }
    }
}
