using WatchTrackerAPI.DTOs;

namespace WatchTrackerAPI.Interfaces
{
    public interface IUserService
    {
        public Task<UserResponse> CreateUser(CreateUserRequest user);

        public Task<List<UserResponse>> GetAllUsers();
        public Task<UserResponse> GetUser(Guid userId);

        public Task DeleteUser(Guid userId);
    }
}
