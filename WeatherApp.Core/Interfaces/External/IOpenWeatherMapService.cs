using WeatherApp.Core.Models.DTOs.External;
using System.Threading.Tasks;

namespace WeatherApp.Core.Interfaces.External
{
    public interface IOpenWeatherMapService
    {
        /// <summary>
        /// Gets weather data from OpenWeatherMap API
        /// </summary>
        Task<OpenWeatherMapResponse> GetWeatherDataAsync(string city);
    }
}