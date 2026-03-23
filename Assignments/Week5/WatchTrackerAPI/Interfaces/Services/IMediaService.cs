using WatchTrackerAPI.DTOs;

namespace WatchTrackerAPI.Interfaces.Services
{
    public interface IMediaService
    {
        public Task<PagedResponse<MediaResponse>> GetAllMedia(MediaQueryParams mediaParams);
        public Task<MediaResponse> GetMedia(Guid id);
        public Task<MediaResponse> CreateMedia(CreateMediaRequest request);
        public Task DeleteMedia(Guid id);
    }
}
