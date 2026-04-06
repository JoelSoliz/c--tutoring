namespace WatchPartyAPI.DTOs.Responses
{
    public class EpisodePaginatedResponse
    {
        public int Limit { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public IEnumerable<EpisodeResponse> Data { get; set; }
    }
}
