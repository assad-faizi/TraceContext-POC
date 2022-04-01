using Microsoft.AspNetCore.Mvc;

namespace ServiceA.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceAController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello from Service A");
        }
    }
}
