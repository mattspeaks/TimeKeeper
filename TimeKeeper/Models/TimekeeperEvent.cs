namespace TimeKeeper.Models
{
    public class TimekeeperEvent
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public string? Description { get; set; }
        public DateTime End { get; set; }
        public int TimekeeperDayId { get; set; }

    }
}
