namespace TimeKeeper.Models
{
    public class TimekeeperDay
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public string? UserId { get; set; }
        public List<TimekeeperEvent>? TimekeeperEvents { get; set; }

    }
}
