using Microsoft.AspNetCore.Mvc;
using TimeKeeper.Interfaces;
using TimeKeeper.Models.ViewModels;

namespace TimeKeeper.Controllers
{
    public class TimekeeperDayController : Controller
    {
        private readonly ITimekeeperDayService _timekeeperDayService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public TimekeeperDayController(ITimekeeperDayService timekeeperDayService, IHttpContextAccessor httpContextAccessor, IUserService userService) 
        {
            _timekeeperDayService = timekeeperDayService;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;

        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = _userService.GetUserId(_httpContextAccessor.HttpContext?.User);
            var model = await _timekeeperDayService.GetTodayByUserId(userId);
            return View("Index", model);
        }
    }
}
