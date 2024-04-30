using Microsoft.AspNetCore.Mvc;

namespace DigitalPlayground.Controllers
{
    public class ReviewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
