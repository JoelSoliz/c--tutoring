using System.ComponentModel.DataAnnotations;

namespace WatchPartyAPI.Models
{
    public class User
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string UserName { get; set; }
        [MaxLength(255)]
        public string Email { get; set; }

        [MaxLength(255)]
        public string PasswordHash { get; set; }

        public ICollection<Participant> Participations { get; set; }
    }
}
