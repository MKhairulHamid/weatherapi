# Weather API Documentation

Base URL: `https://weatherappapi20250207082422.azurewebsites.net`

## Endpoints

### 1. Get Countries List

Returns a list of available countries.

```http
GET /api/Location/countries
```

#### Response

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
        },
        {
            "id": "AU",
            "name": "Australia"
        },
        {
            "id": "CA",
            "name": "Canada"
        },
        {
            "id": "DE",
            "name": "Germany"
        }
    ]
}
```

### 2. Get Cities by Country

Returns a list of cities for a specific country.

```http
GET /api/Location/cities/{countryId}
```

#### Parameters

| Name      | Type   | Description                                |
|-----------|--------|--------------------------------------------|
| countryId | string | The two-letter country code (e.g., "US")   |

#### Response

```json
{
    "success": true,
    "message": "Success",
    "data": [
        {
            "id": "NY",
            "name": "New York",
            "countryId": "US"
        },
        {
            "id": "LA",
            "name": "Los Angeles",
            "countryId": "US"
        },
        {
            "id": "CH",
            "name": "Chicago",
            "countryId": "US"
        },
        {
            "id": "HO",
            "name": "Houston",
            "countryId": "US"
        },
        {
            "id": "PH",
            "name": "Phoenix",
            "countryId": "US"
        }
    ]
}
```

### 3. Get Weather Forecast

Returns weather information for a specific city.

```http
GET /WeatherForecast/{city}
```

#### Parameters

| Name | Type   | Description                          |
|------|--------|--------------------------------------|
| city | string | The name of the city (e.g., "London")|

#### Response

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

## Response Formats

### Success Response Structure
```json
{
    "success": true,
    "message": "Success",
    "data": [ ... ]
}
```

### Error Response Structure
```json
{
    "success": false,
    "message": "Error message here",
    "data": null
}
```

## Status Codes

| Status Code | Description |
|------------|-------------|
| 200 | Success |
| 400 | Bad Request |
| 404 | Not Found |
| 500 | Internal Server Error |

## Rate Limiting

- Free tier limitations apply
- Maximum of 60 requests per minute

## Notes

- All responses are in JSON format
- Timestamps are in UTC
- Temperature values are provided in both Celsius and Fahrenheit
- Wind speed is in meters per second
- Pressure is in hectopascals (hPa)
