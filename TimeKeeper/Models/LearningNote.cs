namespace TimeKeeper.Models
{
    public class LearningNote
    {
        public int Id { get; set; }
        public string? Category { get; set; } = "LearningNote";
        public string? Label { get; set; }
        public string? Description { get; set; }
        public string? UserId { get; set; }

    }
}
