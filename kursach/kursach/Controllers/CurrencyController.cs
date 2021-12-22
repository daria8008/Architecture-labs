using kursach.DataAccess;
using kursach.DataAccess.Contracts;
using kursach.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace kursach.Controllers
{
    public class CurrencyController : Controller
    {
        public CurrencyController(IPrivatDataAccess privatDataAccess,
            INbuDataAccess nbuDataAccess)
        {
            _privatDataAccess = privatDataAccess;
            _nbuDataAccess = nbuDataAccess;
        }

        [HttpGet]
        [ActionName("Data")]
        public async Task<JsonResult> GetCurrencyData()
        {
            var privatData = await _privatDataAccess.GetCurrencyDataAsync();
            var nbuData = await _nbuDataAccess.GetCurrencyDataAsync();
            privatData.NormalizeCurrencies();
            nbuData.NormalizeCurrencies();

            var vm = new CurrencyDataViewModel(new[] {
                privatData.ToViewModel(BankNames.Privat),
                nbuData.ToViewModel(BankNames.Nbu)
            });

            return new JsonResult(vm);
        }

        private readonly IPrivatDataAccess _privatDataAccess;
        private readonly INbuDataAccess _nbuDataAccess;
    }
}
