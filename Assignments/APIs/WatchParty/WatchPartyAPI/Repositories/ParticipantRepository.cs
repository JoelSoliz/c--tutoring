using Microsoft.EntityFrameworkCore;
using WatchPartyAPI.Data;
using WatchPartyAPI.Interfaces.Repositories;
using WatchPartyAPI.Models;

namespace WatchPartyAPI.Repositories
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly AppDbContext _dbContext;

        public ParticipantRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddParticipant(Participant participant)
        {
            _dbContext.Participants.Add(participant);
            await _dbContext.SaveChangesAsync();
        }


        public async Task<Participant?> GetParticipant(Guid userId, Guid watchPartyId)
        {
            var participant = await _dbContext.Participants.FirstOrDefaultAsync(p => p.UserId == userId && p.WatchPartyId == watchPartyId);
            return participant;
        }
    }
}
