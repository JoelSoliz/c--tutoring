using WatchTrackerAPI.DTOs;
using WatchTrackerAPI.Models.Enums;

namespace WatchTrackerAPI.Interfaces
{
    public interface IMediaService
    {
        public PagedResponse<MediaResponse> GetAllMedia(int page, int pageSize, MediaTypes? type);
        public MediaResponse GetMedia(Guid id);
        public MediaResponse CreateMedia(CreateMediaRequest request);
    }
}
