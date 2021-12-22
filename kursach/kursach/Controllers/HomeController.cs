using Microsoft.AspNetCore.Mvc;

namespace kursach.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Widget()
        {
            return View();
        }
    }
}
