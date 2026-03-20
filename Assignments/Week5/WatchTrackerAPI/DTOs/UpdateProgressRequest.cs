using WatchTrackerAPI.Models.Enums;

namespace WatchTrackerAPI.DTOs
{
    public class UpdateProgressRequest
    {
        public int EpisodesWatched { get; set; }
        public WatchStatus WatchStatus { get; set; }
        public int? PersonalRating { get; set; }
    }
}
