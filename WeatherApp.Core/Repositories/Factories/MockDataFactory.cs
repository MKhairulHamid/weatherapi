using WeatherApp.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using WeatherApp.Core.Models.DTOs.External;

namespace WeatherApp.Core.Repositories.Factories
{
    public static class MockDataFactory
    {
        public static WeatherResponseDto CreateMockWeatherResponse(string city)
        {
            var random = new Random();
            return new WeatherResponseDto
            {
                Location = city,
                Time = DateTime.UtcNow,
                Wind = new WindInfo
                {
                    Speed = random.Next(0, 30),
                    Direction = GetRandomDirection()
                },
                Visibility = $"{random.Next(5, 20)}km",
                SkyConditions = GetRandomSkyCondition(),
                TemperatureFahrenheit = random.Next(32, 100),
                TemperatureCelsius = random.Next(0, 38),
                DewPoint = random.Next(30, 70),
                RelativeHumidity = random.Next(30, 90),
                Pressure = random.Next(980, 1030)
            };
        }

        public static CountryDto CreateMockCountry(string id = null, string name = null)
        {
            return new CountryDto
            {
                Id = id ?? Guid.NewGuid().ToString(),
                Name = name ?? $"Country-{Guid.NewGuid().ToString().Substring(0, 8)}"
            };
        }

        public static CityDto CreateMockCity(string countryId, string id = null, string name = null)
        {
            return new CityDto
            {
                Id = id ?? Guid.NewGuid().ToString(),
                Name = name ?? $"City-{Guid.NewGuid().ToString().Substring(0, 8)}",
                CountryId = countryId
            };
        }

        private static string GetRandomDirection()
        {
            var directions = new[] { "N", "NE", "E", "SE", "S", "SW", "W", "NW" };
            return directions[new Random().Next(directions.Length)];
        }

        private static string GetRandomSkyCondition()
        {
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
            return conditions[new Random().Next(conditions.Length)];
        }
    }
}