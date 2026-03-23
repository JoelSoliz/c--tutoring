using WatchTrackerAPI.Models.Enums;

namespace WatchTrackerAPI.DTOs
{
    public class MediaProgressQueryParams : PaginationParams
    {
        public WatchStatus? Status { get; set; } = null;
    }
}
