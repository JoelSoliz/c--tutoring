namespace WatchPartyAPI.DTOs.Requests
{
    public class CreateEpisodeRequest
    {
        public string AnimeTitle { get; set; }
        public int EpisodeNumber { get; set; }
        public int DurationSeconds { get; set; }
    }
}
