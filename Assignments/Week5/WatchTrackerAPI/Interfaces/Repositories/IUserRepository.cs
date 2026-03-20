using WatchTrackerAPI.Models.Entities;

namespace WatchTrackerAPI.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task CreateUser(User user);

        public Task UpdateUser(User user);
        public Task<User?> GetUser(Guid userId);

        public Task<List<User>> GetAllUsers();
    }
}
