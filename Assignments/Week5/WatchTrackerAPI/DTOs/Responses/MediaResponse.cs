using WatchTrackerAPI.Models.Enums;

namespace WatchTrackerAPI.DTOs.Responses
{
    public class MediaResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public MediaTypes Type { get; set; }
        public int? TotalEpisodes { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public GenreResponse Genre { get; set; }
        public string Image { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
