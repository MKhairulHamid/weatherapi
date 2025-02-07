using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using WeatherApp.Core.Interfaces.Services;
using WeatherApp.Core.Interfaces.External;
using WeatherApp.Core.Interfaces.Infrastructure;
using WeatherApp.Core.Models.DTOs;
using System;
using System.Threading.Tasks;
using WeatherApp.Core.Models.DTOs.External;

namespace WeatherApp.Core.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IOpenWeatherMapService _openWeatherMapService;
        private readonly ICacheService _cacheService;
        private readonly ILogger<WeatherService> _logger;
        private readonly IConfiguration _configuration;

        public WeatherService(
            IOpenWeatherMapService openWeatherMapService,
            ICacheService cacheService,
            ILogger<WeatherService> logger,
            IConfiguration configuration)
        {
            _openWeatherMapService = openWeatherMapService ?? throw new ArgumentNullException(nameof(openWeatherMapService));
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<WeatherResponseDto> GetWeatherForCityAsync(string city)
        {
            try
            {
                // Try to get from cache first
                var cacheKey = $"weather_{city}".ToLower();
                var cachedData = await _cacheService.GetAsync<WeatherResponseDto>(cacheKey);

                if (cachedData != null)
                {
                    _logger.LogInformation("Weather data for {City} retrieved from cache", city);
                    return cachedData;
                }

                // Get fresh data from OpenWeatherMap
                var weatherData = await _openWeatherMapService.GetWeatherDataAsync(city);

                // Map to our DTO
                var response = new WeatherResponseDto
                {
                    Location = weatherData.Name,
                    Time = DateTime.UtcNow,
                    Wind = new WindInfo
                    {
                        Speed = weatherData.Wind.Speed,
                        Direction = ConvertDegreesToDirection(weatherData.Wind.Deg)
                    },
                    Visibility = $"{weatherData.Visibility / 1000}km",
                    SkyConditions = weatherData.Weather[0].Description,
                    TemperatureFahrenheit = weatherData.Main.Temp,
                    TemperatureCelsius = ConvertFahrenheitToCelsius(weatherData.Main.Temp),
                    DewPoint = CalculateDewPoint(weatherData.Main.Temp, weatherData.Main.Humidity),
                    RelativeHumidity = weatherData.Main.Humidity,
                    Pressure = weatherData.Main.Pressure
                };

                // Cache the response
                await _cacheService.SetAsync(cacheKey, response, TimeSpan.FromMinutes(30));

                _logger.LogInformation("Weather data retrieved successfully for {City}", city);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving weather data for {City}", city);
                throw;
            }
        }

        public double ConvertFahrenheitToCelsius(double fahrenheit)
        {
            return Math.Round((fahrenheit - 32) * 5 / 9, 2);
        }

        private string ConvertDegreesToDirection(double degrees)
        {
            string[] directions = { "N", "NE", "E", "SE", "S", "SW", "W", "NW" };
            int index = (int)Math.Round(degrees / 45) % 8;
            return directions[index];
        }

        private double CalculateDewPoint(double temperatureFahrenheit, int humidity)
        {
            // Magnus formula for dew point calculation
            double celsius = ConvertFahrenheitToCelsius(temperatureFahrenheit);
            double alpha = ((17.27 * celsius) / (237.7 + celsius)) + Math.Log(humidity / 100.0);
            double dewPointCelsius = (237.7 * alpha) / (17.27 - alpha);
            return Math.Round((dewPointCelsius * 9 / 5) + 32, 2); // Convert back to Fahrenheit
        }
    }
}