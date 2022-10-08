using Domain;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "50° / 38° °F  Afternoon Clouds", "48° / 34° °F  Sprinkles. Decreasing Clouds.", "63° / 42° °F  Clearing Skies.", "65° / 42° °F  Sunny.", "52° / 40° °F  Sprinkles Early. Breaks Of Sun Late.", "53° / 37° °F  Partly Cloudy.", "51° / 40° °F  Mostly Cloudy.", "48° / 34° °F  Cloudy With A Chance of Rain", "48° / 32° °F Rain Likely.", "58° / 47° °F T-Storms.", "63° / 49° °F Showers.", "58° / 46° Partly Sunny.", "67° / 55° °F Mostly Sunny.", "51° / 38° °F Mostly Cloudy.", "48° / 34° °F Scattered Rain.", "56° / 42° °F Drizzle Likely.", "36° / 19°  °F Cold And Windy.", "41° / 22° °F Blustery.", "45° / 23°  °F Chance Of Freezing Rain."
    };

    private readonly ILogger<WeatherForecastController> _logger;

    private readonly DataContext _context;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpPost]

    public ActionResult<WeatherForecast> Create()
    {
        Console.WriteLine($"Database path: {_context.DbPath}");
        Console.WriteLine("Insert a new WeatherForecast");

        var forecast = new WeatherForecast()
        {
            Date = new DateTime(),
            TemperatureC = 75,
            Summary = "Warm"
        };

        _context.WeatherForecasts.Add(forecast);
        var success = _context.SaveChanges() > 0;

        if (success)
        {
            return forecast;
        }

        throw new Exception("Error creating WeatherForecast");
    }
}

