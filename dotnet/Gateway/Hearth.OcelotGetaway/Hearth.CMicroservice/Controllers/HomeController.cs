using Microsoft.AspNetCore.Mvc;

namespace Hearth.CMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Welcome to Hearth.CMicroservice!");
        }
    }
}
