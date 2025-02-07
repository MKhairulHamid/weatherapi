// WeatherResponseDto.cs
using System.Collections.Generic;
using System;

namespace WeatherApp.Core.Models.DTOs.External
{
    public class WeatherResponseDto
    {
        public string Location { get; set; }
        public DateTime Time { get; set; }
        public WindInfo Wind { get; set; }
        public string Visibility { get; set; }
        public string SkyConditions { get; set; }
        public double TemperatureCelsius { get; set; }
        public double TemperatureFahrenheit { get; set; }
        public double DewPoint { get; set; }
        public int RelativeHumidity { get; set; }
        public double Pressure { get; set; }
    }

    public class WindInfo
    {
        public double Speed { get; set; }
        public string Direction { get; set; }
    }
}

// OpenWeatherMapResponse.cs
namespace WeatherApp.Core.Models.DTOs.External
{
    public class OpenWeatherMapResponse
    {
        public MainInfo Main { get; set; }
        public WindData Wind { get; set; }
        public List<WeatherInfo> Weather { get; set; }
        public string Name { get; set; }
        public int Visibility { get; set; }

        public class MainInfo
        {
            public double Temp { get; set; }
            public int Humidity { get; set; }
            public double Pressure { get; set; }
        }

        public class WindData
        {
            public double Speed { get; set; }
            public double Deg { get; set; }
        }

        public class WeatherInfo
        {
            public string Description { get; set; }
        }
    }
}