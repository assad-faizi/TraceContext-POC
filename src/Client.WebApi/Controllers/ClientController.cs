using Microsoft.AspNetCore.Mvc;

namespace Client.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public ClientController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ServiceAHttpClient");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _httpClient.GetAsync("api/ServiceA");
            response.EnsureSuccessStatusCode();

            var request = await response.Content.ReadAsStringAsync();

            return Ok(request);
        }
    }
}
