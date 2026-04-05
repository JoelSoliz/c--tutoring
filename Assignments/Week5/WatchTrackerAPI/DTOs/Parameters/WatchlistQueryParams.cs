using WatchTrackerAPI.Models.Enums;

namespace WatchTrackerAPI.DTOs.Parameters
{
    public class WatchlistQueryParams
    {
        public string Genre { get; set; }
        public MediaTypes Type { get; set; }
    }
}
