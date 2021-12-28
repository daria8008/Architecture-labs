using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ServicesContracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecruitingAgencySvc.Controllers
{
    public static class CacheKeys
    {
        public static string Vanacies(string name, int salary) => $"vacancies_{name}_{salary}";
    }

    [ApiController]
    [Route("vacancies")]
    public class VacanciesController : ControllerBase
    {
        public VacanciesController(IMemoryCache memoryCache)
        {
            _vacanciesDA = new VacanciesDataAccess();
            _memoryCache = memoryCache;

        }

        [HttpGet("by-name-and-salary")]
        public async Task<IActionResult> GetVacanciesByNameAndSalary([FromQuery] string name, [FromQuery] int salary)
        {
            var cacheKey = CacheKeys.Vanacies(name, salary);

            if (!_memoryCache.TryGetValue(cacheKey, out List<Vacancy> vacancies))
            {
                vacancies = await _vacanciesDA.GetVacanciesByNameAndSalaryAsync(name, salary);

                _memoryCache.Set(cacheKey, vacancies, GetCacheExpirationDate());
            }
           
            return new JsonResult(vacancies);
        }

        public static DateTime GetCacheExpirationDate()
        {
            var currentTime = DateTimeOffset.Now.TimeOfDay;
            var newTime = DateTimeOffset.Now.Date;

            if (currentTime.Hours < CacheExpirationHour)
            {
                newTime = newTime + new TimeSpan(CacheExpirationHour, 0, 0);
            }
            else
            {
                newTime = newTime.AddDays(1).Add(new TimeSpan(CacheExpirationHour, 0, 0));
            }

            return newTime;
        }

        private readonly VacanciesDataAccess _vacanciesDA;
        private readonly IMemoryCache _memoryCache;
        private const int CacheExpirationHour = 9;
    }
}
 