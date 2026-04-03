using Microsoft.EntityFrameworkCore;
using WatchPartyAPI.Data;
using WatchPartyAPI.Interfaces.Repositories;
using WatchPartyAPI.Models;

namespace WatchPartyAPI.Repositories
{
    public class EpisodeRepository : IEpisodeRepository
    {
        private readonly AppDbContext _dbContext;

        public EpisodeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Episode> Create(Episode episode)
        {
            _dbContext.Episodes.Add(episode);
            await _dbContext.SaveChangesAsync();
            return episode;
        }

        public async Task<Episode?> GetById(Guid id)
        {
            var episode = await _dbContext.Episodes.FirstOrDefaultAsync(e => e.Id == id);
            return episode;
        }
    }
}
