using Microsoft.AspNetCore.Mvc;
using SampleService.Services;

namespace SampleService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;

        private IRootServiceClient _rootServiceClient;

        public WeatherForecastController(
            IRootServiceClient rootServiceClient,
            ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _rootServiceClient = rootServiceClient;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<ActionResult<IEnumerable<RootServiceReference.WeatherForecast>>> Get()
        {
            return Ok(await _rootServiceClient.Get());
        }
    }
}