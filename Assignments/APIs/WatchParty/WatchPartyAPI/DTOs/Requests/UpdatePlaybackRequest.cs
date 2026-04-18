using WatchPartyAPI.Models.Enums;

namespace WatchPartyAPI.DTOs.Requests
{
    public class UpdatePlaybackRequest
    {
        public PartyStatus Status { get; set; }
    }
}
