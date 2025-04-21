using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hearth.AMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Welcome to Hearth.AMicroservice!");
        }
    }
}
