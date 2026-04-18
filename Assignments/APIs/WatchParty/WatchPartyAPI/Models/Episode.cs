using System.ComponentModel.DataAnnotations;

namespace WatchPartyAPI.Models
{
    public class Episode
    {
        public Guid Id { get; set; }
        [MaxLength(255)]
        public string AnimeTitle { get; set; }
        public int EpisodeNumber { get; set; }
        public int DurationSeconds { get; set; }

        public ICollection<WatchParty> WatchParties { get; set; } //extension of IEnumerable
    }
}
