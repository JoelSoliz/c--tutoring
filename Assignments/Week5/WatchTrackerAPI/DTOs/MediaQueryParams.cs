using WatchTrackerAPI.Models.Enums;

namespace WatchTrackerAPI.DTOs
{
    public class MediaQueryParams : PaginationParams
    {
        public MediaTypes? Type { get; set; } = null;
    }
}
