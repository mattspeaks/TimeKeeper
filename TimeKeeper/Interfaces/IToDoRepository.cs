using TimeKeeper.Models;

namespace TimeKeeper.Interfaces
{
    public interface IToDoRepository
    {
        Task<List<ToDoNote>> GetToDoNoteByUserID(string userId);


        //Is there a whole or vulnerability here where you could get a note by the ID without being authenticated as the correct user? 
        Task<ToDoNote> GetToDoNoteById(int id);

        Task SaveToDoNote(ToDoNote toDoNote);

        Task UpdateToDoNote(ToDoNote toDoNote);

        Task DeleteToDoNote(int id);

    }
}
