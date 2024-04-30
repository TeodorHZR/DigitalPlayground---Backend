using Microsoft.AspNetCore.Mvc;

namespace DigitalPlayground.Controllers
{
    public class SkinTransactionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
