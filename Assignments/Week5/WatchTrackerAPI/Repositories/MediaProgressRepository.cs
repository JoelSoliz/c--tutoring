using Microsoft.EntityFrameworkCore;
using WatchTrackerAPI.Data;
using WatchTrackerAPI.DTOs.Parameters;
using WatchTrackerAPI.Interfaces.Repositories;
using WatchTrackerAPI.Models.Entities;
using WatchTrackerAPI.Models.Enums;

namespace WatchTrackerAPI.Repositories
{
    public class MediaProgressRepository : IMediaProgressRepository
    {
        private readonly AppDBContext _dbContext;

        public MediaProgressRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateMediaProgress(UserMediaProgress mediaProgress)
        {
            _dbContext.MediaProgresses.Add(mediaProgress);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<(List<UserMediaProgress> Items, int TotalCount)> GetAllMediaProgress(int page, int pageSize, WatchStatus? status, Guid userId)
        {
            IQueryable<UserMediaProgress> allMediaProgress = _dbContext.MediaProgresses.Include(mediaProgress => mediaProgress.Media);
            allMediaProgress = allMediaProgress.Where(p => p.UserId == userId && p.IsDeleted == false); //searching progresses

            if (status != null) //filter by status
            {
                allMediaProgress = allMediaProgress.Where(m => m.Status == status);
            }

            var totalCount = await allMediaProgress.CountAsync();
            allMediaProgress = allMediaProgress.Skip((page - 1) * pageSize).Take(pageSize);

            var progresses = await allMediaProgress.ToListAsync();

            return (progresses, totalCount);
        }

        public async Task<UserMediaProgress?> GetMediaProgress(Guid userId, Guid mediaId)
        {
            var mediaProgress = await _dbContext.MediaProgresses.Include(mediaProgress => mediaProgress.Media).FirstOrDefaultAsync(p => p.UserId == userId && p.MediaId == mediaId && p.IsDeleted == false);
            return mediaProgress;
        }

        public async Task UpdateMediaProgress(UserMediaProgress mediaProgress)
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<UserMediaProgress>> GetRomanticWatchList(Guid userId, WatchlistQueryParams watchlistParams)
        {
            var normalizedGenre = char.ToUpper(watchlistParams.Genre[0]) + watchlistParams.Genre.Substring(1).ToLower();
            var romanticMovies = await _dbContext.MediaProgresses
                .Include(progress => progress.Media).ThenInclude(media => media.Genre)
                .Where(progress => progress.UserId == userId &&
                                   progress.IsDeleted == false &&
                                   progress.Status == WatchStatus.PlanToWatch &&
                                   progress.Media.Type == MediaTypes.Movie &&
                                   progress.Media.Genre.Name == normalizedGenre)
                .ToListAsync();

            return romanticMovies;
        }

        public async Task<List<UserMediaProgress>> GetRecentActivity(Guid userId, RecentActivityQueryParams recentActivityParams)
        {
            var limitdate = DateTime.UtcNow.AddDays(-recentActivityParams.Days); //date of the days above
            var recentActivity = await _dbContext.MediaProgresses
                .Where(progress => progress.UserId == userId &&
                                   progress.IsDeleted == false &&
                                   progress.LastUpdatedAt >= limitdate)
                .OrderByDescending(progress => progress.LastUpdatedAt)
                .ToListAsync();

            return recentActivity;
        }
    }
}
