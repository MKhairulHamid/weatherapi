# Weather API

Welcome to the Weather API project! This .NET-based weather service provides comprehensive weather data through a RESTful API interface. The service is currently hosted at `https://weatherappapi20250207082422.azurewebsites.net` and offers detailed weather information for various cities worldwide.

## Project Overview

Our Weather API service is built with modern .NET technologies and follows best practices for API development. The solution is structured into three main projects to ensure maintainability and testability:

- WeatherApp.API - The main API project handling HTTP requests and responses
- WeatherApp.Core - Core business logic and domain models
- WeatherApp.Tests - Unit and integration tests

## Getting Started with Development

### Prerequisites

Before setting up the project for development, ensure you have the following installed:
- .NET 7.0 SDK or later
- Visual Studio 2022 or Visual Studio Code
- Git for version control
- SQL Server (if using local database)

### Local Setup

1. Clone the repository:
```bash
git clone https://github.com/MKhairulHamid/weatherapi.git
cd weatherapi
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Configure the application:
   - Navigate to `WeatherApp.API/appsettings.json`
   - Update the connection strings and API keys as needed
   - Configuration values can be overridden using environment variables

4. Build and run:
```bash
dotnet build
dotnet run --project WeatherApp.API
```

### Development Tools

Running tests:
```bash
dotnet test
```

Adding new migrations:
```bash
cd WeatherApp.API
dotnet ef migrations add MigrationName
```

## API Documentation

### Base URL
`https://weatherappapi20250207082422.azurewebsites.net`

### Authentication
Currently, the API uses a free tier model with rate limiting. Authentication implementation is planned for future releases.

### Rate Limiting
- Maximum of 60 requests per minute
- Free tier limitations apply

### Available Endpoints

#### 1. Get Countries List
Returns a list of available countries.

```http
GET /api/Location/countries
```

Example Response:
```json
{
    "success": true,
    "message": "Success",
    "data": [
        {
            "id": "US",
            "name": "United States"
        },
        {
            "id": "GB",
            "name": "United Kingdom"
        }
    ]
}
```

#### 2. Get Cities by Country
Returns a list of cities for a specific country.

```http
GET /api/Location/cities/{countryId}
```

Parameters:

| Name      | Type   | Description                              |
|-----------|--------|------------------------------------------|
| countryId | string | The two-letter country code (e.g., "US") |

Example Response:
```json
{
    "success": true,
    "message": "Success",
    "data": [
        {
            "id": "NY",
            "name": "New York",
            "countryId": "US"
        }
    ]
}
```

#### 3. Get Weather Forecast
Returns detailed weather information for a specific city.

```http
GET /WeatherForecast/{city}
```

Parameters:

| Name | Type   | Description                           |
|------|--------|---------------------------------------|
| city | string | The name of the city (e.g., "London") |

Example Response:
```json
{
    "location": "London",
    "time": "2025-02-07T01:43:38.4262498Z",
    "wind": {
        "speed": 12.66,
        "direction": "NE"
    },
    "visibility": "10km",
    "skyConditions": "few clouds",
    "temperatureCelsius": 13,
    "temperatureFahrenheit": 37.4,
    "dewPoint": 34.79,
    "relativeHumidity": 86,
    "pressure": 1035
}
```

### Response Formats

All API responses follow a consistent format:

Success Response:
```json
{
    "success": true,
    "message": "Success",
    "data": [ ... ]
}
```

Error Response:
```json
{
    "success": false,
    "message": "Error message here",
    "data": null
}
```

### Status Codes

| Status Code | Description           |
|-------------|-----------------------|
| 200         | Success              |
| 400         | Bad Request          |
| 404         | Not Found            |
| 500         | Internal Server Error |

### Data Specifications

- All responses are in JSON format
- Timestamps are in UTC
- Temperature values are provided in both Celsius and Fahrenheit
- Wind speed is in meters per second
- Pressure is in hectopascals (hPa)
- Visibility is provided in kilometers

## Contributing

We welcome contributions to improve the Weather API! Here's how you can help:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

Please ensure your code:
- Follows C# coding conventions
- Includes appropriate XML documentation
- Includes unit tests for new features
- Follows the repository's .editorconfig settings

## Support

For support questions:
1. Open an issue in the GitHub repository
2. Check existing issues for answers
3. Contact the maintainers if necessary

## License

[Add your license information here]
