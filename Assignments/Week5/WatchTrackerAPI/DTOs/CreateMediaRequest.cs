using System.ComponentModel.DataAnnotations;
using WatchTrackerAPI.Models.Enums;

namespace WatchTrackerAPI.DTOs
{
    public class CreateMediaRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public MediaTypes Type { get; set; }
        [Required]
        public int? TotalEpisodes { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
    }
}
