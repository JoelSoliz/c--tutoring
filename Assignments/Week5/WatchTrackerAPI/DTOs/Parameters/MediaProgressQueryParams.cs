using WatchTrackerAPI.Models.Enums;

namespace WatchTrackerAPI.DTOs.Parameters
{
    public class MediaProgressQueryParams : PaginationParams
    {
        public WatchStatus? Status { get; set; } = null;
    }
}
