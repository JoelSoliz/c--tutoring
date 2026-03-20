using WatchTrackerAPI.DTOs;

namespace WatchTrackerAPI.Interfaces.Repositories
{
    public interface IUserStatsRepository
    {
        public Task<List<MediaProgressResponse>> GetTopAnimes(Guid userId, TopRankingQueryParams topParams);

        public Task<List<MediaProgressResponse>> MonthlyPersonalRanking(Guid userId, TopRankingQueryParams topParams);
    }
}
