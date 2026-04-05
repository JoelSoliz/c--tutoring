using WatchTrackerAPI.DTOs.Requests;
using WatchTrackerAPI.DTOs.Responses;

namespace WatchTrackerAPI.Interfaces.Services
{
    public interface IAuthService
    {
        public Task<AuthResponse> Login(LoginUserRequest loginRequest);
        public Task<AuthResponse> Register(RegisterUserRequest registerRequest);

    }
}
