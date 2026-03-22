using Microsoft.AspNetCore.Identity;
using MultiverseBistroAPI.DTOs;
using MultiverseBistroAPI.Interfaces.Repositories;
using MultiverseBistroAPI.Interfaces.Services;
using MultiverseBistroAPI.Models;

namespace MultiverseBistroAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _hasher;

        public AuthService(IJwtService jwtService, IUserRepository userRepository)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;
            _hasher = new PasswordHasher<User>();
        }

        public TokenDTO Login(LoginDTO loginDTO)
        {
            var existingUser = _userRepository.GetUserByEmail(loginDTO.Email);
            if (existingUser == null)
            {
                throw new Exception("Invalid credentials.");
            }

            var resultValidation = _hasher.VerifyHashedPassword(existingUser, existingUser.Password, loginDTO.Password);
            if (resultValidation == PasswordVerificationResult.Failed)
            {
                throw new Exception("Invalid credentials.");
            }

            return new TokenDTO()
            {
                AccessToken = _jwtService.GenerateToken(existingUser)
            };
        }

        public TokenDTO Register(RegisterDTO registerDTO)
        {
            var existingUser = _userRepository.GetUserByEmail(registerDTO.Email);
            if (existingUser != null)
            {
                throw new Exception("Email already exist.");
            }

            var user = new User()
            {
                UserId = Guid.NewGuid(),
                Email = registerDTO.Email,
                Role = registerDTO.Role,
            };
            user.Password = _hasher.HashPassword(user, registerDTO.Password);
            _userRepository.Add(user);
            _userRepository.Save();

            return new TokenDTO()
            {
                AccessToken = _jwtService.GenerateToken(user)
            };
        }
    }
}
