using TimeKeeper.Models.ViewModels;

namespace TimeKeeper.Interfaces
{
    public interface ITimekeeperDayService
    {
        Task<TimekeeperDayViewModel> GetTodayByUserId(string id);
           
    }
}
