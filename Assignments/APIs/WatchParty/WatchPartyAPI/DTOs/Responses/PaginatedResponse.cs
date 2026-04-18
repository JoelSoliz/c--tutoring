namespace WatchPartyAPI.DTOs.Responses
{
    public class PaginatedResponse
    {
        public int Limit { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
    }
}
