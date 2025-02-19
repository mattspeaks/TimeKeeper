using Microsoft.AspNetCore.Mvc;

namespace TimeKeeper.Controllers
{
    public class TimekeeperDayController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
