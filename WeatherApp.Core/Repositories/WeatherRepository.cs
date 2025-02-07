using Microsoft.Extensions.Logging;
using WeatherApp.Core.Interfaces.Repositories;
using WeatherApp.Core.Models.DTOs;
using System;
using System.Threading.Tasks;
using WeatherApp.Core.Models.DTOs.External;

namespace WeatherApp.Core.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly ILogger<WeatherRepository> _logger;
        private readonly Random _random;

        public WeatherRepository(ILogger<WeatherRepository> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _random = new Random();
        }

        public Task<WeatherResponseDto> GetMockWeatherDataAsync(string city)
        {
            try
            {
                _logger.LogInformation("Generating mock weather data for {City}", city);

                // Generate random but realistic weather data
                var tempF = _random.Next(32, 100);  // Temperature between 32°F and 100°F
                var humidity = _random.Next(30, 90); // Humidity between 30% and 90%
                var windSpeed = _random.Next(0, 30); // Wind speed between 0 and 30 mph
                var windDirection = _random.Next(0, 360); // Wind direction in degrees
                var pressure = _random.Next(980, 1030); // Pressure between 980 and 1030 hPa
                var visibility = _random.Next(5, 20); // Visibility between 5 and 20 km

                var conditions = new[]
                {
                    "Clear sky",
                    "Few clouds",
                    "Scattered clouds",
                    "Broken clouds",
                    "Light rain",
                    "Moderate rain",
                    "Sunny",
                    "Partly cloudy"
                };

                var directions = new[] { "N", "NE", "E", "SE", "S", "SW", "W", "NW" };
                var directionIndex = (int)Math.Round(windDirection / 45.0) % 8;

                var response = new WeatherResponseDto
                {
                    Location = city,
                    Time = DateTime.UtcNow,
                    Wind = new WindInfo
                    {
                        Speed = windSpeed,
                        Direction = directions[directionIndex]
                    },
                    Visibility = $"{visibility}km",
                    SkyConditions = conditions[_random.Next(conditions.Length)],
                    TemperatureFahrenheit = tempF,
                    TemperatureCelsius = Math.Round((tempF - 32) * 5 / 9.0, 2),
                    DewPoint = CalculateDewPoint(tempF, humidity),
                    RelativeHumidity = humidity,
                    Pressure = pressure
                };

                _logger.LogInformation("Successfully generated mock weather data for {City}", city);
                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating mock weather data for {City}", city);
                throw;
            }
        }

        private double CalculateDewPoint(double tempF, int humidity)
        {
            // Convert to Celsius for calculation
            var tempC = (tempF - 32) * 5 / 9.0;

            // Magnus formula coefficients
            const double a = 17.27;
            const double b = 237.7;

            // Calculate gamma
            var gamma = ((a * tempC) / (b + tempC)) + Math.Log(humidity / 100.0);

            // Calculate dew point in Celsius
            var dewPointC = (b * gamma) / (a - gamma);

            // Convert back to Fahrenheit
            return Math.Round((dewPointC * 9 / 5.0) + 32, 2);
        }
    }
}