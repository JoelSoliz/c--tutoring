using System.ComponentModel.DataAnnotations;
using WatchPartyAPI.Models.Enums;

namespace WatchPartyAPI.Models
{
    public class Participant
    {
        public Guid UserId { get; set; }

        public Guid WatchPartyId { get; set; }
        [MaxLength(50)]
        public ParticipantRole Role { get; set; }

        public User User { get; set; }
        public WatchParty WatchParty { get; set; }
    }
}
