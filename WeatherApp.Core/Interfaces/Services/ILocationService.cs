using WeatherApp.Core.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherApp.Core.Interfaces.Services
{
    public interface ILocationService
    {
        /// <summary>
        /// Gets list of all available countries
        /// </summary>
        Task<IEnumerable<CountryDto>> GetCountriesAsync();

        /// <summary>
        /// Gets cities for a specific country
        /// </summary>
        Task<IEnumerable<CityDto>> GetCitiesByCountryAsync(string countryId);
    }
}