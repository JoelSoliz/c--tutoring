using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiverseBistroAPI.DTOs;
using MultiverseBistroAPI.Interfaces.Services;
using MultiverseBistroAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MultiverseBistroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDTO data)
        {
            return Ok(_authService.Register(data));
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO data)
        {
            return Ok(_authService.Login(data));
        }

        [HttpGet]
        [Authorize]
        public IActionResult Me()
        {
            return Ok(new
            {
                User = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                Role = User.FindFirst(ClaimTypes.Role)?.Value
            });
        }
    }
}
