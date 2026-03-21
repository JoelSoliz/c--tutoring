using WatchTrackerAPI.DTOs;
using WatchTrackerAPI.Models.Entities;

namespace WatchTrackerAPI.Interfaces.Repositories
{
    public interface IUserStatsRepository
    {
        public Task<List<UserMediaProgress>> GetTopAnimes(Guid userId, TopRankingQueryParams topParams);

        public Task<List<UserMediaProgress>> MonthlyPersonalRanking(Guid userId, TopRankingQueryParams topParams);
    }
}
