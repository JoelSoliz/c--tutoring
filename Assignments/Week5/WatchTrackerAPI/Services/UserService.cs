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
        public UserResponse CreateUser(CreateUserRequest user)
        {
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Name = user.Name,
                Email = user.Email,
            };

            _dbContext.Users.Add(newUser);

            return new UserResponse
            {
                Id = newUser.Id,
                Name = newUser.Name,
                Email = newUser.Email,
                Progresses = newUser.Progresses,
            };
        }

        public UserResponse GetUser(Guid userId)
        {
            var user = _dbContext.Users.FirstOrDefault(user => user.Id == userId);
            if (user == null)
            {
                throw new InvalidOperationException($"The User with {userId} was not found");
            }

            var response = new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Progresses = user.Progresses,
            };
            return response;
        }
    }
}
