using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using WeatherApp.API.Controllers;
using WeatherApp.Core.Interfaces.Services;
using WeatherApp.Core.Models;
using WeatherApp.Core.Models.DTOs;
using WeatherApp.Core.Models.DTOs.External;
using Xunit;

namespace WeatherApp.Tests.Controllers
{
    public class WeatherControllerTests
    {
        private readonly Mock<IWeatherService> _mockWeatherService;
        private readonly Mock<ILogger<WeatherForecastController>> _mockLogger;
        private readonly WeatherForecastController _controller;

        public WeatherControllerTests()
        {
            _mockWeatherService = new Mock<IWeatherService>();
            _mockLogger = new Mock<ILogger<WeatherForecastController>>();
            _controller = new WeatherForecastController(_mockWeatherService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetWeather_WhenServiceThrows_ReturnsInternalServerError()
        {
            // Arrange
            var city = "London";
            _mockWeatherService
                .Setup(x => x.GetWeatherForCityAsync(city))
                .ThrowsAsync(new Exception("Service error"));

            // Act
            var result = await _controller.Get(city);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        public async Task GetWeather_WithInvalidCity_ReturnsBadRequest(string city)
        {
            // Act
            var result = await _controller.Get(city);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Equal("City name cannot be empty", badRequestResult.Value);
        }

        [Fact]
        public async Task GetWeather_WithValidCity_ReturnsOkResult()
        {
            // Arrange
            var city = "London";
            var weatherResponse = new WeatherResponseDto
            {
                Location = city,
                Time = DateTime.UtcNow,
                Wind = new Core.Models.DTOs.External.WindInfo
                {
                    Speed = 10,
                    Direction = "N"
                },
                Visibility = "10km",
                SkyConditions = "Clear",
                TemperatureCelsius = 20,
                TemperatureFahrenheit = 68,
                DewPoint = 15,
                RelativeHumidity = 65,
                Pressure = 1013.25
            };

            _mockWeatherService
                .Setup(x => x.GetWeatherForCityAsync(city))
                .ReturnsAsync(weatherResponse);

            // Act
            var result = await _controller.Get(city);

            // Assert
            var okResult = Assert.IsType<ActionResult<WeatherForecast>>(result);
            var value = Assert.IsType<OkObjectResult>(okResult.Result);
            var forecast = Assert.IsType<WeatherForecast>(value.Value);
            Assert.Equal(city, forecast.Location);
            Assert.Equal(20, forecast.TemperatureCelsius);
        }
    }
}