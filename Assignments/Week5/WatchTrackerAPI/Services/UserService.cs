using WatchTrackerAPI.DTOs;
using WatchTrackerAPI.Interfaces.Repositories;
using WatchTrackerAPI.Interfaces.Services;
using WatchTrackerAPI.Models.Entities;

namespace WatchTrackerAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserResponse> CreateUser(CreateUserRequest user)
        {
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Name = user.Name,
                Email = user.Email,
            };

            await _userRepository.CreateUser(newUser);

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
            var users = await _userRepository.GetAllUsers();
            return users.Select(user => new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Progresses = user.Progresses,

            }).ToList();
        }
        public async Task DeleteUser(Guid userId)
        {
            var exists = await FindValidUser(userId);
            if (exists == null)
            {
                throw new InvalidOperationException("The user doesn't exists");
            }
            else
            {
                exists.IsDeleted = true;
                await _userRepository.UpdateUser(exists);
            }
        }

        private async Task<User> FindValidUser(Guid userId)
        {
            var user = await _userRepository.GetUser(userId);
            if (user == null)
            {
                throw new InvalidOperationException($"The User with {userId} was not found");
            }
            return user;
        }
    }
}
