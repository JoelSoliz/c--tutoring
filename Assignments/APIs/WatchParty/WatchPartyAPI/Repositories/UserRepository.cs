using Microsoft.EntityFrameworkCore;
using WatchPartyAPI.Data;
using WatchPartyAPI.Interfaces.Repositories;
using WatchPartyAPI.Models;

namespace WatchPartyAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        public UserRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        public async Task CreateUser(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllUsers()
        {
            IQueryable<User> users = _dbContext.Users;
            var allUsers = await users.ToListAsync();
            return allUsers;
        }

        public async Task<User?> GetUser(Guid userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId);
            return user;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
            return user;
        }
    }
}
