using Microsoft.AspNetCore.Mvc;

namespace WeatherController.Controllers;

[ApiController]
[Route("weather")]

public class WeatherController : ControllerBase
{

    private readonly ILogger<WeatherController> _logger;

    public WeatherController(ILogger<WeatherController> logger)
    {
        _logger = logger;
    }


    [HttpGet("today")]

    public string WeatherToday()
    {
        _logger.LogInformation("joo");
        return "Test";
    }

}