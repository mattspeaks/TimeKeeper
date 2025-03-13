using TimeKeeper.Interfaces;
using TimeKeeper.Models;
using TimeKeeper.Models.ViewModels;

namespace TimeKeeper.Services
{
    public class TimekeeperDayService : ITimekeeperDayService
    {

        private readonly ITimekeeperDayRepository _timekeeperDayRepository;
        private readonly ITimekeeperEventRepository _timekeeperEventRepository;
        public TimekeeperDayService(ITimekeeperDayRepository timekeeperDayRepo, ITimekeeperEventRepository timekeeperEventRepo)
        {
            _timekeeperDayRepository = timekeeperDayRepo;
            _timekeeperEventRepository = timekeeperEventRepo;
        }

        public async Task<TimekeeperDayViewModel> GetTodayByUserId(string id)
        {
            var viewModel = new TimekeeperDayViewModel();

            try
            {
                var days = await _timekeeperDayRepository.GetTimekeeperDayByUserId(id);
                if (days.Count == 0)
                {
                    return viewModel;
                }
                var todayDateOnly = DateOnly.FromDateTime(DateTime.Today);
                var today = days.First(d => d.Date == todayDateOnly);
                if (today == null)
                {
                    return viewModel;
                }
                viewModel.TimekeeperDayId = today.Id;
                viewModel.Date = today.Date;
                viewModel.UserId = today.UserId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            try
            {
                var events = await _timekeeperEventRepository.GetEventsByDayID(viewModel.TimekeeperDayId);
                viewModel.TimekeeperEvents = events;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return viewModel;
        }
    }
}
