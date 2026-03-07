using System.ComponentModel.DataAnnotations;

namespace WatchTrackerAPI.DTOs
{
    public class UpdatePersonalRatingRequest
    {
        [Required]
        public int? PersonalRating { get; set; }
    }
}
