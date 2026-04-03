using System.ComponentModel.DataAnnotations;
using WatchPartyAPI.Models.Enums;

namespace WatchPartyAPI.Models
{
    public class WatchParty
    {
        public Guid Id { get; set; }

        [MaxLength(255)]
        public string Title { get; set; }
        public Guid HostUserId { get; set; }
        public Guid CurrentEpisodeId { get; set; }
        [MaxLength(50)]
        public PartyStatus Status { get; set; }

        public User Host { get; set; }
        public Episode CurrentEpisode { get; set; }
        public ICollection<Participant> Participants { get; set; }
    }
}
