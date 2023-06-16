using IOption.Validators;
using LanguageExt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace IOption.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IOptions<Example3Option> _cfg;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IOptions<Example3Option> cfg)
    {
        _logger = logger;
        _cfg = cfg;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }

    private Option<Example3Option> GetOptionString()
    {
        return _cfg.Value;
    }

    [HttpGet("HelloWorld")]
    public IActionResult GetMsg()
    {
        var xx = GetOptionString();
        return xx.Match<IActionResult>(Ok, NotFound());
    }
}