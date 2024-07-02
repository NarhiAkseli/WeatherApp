using System.Diagnostics.Metrics;
using System.Drawing.Text;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using WeatherApp.Models;

namespace WeatherApp.Controllers;

[ApiController]
[Route("weather")]

public class WeatherController : ControllerBase
{

    private readonly ILogger<WeatherController> _logger;
    private readonly IConfiguration _configuration;
    public WeatherController(ILogger<WeatherController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    private Measurement SingleMeasurementReader(string sql)
    {
        Measurement measurement = new();

        using NpgsqlConnection connection = new NpgsqlConnection(_configuration["ConnectionStrings:PostgreSQL"]);
        connection.Open();
        using (var cmd = new NpgsqlCommand(sql, connection))
        {
            
            //todo add check that reader returns only 1, handle no results found
            var reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                measurement.SearchFromDatabase(reader); 
            }

        }
        connection.Close();
        return measurement;
            
    }

    private List<Measurement> MultiMeasurementReader(string sql)
    {
        List<Measurement> measurements = new();

        using NpgsqlConnection connection = new NpgsqlConnection(_configuration["ConnectionStrings:PostgreSQL"]);
        connection.Open();
        using (var cmd = new NpgsqlCommand(sql, connection))
        {
            var reader = cmd.ExecuteReader();
               while(reader.Read())
               {
                Measurement measurement = new();
                measurement.SearchFromDatabase(reader);
                measurements.Add(measurement);
               }
        }
        connection.Close();
        return measurements;
            
    }


[HttpGet("today")]

    public Measurement WeatherToday([FromQuery] string location = "Ulko")
    {
        DateTime startDate = DateTime.Now.AddDays(-2);
        string sql = $"SELECT * FROM MEASUREMENT WHERE NAME = '{location}' AND date < '{FormatTime(startDate)}' ORDER BY ID DESC LIMIT 1; ";

        Measurement measurement = SingleMeasurementReader(sql);

        return measurement;
    }

    [HttpGet("alltoday")]

    public List<Measurement> AllWeatherToday([FromQuery] string location = "Ulko")
    {

        DateTime startdate = DateTime.Now.AddDays(-2);
        DateTime enddate = DateTime.Now.AddDays(-1);

        string sql = $"SELECT * FROM MEASUREMENT WHERE NAME = '{location}' AND DATE >= '{FormatTime(startdate)}' AND DATE < '{FormatTime(enddate)}' ";
        List<Measurement> measurements = MultiMeasurementReader(sql);

        return measurements;
    }

    public string FormatTime(DateTime time)
    {
        return time.ToString("yyyy-MM-dd");
    }

}