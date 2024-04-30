using Microsoft.AspNetCore.Mvc;

namespace DigitalPlayground.Controllers
{
    public class GameCategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
