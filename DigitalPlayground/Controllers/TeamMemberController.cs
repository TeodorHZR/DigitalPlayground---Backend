using Microsoft.AspNetCore.Mvc;

namespace DigitalPlayground.Controllers
{
    public class TeamMemberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
