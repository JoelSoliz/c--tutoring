using System.ComponentModel.DataAnnotations;
using WatchTrackerAPI.Models.Enums;

namespace WatchTrackerAPI.DTOs.Requests
{
    public class CreateMediaRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public MediaTypes Type { get; set; }
        [Required]
        public int? TotalEpisodes { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public string Genre { get; set; }
    }
}
