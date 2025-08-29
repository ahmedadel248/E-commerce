using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NotFoundPage()
        {
            return View();
        }
    }
}
