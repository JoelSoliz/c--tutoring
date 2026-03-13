using WatchTrackerAPI.DTOs;

namespace WatchTrackerAPI.Interfaces
{
    public interface IMediaProgressService
    {
        public Task<MediaProgressResponse> CreateOrUpdateProgress(CreateOrUpdateProgressRequest request, Guid userId);
        public Task<MediaProgressResponse> UpdatePersonalRating(UpdatePersonalRatingRequest request, Guid userId, Guid mediaId);

        public Task<PagedResponse<MediaProgressResponse>> GetAllUserProgress(Guid userId, MediaProgressQueryParams mediaProgressParams);

        public Task DeleteUserProgress(Guid userId, Guid mediaId);
    }
}
