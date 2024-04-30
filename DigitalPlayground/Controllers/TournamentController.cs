using Microsoft.AspNetCore.Mvc;

namespace DigitalPlayground.Controllers
{
    public class TournamentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
