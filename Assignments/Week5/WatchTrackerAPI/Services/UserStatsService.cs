using WatchTrackerAPI.DTOs;
using WatchTrackerAPI.Interfaces.Repositories;
using WatchTrackerAPI.Interfaces.Services;

namespace WatchTrackerAPI.Services
{
    public class UserStatsService : IUserStatsService
    {
        private readonly IUserStatsRepository _userStatsRepository;
        public UserStatsService(IUserStatsRepository userStatsRepository)
        {
            _userStatsRepository = userStatsRepository;
        }

        public async Task<List<MediaProgressResponse>> GetTopAnimes(Guid userId, TopRankingQueryParams topParams)
        {
            var topAnimes = await _userStatsRepository.GetTopAnimes(userId, topParams);
            var top = topAnimes.Select(progress =>
                                new MediaProgressResponse
                                {
                                    UserId = userId,
                                    MediaId = progress.MediaId,
                                    MediaTitle = progress.Media.Title,
                                    MediaType = progress.Media.Type,
                                    Genre = progress.Media.Genre.Name,
                                    TotalEpisodes = progress.Media.TotalEpisodes,
                                    EpisodesWatched = progress.EpisodesWatched,
                                    WatchStatus = progress.Status,
                                    PersonalRating = progress.PersonalRating,
                                    StartedAt = progress.StartedAt,
                                    FinishedAt = progress.FinishedAt,
                                    LastUpdatedAt = progress.LastUpdatedAt
                                });
            return top.ToList();
        }
    }
}

