using Microsoft.Extensions.Logging;
using WeatherApp.Core.Interfaces.Services;
using WeatherApp.Core.Interfaces.Repositories;
using WeatherApp.Core.Interfaces.Infrastructure;
using WeatherApp.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherApp.Core.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly ICacheService _cacheService;
        private readonly ILogger<LocationService> _logger;

        public LocationService(
            ILocationRepository locationRepository,
            ICacheService cacheService,
            ILogger<LocationService> logger)
        {
            _locationRepository = locationRepository ?? throw new ArgumentNullException(nameof(locationRepository));
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<CountryDto>> GetCountriesAsync()
        {
            try
            {
                // Try to get from cache first
                var cacheKey = "countries_list";
                var cachedData = await _cacheService.GetAsync<IEnumerable<CountryDto>>(cacheKey);

                if (cachedData != null)
                {
                    _logger.LogInformation("Countries list retrieved from cache");
                    return cachedData;
                }

                // Get from repository
                var countries = await _locationRepository.GetCountriesAsync();

                // Cache the response (longer duration as this data rarely changes)
                await _cacheService.SetAsync(cacheKey, countries, TimeSpan.FromDays(1));

                _logger.LogInformation("Countries list retrieved successfully");
                return countries;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving countries list");
                throw;
            }
        }

        public async Task<IEnumerable<CityDto>> GetCitiesByCountryAsync(string countryId)
        {
            if (string.IsNullOrWhiteSpace(countryId))
            {
                throw new ArgumentException("Country ID cannot be empty", nameof(countryId));
            }

            try
            {
                // Try to get from cache first
                var cacheKey = $"cities_{countryId}".ToLower();
                var cachedData = await _cacheService.GetAsync<IEnumerable<CityDto>>(cacheKey);

                if (cachedData != null)
                {
                    _logger.LogInformation("Cities list for country {CountryId} retrieved from cache", countryId);
                    return cachedData;
                }

                // Get from repository
                var cities = await _locationRepository.GetCitiesByCountryAsync(countryId);

                // Cache the response
                await _cacheService.SetAsync(cacheKey, cities, TimeSpan.FromHours(24));

                _logger.LogInformation("Cities list retrieved successfully for country {CountryId}", countryId);
                return cities;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving cities list for country {CountryId}", countryId);
                throw;
            }
        }
    }
}