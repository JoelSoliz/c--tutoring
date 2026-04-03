using WatchPartyAPI.DTOs.Requests;
using WatchPartyAPI.DTOs.Responses;

namespace WatchPartyAPI.Interfaces.Services
{
    public interface IEpisodeService
    {
        public Task<EpisodeResponse> CreateEpisode(CreateEpisodeRequest request);

        public Task<EpisodeResponse> GetEpisode(Guid episodeId);
    }
}
