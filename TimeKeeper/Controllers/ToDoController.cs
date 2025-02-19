using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TimeKeeper.Interfaces;
using TimeKeeper.Models;

namespace TimeKeeper.Controllers
{
    public class ToDoController : Controller
    {
        private readonly IToDoNoteService _toDoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;


        public ToDoController(IToDoNoteService toDoService, IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _toDoService = toDoService;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = _userService.GetUserId(_httpContextAccessor.HttpContext?.User);
            return View("Index", await _toDoService.GetToDoNoteByUserID(userId));
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Label, Description, IsCompleted")] ToDoNote toDoNote)
        {
            toDoNote.UserId = _userService.GetUserId(_httpContextAccessor.HttpContext?.User);
            await _toDoService.SaveToDoNote(toDoNote);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Complete(int id)
        {
            var toDoNote = await _toDoService.GetToDoNoteById(id);
            toDoNote.IsCompleted = true;
            await _toDoService.UpdateToDoNote(toDoNote);
            
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Undo(int id)
        {
            var toDoNote = await _toDoService.GetToDoNoteById(id);
            toDoNote.IsCompleted = false;
            await _toDoService.UpdateToDoNote(toDoNote);
            
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = _userService.GetUserId(_httpContextAccessor.HttpContext?.User);
            var model = await _toDoService.GetToDoNoteById(id);
            return View("Edit", model);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateToDoNote(int id, [Bind("Label, Description, IsCompleted")] ToDoNote aToDoNote)
        {
            var userId = _userService.GetUserId(_httpContextAccessor.HttpContext?.User);

            var toDoNote = await _toDoService.GetToDoNoteById(id);
            toDoNote.Label = aToDoNote.Label;
            toDoNote.Description = aToDoNote.Description;
            toDoNote.IsCompleted = aToDoNote.IsCompleted;

            await _toDoService.UpdateToDoNote(toDoNote);
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Destroy(int id)
        {
            var userId = _userService.GetUserId(_httpContextAccessor.HttpContext?.User);
            await _toDoService.DeleteToDoNote(id);

            return RedirectToAction("Index");
        }
    }
}


