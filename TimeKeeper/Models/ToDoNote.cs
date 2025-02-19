namespace TimeKeeper.Models
{
    public class ToDoNote
    {
        public int Id { get; set; }
        public string? Category { get; set; } = "ToDo";
        public string? Label { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public string? UserId { get; set; }
    }
}
