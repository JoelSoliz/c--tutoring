using WatchTrackerAPI.Models.Entities;
using WatchTrackerAPI.Models.Enums;

namespace WatchTrackerAPI.DTOs.Responses
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public UserRoles Roles { get; set; }
        public List<UserMediaProgress> Progresses { get; set; }
    }
}
