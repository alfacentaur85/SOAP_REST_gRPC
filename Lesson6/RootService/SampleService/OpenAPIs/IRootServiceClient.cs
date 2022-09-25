namespace SampleService.Services
{
    public interface IRootServiceClient
    {
        public RootServiceReference.RootServiceClient Client { get; }

        public Task<ICollection<RootServiceReference.WeatherForecast>> Get();
    }
}
