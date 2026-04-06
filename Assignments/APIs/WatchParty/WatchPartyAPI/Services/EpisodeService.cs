using WatchPartyAPI.DTOs.Requests;
using WatchPartyAPI.DTOs.Responses;
using WatchPartyAPI.Exceptions;
using WatchPartyAPI.Interfaces.Repositories;
using WatchPartyAPI.Interfaces.Services;
using WatchPartyAPI.Models;

namespace WatchPartyAPI.Services
{
    public class EpisodeService : IEpisodeService
    {
        private readonly IEpisodeRepository _episodeRepository;

        public EpisodeService(IEpisodeRepository episodeRepository)
        {
            _episodeRepository = episodeRepository;
        }

        public async Task<EpisodeResponse> CreateEpisode(CreateEpisodeRequest request)
        {
            var episode = new Episode
            {
                Id = Guid.NewGuid(),
                AnimeTitle = request.AnimeTitle,
                EpisodeNumber = request.EpisodeNumber,
                DurationSeconds = request.DurationSeconds,
            };

            await _episodeRepository.Create(episode);
            return new EpisodeResponse
            {
                Id = episode.Id,
                AnimeTitle = episode.AnimeTitle,
                EpisodeNumber = episode.EpisodeNumber,
                DurationSeconds = episode.DurationSeconds,
            };
        }

        public async Task<EpisodeResponse> GetEpisode(Guid episodeId)
        {
            var episode = await _episodeRepository.GetById(episodeId);
            if (episode == null) throw new NotFoundException("Episode", episodeId);
            return new EpisodeResponse
            {
                Id = episode.Id,
                AnimeTitle = episode.AnimeTitle,
                EpisodeNumber = episode.EpisodeNumber,
                DurationSeconds = episode.DurationSeconds,
            };
        }

        public async Task<EpisodePaginatedResponse> GetEpisodes(int limit, int page)
        {
            var episodes = await _episodeRepository.GetAllEpisodes(limit, page);
            var response = episodes
                .Select(episode => new EpisodeResponse
                {
                    Id = episode.Id,
                    AnimeTitle = episode.AnimeTitle,
                    EpisodeNumber = episode.EpisodeNumber,
                    DurationSeconds = episode.DurationSeconds
                });
            return new EpisodePaginatedResponse
            {
                Data = response,
                Limit = limit,
                Page = page,
                TotalCount = episodes.Count()
            };
        }
    }
}
