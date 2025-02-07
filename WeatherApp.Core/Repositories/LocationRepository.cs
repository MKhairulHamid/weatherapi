using Microsoft.Extensions.Logging;
using WeatherApp.Core.Interfaces.Repositories;
using WeatherApp.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Core.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ILogger<LocationRepository> _logger;
        private static readonly Dictionary<string, List<CityDto>> _citiesByCountry;
        private static readonly List<CountryDto> _countries;

        static LocationRepository()
        {
            // Initialize mock data
            _countries = new List<CountryDto>
            {
                new CountryDto { Id = "US", Name = "United States" },
                new CountryDto { Id = "GB", Name = "United Kingdom" },
                new CountryDto { Id = "AU", Name = "Australia" },
                new CountryDto { Id = "CA", Name = "Canada" },
                new CountryDto { Id = "DE", Name = "Germany" }
            };

            _citiesByCountry = new Dictionary<string, List<CityDto>>
            {
                ["US"] = new List<CityDto>
                {
                    new CityDto { Id = "NY", Name = "New York", CountryId = "US" },
                    new CityDto { Id = "LA", Name = "Los Angeles", CountryId = "US" },
                    new CityDto { Id = "CH", Name = "Chicago", CountryId = "US" },
                    new CityDto { Id = "HO", Name = "Houston", CountryId = "US" },
                    new CityDto { Id = "PH", Name = "Phoenix", CountryId = "US" }
                },
                ["GB"] = new List<CityDto>
                {
                    new CityDto { Id = "LON", Name = "London", CountryId = "GB" },
                    new CityDto { Id = "MAN", Name = "Manchester", CountryId = "GB" },
                    new CityDto { Id = "BIR", Name = "Birmingham", CountryId = "GB" },
                    new CityDto { Id = "LIV", Name = "Liverpool", CountryId = "GB" },
                    new CityDto { Id = "EDI", Name = "Edinburgh", CountryId = "GB" }
                },
                ["AU"] = new List<CityDto>
                {
                    new CityDto { Id = "SYD", Name = "Sydney", CountryId = "AU" },
                    new CityDto { Id = "MEL", Name = "Melbourne", CountryId = "AU" },
                    new CityDto { Id = "BRI", Name = "Brisbane", CountryId = "AU" },
                    new CityDto { Id = "PER", Name = "Perth", CountryId = "AU" },
                    new CityDto { Id = "ADE", Name = "Adelaide", CountryId = "AU" }
                },
                ["CA"] = new List<CityDto>
                {
                    new CityDto { Id = "TOR", Name = "Toronto", CountryId = "CA" },
                    new CityDto { Id = "VAN", Name = "Vancouver", CountryId = "CA" },
                    new CityDto { Id = "MTL", Name = "Montreal", CountryId = "CA" },
                    new CityDto { Id = "CAL", Name = "Calgary", CountryId = "CA" },
                    new CityDto { Id = "OTT", Name = "Ottawa", CountryId = "CA" }
                },
                ["DE"] = new List<CityDto>
                {
                    new CityDto { Id = "BER", Name = "Berlin", CountryId = "DE" },
                    new CityDto { Id = "MUN", Name = "Munich", CountryId = "DE" },
                    new CityDto { Id = "HAM", Name = "Hamburg", CountryId = "DE" },
                    new CityDto { Id = "FRA", Name = "Frankfurt", CountryId = "DE" },
                    new CityDto { Id = "COL", Name = "Cologne", CountryId = "DE" }
                }
            };
        }

        public LocationRepository(ILogger<LocationRepository> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<IEnumerable<CountryDto>> GetCountriesAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving countries list from repository");
                return Task.FromResult(_countries.AsEnumerable());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving countries list from repository");
                throw;
            }
        }

        public Task<IEnumerable<CityDto>> GetCitiesByCountryAsync(string countryId)
        {
            try
            {
                _logger.LogInformation("Retrieving cities for country {CountryId}", countryId);

                if (_citiesByCountry.TryGetValue(countryId, out var cities))
                {
                    return Task.FromResult(cities.AsEnumerable());
                }

                _logger.LogWarning("No cities found for country {CountryId}", countryId);
                return Task.FromResult(Enumerable.Empty<CityDto>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving cities for country {CountryId}", countryId);
                throw;
            }
        }
    }
}