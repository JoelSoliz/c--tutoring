using WatchTrackerAPI.DTOs;

namespace WatchTrackerAPI.Interfaces.Services
{
    public interface IMediaProgressService
    {
        public Task<MediaProgressResponse> CreateUserProgress(CreateProgressRequest request, Guid userId);
        public Task<MediaProgressResponse> UpdateUserProgress(UpdateProgressRequest request, Guid userId, Guid mediaId);

        public Task<PagedResponse<MediaProgressResponse>> GetAllUserProgress(Guid userId, MediaProgressQueryParams mediaProgressParams);

        public Task<MediaProgressResponse> GetUserProgress(Guid userId, Guid mediaId);

        public Task DeleteUserProgress(Guid userId, Guid mediaId);
    }
}
