using Microsoft.AspNetCore.Mvc;

namespace Client.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ClientController> _logger;

        public ClientController(IHttpClientFactory httpClientFactory, ILogger<ClientController> logger)
        {
            _httpClient = httpClientFactory.CreateClient("ServiceAHttpClient");
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/ServiceA");
                response.EnsureSuccessStatusCode();

                var request = await response.Content.ReadAsStringAsync();

                return Ok(request);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "[Custom Log]::OMG!!! Seems like Service A is broken.");
                throw;
            }
           
        }
    }
}
