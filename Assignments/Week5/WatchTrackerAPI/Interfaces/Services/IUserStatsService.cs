using WatchTrackerAPI.DTOs;

namespace WatchTrackerAPI.Interfaces.Services
{
    public interface IUserStatsService
    {
        public Task<List<MediaProgressResponse>> GetTopAnimes(Guid userId, TopRankingQueryParams topParams);
    }
}
