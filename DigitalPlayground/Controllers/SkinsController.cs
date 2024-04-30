using Microsoft.AspNetCore.Mvc;

namespace DigitalPlayground.Controllers
{
    public class SkinsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
