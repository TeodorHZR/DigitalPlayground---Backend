using Microsoft.AspNetCore.Mvc;

namespace DigitalPlayground.Controllers
{
    public class GameTransactionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
