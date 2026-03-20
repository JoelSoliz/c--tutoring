using WatchTrackerAPI.Models.Entities;
using WatchTrackerAPI.Models.Enums;

namespace WatchTrackerAPI.Interfaces.Repositories
{
    public interface IMediaProgressRepository
    {
        public Task CreateMediaProgress(UserMediaProgress mediaProgress);

        public Task UpdateMediaProgress(UserMediaProgress mediaProgress);
        public Task<UserMediaProgress?> GetMediaProgress(Guid userId, Guid mediaId);

        public Task<(List<UserMediaProgress> Items, int TotalCount)> GetAllMediaProgress(int page, int pageSize, WatchStatus? status, Guid userId);
    }
}
