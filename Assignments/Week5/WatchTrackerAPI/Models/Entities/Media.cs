using WatchTrackerAPI.Models.Enums;

namespace WatchTrackerAPI.Models.Entities
{
    public class Media
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public MediaTypes Type { get; set; }
        public int? TotalEpisodes { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Guid GenreId { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<UserMediaProgress> UserProgress { get; set; }

        public bool IsDeleted { get; set; } = false;

        public Genre Genre { get; set; }
    }
}
