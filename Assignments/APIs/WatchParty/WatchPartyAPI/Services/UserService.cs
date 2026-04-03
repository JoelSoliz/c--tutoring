using WatchPartyAPI.DTOs.Responses;
using WatchPartyAPI.Interfaces.Repositories;
using WatchPartyAPI.Interfaces.Services;
using WatchPartyAPI.Models;

namespace WatchPartyAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse> GetUser(Guid userId)
        {
            var user = await FindValidUser(userId);
            var response = new UserResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };
            return response;
        }

        public async Task<List<UserResponse>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return users.Select(user => new UserResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            }).ToList();
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

