using Microsoft.EntityFrameworkCore;
using TimeKeeper.Interfaces;
using TimeKeeper.Models;

namespace TimeKeeper.Repositories
{
    public class TimekeeperEventRepository : ITimekeeperEventRepository
    {
        private readonly ApplicationDbContext _context;

        public TimekeeperEventRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<TimekeeperEvent>> GetEventsByDayID(int id)
        {
            return await _context.TimekeeperEvent.Where(e => e.TimekeeperDayId == id).ToListAsync();
        }




    }
}
