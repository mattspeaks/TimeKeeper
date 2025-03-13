namespace TimeKeeper.Models
{
    public class TimekeeperEvent
    {
        public int Id { get; set; }
        public TimeOnly Start { get; set; }
        public string? Description { get; set; }
        public TimeOnly End { get; set; }
        public int TimekeeperDayId { get; set; }

    }
}
