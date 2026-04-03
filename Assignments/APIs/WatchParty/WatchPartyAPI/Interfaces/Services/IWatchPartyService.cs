using WatchPartyAPI.DTOs.Requests;
using WatchPartyAPI.DTOs.Responses;

namespace WatchPartyAPI.Interfaces.Services
{
    public interface IWatchPartyService
    {
        public Task<WatchPartyResponse> CreateParty(CreatePartyRequest request, Guid hostUserId);
        public Task JoinParty(Guid watchPartyId, Guid userId);
        public Task UpdatePlayback(Guid watchPartyId, Guid userId, UpdatePlaybackRequest request);
        public Task ChangeEpisode(Guid watchPartyId, Guid userId, ChangeEpisodeRequest request);
        public Task EndParty(Guid watchPartyId, Guid userId);
        public Task<WatchPartyResponse> GetParty(Guid watchPartyId);
    }
}
