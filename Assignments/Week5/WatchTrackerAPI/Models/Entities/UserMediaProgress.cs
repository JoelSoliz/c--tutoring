using WatchTrackerAPI.Models.Enums;

namespace WatchTrackerAPI.Models.Entities
{
    public class UserMediaProgress
    {
        public Guid UserId { get; set; }
        public Guid MediaId { get; set; }
        public int EpisodesWatched { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public WatchStatus Status { get; set; }
        public int? PersonalRating { get; set; }

        public User User { get; set; }
        public Media Media { get; set; }
    }
}
