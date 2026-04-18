using WatchPartyAPI.DTOs.Responses;
using WatchPartyAPI.Exceptions;
using WatchPartyAPI.Interfaces.Repositories;
using WatchPartyAPI.Interfaces.Services;
using WatchPartyAPI.Models;
using WatchPartyAPI.Models.Enums;

namespace WatchPartyAPI.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly IWatchPartyRepository _watchPartyRepository;

        public ParticipantService(
            IParticipantRepository participantRepository,
            IWatchPartyRepository watchPartyRepository)
        {
            _participantRepository = participantRepository;
            _watchPartyRepository = watchPartyRepository;
        }

        public async Task<ParticipantResponse> CreateParticipant(Guid userId, Guid watchPartyId, ParticipantRole role)
        {
            var party = await _watchPartyRepository.GetById(watchPartyId);
            if (party == null) throw new NotFoundException("WatchParty", watchPartyId);

            if (party.Status == PartyStatus.Ended)
                throw new BusinessException("Cannot join a party that has ended");

            var existing = await _participantRepository.GetParticipant(userId, watchPartyId);
            if (existing != null)
                throw new ConflictException("User is already a participant");

            var participant = new Participant
            {
                UserId = userId,
                WatchPartyId = watchPartyId,
                Role = role
            };

            await _participantRepository.AddParticipant(participant);

            return new ParticipantResponse
            {
                UserId = participant.UserId,
                WatchPartyId = participant.WatchPartyId,
                Role = participant.Role
            };
        }
    }
}
