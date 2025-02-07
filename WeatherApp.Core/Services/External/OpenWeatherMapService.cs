using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherApp.Core.Interfaces.External;
using WeatherApp.Core.Models.DTOs.External;

namespace WeatherApp.Core.Services.External
{
    public class OpenWeatherMapService : IOpenWeatherMapService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<OpenWeatherMapService> _logger;

        public OpenWeatherMapService(
            HttpClient httpClient,
            IConfiguration configuration,
            ILogger<OpenWeatherMapService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<OpenWeatherMapResponse> GetWeatherDataAsync(string city)
        {
            try
            {
                var apiKey = _configuration["OpenWeatherMap:ApiKey"];
                var response = await _httpClient.GetAsync(
                    $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=imperial");

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var weatherData = JsonSerializer.Deserialize<OpenWeatherMapResponse>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return weatherData;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error calling OpenWeatherMap API for city {City}", city);
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error deserializing OpenWeatherMap response for city {City}", city);
                throw;
            }
        }
    }
}