using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using WeatherApp.Models;

namespace WeatherApp.Controllers;

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

    public Measurement WeatherToday([FromQuery] string location = "Ulko")
    {
        string sql = $"SELECT * FROM MEASUREMENT WHERE NAME = '{location}' ORDER BY ID DESC LIMIT 1; ";

        Measurement measurement = new();
        measurement.SearchFromDatabase(sql);

        return measurement;
    }

}