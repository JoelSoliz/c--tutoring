using System.ComponentModel.DataAnnotations;
using WatchTrackerAPI.Models.Enums;

namespace WatchTrackerAPI.Models.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Email { get; set; }

        [MaxLength(255)]
        public string PasswordHash { get; set; }

        public UserRoles Role { get; set; } = UserRoles.User;

        public List<UserMediaProgress> Progresses { get; set; } = new List<UserMediaProgress>(); //initialize directly

        public bool IsDeleted { get; set; } = false;
    }
}
