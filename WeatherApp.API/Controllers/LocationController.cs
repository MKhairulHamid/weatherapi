using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.Core.Interfaces.Services;
using WeatherApp.Core.Models.DTOs;

namespace WeatherApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;
        private readonly ILogger<LocationController> _logger;

        public LocationController(ILocationService locationService, ILogger<LocationController> logger)
        {
            _locationService = locationService ?? throw new ArgumentNullException(nameof(locationService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets list of all available countries
        /// </summary>
        [HttpGet("countries")]
        [ProducesResponseType(typeof(ApiResponse<List<CountryDto>>), 200)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                _logger.LogInformation("Getting countries list");
                List<CountryDto> countries = (List<CountryDto>)await _locationService.GetCountriesAsync();
                return Ok(ApiResponse<List<CountryDto>>.SuccessResponse(countries));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting countries list");
                throw;
            }
        }

        /// <summary>
        /// Gets cities for a specific country
        /// </summary>
        [HttpGet("cities/{countryId}")]
        [ProducesResponseType(typeof(ApiResponse<List<CityDto>>), 200)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> GetCities(string countryId)
        {
            try
            {
                _logger.LogInformation("Getting cities for country: {CountryId}", countryId);

                if (string.IsNullOrWhiteSpace(countryId))
                {
                    return BadRequest(ApiResponse<CityDto[]>.ErrorResponse("Country ID cannot be empty"));
                }

                List<CityDto> cities = (List<CityDto>)await _locationService.GetCitiesByCountryAsync(countryId);
                return Ok(ApiResponse<List<CityDto>>.SuccessResponse(cities));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting cities for country: {CountryId}", countryId);
                throw;
            }
        }
    }
}