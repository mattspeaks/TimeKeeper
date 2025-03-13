using TimeKeeper.Models;

namespace TimeKeeper.Interfaces
{
    public interface ITimekeeperDayRepository
    {

        Task<List<TimekeeperDay>> GetTimekeeperDayByUserId(string id);

    }
}
