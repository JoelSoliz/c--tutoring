namespace WatchPartyAPI.DTOs.Responses
{
    public class WatchPartyPaginatedResponse : PaginatedResponse
    {
        public IEnumerable<WatchPartyResponse> Data { get; set; }
    }
}
