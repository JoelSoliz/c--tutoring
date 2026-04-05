using WatchTrackerAPI.DTOs.Responses;

namespace WatchTrackerAPI.Interfaces.Services
{
    public interface IUserService
    {
        public Task<List<UserResponse>> GetAllUsers();
        public Task<UserResponse> GetUser(Guid userId);

        public Task DeleteUser(Guid userId);
    }
}
