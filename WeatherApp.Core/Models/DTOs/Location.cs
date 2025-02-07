// CountryDto.cs
namespace WeatherApp.Core.Models.DTOs
{
    public class CountryDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}

// CityDto.cs
namespace WeatherApp.Core.Models.DTOs
{
    public class CityDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CountryId { get; set; }
    }
}