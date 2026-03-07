using WatchTrackerAPI.Models.Enums;

namespace WatchTrackerAPI.DTOs
{
    public class MediaProgressResponse
    {
        public Guid UserId { get; set; }
        public Guid MediaId { get; set; }
        public string MediaTitle { get; set; }
        public MediaTypes MediaType { get; set; }
        public int? TotalEpisodes { get; set; }
        public int EpisodesWatched { get; set; }
        public WatchStatus WatchStatus { get; set; }
        public int? PersonalRating { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}
