using Microsoft.EntityFrameworkCore;
using TimeKeeper.Interfaces;
using TimeKeeper.Models;

namespace TimeKeeper.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly ApplicationDbContext _context;


        public ToDoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ToDoNote>> GetToDoNoteByUserID(string userId)
        {
            return await _context.ToDoNote.Where(td => td.UserId == userId).ToListAsync();
        }

        public async Task<ToDoNote> GetToDoNoteById(int id)
        {
            return await _context.ToDoNote.Where(td => td.Id == id).FirstOrDefaultAsync();
        }

        public async Task SaveToDoNote(ToDoNote toDoNote)
        {
            _context.ToDoNote.Add(toDoNote);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateToDoNote(ToDoNote toDoNote)
        {
            _context.ToDoNote.Update(toDoNote);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteToDoNote(int id)
        {
            var toDoNote = await GetToDoNoteById(id);
            _context.ToDoNote.Remove(toDoNote);
            await _context.SaveChangesAsync();
        }

    }
}
