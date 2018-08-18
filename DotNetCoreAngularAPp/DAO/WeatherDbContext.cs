using DotNetCoreAngularApp.Model;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreAngularApp
{
    public  class WeatherDbContext :DbContext
    {
        public WeatherDbContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<WeatherForecast> Weathers { get; set; }
    }
}