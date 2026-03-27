using WatchTrackerAPI.DTOs.Parameters;
using WatchTrackerAPI.DTOs.Responses;

namespace WatchTrackerAPI.Interfaces.Services
{
    public interface IUserStatsService
    {
        public Task<List<MediaProgressResponse>> GetTopAnimes(Guid userId, TopRankingQueryParams topParams);

        public Task<List<MediaProgressResponse>> GetMonthlyRanking(Guid userId, TopRankingQueryParams topParams);
    }
}
