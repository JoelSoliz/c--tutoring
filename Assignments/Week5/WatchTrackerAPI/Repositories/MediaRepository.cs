using Microsoft.EntityFrameworkCore;
using WatchTrackerAPI.Data;
using WatchTrackerAPI.Interfaces.Repositories;
using WatchTrackerAPI.Models.Entities;
using WatchTrackerAPI.Models.Enums;

namespace WatchTrackerAPI.Repositories
{
    public class MediaRepository : IMediaRepository
    {
        private readonly AppDBContext _dbContext;
        public MediaRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateMedia(Media media)
        {
            _dbContext.MediaContent.Add(media);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateMedia(Media media)
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<(List<Media> Items, int TotalCount)> GetAllMedia(int page, int pageSize, MediaTypes? type)
        {
            IQueryable<Media> media = _dbContext.MediaContent.Include(m => m.Genre).Where(media => media.IsDeleted == false);
            if (type != null)
            {
                media = media.Where(m => m.Type == type);
            }
            var totalCount = await media.CountAsync();
            media = media.Skip((page - 1) * pageSize).Take(pageSize);
            var mediaList = await media.AsNoTracking().ToListAsync();
            return (mediaList, totalCount);
        }

        public async Task<Media?> GetMedia(Guid mediaId)
        {
            var media = await _dbContext.MediaContent.Include(m => m.Genre).FirstOrDefaultAsync(media => media.Id == mediaId && media.IsDeleted == false);
            return media;
        }

        public async Task<List<Media>> GetMediaByIds(List<Guid> mediaIds)
        {
            var medias = await _dbContext.MediaContent.Include(m => m.Genre).Where(media => mediaIds.Contains(media.Id)).AsNoTracking().ToListAsync(); // WHERE mediaId IN mediaIds
            return medias;

        }
    }
}
