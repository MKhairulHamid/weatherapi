using Microsoft.AspNetCore.Mvc;

namespace WeatherApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public abstract class BaseApiController : ControllerBase
    {
        // Common controller functionality can be added here
        protected IActionResult InternalServerError(string message = "An unexpected error occurred")
        {
            return StatusCode(500, message);
        }

        protected IActionResult NotFound(string message = "Resource not found")
        {
            return StatusCode(404, message);
        }
    }
}