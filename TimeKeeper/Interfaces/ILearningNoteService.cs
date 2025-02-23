using TimeKeeper.Models;

namespace TimeKeeper.Interfaces
{
    public interface ILearningNoteService
    {
        Task<List<LearningNote>> GetLearningNotes(string userId, string? SearchString);

        Task<LearningNote> GetLearningNote(int id);

        Task<bool> SaveLearningNote(LearningNote learningNote);

        Task<bool> UpdateLearningNote(LearningNote learningNote);

        Task<bool> DeleteLearningNote(int id);
    }
}
