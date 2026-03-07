using Microsoft.AspNetCore.Mvc;
using WatchTrackerAPI.DTOs;
using WatchTrackerAPI.Interfaces;

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
        public IActionResult CreateUser(CreateUserRequest newUser)
        {
            var user = _userService.CreateUser(newUser);
            return CreatedAtAction("GetUser", new { id = user.Id }, newUser);
        }

        [HttpGet("{userId}")]
        public IActionResult GetUser(Guid userId)
        {
            try
            {
                var media = _userService.GetUser(userId);
                return Ok(media);
            }
            catch (InvalidOperationException exception)
            {
                return NotFound(exception.Message);
            }
        }
    }
}
