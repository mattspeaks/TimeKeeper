namespace TimeKeeper.Models.ViewModels
{
    public class TimekeeperDayViewModel
    {
        public int TimekeeperDayId { get; set; }

        public DateOnly Date { get; set; }

        public string? UserId { get; set; }

        public List<TimekeeperEvent>? TimekeeperEvents { get; set; }

    }
}
