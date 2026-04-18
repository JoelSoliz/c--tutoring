using WatchPartyAPI.Models.Enums;

namespace WatchPartyAPI.DTOs.Responses
{
    public class WatchPartyResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid HostUserId { get; set; }
        public Guid CurrentEpisodeId { get; set; }
        public PartyStatus Status { get; set; }
    }
}
