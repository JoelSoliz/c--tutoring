using Microsoft.AspNetCore.Identity;
using WatchPartyAPI.DTOs.Requests;
using WatchPartyAPI.DTOs.Responses;
using WatchPartyAPI.Interfaces.Repositories;
using WatchPartyAPI.Interfaces.Services;
using WatchPartyAPI.Models;

namespace WatchPartyAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly PasswordHasher<User> _hasher;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository userRepository, IJwtTokenService jwtTokenService, IConfiguration config)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
            _hasher = new PasswordHasher<User>();
            _config = config;
        }

        public async Task<AuthResponse> Login(LoginUserRequest loginRequest)
        {
            var user = await _userRepository.GetUserByEmail(loginRequest.Email);
            var expirationMinutes = int.Parse(_config["Jwt:ExpirationMinutes"]);
            if (user == null)
            {
                throw new Exception("Invalid credentials.");
            }

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, loginRequest.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new Exception("Invalid credentials.");
            }

            var token = _jwtTokenService.GenerateToken(user);
            return new AuthResponse
            {
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(expirationMinutes),
                UserId = user.Id,
                Username = user.UserName
            };
        }

        public async Task<AuthResponse> Register(RegisterUserRequest registerRequest)
        {
            var exists = await _userRepository.GetUserByEmail(registerRequest.Email);
            var expirationMinutes = int.Parse(_config["Jwt:ExpirationMinutes"]);
            if (exists != null)
            {
                throw new Exception("Email already registered");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = registerRequest.UserName,
                Email = registerRequest.Email
            };

            user.PasswordHash = _hasher.HashPassword(user, registerRequest.Password);
            await _userRepository.CreateUser(user);

            var token = _jwtTokenService.GenerateToken(user);

            return new AuthResponse
            {
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(expirationMinutes),
                UserId = user.Id,
                Username = user.UserName
            };
        }
    }
}
