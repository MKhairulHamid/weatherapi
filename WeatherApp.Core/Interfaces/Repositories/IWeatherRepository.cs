using WeatherApp.Core.Models.DTOs;
using System.Threading.Tasks;
using WeatherApp.Core.Models.DTOs.External;

namespace WeatherApp.Core.Interfaces.Repositories
{
    public interface IWeatherRepository
    {
        /// <summary>
        /// Gets mock weather data for testing purposes
        /// </summary>
        Task<WeatherResponseDto> GetMockWeatherDataAsync(string city);
    }
}