using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hearth.CMicroservice.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Healthy");
        }
    }
}
