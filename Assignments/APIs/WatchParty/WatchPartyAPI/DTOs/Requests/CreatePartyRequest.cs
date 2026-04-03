using System.ComponentModel.DataAnnotations;

namespace WatchPartyAPI.DTOs.Requests
{
    public class CreatePartyRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public Guid CurrentEpisodeId { get; set; }
    }
}
