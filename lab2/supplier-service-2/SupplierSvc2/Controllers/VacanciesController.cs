using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ServicesContracts;
using SupplierSvc2.DataAccess;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SupplierSvc2.Controllers
{
    public static class CacheKeys
    {
        public static string Vanacies(int pageNumber) => $"vacancies_{pageNumber}";
    }

    [ApiController]
    [Route("vacancies")]
    public class VacanciesController : ControllerBase
    {
        public VacanciesController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetVacancies([FromQuery] int page_number)
        {
            var cacheKey = CacheKeys.Vanacies(page_number);

            if (!_memoryCache.TryGetValue(cacheKey, out Vacancy[] vacancies))
            {
                vacancies = await DB.GetVacanciesAsync(page_number, PageSize);

                _memoryCache.Set(cacheKey, vacancies, GetCacheExpirationDate());
            }

            return new JsonResult(vacancies);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetVacancyById(string id)
        //{
        //    var vacancy = DB.GetVacancyById(id);

        //    return vacancy != null
        //        ? new JsonResult(vacancy)
        //        : new NotFoundResult();
        //}

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

        private readonly IMemoryCache _memoryCache;

        private const int PageSize = 5000;
        private const int CacheExpirationHour = 9;
    }
}