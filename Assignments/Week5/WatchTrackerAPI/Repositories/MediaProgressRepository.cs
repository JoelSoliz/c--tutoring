using Microsoft.EntityFrameworkCore;
using WatchTrackerAPI.Data;
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
            IQueryable<UserMediaProgress> allMediaProgress = _dbContext.MediaProgresses;
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
            var mediaProgress = await _dbContext.MediaProgresses.FirstOrDefaultAsync(p => p.UserId == userId && p.MediaId == mediaId && p.IsDeleted == false);
            return mediaProgress;
        }

        public async Task UpdateMediaProgress(UserMediaProgress mediaProgress)
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
