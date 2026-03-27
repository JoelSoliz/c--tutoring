using WatchTrackerAPI.DTOs.Parameters;
using WatchTrackerAPI.DTOs.Responses;
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
                                    MediaTitle = progress.Media.Title
                                });
            return top.ToList();
        }

        public async Task<List<MediaProgressResponse>> GetMonthlyRanking(Guid userId, TopRankingQueryParams topParams)
        {
            var monthlyRanking = await _userStatsRepository.MonthlyPersonalRanking(userId, topParams);
            var ranking = monthlyRanking.Select(progress =>
                                          new MediaProgressResponse
                                          {
                                              UserId = userId,
                                              MediaId = progress.MediaId,
                                              MediaTitle = progress.Media.Title
                                          });
            return ranking.ToList();
        }
    }
}

