using Microsoft.EntityFrameworkCore;
using WatchPartyAPI.Data;
using WatchPartyAPI.Interfaces.Repositories;
using WatchPartyAPI.Models;

namespace WatchPartyAPI.Repositories
{
    public class WatchPartyRepository : IWatchPartyRepository
    {
        private readonly AppDbContext _dbContext;

        public WatchPartyRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<WatchParty> Create(WatchParty watchParty)
        {
            _dbContext.Add(watchParty);
            await _dbContext.SaveChangesAsync();
            return watchParty;
        }

        public async Task<WatchParty?> GetById(Guid id)
        {
            var watchParty = await _dbContext.WatchParties.Include(wp => wp.Participants).FirstOrDefaultAsync(wp => wp.Id == id);
            return watchParty;

        }

        public async Task Update(WatchParty watchParty)
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
