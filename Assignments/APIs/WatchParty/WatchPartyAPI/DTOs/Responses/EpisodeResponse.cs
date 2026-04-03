namespace WatchPartyAPI.DTOs.Responses
{
    public class EpisodeResponse
    {
        public Guid Id { get; set; }
        public string AnimeTitle { get; set; }
        public int EpisodeNumber { get; set; }
        public int DurationSeconds { get; set; }
    }
}
