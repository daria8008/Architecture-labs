using DataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace RecruitingAgencySvc.Controllers
{
    [ApiController]
    [Route("vacancies")]
    public class VacanciesController : ControllerBase
    {
        public VacanciesController()
        {
            _vacanciesDA = new VacanciesDataAccess();
        }

        [HttpGet("by-name-and-salary")]
        public async Task<IActionResult> GetVacanciesByNameAndSalary([FromQuery] string name, [FromQuery] int salary)
        {
            var res = await _vacanciesDA.GetVacanciesByNameAndSalaryAsync(name, salary);
            return new JsonResult(res);
        }

        private readonly VacanciesDataAccess _vacanciesDA;
    }
}
