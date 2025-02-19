using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TimeKeeper.Interfaces;


namespace TimeKeeper.Controllers
{
    public class LearningNoteController : Controller
    {
        private readonly ILearningNoteService _learningNoteService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;

        public LearningNoteController(ILearningNoteService learningNoteService, IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _learningNoteService = learningNoteService;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = _userService.GetUserId(_httpContextAccessor.HttpContext?.User);
            var model = await _learningNoteService.GetLearningNotes(userId);
            return View("Index", model);
        }

        public async Task<IActionResult> Create()
        {

            return RedirectToAction("Index");
        }
    }
}

