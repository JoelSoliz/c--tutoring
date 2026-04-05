using WatchTrackerAPI.Models.Enums;

namespace WatchTrackerAPI.DTOs.Parameters
{
    public class MediaQueryParams : PaginationParams
    {
        public MediaTypes? Type { get; set; } = null;
    }
}
