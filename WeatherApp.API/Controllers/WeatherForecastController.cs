// WeatherApp.API/Controllers/WeatherForecastController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WeatherApp.Core.Interfaces.Services;
using WeatherApp.Core.Models;

namespace WeatherApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IWeatherService weatherService, ILogger<WeatherForecastController> logger)
        {
            _weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{city}")]
        public async Task<ActionResult<WeatherForecast>> Get(string city)
        {
            // Input validation
            if (string.IsNullOrWhiteSpace(city))
            {
                _logger.LogWarning("City name is empty or null");
                return BadRequest("City name cannot be empty");
            }

            try
            {
                _logger.LogInformation("Fetching weather data for city: {City}", city);
                var weatherData = await _weatherService.GetWeatherForCityAsync(city);

                var forecast = new WeatherForecast
                {
                    Location = weatherData.Location,
                    Time = weatherData.Time,
                    Wind = new WindInfo
                    {
                        Speed = weatherData.Wind.Speed,
                        Direction = weatherData.Wind.Direction
                    },
                    Visibility = weatherData.Visibility,
                    SkyConditions = weatherData.SkyConditions,
                    TemperatureCelsius = (int)weatherData.TemperatureCelsius, // Cast to int as per WeatherForecast model
                    DewPoint = weatherData.DewPoint,
                    RelativeHumidity = weatherData.RelativeHumidity,
                    Pressure = weatherData.Pressure
                };

                return Ok(forecast);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching weather data for city: {City}", city);
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
    }
}