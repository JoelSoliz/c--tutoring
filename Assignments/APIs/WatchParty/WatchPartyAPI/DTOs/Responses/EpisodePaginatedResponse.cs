namespace WatchPartyAPI.DTOs.Responses
{
    public class EpisodePaginatedResponse : PaginatedResponse
    {
        public IEnumerable<EpisodeResponse> Data { get; set; }
    }
}
