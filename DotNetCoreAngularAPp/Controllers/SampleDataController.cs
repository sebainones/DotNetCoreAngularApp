using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreAngularApp.Model;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreAngularApp.Controllers
{
    [Route("/api/SampleData")]
    public class SampleDataController : Controller
    {
        private static string[] Summaries = { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

        private static string[] Cities = new[] { "Madrid", "Barcelona", "Mendoza", "Nyon", "Torino" };

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                return Ok(WeatherForecasts());
            }
            catch (Exception ex)
            {
                //_logger.LogError("Failed to get episode from the API", ex);

                return BadRequest(ex.Data);
            }
        }

        //TODO: Get con Action
        [HttpGet("WeatherForecasts")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Name = Cities[rng.Next(Cities.Length)],
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }
    }
}
