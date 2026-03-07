using WatchTrackerAPI.DTOs;
using WatchTrackerAPI.Models.Enums;

namespace WatchTrackerAPI.Interfaces
{
    public interface IMediaProgressService
    {
        public MediaProgressResponse CreateOrUpdateProgress(CreateOrUpdateProgressRequest request, Guid userId);
        public MediaProgressResponse UpdatePersonalRating(UpdatePersonalRatingRequest request, Guid userId, Guid mediaId);

        public PagedResponse<MediaProgressResponse> GetAllUserProgress(int page, int pageSize, Guid userId, WatchStatus? status);
    }
}
