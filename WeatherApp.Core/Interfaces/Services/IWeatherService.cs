using WeatherApp.Core.Models.DTOs;
using System.Threading.Tasks;
using WeatherApp.Core.Models.DTOs.External;

namespace WeatherApp.Core.Interfaces.Services
{
    public interface IWeatherService
    {
        /// <summary>
        /// Gets weather information for a specific city
        /// </summary>
        Task<WeatherResponseDto> GetWeatherForCityAsync(string city);

        /// <summary>
        /// Converts temperature from Fahrenheit to Celsius
        /// </summary>
        double ConvertFahrenheitToCelsius(double fahrenheit);
    }
}
