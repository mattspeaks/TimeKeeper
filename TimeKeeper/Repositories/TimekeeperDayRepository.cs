using TimeKeeper.Interfaces;
using TimeKeeper.Models;
using Microsoft.EntityFrameworkCore;


namespace TimeKeeper.Repositories
{
    public class TimekeeperDayRepository : ITimekeeperDayRepository
    {
        private readonly ApplicationDbContext _context;

        public TimekeeperDayRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<TimekeeperDay>> GetTimekeeperDayByUserId(string userId)
        {
            return await _context.TimekeeperDay.Where(td => td.UserId == userId).ToListAsync();
        }
    }
}
