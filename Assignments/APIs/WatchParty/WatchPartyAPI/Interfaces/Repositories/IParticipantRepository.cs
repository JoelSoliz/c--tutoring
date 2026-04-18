using WatchPartyAPI.Models;

namespace WatchPartyAPI.Interfaces.Repositories
{
    public interface IParticipantRepository
    {
        public Task<Participant?> GetParticipant(Guid userId, Guid watchPartyId);
        public Task AddParticipant(Participant participant);
    }
}
