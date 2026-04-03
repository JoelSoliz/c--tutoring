using WatchPartyAPI.DTOs.Responses;

namespace WatchPartyAPI.Interfaces.Services
{
    public interface IUserService
    {
        public Task<List<UserResponse>> GetAllUsers();
        public Task<UserResponse> GetUser(Guid userId);

    }
}
