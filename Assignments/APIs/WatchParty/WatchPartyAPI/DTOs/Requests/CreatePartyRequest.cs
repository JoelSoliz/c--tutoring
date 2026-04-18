namespace WatchPartyAPI.DTOs.Requests
{
    public class CreatePartyRequest
    {
        public string Title { get; set; }
        public Guid CurrentEpisodeId { get; set; }
    }
}
