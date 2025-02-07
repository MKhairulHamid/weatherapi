using System;

namespace WeatherApp.Core.Models
{
    public class WeatherForecast
    {
        public string Location { get; set; }
        public DateTime Time { get; set; }
        public WindInfo Wind { get; set; }
        public string Visibility { get; set; }
        public string SkyConditions { get; set; }
        public double TemperatureCelsius { get; set; }
        public double TemperatureFahrenheit => 32 + (TemperatureCelsius * 9 / 5.0);
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