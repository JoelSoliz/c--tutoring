using System.ComponentModel.DataAnnotations;

namespace WatchTrackerAPI.DTOs
{
    public class CreateUserRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
