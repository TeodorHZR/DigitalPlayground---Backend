using Microsoft.AspNetCore.Mvc;

namespace DigitalPlayground.Controllers
{
    public class TeamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
