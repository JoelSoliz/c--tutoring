using System.ComponentModel.DataAnnotations;

namespace WatchPartyAPI.DTOs.Requests
{
    public class LoginUserRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
