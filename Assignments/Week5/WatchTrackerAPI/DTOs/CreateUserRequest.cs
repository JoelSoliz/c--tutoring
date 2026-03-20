using System.ComponentModel.DataAnnotations;

namespace WatchTrackerAPI.DTOs
{
    public class CreateUserRequest
    {
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
