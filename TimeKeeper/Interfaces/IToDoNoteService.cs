using TimeKeeper.Models;


namespace TimeKeeper.Interfaces

{
    public interface IToDoNoteService
    {
        Task<List<ToDoNote>> GetToDoNoteByUserID(string userId);
        Task<ToDoNote> GetToDoNoteById(int id);
        Task<bool> SaveToDoNote(ToDoNote toDoNote);
        Task<bool> UpdateToDoNote(ToDoNote toDoNote);
        Task<bool> DeleteToDoNote(int id);
    }

}
