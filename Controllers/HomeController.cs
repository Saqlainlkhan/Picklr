using Microsoft.AspNetCore.Mvc;

namespace Picklr.Controllers
{
    public class HomeController : Controller
    {
        // Default home page — uses _MainLayout via _ViewStart
        public IActionResult Index()
        {
            return View();
        }

        // Client-facing pages — all return placeholder text for Phase 1
        public ContentResult About()
        {
            return Content("About page — under construction.");
        }

        public ContentResult Club()
        {
            return Content("Club page — under construction.");
        }

        public ContentResult Program()
        {
            return Content("Program page — under construction.");
        }

        public ContentResult Shop()
        {
            return Content("Shop page — under construction.");
        }
    }
}
