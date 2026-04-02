using Microsoft.AspNetCore.Identity;
using WatchTrackerAPI.Models.Entities;
using WatchTrackerAPI.Models.Enums;

namespace WatchTrackerAPI.Data
{
    public class DbSeeder
    {
        public static async Task SeedAsync(AppDBContext context)
        {
            if (context.Users.Any(u => u.Role == UserRoles.Moderator)) return;

            var hasher = new PasswordHasher<User>();

            var moderator = new User
            {
                Id = Guid.NewGuid(),
                Name = "Admin",
                Email = "admin@watchtracker.com",
                Role = UserRoles.Moderator
            };

            moderator.PasswordHash = hasher.HashPassword(moderator, "Admin1234!");

            context.Users.Add(moderator);
            await context.SaveChangesAsync();
        }
    }
}
