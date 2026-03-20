using WatchTrackerAPI.Models.Entities;
using WatchTrackerAPI.Models.Enums;

namespace WatchTrackerAPI.Interfaces.Repositories
{
    public interface IMediaRepository
    {
        public Task CreateMedia(Media media);
        public Task<Media?> GetMedia(Guid mediaId);

        public Task<(List<Media> Items, int TotalCount)> GetAllMedia(int page, int pageSize, MediaTypes? type);

        public Task UpdateMedia(Media media);

        public Task<List<Media>> GetMediaByIds(List<Guid> mediaIds);
    }
}
