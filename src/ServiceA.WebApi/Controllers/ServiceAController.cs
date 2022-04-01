using Microsoft.AspNetCore.Mvc;

namespace ServiceA.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceAController : ControllerBase
    {
        private readonly ILogger<ServiceAController> _logger;

        public ServiceAController(ILogger<ServiceAController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                throw new Exception("Boo Hoo!!! Service A threw an Exception!!!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Custom Log]::Erm something broke in Service A");
                throw;
            }
        }
    }
}
