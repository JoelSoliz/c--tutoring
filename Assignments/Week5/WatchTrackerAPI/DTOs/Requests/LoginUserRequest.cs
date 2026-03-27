using System.ComponentModel.DataAnnotations;

namespace WatchTrackerAPI.DTOs.Requests
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
