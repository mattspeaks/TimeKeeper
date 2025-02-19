using TimeKeeper.Models;
using TimeKeeper.Repositories;
using TimeKeeper.Interfaces;

namespace TimeKeeper.Services
{
    public class ToDoNoteService : IToDoNoteService
    {
        private readonly IToDoRepository _toDoRepository;

        public ToDoNoteService(IToDoRepository toDoRepo)
        {
            _toDoRepository = toDoRepo;
        }

        public async Task<ToDoNote> GetToDoNoteById(int id)
        {
            try
            {
                var aToDoNote = await _toDoRepository.GetToDoNoteById(id);
                return aToDoNote;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
        public async Task<List<ToDoNote>> GetToDoNoteByUserID(string userId)
        {
            try
            {
                var toDoNotes = await _toDoRepository.GetToDoNoteByUserID(userId);
                return toDoNotes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> SaveToDoNote(ToDoNote toDoNote)
        {
            try
            {
                await _toDoRepository.SaveToDoNote(toDoNote);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateToDoNote(ToDoNote toDoNote)
        {
            try
            {
                await _toDoRepository.UpdateToDoNote(toDoNote);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteToDoNote(int id)
        {
            try
            {
                await _toDoRepository.DeleteToDoNote(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
