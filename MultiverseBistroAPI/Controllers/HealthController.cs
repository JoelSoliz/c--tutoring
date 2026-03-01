using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiverseBistroAPI.Models;

namespace MultiverseBistroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new HealthModel());
        }
    }
}
