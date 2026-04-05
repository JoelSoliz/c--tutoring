using System.ComponentModel.DataAnnotations;
using WatchTrackerAPI.Models.Enums;

namespace WatchTrackerAPI.Models.Entities
{
    public class Media
    {
        public Guid Id { get; set; }

        [MaxLength(255)]
        public string Title { get; set; }
        [MaxLength(20)]
        public MediaTypes Type { get; set; }
        public int? TotalEpisodes { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public Guid GenreId { get; set; }
        [MaxLength(255)]
        public string Image { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<UserMediaProgress> UserProgress { get; set; }

        public bool IsDeleted { get; set; } = false;

        public Genre Genre { get; set; }
    }
}
