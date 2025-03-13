using TimeKeeper.Models;

namespace TimeKeeper.Interfaces
{
    public interface ITimekeeperEventRepository
    {
        Task<List<TimekeeperEvent>> GetEventsByDayID(int id);
    }
}
