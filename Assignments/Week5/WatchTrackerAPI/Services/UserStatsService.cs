using Microsoft.EntityFrameworkCore;
using WatchTrackerAPI.Data;
using WatchTrackerAPI.DTOs;
using WatchTrackerAPI.Interfaces;
using WatchTrackerAPI.Models.Enums;

namespace WatchTrackerAPI.Services
{
    public class UserStatsService : IUserStatsService
    {
        private readonly AppDBContext _dbContext;
        public UserStatsService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<MediaProgressResponse>> GetTopAnimes(Guid userId, TopAnimeQueryParams topParams)
        {
            var topAnimes = await _dbContext.MediaProgresses
                .Include(progress => progress.Media) //JOIN WITH MEDIA ENTITY
                .Where(progress => progress.UserId == userId &&
                                   progress.isDeleted == false &&
                                   progress.Media.Type == MediaTypes.Anime &&
                                   progress.Status == WatchStatus.Completed &&
                                   progress.PersonalRating != null)
                .OrderByDescending(progress => progress.PersonalRating)
                .Take(topParams.Limit)
                .Select(progress =>
                                new MediaProgressResponse
                                {
                                    UserId = userId,
                                    MediaId = progress.MediaId,
                                    MediaTitle = progress.Media.Title,
                                    MediaType = progress.Media.Type,
                                    TotalEpisodes = progress.Media.TotalEpisodes,
                                    EpisodesWatched = progress.EpisodesWatched,
                                    WatchStatus = progress.Status,
                                    PersonalRating = progress.PersonalRating,
                                    StartedAt = progress.StartedAt,
                                    FinishedAt = progress.FinishedAt,
                                    LastUpdatedAt = progress.LastUpdatedAt
                                })
                .ToListAsync();

            return topAnimes;
        }
    }
}

