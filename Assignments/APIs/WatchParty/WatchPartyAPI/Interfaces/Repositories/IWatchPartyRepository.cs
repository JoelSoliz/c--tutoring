using WatchPartyAPI.Models;

namespace WatchPartyAPI.Interfaces.Repositories
{
    public interface IWatchPartyRepository
    {
        public Task<WatchParty?> GetById(Guid id);

        public Task<IEnumerable<WatchParty>> GetAllParties(int limit, int page);
        public Task<WatchParty> Create(WatchParty watchParty);
        public Task Update(WatchParty watchParty);
    }
}
