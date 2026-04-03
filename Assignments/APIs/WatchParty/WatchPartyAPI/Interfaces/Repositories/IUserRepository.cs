using WatchPartyAPI.Models;

namespace WatchPartyAPI.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task CreateUser(User user);

        public Task UpdateUser(User user);
        public Task<User?> GetUser(Guid userId);
        public Task<User?> GetUserByEmail(string email);

        public Task<List<User>> GetAllUsers();
    }
}
