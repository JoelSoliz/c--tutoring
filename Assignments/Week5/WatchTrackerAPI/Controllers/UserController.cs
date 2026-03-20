using Microsoft.AspNetCore.Mvc;
using WatchTrackerAPI.DTOs;
using WatchTrackerAPI.Interfaces.Services;

namespace WatchTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserRequest newUser)
        {
            var user = await _userService.CreateUser(newUser);
            return CreatedAtAction("GetUser", new { userId = user.Id }, user);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            try
            {
                var media = await _userService.GetUser(userId);
                return Ok(media);
            }
            catch (InvalidOperationException exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            try
            {
                await _userService.DeleteUser(userId);
                return NoContent();
            }
            catch (InvalidOperationException exception)
            {
                return NotFound(exception.Message);
            }
        }
    }
}
