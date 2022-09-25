using RootServiceReference;
using SampleService.Services;

namespace SampleService.OpenAPIs.Impl
{
    public class RootServiceClient : IRootServiceClient
    {

        private readonly ILogger<RootServiceClient> _logger;
        private RootServiceReference.RootServiceClient _httpClient;

        public RootServiceClient(HttpClient httpClient,
            ILogger<RootServiceClient> logger)
        {
            _logger = logger;
            _httpClient = new RootServiceReference.RootServiceClient("http://localhost:5075/", httpClient);
        }

        public RootServiceReference.RootServiceClient Client => _httpClient;

        public async Task<ICollection<WeatherForecast>> Get()
        {
            return await _httpClient.GetWeatherForecastAsync();
        }
    }
}
