using System.ComponentModel.DataAnnotations;

namespace WatchTrackerAPI.Models.Entities
{
    public class Genre
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Media> Medias { get; set; }
    }
}