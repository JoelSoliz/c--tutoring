using System.ComponentModel.DataAnnotations;

namespace WatchTrackerAPI.DTOs.Requests
{
    public class RegisterUserRequest
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
