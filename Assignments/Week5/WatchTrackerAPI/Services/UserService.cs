using Microsoft.EntityFrameworkCore;
using WatchTrackerAPI.Data;
using WatchTrackerAPI.DTOs;
using WatchTrackerAPI.Interfaces;
using WatchTrackerAPI.Models.Entities;

namespace WatchTrackerAPI.Services
{
    public class UserService : IUserService
    {
        private readonly AppDBContext _dbContext;
        public UserService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<UserResponse> CreateUser(CreateUserRequest user)
        {
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Name = user.Name,
                Email = user.Email,
            };

            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();

            return new UserResponse
            {
                Id = newUser.Id,
                Name = newUser.Name,
                Email = newUser.Email,
                Progresses = newUser.Progresses,
            };
        }

        public async Task<UserResponse> GetUser(Guid userId)
        {
            var user = await FindValidUser(userId);
            var response = new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Progresses = user.Progresses,
            };
            return response;
        }

        public async Task<List<UserResponse>> GetAllUsers()
        {
            IQueryable<User> query = _dbContext.Users;
            var users = await query.Select(user => new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Progresses = user.Progresses,
            }).ToListAsync();

            return users;
        }
        public async Task DeleteUser(Guid userId)
        {
            var user = await FindValidUser(userId);
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<User> FindValidUser(Guid userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId);
            if (user == null)
            {
                throw new InvalidOperationException($"The User with {userId} was not found");
            }
            return user;
        }
    }
}
