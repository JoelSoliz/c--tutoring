using WatchPartyAPI.Models;

namespace WatchPartyAPI.Interfaces.Repositories
{
    public interface IEpisodeRepository
    {
        public Task<Episode?> GetById(Guid id);
        public Task<Episode> Create(Episode episode);
    }
}
