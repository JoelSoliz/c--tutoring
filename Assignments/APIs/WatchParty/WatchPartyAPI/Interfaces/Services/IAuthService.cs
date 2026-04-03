using WatchPartyAPI.DTOs.Requests;
using WatchPartyAPI.DTOs.Responses;

namespace WatchPartyAPI.Interfaces.Services
{
    public interface IAuthService
    {
        public Task<AuthResponse> Login(LoginUserRequest loginRequest);
        public Task<AuthResponse> Register(RegisterUserRequest registerRequest);
    }
}
