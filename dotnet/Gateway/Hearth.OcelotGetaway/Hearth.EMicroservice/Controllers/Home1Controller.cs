using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hearth.EMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Home1Controller : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Welcome to Hearth.EMicroservice!");
        }
    }
}
