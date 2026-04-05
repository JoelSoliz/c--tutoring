using Microsoft.EntityFrameworkCore;
using WatchTrackerAPI.Data;
using WatchTrackerAPI.Interfaces.Repositories;
using WatchTrackerAPI.Models.Entities;

namespace WatchTrackerAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDBContext _dbContext;
        public UserRepository(AppDBContext appDbContext)
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
            users = users.Where(user => user.IsDeleted == false);
            var allUsers = await users.ToListAsync();
            return allUsers;
        }

        public async Task<User?> GetUser(Guid userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId && user.IsDeleted == false);
            return user;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
            return user;
        }
    }
}
