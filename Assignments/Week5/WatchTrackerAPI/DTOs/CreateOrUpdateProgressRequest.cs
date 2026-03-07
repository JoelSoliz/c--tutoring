using System.ComponentModel.DataAnnotations;
using WatchTrackerAPI.Models.Enums;

namespace WatchTrackerAPI.DTOs
{
    public class CreateOrUpdateProgressRequest
    {
        [Required]
        public Guid MediaId { get; set; }
        public int EpisodesWatched { get; set; }
        public WatchStatus WatchStatus { get; set; }
        public int? PersonalRating { get; set; }
    }
}
