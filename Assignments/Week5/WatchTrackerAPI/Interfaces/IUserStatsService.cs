using WatchTrackerAPI.DTOs;

namespace WatchTrackerAPI.Interfaces
{
    public interface IUserStatsService
    {
        public Task<List<MediaProgressResponse>> GetTopAnimes(Guid userId, TopAnimeQueryParams topParams);
    }
}
