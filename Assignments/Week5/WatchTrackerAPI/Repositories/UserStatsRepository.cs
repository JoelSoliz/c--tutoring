using Microsoft.EntityFrameworkCore;
using WatchTrackerAPI.Data;
using WatchTrackerAPI.DTOs.Parameters;
using WatchTrackerAPI.Interfaces.Repositories;
using WatchTrackerAPI.Models.Entities;
using WatchTrackerAPI.Models.Enums;

namespace WatchTrackerAPI.Repositories
{
    public class UserStatsRepository : IUserStatsRepository
    {
        private readonly AppDBContext _dbContext;

        public UserStatsRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<UserMediaProgress>> GetTopAnimes(Guid userId, TopRankingQueryParams topParams)
        {
            var topAnimes = await _dbContext.MediaProgresses
                .Include(progress => progress.Media).ThenInclude(media => media.Genre) //JOIN WITH MEDIA ENTITY
                .Where(progress => progress.UserId == userId &&
                                   progress.IsDeleted == false &&
                                   progress.Media.Type == MediaTypes.Anime &&
                                   progress.Status == WatchStatus.Completed &&
                                   progress.PersonalRating != null)
                .OrderByDescending(progress => progress.PersonalRating)
                .Take(topParams.Limit)
                .ToListAsync();

            return topAnimes;
        }

        public async Task<List<UserMediaProgress>> MonthlyPersonalRanking(Guid userId, TopRankingQueryParams topParams)
        {
            var monthlyRanking = await _dbContext.MediaProgresses
                .Include(progress => progress.Media)
                .Where(progress => progress.StartedAt != null &&
                                   progress.StartedAt.Value.Month == DateTime.UtcNow.Month &&
                                   progress.StartedAt.Value.Year == DateTime.UtcNow.Year &&
                                   progress.IsDeleted == false &&
                                   progress.PersonalRating != null)
                .OrderByDescending(progress => progress.PersonalRating)
                .Take(topParams.Limit)
                .ToListAsync();

            return monthlyRanking;
        }
    }
}
