using WatchPartyAPI.Models.Enums;

namespace WatchPartyAPI.DTOs.Responses
{
    public class ParticipantResponse
    {
        public Guid UserId { get; set; }
        public Guid WatchPartyId { get; set; }
        public ParticipantRole Role { get; set; }
    }
}
