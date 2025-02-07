using WeatherApp.Core.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherApp.Core.Interfaces.Repositories
{
    public interface ILocationRepository
    {
        /// <summary>
        /// Gets all countries from the data store
        /// </summary>
        Task<IEnumerable<CountryDto>> GetCountriesAsync();

        /// <summary>
        /// Gets cities for a specific country from the data store
        /// </summary>
        Task<IEnumerable<CityDto>> GetCitiesByCountryAsync(string countryId);
    }
}