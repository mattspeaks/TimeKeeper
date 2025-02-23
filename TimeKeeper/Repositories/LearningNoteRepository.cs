using Microsoft.EntityFrameworkCore;
using TimeKeeper.Interfaces;
using TimeKeeper.Models;

namespace TimeKeeper.Repositories
{
    public class LearningNoteRepository : ILearningNoteRepository
    {
        private readonly ApplicationDbContext _context;

        public LearningNoteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<LearningNote>> GetLearningNoteByUserId(string userId, string? SearchString)
        {
            if (string.IsNullOrEmpty(SearchString))
            {
                return await _context.LearningNote
                    .Where(ln => ln.UserId == userId)
                    .ToListAsync();
            }
            return await _context.LearningNote
                .Where(ln => ln.UserId == userId &&
                             (ln.Label != null && ln.Label.Contains(SearchString) ||
                              ln.Description != null && ln.Description.Contains(SearchString)))
                .ToListAsync();
        }

       public async Task<LearningNote> GetLearningNoteById(int id)
        {
            return await _context.LearningNote.Where(ln => ln.Id == id).FirstOrDefaultAsync();
        }

        public async Task SaveLearningNote(LearningNote learningNote)
        {
            _context.LearningNote.Add(learningNote);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLearningNote(LearningNote learningNote)
        {
            _context.LearningNote.Update(learningNote);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLearningNote(int id)
        {
            var learningNote = await GetLearningNoteById(id);
            _context.LearningNote.Remove(learningNote);
            await _context.SaveChangesAsync();
        }

    }
}
