using TimeKeeper.Interfaces;
using TimeKeeper.Models;

namespace TimeKeeper.Services
{
    public class LearningNoteService : ILearningNoteService
    {
        private readonly ILearningNoteRepository _learningNoteRepository;


        public LearningNoteService(ILearningNoteRepository learningNotRepo)
        {
            _learningNoteRepository = learningNotRepo;
        }

        public async Task<LearningNote> GetLearningNote(int id)
        {
            try
            {
                var aLearningNote = await _learningNoteRepository.GetLearningNoteById(id);
                return aLearningNote;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public async Task<List<LearningNote>> GetLearningNotes(string userId, string? searchString)
        {
            try
            {
                var learningNotes = await _learningNoteRepository.GetLearningNoteByUserId(userId, searchString);
                return learningNotes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> SaveLearningNote(LearningNote learningNote)
        {
            try
            {
                await _learningNoteRepository.SaveLearningNote(learningNote);
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateLearningNote(LearningNote learningNote)
        {
            try
            {
                await _learningNoteRepository.UpdateLearningNote(learningNote);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteLearningNote(int id)
        {
            try
            {
                await _learningNoteRepository.DeleteLearningNote(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
