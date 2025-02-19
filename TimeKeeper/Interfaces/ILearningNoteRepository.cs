using TimeKeeper.Models;

namespace TimeKeeper.Interfaces
{
    public interface ILearningNoteRepository
    {

        Task<List<LearningNote>> GetLearningNoteByUserId(string userId);

        Task<LearningNote> GetLearningNoteById(int id);

        Task SaveLearningNote(LearningNote learningNote);

        Task UpdateLearningNote(LearningNote learningNote);

        Task DeleteLearningNote(int id);

    }
}
