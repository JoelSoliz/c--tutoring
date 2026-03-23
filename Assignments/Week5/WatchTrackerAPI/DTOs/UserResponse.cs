using WatchTrackerAPI.Models.Entities;

namespace WatchTrackerAPI.DTOs
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<UserMediaProgress> Progresses { get; set; }
    }
}
