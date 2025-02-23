using Microsoft.AspNetCore.Mvc;
using TimeKeeper.Interfaces;
using TimeKeeper.Models;


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
        public async Task<IActionResult> Index(string? searchString)
        {
            var userId = _userService.GetUserId(_httpContextAccessor.HttpContext?.User);
            var model = await _learningNoteService.GetLearningNotes(userId, searchString);
            ViewBag.SearchString = searchString;
            return View("Index", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetLearningNote(int id)
        {
            var userId = _userService.GetUserId(_httpContextAccessor.HttpContext?.User);
            var model = await _learningNoteService.GetLearningNote(id);
            return View("LearningNote", model);
        }


        [HttpPost]
        public async Task<IActionResult> CreateLearningNote([Bind("Label","Description")] LearningNote aLearningNote)
        {
            var userId = _userService.GetUserId(_httpContextAccessor.HttpContext?.User);
            aLearningNote.UserId = userId;
            await _learningNoteService.SaveLearningNote(aLearningNote);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLearningNote(int id, [Bind("Label", "Description")] LearningNote aLearningNote)
        {
            var userId = _userService.GetUserId(_httpContextAccessor.HttpContext?.User);
            var learningNote = await _learningNoteService.GetLearningNote(id);
            learningNote.Label = aLearningNote.Label;
            learningNote.Description = aLearningNote.Description;
            await _learningNoteService.UpdateLearningNote(learningNote);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Destroy(int id)
        {
            var userId = _userService.GetUserId(_httpContextAccessor.HttpContext?.User);
            await _learningNoteService.DeleteLearningNote(id);
            return RedirectToAction("Index");
        }


    }
}

