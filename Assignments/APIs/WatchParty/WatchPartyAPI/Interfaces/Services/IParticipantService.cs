using WatchPartyAPI.DTOs.Responses;
using WatchPartyAPI.Models.Enums;

namespace WatchPartyAPI.Interfaces.Services
{
    public interface IParticipantService
    {
        Task<ParticipantResponse> CreateParticipant(Guid userId, Guid watchPartyId, ParticipantRole role);
    }
}
